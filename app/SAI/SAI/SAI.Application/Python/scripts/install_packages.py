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
        logger.error(f"show_progress 오류: {e}", exc_info=True)
        print(f"PROGRESS::show_progress 오류: {str(e)}", flush=True)

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
    
def _get_package_version(package_name):
    """
    설치된 패키지의 버전 확인 (내부 헬퍼 함수)
    
    Args:
        package_name (str): 패키지 이름
        
    Returns:
        str: 패키지 버전 또는 None
    """
    try:
        # pip show 명령으로 패키지 정보 확인
        process = subprocess.run(
            [sys.executable, "-m", "pip", "show", package_name],
            stdout=subprocess.PIPE,
            stderr=subprocess.PIPE,
            universal_newlines=True,
            check=False
        )
        
        if process.returncode == 0:
            # 출력에서 버전 정보 추출
            for line in process.stdout.splitlines():
                if line.startswith("Version:"):
                    return line.split(":", 1)[1].strip()
        
        return None
    except Exception:
        return None


def _check_cuda_support():
    """
    CUDA 지원 여부 확인 (내부 헬퍼 함수)
    
    Returns:
        dict: CUDA 지원 정보
    """
    try:
        import torch
        if torch.cuda.is_available():
            return {
                "available": True,
                "version": torch.version.cuda,
                "device_count": torch.cuda.device_count(),
                "device_name": torch.cuda.get_device_name(0) if torch.cuda.device_count() > 0 else None
            }
        else:
            return {"available": False}
    except ImportError:
        return {"available": False, "error": "PyTorch가 설치되지 않았습니다."}
    except Exception as e:
        return {"available": False, "error": str(e)}


# ===== 패키지 설치 캐싱 관련 함수 =====

def _is_package_installed(package):
    """
    패키지가 이미 설치되어 있는지 확인 (내부 헬퍼 함수)
    
    Args:
        package (str): 확인할 패키지 이름 (버전 포함 가능)
        
    Returns:
        bool: 설치 여부
    """
    # 패키지 이름과 버전 분리
    if '==' in package:
        pkg_name, required_version = package.split('==')
    else:
        pkg_name, required_version = package, None
    
    try:
        # pip list 명령으로 설치된 패키지 확인
        process = subprocess.run(
            [sys.executable, "-m", "pip", "show", pkg_name],
            stdout=subprocess.PIPE,
            stderr=subprocess.PIPE,
            universal_newlines=True,
            check=False
        )
        
        if process.returncode != 0:
            return False  # 패키지가 설치되어 있지 않음
        
        # 버전 확인이 필요한 경우
        if required_version:
            for line in process.stdout.splitlines():
                if line.startswith("Version:"):
                    installed_version = line.split(":", 1)[1].strip()
                    return installed_version == required_version
        
        return True  # 패키지가 설치되어 있음 (버전 제약 없음)
    except Exception:
        return False  # 확인 중 오류 발생


def check_installed_packages(packages):
    """
    여러 패키지의 설치 상태 확인
    
    Args:
        packages (list): 확인할 패키지 목록
        
    Returns:
        dict: 패키지별 설치 상태 및 버전 정보
    """
    results = {
        "installed": [],
        "missing": [],
        "versions": {}
    }
    
    for package in packages:
        if _is_package_installed(package):
            pkg_name = package.split('==')[0]
            results["installed"].append(package)
            version = _get_package_version(pkg_name)
            if version:
                results["versions"][pkg_name] = version
        else:
            results["missing"].append(package)
    
    return results


def install_if_needed(packages=None, start_time=None):
    """
    필요한 패키지만 설치 (이미 설치된 패키지는 건너뜀)
    
    Args:
        packages (list): 설치할 패키지 목록
        start_time (float): 시작 시간
        
    Returns:
        dict: 설치 결과 정보
    """
    if start_time is None:
        start_time = time.time()
    
    if packages is None:
        packages = ["ultralytics", "opencv-python"]
    
    show_progress("패키지 설치 상태 확인 중...", start_time, 0)
    
    # 설치 상태 확인
    status = check_installed_packages(packages)
    
    # 필요한 패키지만 설치
    if not status["missing"]:
        show_progress("모든 패키지가 이미 설치되어 있습니다.", start_time, 100)
        return {
            "success": True,
            "message": "모든 패키지가 이미 설치되어 있습니다.",
            "installed_packages": status["installed"],
            "versions": status["versions"],
            "elapsed_time": time.time() - start_time
        }
    
    # 필요한 패키지 설치
    show_progress(f"{len(status['missing'])}개 패키지 설치 필요: {', '.join(status['missing'])}", start_time, 10)
    install_result = install_packages_with_progress(status["missing"], start_time)
    
    # 결과 통합
    combined_result = {
        "success": install_result["success"],
        "already_installed": status["installed"],
        "newly_installed": install_result["installed_packages"],
        "failed_packages": install_result["failed_packages"],
        "versions": {**status["versions"], **(install_result["versions"] if "versions" in install_result else {})},
        "elapsed_time": time.time() - start_time
    }
    
    return combined_result


