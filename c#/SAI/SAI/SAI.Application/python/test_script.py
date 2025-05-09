import os
import sys
import subprocess
import logging
import json
import platform
import re
import time
from datetime import datetime

# 로깅 설정 - 시간 포맷 변경 및 상세 정보 표시
logging.basicConfig(
    level=logging.INFO,
    format='%(asctime)s - %(levelname)s - %(message)s',
    datefmt='%Y-%m-%d %H:%M:%S'
)
logger = logging.getLogger(__name__)

# 진행 상황 표시 함수
def show_progress(message, start_time=None):
    """진행 상황 및 경과 시간 표시"""
    if start_time:
        elapsed = time.time() - start_time
        minutes, seconds = divmod(elapsed, 60)
        time_str = f"[{int(minutes):02d}:{int(seconds):02d}]"
        logger.info(f"{time_str} {message}")
    else:
        logger.info(message)
    
    # C# 애플리케이션에서 쉽게 파싱할 수 있는 태그 추가
    print(f"PROGRESS:{message}")
    sys.stdout.flush()  # 즉시 출력

def install_torch_cuda():
    """CUDA 지원 PyTorch 설치"""
    start_time = time.time()
    show_progress("CUDA 지원 PyTorch 설치 시작...", start_time)
    
    # 기존 PyTorch 제거
    show_progress("기존 PyTorch 제거 중...", start_time)
    subprocess.run([sys.executable, "-m", "pip", "uninstall", "-y", "torch", "torchvision", "torchaudio"])
    
    # 시스템에 맞는 CUDA 버전 감지 시도
    cuda_version = None
    try:
        if platform.system() == "Windows":
            # nvcc로 CUDA 버전 확인 시도
            show_progress("CUDA 버전 감지 중 (nvcc)...", start_time)
            nvcc_result = subprocess.run("nvcc --version", shell=True, capture_output=True, text=True)
            if nvcc_result.returncode == 0:
                match = re.search(r"release (\d+\.\d+)", nvcc_result.stdout)
                if match:
                    cuda_version = match.group(1)
                    show_progress(f"NVCC로 감지한 CUDA 버전: {cuda_version}", start_time)
            
            # NVIDIA 드라이버 확인
            show_progress("NVIDIA 드라이버 확인 중...", start_time)
            nvidia_smi_result = subprocess.run("nvidia-smi", shell=True, capture_output=True, text=True)
            if nvidia_smi_result.returncode == 0:
                show_progress("NVIDIA GPU 드라이버 감지됨", start_time)
                match = re.search(r"CUDA Version: (\d+\.\d+)", nvidia_smi_result.stdout)
                if match:
                    cuda_version = match.group(1)
                    show_progress(f"nvidia-smi로 감지한 CUDA 버전: {cuda_version}", start_time)
    except Exception as e:
        show_progress(f"CUDA 버전 감지 중 오류: {e}", start_time)
    
    # CUDA 버전에 따른 설치 URL 선택
    if cuda_version:
        major_version = int(float(cuda_version))
        if major_version >= 12:
            cuda_tag = "cu121"
        elif major_version >= 11:
            cuda_tag = "cu118"
        elif major_version >= 10:
            cuda_tag = "cu102"
        else:
            show_progress(f"감지된 CUDA 버전 {cuda_version}는 지원되지 않습니다. 최신 버전으로 시도합니다.", start_time)
            cuda_tag = "cu118"  # 안전한 기본값
    else:
        show_progress("CUDA 버전을 감지할 수 없습니다. 최신 CUDA 버전으로 시도합니다.", start_time)
        cuda_tag = "cu118"  # 일반적으로 많이 사용되는 버전
    
    show_progress(f"{cuda_tag} 버전의 PyTorch 설치 중... (약 2-5분 소요)", start_time)
    show_progress("큰 파일을 다운로드하므로 시간이 소요됩니다. 진행 중...", start_time)
    
    # PyTorch CUDA 버전 설치 - 진행 상황 표시 개선
    torch_url = f"https://download.pytorch.org/whl/{cuda_tag}"
    install_cmd = [
        sys.executable, "-m", "pip", "install", 
        "torch", "torchvision", "torchaudio", 
        "--index-url", torch_url,
        "--verbose"  # 상세 출력
    ]
    
    # 실시간 출력 캡처를 위한 Popen 사용
    process = subprocess.Popen(
        install_cmd,
        stdout=subprocess.PIPE,
        stderr=subprocess.STDOUT,
        universal_newlines=True,
        bufsize=1
    )
    
    # 10초마다 진행 중임을 표시
    last_progress = time.time()
    for line in iter(process.stdout.readline, ''):
        if line.strip():
            # 중요한 내용만 선별하여 표시
            if "Downloading" in line or "Installing" in line or "ERROR" in line:
                show_progress(line.strip(), start_time)
                last_progress = time.time()
            # 주기적으로 진행 중임을 표시
            elif time.time() - last_progress > 10:
                show_progress("PyTorch 설치 진행 중...", start_time)
                last_progress = time.time()
    
    process.wait()
    
    # 설치 확인
    try:
        show_progress("PyTorch 설치 확인 중...", start_time)
        import torch
        if torch.cuda.is_available():
            gpu_name = torch.cuda.get_device_name(0)
            show_progress(f"PyTorch {torch.__version__} 설치 성공! CUDA 사용 가능", start_time)
            show_progress(f"감지된 GPU: {gpu_name}", start_time)
            
            # 총 설치 시간 표시
            elapsed = time.time() - start_time
            minutes, seconds = divmod(elapsed, 60)
            show_progress(f"총 설치 시간: {int(minutes)}분 {int(seconds)}초", start_time)
            
            return True, "cuda"
        else:
            show_progress(f"PyTorch {torch.__version__} 설치됨, 그러나 CUDA를 감지할 수 없습니다.", start_time)
            show_progress("NVIDIA 드라이버가 제대로 설치되어 있는지 확인해주세요.", start_time)
            return False, "cpu"
    except ImportError:
        show_progress("PyTorch 설치 실패", start_time)
        return False, "cpu"

