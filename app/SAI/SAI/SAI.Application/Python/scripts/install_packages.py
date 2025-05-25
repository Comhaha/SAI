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

# CUDA 감지 함수 추가
def detect_cuda():
    """시스템의 CUDA 버전 감지"""
    print("CUDA 버전 감지 중...")
    try:
        if platform.system() == "Windows":
            # nvcc로 CUDA 버전 확인 시도
            try:
                nvcc_result = subprocess.run("nvcc --version", shell=True, capture_output=True, text=True)
                if nvcc_result.returncode == 0:
                    match = re.search(r"release (\d+\.\d+)", nvcc_result.stdout)
                    if match:
                        cuda_version = match.group(1)
                        print(f"NVCC로 감지한 CUDA 버전: {cuda_version}")
                        return cuda_version
            except:
                pass

            # NVIDIA 드라이버 확인
            try:
                nvidia_smi_result = subprocess.run("nvidia-smi", shell=True, capture_output=True, text=True)
                if nvidia_smi_result.returncode == 0:
                    match = re.search(r"CUDA Version: (\d+\.\d+)", nvidia_smi_result.stdout)
                    if match:
                        cuda_version = match.group(1)
                        print(f"nvidia-smi로 감지한 CUDA 버전: {cuda_version}")
                        return cuda_version
            except:
                pass
        
        # Linux에서 CUDA 버전 확인
        elif platform.system() == "Linux":
            try:
                nvidia_smi_result = subprocess.run("nvidia-smi", shell=True, capture_output=True, text=True)
                if nvidia_smi_result.returncode == 0:
                    match = re.search(r"CUDA Version: (\d+\.\d+)", nvidia_smi_result.stdout)
                    if match:
                        cuda_version = match.group(1)
                        print(f"nvidia-smi로 감지한 CUDA 버전: {cuda_version}")
                        return cuda_version
            except:
                pass
            
            # nvcc 확인
            try:
                nvcc_result = subprocess.run("nvcc --version", shell=True, capture_output=True, text=True)
                if nvcc_result.returncode == 0:
                    match = re.search(r"release (\d+\.\d+)", nvcc_result.stdout)
                    if match:
                        cuda_version = match.group(1)
                        print(f"NVCC로 감지한 CUDA 버전: {cuda_version}")
                        return cuda_version
            except:
                pass
    except Exception as e:
        print(f"CUDA 버전 감지 중 오류 발생: {e}")
    
    return None

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

        
# ================ 태그 프로그래스 출력 함수 ================
def show_tagged_progress(tag, message, start_time=None, progress=None):
    """
    태그를 자동으로 붙여서 show_progress를 호출하는 래퍼 함수
    tag: 문자열(예: 'INFO', 'ERROR', 'DATASET', 'TRAIN', 'INFER' 등)
    message: 실제 메시지
    start_time, progress: 기존 show_progress와 동일
    """
    tagged_message = f"[{tag}] {message}"
    show_progress(tagged_message, start_time, progress)

print("[DEBUG] install_packages.py 초기화 완료", flush=True)

# CUDA 버전을 확인하는 함수 (tutorial_train_script.py에서 사용)
def check_cuda_version():
    """
    시스템의 CUDA 버전을 확인하고 반환합니다.
    
    Returns:
        str or None: CUDA 버전(예: "12.1") 또는 감지되지 않은 경우 None
    """
    return detect_cuda()