# 패키지 그룹 정의
BASIC_PACKAGES = ["numpy==1.24.3", "matplotlib", "pandas"]
YOLO_PACKAGES = ["ultralytics", "opencv-python"]
VISUALIZATION_PACKAGES = ["matplotlib", "seaborn", "plotly", "tqdm"]
DATA_PROCESSING_PACKAGES = ["pandas", "numpy==1.24.3", "scikit-learn"]


def install_with_profile(profile_name, start_time=None):
    """
    지정된 프로필에 따라 패키지 설치
    
    Args:
        profile_name (str): 프로필 이름 ('basic', 'yolo', 'viz', 'data', 'all')
        start_time (float): 시작 시간
        
    Returns:
        dict: 설치 결과 정보
    """
    if start_time is None:
        start_time = time.time()
    
    profiles = {
        "basic": BASIC_PACKAGES,
        "yolo": YOLO_PACKAGES,
        "viz": VISUALIZATION_PACKAGES,
        "data": DATA_PROCESSING_PACKAGES,
        "all": list(set(BASIC_PACKAGES + YOLO_PACKAGES + VISUALIZATION_PACKAGES + DATA_PROCESSING_PACKAGES))
    }
    
    if profile_name not in profiles:
        return {
            "success": False,
            "error": f"알 수 없는 프로필: {profile_name}. 사용 가능한 프로필: {', '.join(profiles.keys())}"
        }
    
    show_progress(f"'{profile_name}' 프로필 패키지 설치 중...", start_time, 0)
    return install_if_needed(profiles[profile_name], start_time)


def check_pytorch_cuda_compatibility():
    """현재 설치된 PyTorch가 CUDA와 호환되는지 확인"""
    try:
        import torch
        
        # PyTorch 설치 확인
        print(f"현재 설치된 PyTorch 버전: {torch.__version__}")
        
        # CUDA 지원 확인
        if torch.cuda.is_available():
            cuda_version = torch.version.cuda
            print(f"PyTorch CUDA 버전: {cuda_version}")
            gpu_name = torch.cuda.get_device_name(0)
            print(f"감지된 GPU: {gpu_name}")
            
            # CUDA 버전 감지
            system_cuda = detect_cuda()
            if system_cuda:
                # CUDA 버전 비교 (주 버전만 비교)
                system_major = int(float(system_cuda))
                torch_major = int(float(cuda_version)) if cuda_version else 0
                
                if system_major == torch_major:
                    print("현재 설치된 PyTorch가 시스템 CUDA 버전과 호환됩니다.")
                    return True, "cuda", torch.__version__
                else:
                    print(f"설치된 PyTorch CUDA 버전({torch_major})이 시스템 CUDA 버전({system_major})과 일치하지 않습니다.")
                    return False, "incompatible", torch.__version__
            else:
                print("시스템 CUDA 버전을 감지할 수 없지만, PyTorch는 CUDA를 지원합니다.")
                return True, "cuda", torch.__version__
        else:
            print("설치된 PyTorch가 CUDA를 지원하지 않습니다.")
            return False, "cpu", torch.__version__
    except ImportError:
        print("PyTorch가 설치되어 있지 않습니다.")
        return False, "not_installed", None


def clean_pytorch_installation():
    """기존 PyTorch 설치 완전 제거"""
    print("기존 PyTorch 설치 제거 중...")
    
    # pip로 기본 제거
    subprocess.run([sys.executable, "-m", "pip", "uninstall", "-y", "torch", "torchvision", "torchaudio"])
    
    # site-packages 찾기
    import site
    site_packages = site.getsitepackages()[0]
    
    # torch 관련 디렉토리 찾아 제거
    torch_dirs = ['torch', 'torchvision', 'torchaudio']
    for dir_name in torch_dirs:
        dir_path = os.path.join(site_packages, dir_name)
        if os.path.exists(dir_path):
            print(f"폴더 삭제 중: {dir_path}")
            try:
                import shutil
                shutil.rmtree(dir_path)
            except Exception as e:
                print(f"폴더 삭제 실패: {e}")
    
    print("PyTorch 설치 정리 완료")