def check_gpu():
    """GPU 상태 확인 및 정보 반환"""
    start_time = time.time()
    show_progress("GPU 확인 중...", start_time)
    
    try:
        import torch
        if torch.cuda.is_available():
            gpu_count = torch.cuda.device_count()
            gpu_names = [torch.cuda.get_device_name(i) for i in range(gpu_count)]
            cuda_version = torch.version.cuda
            gpu_memory = []
            
            show_progress(f"GPU {gpu_count}개 감지됨", start_time)
            
            for i in range(gpu_count):
                try:
                    props = torch.cuda.get_device_properties(i)
                    mem_gb = props.total_memory / (1024**3)
                    gpu_memory.append(round(mem_gb, 1))
                    show_progress(f"GPU {i}: {gpu_names[i]} ({gpu_memory[-1]} GB)", start_time)
                except:
                    gpu_memory.append(None)
                    show_progress(f"GPU {i}: {gpu_names[i]} (메모리 정보 없음)", start_time)
            
            show_progress(f"CUDA 버전: {cuda_version}", start_time)
            
            return {
                "available": True,
                "count": gpu_count,
                "names": gpu_names,
                "cuda_version": cuda_version,
                "memory_gb": gpu_memory
            }
        else:
            show_progress("GPU 감지 안됨: CPU 모드로 실행합니다.", start_time)
            return {"available": False}
    except Exception as e:
        show_progress(f"GPU 확인 오류: {e}", start_time)
        return {"available": False, "error": str(e)}

def fix_data_path(data_path):
    """데이터 경로 수정 및 확인"""
    start_time = time.time()
    show_progress(f"데이터 경로 확인: {data_path}", start_time)
    
    # 경로 정규화
    normalized_path = os.path.normpath(data_path)
    
    # 경로가 존재하는지 확인
    if os.path.exists(normalized_path):
        show_progress(f"데이터 파일 확인됨: {normalized_path}", start_time)
        return normalized_path
    
    # 경로가 존재하지 않으면 대체 방법 시도
    show_progress(f"데이터 파일이 존재하지 않음: {normalized_path}", start_time)
    show_progress("대체 경로 탐색 중...", start_time)
    
    # 가능한 대체 경로 찾기
    dir_name = os.path.dirname(normalized_path)
    if os.path.exists(dir_name):
        show_progress(f"디렉토리는 존재함: {dir_name}", start_time)
        
        # 디렉토리 내 파일 목록 확인
        files = os.listdir(dir_name)
        yaml_files = [f for f in files if f.endswith('.yaml')]
        
        if yaml_files:
            new_path = os.path.join(dir_name, yaml_files[0])
            show_progress(f"대체 데이터 파일 발견: {new_path}", start_time)
            return new_path
        else:
            show_progress(f"디렉토리에 YAML 파일이 없음: {dir_name}", start_time)
    
    # 디렉토리 내부 탐색
    show_progress("데이터 파일 검색 중...", start_time)
    parent_dir = os.path.dirname(dir_name)
    if os.path.exists(parent_dir):
        for root, dirs, files in os.walk(parent_dir):
            yaml_files = [f for f in files if f.endswith('.yaml')]
            if yaml_files:
                new_path = os.path.join(root, yaml_files[0])
                show_progress(f"대체 데이터 파일 발견: {new_path}", start_time)
                return new_path
    
    # data.yaml 직접 생성 (마지막 수단)
    try:
        show_progress(f"데이터 파일을 찾을 수 없어 새로 생성합니다: {normalized_path}", start_time)
        os.makedirs(os.path.dirname(normalized_path), exist_ok=True)
        with open(normalized_path, 'w') as f:
            f.write("""
path: .
train: train/images
val: valid/images
test: test/images

names:
  0: strawberry
""")
        show_progress(f"데이터 파일 생성 완료: {normalized_path}", start_time)
        return normalized_path
    except Exception as e:
        show_progress(f"데이터 파일 생성 실패: {e}", start_time)
        raise
    
    return normalized_path

