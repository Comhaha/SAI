#!/usr/bin/env python
# -*- coding: utf-8 -*-

"""
install_packages.py - 패키지 설치 유틸리티

이 스크립트는 AI 학습용 필수 패키지 설치 및 CUDA 지원 PyTorch 설치를 관리합니다.
튜토리얼 모드 및 실습 모드에서 공통으로 사용됩니다.
"""

import os
import sys
import subprocess
import platform
import re
import time
import io
import logging
from datetime import datetime

print("[DEBUG] install_packages.py 시작", flush=True)

# 표준 출력 스트림 설정
try:
    sys.stdout = io.TextIOWrapper(sys.stdout.buffer, encoding='utf-8')
    sys.stderr = io.TextIOWrapper(sys.stderr.buffer, encoding='utf-8')
    print("[DEBUG] 표준 출력 스트림 설정 완료", flush=True)
except Exception as e:
    print(f"[DEBUG] 표준 출력 스트림 설정 실패: {e}", flush=True)

# 로깅 설정
logging.basicConfig(
    encoding='utf-8',
    level=logging.INFO,
    format='%(asctime)s - %(levelname)s - %(message)s',
    datefmt='%Y-%m-%d %H:%M:%S'
)
logger = logging.getLogger(__name__)
print("[DEBUG] 로깅 설정 완료", flush=True)

base_dir = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
print(f"[DEBUG] base_dir 설정됨: {base_dir}", flush=True)

def show_progress(message, start_time=None, progress=None):
    try:
        elapsed_str = ""
        if start_time:
            elapsed = time.time() - start_time
            minutes, seconds = divmod(elapsed, 60)
            elapsed_str = f"[{int(minutes):02d}:{int(seconds):02d}] "

        progress_str = ""
        if progress is not None:
            progress_str = f"[{progress:.1f}%] "

        full_message = f"{elapsed_str}{progress_str}{message}"
        logger.info(full_message)
        if progress is not None:
            print(f"PROGRESS:{progress:.1f}:{message}", flush=True)
        else:
            print(f"PROGRESS::{message}", flush=True)
    except Exception as e:
        print(f"[DEBUG] show_progress 오류: {e}", flush=True)
        logger.error(f"show_progress 오류: {e}")

print("[DEBUG] install_packages.py 초기화 완료", flush=True)

def install_packages_with_progress(packages=None, start_time=None):
    print("[DEBUG] install_packages_with_progress 진입")
    if start_time is None:
        start_time = time.time()

    if packages is None:
        packages = ["ultralytics", "opencv-python"]

    show_progress("패키지 설치 시작...", start_time, 0)

    results = {
        "success": True,
        "installed_packages": [],
        "failed_packages": [],
        "versions": {}
    }

    try:
        numpy_result = _install_single_package("numpy==1.24.3", start_time, 10)
        if numpy_result["success"]:
            results["installed_packages"].append("numpy==1.24.3")
            results["versions"]["numpy"] = numpy_result.get("version")
        else:
            results["failed_packages"].append("numpy==1.24.3")
            show_progress("NumPy 다운그레이드 실패, 계속 진행합니다.", start_time, 20)

        total_packages = len(packages)
        progress_per_package = 70 / total_packages

        for i, package in enumerate(packages):
            print(f"[DEBUG] {package} 설치 시작")
            progress_start = 20 + (i * progress_per_package)
            progress_end = 20 + ((i + 1) * progress_per_package)
            show_progress(f"{package} 설치 중... ({i+1}/{total_packages})", start_time, progress_start)
            pkg_result = _install_single_package(package, start_time, progress_start)

            if pkg_result["success"]:
                results["installed_packages"].append(package)
                results["versions"][package] = pkg_result.get("version")
                show_progress(f"{package} 설치 완료", start_time, progress_end)
            else:
                results["failed_packages"].append(package)
                show_progress(f"{package} 설치 실패, 계속 진행합니다.", start_time, progress_end)

        show_progress("설치 상태 확인 및 의존성 검사 중...", start_time, 90)
        cuda_support = _check_cuda_support()
        results["cuda_support"] = cuda_support

        for package in results["installed_packages"]:
            pkg_name = package.split('==')[0]
            if pkg_name not in results["versions"]:
                version = _get_package_version(pkg_name)
                if version:
                    results["versions"][pkg_name] = version

        if not results["failed_packages"]:
            show_progress("모든 패키지 설치 완료!", start_time, 100)
        else:
            failed_count = len(results["failed_packages"])
            show_progress(f"패키지 설치 부분 완료 ({failed_count}개 패키지 설치 실패)", start_time, 100)
            results["success"] = False

        results["elapsed_time"] = time.time() - start_time
        return results

    except Exception as e:
        error_msg = f"패키지 설치 중 오류 발생: {e}"
        show_progress(error_msg, start_time, 100)
        results["success"] = False
        results["error"] = error_msg
        results["elapsed_time"] = time.time() - start_time
        return results

def _install_single_package(package, start_time, progress_base=0):
    print(f"[DEBUG] _install_single_package 진입: {package}")
    try:
        install_cmd = [
            sys.executable, "-m", "pip", "install",
            package,
            "--verbose"
        ]
        show_progress(f"{package} 설치 명령 실행 중...", start_time, progress_base + 2)
        process = subprocess.Popen(
            install_cmd,
            stdout=subprocess.PIPE,
            stderr=subprocess.STDOUT,
            universal_newlines=True,
            bufsize=1
        )
        last_progress_time = time.time()
        progress = 0

        for line in iter(process.stdout.readline, ''):
            line = line.strip()
            print(f"[DEBUG] pip output: {line}")
            if "Collecting" in line:
                progress = 20
                show_progress(f"{package} 다운로드 중: {line}", start_time, progress_base + progress)
            elif "Downloading" in line:
                progress = 40
                show_progress(f"{package} 다운로드 중: {line}", start_time, progress_base + progress)
            elif "Installing" in line:
                progress = 60
                show_progress(f"{package} 설치 중: {line}", start_time, progress_base + progress)
            elif "Successfully installed" in line:
                progress = 100
                show_progress(f"{package} 설치 완료: {line}", start_time, progress_base + progress)

            if time.time() - last_progress_time > 5:
                show_progress(f"{package} 설치 진행 중...", start_time, progress_base + progress)
                last_progress_time = time.time()

        process.wait()

        if process.returncode == 0:
            version = _get_package_version(package.split('==')[0])
            return {"success": True, "version": version}
        else:
            return {"success": False, "error": f"pip 명령 실패 (반환 코드: {process.returncode})"}

    except Exception as e:
        print(f"[DEBUG] _install_single_package 예외: {e}")
        return {"success": False, "error": str(e)}