def install_pytorch_cuda(start_time=None):
    """
    CUDA 지원 PyTorch 설치 래퍼 함수
    
    Args:
        start_time (float): 시작 시간
        
    Returns:
        tuple: (성공 여부, 장치 유형)
    """
    if start_time is None:
        start_time = time.time()
    
    show_progress("CUDA 지원 PyTorch 설치 준비 중...", start_time, 0)
    
    # 현재 설치 확인
    compatible, status, current_version = check_pytorch_cuda_compatibility()
    
    if compatible and status == "cuda":
        show_progress(f"이미 호환되는 PyTorch({current_version})가 설치되어 있습니다. 재설치하지 않습니다.", start_time, 100)
        with open("pytorch_install_result.txt", "w") as f:
            f.write("CUDA_SUCCESS")
        return True, "cuda"
    
    # CUDA 버전 감지
    cuda_version = detect_cuda()
    show_progress(f"감지된 CUDA 버전: {cuda_version}", start_time, 20)
    
    # 호환되지 않거나 CPU 버전인 경우에만 기존 설치 제거
    if not compatible or status == "cpu":
        show_progress("호환되지 않는 PyTorch 설치 제거 중...", start_time, 30)
        clean_pytorch_installation()
    
    if cuda_version:
        major_version = int(float(cuda_version))
        if major_version >= 12:
            cuda_tag = "cu121"
        elif major_version >= 11:
            cuda_tag = "cu118"
        elif major_version >= 10:
            cuda_tag = "cu102"
        else:
            show_progress(f"감지된 CUDA 버전 {cuda_version}는 지원되지 않습니다. CUDA 11.8로 시도합니다.", start_time, 40)
            cuda_tag = "cu118"
    else:
        show_progress("CUDA 버전을 감지할 수 없습니다. CUDA 11.8로 시도합니다.", start_time, 40)
        cuda_tag = "cu118"
    
    # 안정적인 PyTorch 버전 설치
    show_progress(f"{cuda_tag} 버전의 PyTorch 2.2.0 설치 중...", start_time, 50)
    torch_url = f"https://download.pytorch.org/whl/{cuda_tag}"
    install_cmd = [
        sys.executable, "-m", "pip", "install", 
        "torch==2.2.0", "torchvision==0.17.0", "torchaudio==2.2.0", 
        "--index-url", torch_url
    ]
    
    # 설치 명령 실행
    try:
        process = subprocess.Popen(
            install_cmd,
            stdout=subprocess.PIPE,
            stderr=subprocess.STDOUT,
            universal_newlines=True,
            bufsize=1
        )
        
        # 진행 상황 추적을 위한 변수들
        last_progress_time = time.time()
        progress = 50
        
        # 출력 처리
        for line in iter(process.stdout.readline, ''):
            line = line.strip()
            
            # 진행 단계 감지 및 진행률 업데이트
            if "Collecting" in line:
                progress = 60
                show_progress(f"PyTorch 패키지 다운로드 중: {line}", start_time, progress)
            elif "Downloading" in line:
                progress = 70
                show_progress(f"PyTorch 패키지 다운로드 중: {line}", start_time, progress)
            elif "Installing" in line:
                progress = 80
                show_progress(f"PyTorch 패키지 설치 중: {line}", start_time, progress)
            elif "Successfully installed" in line:
                progress = 90
                show_progress(f"PyTorch 패키지 설치 완료: {line}", start_time, progress)
            
            # 일정 시간 경과시 진행 중임을 표시
            if time.time() - last_progress_time > 5:
                show_progress(f"PyTorch 설치 진행 중...", start_time, progress)
                last_progress_time = time.time()
        
        process.wait()
        
        if process.returncode != 0:
            show_progress("PyTorch 설치 실패", start_time, 100)
            with open("pytorch_install_result.txt", "w") as f:
                f.write("INSTALL_ERROR")
            return False, "cpu"
    except Exception as e:
        show_progress(f"PyTorch 설치 중 오류: {e}", start_time, 100)
        with open("pytorch_install_result.txt", "w") as f:
            f.write(f"INSTALL_ERROR: {e}")
        return False, "cpu"
    
    # 설치 확인
    show_progress("PyTorch 설치 확인 중...", start_time, 95)
    try:
        import torch
        if torch.cuda.is_available():
            show_progress(f"CUDA 사용 가능: {torch.cuda.get_device_name(0)}", start_time, 100)
            show_progress(f"PyTorch 버전: {torch.__version__}", start_time, 100)
            show_progress(f"CUDA 버전: {torch.version.cuda}", start_time, 100)
            with open("pytorch_install_result.txt", "w") as f:
                f.write("CUDA_SUCCESS")
            return True, "cuda"
        else:
            show_progress("PyTorch 설치는 됐지만 CUDA를 감지할 수 없습니다.", start_time, 100)
            with open("pytorch_install_result.txt", "w") as f:
                f.write("GPU_NOT_DETECTED")
            return False, "cpu"
    except Exception as e:
        show_progress(f"PyTorch 설치 확인 중 오류: {e}", start_time, 100)
        with open("pytorch_install_result.txt", "w") as f:
            f.write(f"INSTALL_ERROR: {e}")
        return False, "cpu"