# ================ 패키지 설치 함수 ================
def install_packages_with_progress(packages=None, start_time=None):
    print("[DEBUG] install_packages_with_progress 진입")
    if start_time is None:
        start_time = time.time()

    if packages is None:
        packages = ["ultralytics", "opencv-python"]

    print(f"PROGRESS::패키지 설치 시작...")
    show_progress("패키지 설치 시작...", start_time, 0)

    results = {
        "success": True,
        "installed_packages": [],
        "failed_packages": [],
        "versions": {}
    }

    try:
        # numpy가 packages 리스트에 있는지 확인
        numpy_in_packages = any(pkg.startswith("numpy") for pkg in packages)
        
        # numpy가 packages 리스트에 없으면 먼저 설치
        if not numpy_in_packages:
            numpy_result = _install_single_package("numpy==1.24.3", start_time, 10)
            if numpy_result["success"]:
                results["installed_packages"].append("numpy==1.24.3")
                results["versions"]["numpy"] = numpy_result.get("version")
            else:
                results["failed_packages"].append("numpy==1.24.3")
                print(f"PROGRESS::NumPy 다운그레이드 실패, 계속 진행합니다.")

        total_packages = len(packages)
        progress_per_package = 70 / total_packages

        for i, package in enumerate(packages):
            print(f"PROGRESS::{package} 설치 시작 ({i+1}/{total_packages})")
            progress_start = 20 + (i * progress_per_package)
            progress_end = 20 + ((i + 1) * progress_per_package)
            show_progress(f"{package} 설치 중... ({i+1}/{total_packages})", start_time, progress_start)
            pkg_result = _install_single_package(package, start_time, progress_start)

            if pkg_result["success"]:
                results["installed_packages"].append(package)
                results["versions"][package] = pkg_result.get("version")
                print(f"PROGRESS::{package} 설치 완료")
            else:
                results["failed_packages"].append(package)
                print(f"PROGRESS::{package} 설치 실패, 계속 진행합니다.")

        # 패키지 설치 다하고 이 로그 뜸
        print(f"PROGRESS::설치 상태 확인 및 의존성 검사 중...")

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
            print(f"PROGRESS::모든 패키지 설치 완료!")
        else:
            failed_count = len(results["failed_packages"])
            print(f"PROGRESS::패키지 설치 부분 완료 ({failed_count}개 패키지 설치 실패)")
            results["success"] = False

        results["elapsed_time"] = time.time() - start_time
        return results

    except Exception as e:
        error_msg = f"패키지 설치 중 오류 발생: {e}"
        print(f"PROGRESS::{error_msg}")
        show_tagged_progress('ERROR', error_msg, start_time, 100)
        results["success"] = False
        results["error"] = error_msg
        results["elapsed_time"] = time.time() - start_time
        return results

# ================ 단일 패키지 설치 함수 ================
def _install_single_package(package, start_time, progress_base=0):
    print(f"[DEBUG] _install_single_package 진입: {package}", flush=True)
    try:
        # 현재 프로세스의 환경 변수 복사
        env = os.environ.copy()
        
        # Python과 Scripts 디렉토리 경로
        python_dir = os.path.dirname(sys.executable)
        scripts_dir = os.path.join(python_dir, "Scripts")
        
        # 가상환경 활성화 변수 설정
        env['VIRTUAL_ENV'] = python_dir
        
        # PATH 환경 변수 설정 (가상환경의 python과 scripts를 우선 사용)
        if 'PATH' in env:
            env['PATH'] = f"{python_dir};{scripts_dir};{env['PATH']}"
        else:
            env['PATH'] = f"{python_dir};{scripts_dir}"
        
        # 시스템 파이썬을 가리는 PYTHONHOME 제거
        if 'PYTHONHOME' in env:
            del env['PYTHONHOME']
        
        # pip 실행 파일 경로 확인
        pip_exe = os.path.join(scripts_dir, "pip.exe")
        if os.path.exists(pip_exe):
            # Windows에서 pip.exe 직접 실행
            install_cmd = [
                pip_exe, "install",
                package,
                "--verbose"
            ]
        else:
            # pip.exe가 없으면 python -m pip 형태로 실행
            install_cmd = [
                sys.executable, "-m", "pip", "install",
                package,
                "--verbose"
            ]
        
        show_progress(f"{package} 설치 명령 실행 중...", start_time, progress_base + 2)
        print(f"[DEBUG] 실행할 명령: {' '.join(install_cmd)}", flush=True)
        print(f"[DEBUG] 가상환경: {env.get('VIRTUAL_ENV')}", flush=True)
        print(f"[DEBUG] PATH 환경 변수: {env['PATH']}", flush=True)
        
        # env 매개변수로 환경 변수 전달
        process = subprocess.Popen(
            install_cmd,
            stdout=subprocess.PIPE,
            stderr=subprocess.STDOUT,
            universal_newlines=True,
            bufsize=1,
            env=env  # 여기서 환경 변수 전달
        )
        last_progress_time = time.time()
        progress = 0

        for line in iter(process.stdout.readline, ''):
            line = line.strip()
            if "Collecting" in line:
                progress = 20
                show_tagged_progress('DOWNLOAD', f'{package} 다운로드 중: {line}', start_time, progress_base + progress)
            elif "Downloading" in line:
                progress = 40
                show_tagged_progress('DOWNLOAD', f'{package} 다운로드 중: {line}', start_time, progress_base + progress)
            elif "Installing" in line:
                progress = 60
                show_tagged_progress('INSTALL', f'{package} 설치 중: {line}', start_time, progress_base + progress)
            elif "Successfully installed" in line:
                progress = 100
                show_tagged_progress('SUCCESS', f'{package} 설치 완료: {line}', start_time, progress_base + progress)

            if time.time() - last_progress_time > 5:
                show_tagged_progress('INSTALL', f'{package} 설치 진행 중...', start_time, progress_base + progress)
                last_progress_time = time.time()

        process.wait()

        if process.returncode == 0:
            version = _get_package_version(package.split('==')[0])
            show_tagged_progress('SUCCESS', f'패키지 설치 성공: {package} (버전: {version})', start_time, progress_base + 100)
            return {"success": True, "version": version}
        else:
            error_msg = f"pip 명령 실패 (반환 코드: {process.returncode})"
            show_tagged_progress('ERROR', f'패키지 설치 실패: {package} - {error_msg}', start_time, progress_base + 100)
            return {"success": False, "error": error_msg}

    except Exception as e:
        error_msg = str(e)
        show_tagged_progress('ERROR', f'패키지 설치 중 오류 발생: {package} - {error_msg}', start_time, progress_base + 100)
        return {"success": False, "error": error_msg}
    