def main():
    """메인 실행 함수"""
    total_start_time = time.time()
    current_date = datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    show_progress(f"AI 블록 코딩 튜토리얼 모드 실행 시작 - {current_date}", total_start_time)
    
    # 1. 필수 패키지 설치
    package_start_time = time.time()
    show_progress("필수 패키지 설치 중... (1/7)", total_start_time)
    
    # 설치 진행 상황 표시를 위한 Popen 사용
    pip_process = subprocess.Popen(
        [sys.executable, "-m", "pip", "install", "ultralytics", "--verbose"],
        stdout=subprocess.PIPE,
        stderr=subprocess.STDOUT,
        universal_newlines=True
    )
    
    # 10초마다 진행 상황 업데이트
    last_update = time.time()
    for line in iter(pip_process.stdout.readline, ''):
        if "Installing" in line or "Requirement" in line:
            show_progress(f"ultralytics: {line.strip()}", total_start_time)
            last_update = time.time()
        elif time.time() - last_update > 10:
            show_progress("ultralytics 설치 진행 중...", total_start_time)
            last_update = time.time()
    
    # roboflow 설치
    show_progress("roboflow 설치 중...", total_start_time)
    roboflow_process = subprocess.Popen(
        [sys.executable, "-m", "pip", "install", "roboflow", "--verbose"],
        stdout=subprocess.PIPE,
        stderr=subprocess.STDOUT,
        universal_newlines=True
    )
    
    last_update = time.time()
    for line in iter(roboflow_process.stdout.readline, ''):
        if "Installing" in line or "Requirement" in line:
            show_progress(f"roboflow: {line.strip()}", total_start_time)
            last_update = time.time()
        elif time.time() - last_update > 10:
            show_progress("roboflow 설치 진행 중...", total_start_time)
            last_update = time.time()
    
    pkg_elapsed = time.time() - package_start_time
    show_progress(f"패키지 설치 완료 (소요 시간: {int(pkg_elapsed)}초)", total_start_time)
    
    # 2. PyTorch 설치 확인 및 CUDA 지원 확인
    torch_start_time = time.time()
    show_progress("PyTorch 확인 중... (2/7)", total_start_time)
    try:
        import torch
        show_progress(f"이미 설치된 PyTorch 감지: {torch.__version__}", total_start_time)
        
        # CUDA 지원 확인
        if not torch.cuda.is_available():
            show_progress("설치된 PyTorch에서 CUDA를 감지할 수 없습니다. CUDA 지원 버전으로 재설치합니다.", total_start_time)
            show_progress("이 작업은 약 2-5분 소요될 수 있습니다...", total_start_time)
            cuda_available, device = install_torch_cuda()
        else:
            cuda_available = True
            device = "cuda"
            gpu_name = torch.cuda.get_device_name(0)
            show_progress(f"CUDA 지원 확인됨. GPU: {gpu_name}", total_start_time)
    except ImportError:
        show_progress("PyTorch가 설치되어 있지 않습니다. CUDA 지원 버전으로 설치합니다.", total_start_time)
        show_progress("이 작업은 약 2-5분 소요될 수 있습니다...", total_start_time)
        cuda_available, device = install_torch_cuda()
    
    torch_elapsed = time.time() - torch_start_time
    show_progress(f"PyTorch 확인 완료 (소요 시간: {int(torch_elapsed)}초)", total_start_time)
    
    # 3. GPU 정보 확인
    gpu_start_time = time.time()
    show_progress("GPU 정보 확인 중... (3/7)", total_start_time)
    gpu_info = check_gpu()
    gpu_elapsed = time.time() - gpu_start_time
    show_progress(f"GPU 정보 확인 완료 (소요 시간: {int(gpu_elapsed)}초)", total_start_time)
    
    # 4. YOLO 모델 로드
    model_start_time = time.time()
    show_progress("YOLOv8 모델 로드 중... (4/7)", total_start_time)
    from ultralytics import YOLO
    model = YOLO("yolov8n.pt")
    model_elapsed = time.time() - model_start_time
    show_progress(f"YOLOv8 모델 로드 완료! (소요 시간: {int(model_elapsed)}초)", total_start_time)
    
    # 5. Roboflow에서 데이터셋 다운로드
    data_start_time = time.time()
    show_progress("Roboflow에서 데이터셋 다운로드 중... (5/7)", total_start_time)
    show_progress("이 작업은 약 1-3분 소요될 수 있습니다...", total_start_time)
    
    try:
        from roboflow import Roboflow
        rf = Roboflow(api_key="ozRmezJLAsrdO3TrXlwo")
        show_progress("Roboflow 워크스페이스 로딩 중...", total_start_time)
        project = rf.workspace("ddd-1enry").project("strawberry-ds51b")
        show_progress("Roboflow 프로젝트 로딩 중...", total_start_time)
        version = project.version(1)
        show_progress("데이터셋 다운로드 중... (파일 크기에 따라 1-3분 소요)", total_start_time)
        
        # 다운로드 시작 시간 기록
        download_start = time.time()
        dataset = version.download("yolov8")
        download_elapsed = time.time() - download_start
        
        show_progress(f"데이터셋 다운로드 완료: {dataset.location} (소요 시간: {int(download_elapsed)}초)", total_start_time)
    except Exception as e:
        show_progress(f"데이터셋 다운로드 중 오류 발생: {e}", total_start_time)
        # 대체 데이터셋 경로 설정 (로컬에 이미 있는 경우)
        dataset = type('obj', (), {'location': os.path.join(os.getcwd(), 'strawberry-1')})
        show_progress(f"대체 데이터셋 경로 사용: {dataset.location}", total_start_time)
    
    data_elapsed = time.time() - data_start_time
    show_progress(f"데이터셋 준비 완료 (총 소요 시간: {int(data_elapsed)}초)", total_start_time)
    
    # 6. 데이터 경로 수정 및 확인
    path_start_time = time.time()
    show_progress("데이터 경로 확인 중... (6/7)", total_start_time)
    data_yaml_path = os.path.join(dataset.location, "data.yaml")
    fixed_data_path = fix_data_path(data_yaml_path)
    path_elapsed = time.time() - path_start_time
    show_progress(f"데이터 경로 확인 완료 (소요 시간: {int(path_elapsed)}초)", total_start_time)
    
    # 7. 학습 파라미터 설정 및 실행
    train_start_time = time.time()
    show_progress("모델 학습 준비 중... (7/7)", total_start_time)
    
    batch_size = 16
    if device == "cuda" and gpu_info.get("available", False):
        # GPU 메모리에 따른 배치 크기 조정
        memory = gpu_info.get("memory_gb", [0])[0]
        if memory and memory < 6:
            batch_size = 8
            show_progress(f"GPU 메모리 제한으로 배치 크기 {batch_size}로 조정", total_start_time)
    
    show_progress(f"모델 학습 시작 (디바이스: {device}, 배치 크기: {batch_size}, 에폭: 100)", total_start_time)
    show_progress("학습 중... (YOLOv8 진행 상황이 표시됩니다)", total_start_time)
    show_progress("이 작업은 GPU 사용 시 약 10-30분, CPU 사용 시 1-3시간 소요될 수 있습니다", total_start_time)
    
    try:
        model.train(
            data=fixed_data_path,
            epochs=100,
            batch=batch_size,
            imgsz=640,
            device=device
        )
        train_elapsed = time.time() - train_start_time
        min, sec = divmod(train_elapsed, 60)
        show_progress(f"모델 학습 완료! (소요 시간: {int(min)}분 {int(sec)}초)", total_start_time)
    except Exception as e:
        show_progress(f"학습 중 오류 발생: {e}", total_start_time)
        
        # 메모리 부족 오류 처리
        if "CUDA out of memory" in str(e):
            show_progress("GPU 메모리 부족. 배치 크기를 줄여서 다시 시도합니다.", total_start_time)
            try:
                # 배치 크기 절반으로 줄임
                reduced_batch = max(1, batch_size // 2)
                retry_start = time.time()
                show_progress(f"줄어든 배치 크기로 재시도 중 (배치 크기: {reduced_batch})...", total_start_time)
                
                model.train(
                    data=fixed_data_path,
                    epochs=100,
                    batch=reduced_batch,
                    imgsz=640,
                    device=device
                )
                
                retry_elapsed = time.time() - retry_start
                min, sec = divmod(retry_elapsed, 60)
                show_progress(f"배치 크기 {reduced_batch}로 학습 완료! (소요 시간: {int(min)}분 {int(sec)}초)", total_start_time)
            except Exception as e2:
                show_progress(f"재시도도 실패: {e2}", total_start_time)
                # CPU로 전환
                show_progress("CPU 모드로 전환합니다...", total_start_time)
                cpu_start = time.time()
                show_progress("CPU로 학습 중 (이 작업은 1-3시간 소요될 수 있습니다)...", total_start_time)
                
                model.train(
                    data=fixed_data_path,
                    epochs=50,  # CPU에서는 에폭 수 줄임
                    batch=4,
                    imgsz=640,
                    device="cpu"
                )
                
                cpu_elapsed = time.time() - cpu_start
                hrs, remainder = divmod(cpu_elapsed, 3600)
                mins, secs = divmod(remainder, 60)
                show_progress(f"CPU로 학습 완료! (소요 시간: {int(hrs)}시간 {int(mins)}분 {int(secs)}초)", total_start_time)
        
        # 데이터 경로 오류 처리
        elif "does not exist" in str(e) or "No such file" in str(e):
            show_progress("데이터 경로 오류. data.yaml 파일을 찾을 수 없습니다.", total_start_time)
            show_progress("수동으로 data.yaml 파일을 생성합니다...", total_start_time)
            
            # 데이터 디렉토리 확인
            data_dir = dataset.location
            if os.path.exists(data_dir):
                # 디렉토리 구조 확인
                dirs = os.listdir(data_dir)
                show_progress(f"데이터 디렉토리 내용: {dirs}", total_start_time)
                
                # data.yaml 직접 생성
                manual_yaml_path = os.path.join(data_dir, "data.yaml")
                with open(manual_yaml_path, 'w') as f:
                    f.write(f"""
path: {data_dir}
train: train/images
val: valid/images
test: test/images

names:
  0: strawberry
""")
                
                # 재시도
                show_progress("data.yaml 생성 완료. 학습 재시도...", total_start_time)
                retry_start = time.time()
                
                model.train(
                    data=manual_yaml_path,
                    epochs=100,
                    batch=batch_size if device == "cuda" else 4,
                    imgsz=640,
                    device=device
                )
                
                retry_elapsed = time.time() - retry_start
                min, sec = divmod(retry_elapsed, 60)
                show_progress(f"모델 학습 완료! (소요 시간: {int(min)}분 {int(sec)}초)", total_start_time)
    
    # 8. 학습 결과 처리 중
    show_progress("학습 결과 처리 중...", total_start_time)
    
    # 결과 저장 경로
    results_dir = "runs/detect/train"
    
    # 9. 학습 완료 알림
    total_elapsed = time.time() - total_start_time
    hrs, remainder = divmod(total_elapsed, 3600)
    mins, secs = divmod(remainder, 60)
    
    show_progress(f"튜토리얼 모드 실행 완료! (총 소요 시간: {int(hrs)}시간 {int(mins)}분 {int(secs)}초)", total_start_time)
    show_progress(f"학습된 모델 경로: {results_dir}/weights/best.pt", total_start_time)
    
    # 결과 정보
    result = {
        "success": True,
        "model_path": f"{results_dir}/weights/best.pt",
        "results_path": f"{results_dir}/results.png",
        "device_used": device,
        "gpu_info": gpu_info,
        "total_time_seconds": total_elapsed,
        "timestamp": datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    }
    
    # JSON으로 결과 출력 (C# 프로그램에서 파싱)
    print(f"RESULT_JSON:{json.dumps(result)}")
    return result

if __name__ == "__main__":
    try:
        main()
    except Exception as e:
        logger.error(f"프로그램 실행 중 오류 발생: {e}")
        error_result = {
            "success": False,
            "error": str(e),
            "timestamp": datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        }
        print(f"RESULT_JSON:{json.dumps(error_result)}")