# ===== GPU 정보 확인 관련 함수 =====

def check_gpu(start_time=None):
    """
    GPU 상태 확인 및 정보 반환 래퍼 함수
    
    Args:
        start_time (float): 시작 시간
        
    Returns:
        dict: GPU 정보
    """
    if start_time is None:
        start_time = time.time()
        
    show_progress("GPU 확인 중...", start_time, 0)
    
    try:
        import torch
        show_progress("PyTorch GPU 기능 확인 중...", start_time, 25)
        
        if torch.cuda.is_available():
            show_progress("CUDA 지원 확인됨", start_time, 50)
            gpu_count = torch.cuda.device_count()
            gpu_names = [torch.cuda.get_device_name(i) for i in range(gpu_count)]
            cuda_version = torch.version.cuda
            gpu_memory = []
            
            show_progress(f"GPU {gpu_count}개 감지됨", start_time, 75)
            
            for i in range(gpu_count):
                try:
                    props = torch.cuda.get_device_properties(i)
                    mem_gb = props.total_memory / (1024**3)
                    gpu_memory.append(round(mem_gb, 1))
                    show_progress(f"GPU {i}: {gpu_names[i]} ({gpu_memory[-1]} GB)", start_time, 80 + (i+1) * (20/gpu_count))
                except:
                    gpu_memory.append(None)
                    show_progress(f"GPU {i}: {gpu_names[i]} (메모리 정보 없음)", start_time, 80 + (i+1) * (20/gpu_count))
            
            show_progress(f"CUDA 버전: {cuda_version}", start_time, 100)
            
            return {
                "available": True,
                "count": gpu_count,
                "names": gpu_names,
                "cuda_version": cuda_version,
                "memory_gb": gpu_memory
            }
        else:
            show_progress("GPU 감지 안됨: CPU 모드로 실행합니다.", start_time, 100)
            return {"available": False}
    except Exception as e:
        show_progress(f"GPU 확인 오류: {e}", start_time, 100)
        return {"available": False, "error": str(e)}


# ===== 모듈 직접 실행 시 테스트 코드 =====

if __name__ == "__main__":
    # 테스트 목적으로 직접 실행될 때
    print("패키지 설치 유틸리티 테스트")
    
    # 명령행 인수 확인
    if len(sys.argv) > 1:
        if sys.argv[1] == "install_pytorch":
            # PyTorch 설치 테스트
            start_time = time.time()
            success, device = install_pytorch_cuda(start_time)
            print(f"PyTorch 설치 결과: 성공={success}, 장치={device}")
        
        elif sys.argv[1] == "check_gpu":
            # GPU 확인 테스트
            start_time = time.time()
            gpu_info = check_gpu(start_time)
            print(f"GPU 정보: {gpu_info}")
        
        elif sys.argv[1] == "install_packages":
            # 패키지 설치 테스트
            packages = sys.argv[2:] if len(sys.argv) > 2 else None
            start_time = time.time()
            result = install_packages_with_progress(packages, start_time)
            print(f"패키지 설치 결과: {result}")
        
        elif sys.argv[1] == "install_if_needed":
            # 필요한 패키지만 설치 테스트
            packages = sys.argv[2:] if len(sys.argv) > 2 else None
            start_time = time.time()
            result = install_if_needed(packages, start_time)
            print(f"패키지 설치 결과: {result}")
        
        elif sys.argv[1] == "profile":
            # 프로필 기반 설치 테스트
            profile = sys.argv[2] if len(sys.argv) > 2 else "basic"
            start_time = time.time()
            result = install_with_profile(profile, start_time)
            print(f"프로필 설치 결과: {result}")
        
        else:
            print(f"알 수 없는 명령: {sys.argv[1]}")
            print("사용법:")
            print("  python install_packages.py install_pytorch")
            print("  python install_packages.py check_gpu")
            print("  python install_packages.py install_packages [패키지1 패키지2 ...]")
            print("  python install_packages.py install_if_needed [패키지1 패키지2 ...]")
            print("  python install_packages.py profile [basic|yolo|viz|data|all]")
    
    else:
        print("사용법:")
        print("  python install_packages.py install_pytorch")
        print("  python install_packages.py check_gpu")
        print("  python install_packages.py install_packages [패키지1 패키지2 ...]")
        print("  python install_packages.py install_if_needed [패키지1 패키지2 ...]")
        print("  python install_packages.py profile [basic|yolo|viz|data|all]")

