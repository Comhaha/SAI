# install_pytorch_cuda.py
# 사용자 컴퓨터에 gpu 확인후 적합한 cuda pytorch 설치하게함
# 이미 pytorch가 설치돼 있지만 cuda가 아니면 아니거나 cpu 버전이면 삭제 후 재설치하게함
import sys
import subprocess
import platform
import re
import os
import io

# 표준 출력 스트림 설정
try:
    sys.stdout = io.TextIOWrapper(sys.stdout.buffer, encoding='utf-8')
    sys.stderr = io.TextIOWrapper(sys.stderr.buffer, encoding='utf-8')
except Exception as e:
    # 이미 설정되어 있거나 닫혀있는 경우 무시
    pass

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
    except:
        pass
    return None

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

def install_pytorch_cuda():
    """CUDA 지원 PyTorch 설치"""
    # 현재 설치 확인
    compatible, status, current_version = check_pytorch_cuda_compatibility()
    
    if compatible and status == "cuda":
        print(f"이미 호환되는 PyTorch({current_version})가 설치되어 있습니다. 재설치하지 않습니다.")
        with open("pytorch_install_result.txt", "w") as f:
            f.write("CUDA_SUCCESS")
        return True
    
    # CUDA 버전 감지
    cuda_version = detect_cuda()
    
    # 호환되지 않거나 CPU 버전인 경우에만 기존 설치 제거
    if not compatible or status == "cpu":
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
            print(f"감지된 CUDA 버전 {cuda_version}는 지원되지 않습니다. CUDA 11.8로 시도합니다.")
            cuda_tag = "cu118"
    else:
        print("CUDA 버전을 감지할 수 없습니다. CUDA 11.8로 시도합니다.")
        cuda_tag = "cu118"
    
    # 안정적인 PyTorch 버전 설치
    print(f"PyTorch 1.8.1+cu111 설치 중...")
    torch_url = f"https://download.pytorch.org/whl/cu111"
    
    # PyTorch 1.8.1+cu111 기준으로 통일
    install_cmd = [
        sys.executable, "-m", "pip", "install", 
        "torch==1.8.1+cu111", "torchvision==0.9.1+cu111", "torchaudio==0.8.1",
        "--index-url", torch_url
    ]
    
    result = subprocess.run(install_cmd)
    
    if result.returncode != 0:
        print("PyTorch 설치 실패")
        with open("pytorch_install_result.txt", "w") as f:
            f.write("INSTALL_ERROR")
        return False
    
    # 설치 확인
    print("PyTorch 설치 확인 중...")
    try:
        import torch
        if torch.cuda.is_available():
            print(f"CUDA 사용 가능: {torch.cuda.get_device_name(0)}")
            print(f"PyTorch 버전: {torch.__version__}")
            print(f"CUDA 버전: {torch.version.cuda}")
            with open("pytorch_install_result.txt", "w") as f:
                f.write("CUDA_SUCCESS")
            return True
        else:
            print("PyTorch 설치는 됐지만 CUDA를 감지할 수 없습니다.")
            with open("pytorch_install_result.txt", "w") as f:
                f.write("GPU_NOT_DETECTED")
            return False
    except Exception as e:
        print(f"PyTorch 설치 확인 중 오류: {e}")
        with open("pytorch_install_result.txt", "w") as f:
            f.write(f"INSTALL_ERROR: {e}")
        return False

if __name__ == "__main__":
    print("PyTorch CUDA 설치 스크립트 실행 중...")
    success = install_pytorch_cuda()
    if success:
        print("PyTorch CUDA 설치 완료!")
        sys.exit(0)
    else:
        print("PyTorch CUDA 설치 실패!")
        sys.exit(1)