# ================ 패키지 버전 확인 함수 ================
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

# ================ CUDA 지원 여부 확인 함수 ================
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

# ================ 여러 패키지의 설치 상태 확인 함수 ================
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

# ================ 필요한 패키지만 설치 함수 ================
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
    
    show_tagged_progress('INSTALL', f"'{profile_name}' 프로필 패키지 설치 중...", start_time, 0)
    return install_if_needed(profiles[profile_name], start_time)

# ================ PyTorch CUDA 호환성 확인 함수 ================
def check_torch_cuda_compatibility():
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

# ================ PyTorch 설치 정리 함수 ================
def clean_torch_installation():
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


def install_torch_cuda(start_time=None):
    """
    CUDA 지원 PyTorch 설치 래퍼 함수
    
    Args:
        start_time (float): 시작 시간
        
    Returns:
        tuple: (성공 여부, 장치 유형)
    """
    if start_time is None:
        start_time = time.time()
    
    show_tagged_progress('INSTALL', 'CUDA 지원 PyTorch 설치 준비 중...', start_time, 0)
    
    # 현재 설치 확인
    compatible, status, current_version = check_torch_cuda_compatibility()
    
    if compatible and status == "cuda":
        show_progress(f"이미 호환되는 PyTorch({current_version})가 설치되어 있습니다. 재설치하지 않습니다.", start_time, 100)
        with open("pytorch_install_result.txt", "w") as f:
            f.write("CUDA_SUCCESS")
        return True, "cuda"
    
    # 호환되지 않거나 CPU 버전인 경우에만 기존 설치 제거
    if not compatible or status == "cpu":
        show_progress("호환되지 않는 PyTorch 설치 제거 중...", start_time, 30)
        clean_torch_installation()
    
    # CUDA 버전 감지
    cuda_version = detect_cuda()
    show_progress(f"감지된 CUDA 버전: {cuda_version}", start_time, 40)
    
    # CUDA 태그 결정
    if cuda_version:
        major_version = int(float(cuda_version))
        if major_version >= 12:
            cuda_tag = "cu121"
        elif major_version >= 11:
            cuda_tag = "cu118"
        elif major_version >= 10:
            cuda_tag = "cu102"
        else:
            show_progress(f"감지된 CUDA 버전 {cuda_version}는 지원되지 않습니다. CUDA 11.8로 시도합니다.", start_time, 50)
            cuda_tag = "cu118"
    else:
        show_progress("CUDA 버전을 감지할 수 없습니다. CUDA 11.8로 시도합니다.", start_time, 50)
        cuda_tag = "cu118"
    
    # 안정적인 PyTorch 버전 설치
    show_progress(f"{cuda_tag} 버전의 PyTorch 2.2.0 설치 중...", start_time, 60)
    torch_url = f"https://download.pytorch.org/whl/{cuda_tag}"
    
    # 설치 명령 구성
    install_cmd = [
        sys.executable, "-m", "pip", "install", 
        "torch==2.2.0", "torchvision==0.17.0", "torchaudio==2.2.0", 
        "--index-url", torch_url
    ]
    
    try:
        show_progress(f"PyTorch 패키지 다운로드 및 설치 중...", start_time, 70)
        result = subprocess.run(install_cmd, stdout=subprocess.PIPE, stderr=subprocess.PIPE, text=True)
        
        if result.returncode != 0:
            show_progress(f"PyTorch 설치 실패: {result.stderr}", start_time, 90)
            # CPU 버전 시도
            show_progress("CPU 버전으로 대체 설치 시도 중...", start_time, 91)
            cpu_cmd = [
                sys.executable, "-m", "pip", "install",
                "torch==2.2.0", "torchvision==0.17.0", "torchaudio==2.2.0"
            ]
            result = subprocess.run(cpu_cmd, stdout=subprocess.PIPE, stderr=subprocess.PIPE, text=True)
            if result.returncode != 0:
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
        # 모듈 캐시 초기화 및 재로드
        import importlib
        importlib.invalidate_caches()
        
        # 이미 로드된 모듈 제거
        for module in ['torch', 'torchvision', 'torchaudio']:
            if module in sys.modules:
                del sys.modules[module]
        
        # PyTorch 임포트 및 CUDA 확인
        import torch
        if torch.cuda.is_available():
            show_progress(f"CUDA 사용 가능: {torch.cuda.get_device_name(0)}", start_time, 100)
            show_progress(f"PyTorch 버전: {torch.__version__}", start_time, 100)
            show_progress(f"CUDA 버전: {torch.version.cuda}", start_time, 100)
            with open("pytorch_install_result.txt", "w") as f:
                f.write("CUDA_SUCCESS")
            return True, "cuda"
        else:
            show_progress(f"PyTorch {torch.__version__} CPU 버전으로 설치됨", start_time, 100)
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
    GPU 상태 확인 및 정보 반환 래퍼 함수 - nvidia-smi 사용
    
    Args:
        start_time (float): 시작 시간
        
    Returns:
        dict: GPU 정보
    """
    if start_time is None:
        start_time = time.time()
        
    show_progress("GPU 확인 중...", start_time, 0)
    
    # nvidia-smi 명령을 사용하여 GPU 확인
    try:
        # 먼저 CUDA 버전 확인 
        cuda_version = detect_cuda()
        if cuda_version:
            show_progress(f"CUDA 버전 감지됨: {cuda_version}", start_time, 25)
            
            # nvidia-smi 명령으로 GPU 정보 확인
            show_progress("NVIDIA GPU 정보 확인 중...", start_time, 50)
            try:
                # GPU 모델명 및 개수 확인
                nvidia_smi_result = subprocess.run("nvidia-smi --query-gpu=gpu_name,memory.total --format=csv,noheader", 
                                                  shell=True, capture_output=True, text=True)
                
                if nvidia_smi_result.returncode == 0 and nvidia_smi_result.stdout.strip():
                    # 결과 파싱
                    gpu_info_lines = nvidia_smi_result.stdout.strip().split('\n')
                    gpu_count = len(gpu_info_lines)
                    gpu_names = []
                    gpu_memory = []
                    
                    for i, line in enumerate(gpu_info_lines):
                        parts = line.split(',')
                        gpu_name = parts[0].strip()
                        gpu_names.append(gpu_name)
                        
                        # 메모리 정보 추출 (MiB를 GB로 변환)
                        try:
                            mem_mib = float(parts[1].strip().split()[0])  # "12345 MiB" -> 12345
                            mem_gb = mem_mib / 1024  # MiB를 GB로 변환
                            gpu_memory.append(round(mem_gb, 1))
                        except (IndexError, ValueError):
                            gpu_memory.append(None)
                            
                        show_progress(f"GPU {i}: {gpu_name} ({gpu_memory[-1]} GB)", 
                                      start_time, 75 + (i+1) * (25/max(1, gpu_count)))
                    
                    show_progress(f"GPU {gpu_count}개 감지됨, CUDA 버전: {cuda_version}", start_time, 100)
                    return {
                        "available": True,
                        "count": gpu_count,
                        "names": gpu_names,
                        "cuda_version": cuda_version,
                        "memory_gb": gpu_memory
                    }
                else:
                    # nvidia-smi 명령은 성공했지만 GPU 정보를 얻지 못한 경우
                    show_progress("nvidia-smi 명령은 성공했지만 GPU 정보를 얻지 못했습니다.", start_time, 75)
                    return {"available": False}
            except Exception as e:
                # nvidia-smi 명령 실행 중 오류 발생
                show_progress(f"nvidia-smi 명령 실행 오류: {e}", start_time, 75)
                return {"available": False, "error": f"nvidia-smi 명령 오류: {str(e)}"}
        else:
            # CUDA 버전이 감지되지 않은 경우
            show_progress("CUDA 버전을 감지할 수 없습니다. GPU가 없거나 드라이버가 설치되지 않았을 수 있습니다.", start_time, 100)
            return {"available": False}
            
    except Exception as e:
        # 전체 프로세스 오류
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
            success, device = install_torch_cuda(start_time)
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