import os
import sys
import subprocess
import logging
import json
import platform
import re
import time
import threading
import zipfile
import glob
import io
from datetime import datetime

# 표준 출력 스트림 설정
try:
    sys.stdout = io.TextIOWrapper(sys.stdout.buffer, encoding='utf-8')
    sys.stderr = io.TextIOWrapper(sys.stderr.buffer, encoding='utf-8')
except Exception as e:
    # 이미 설정되어 있거나 닫혀있는 경우 무시
    pass

# base_dir을 스크립트 실행 기준으로 설정
# 
base_dir = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
print(f"Base directory: {base_dir}")

# 로깅 설정 - 시간 포맷 변경 및 상세 정보 표시
logging.basicConfig(
    encoding='utf-8',
    level=logging.INFO,
    format='%(asctime)s - %(levelname)s - %(message)s',
    datefmt='%Y-%m-%d %H:%M:%S'
)
logger = logging.getLogger(__name__)

# 진행 상황 표시 함수
def show_progress(message, start_time=None, progress=None):
    """진행 상황 및 경과 시간 표시"""
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
        
        # C# 애플리케이션에서 쉽게 파싱할 수 있는 태그 추가
        if progress is not None:
            print(f"PROGRESS:{progress:.1f}:{message}", flush=True)
        else:
            print(f"PROGRESS::{message}", flush=True)
    except Exception as e:
        logger.error(f"show_progress 오류: {e}")

# pip 설치 진행률 추적 클래스
class PipProgressTracker:
    def __init__(self, packages, start_time, total_steps=100):
        self.packages = packages
        self.start_time = start_time
        self.total_steps = total_steps
        self.current_step = 0
        self.found_packages = set()
        self.completed = False
        self.last_progress_time = time.time()
    
    def update_from_output(self, line):
        """pip 출력에서 진행 상태 업데이트"""
        for package in self.packages:
            if package in line.lower() and package not in self.found_packages:
                self.found_packages.add(package)
                self.current_step += (self.total_steps / len(self.packages)) * 0.5
                show_progress(f"패키지 {package} 설치 중: {line}", self.start_time, self.current_step)
                return True
        
        if "collecting" in line.lower() or "downloading" in line.lower():
            self.current_step += 0.5
            self.current_step = min(self.current_step, self.total_steps * 0.7)  # 최대 70%까지만
            show_progress(f"패키지 다운로드 중: {line}", self.start_time, self.current_step)
            self.last_progress_time = time.time()
            return True
            
        if "installing collected packages" in line.lower():
            self.current_step = self.total_steps * 0.8
            show_progress(f"패키지 설치 중: {line}", self.start_time, self.current_step)
            self.last_progress_time = time.time()
            return True
            
        if "successfully installed" in line.lower():
            self.current_step = self.total_steps
            self.completed = True
            show_progress(f"패키지 설치 완료: {line}", self.start_time, self.current_step)
            return True
        
        # 일정 시간 경과시 진행 중임을 표시
        if time.time() - self.last_progress_time > 5:
            show_progress("패키지 설치 진행 중...", self.start_time, self.current_step)
            self.last_progress_time = time.time()
            return True
            
        return False

def install_packages_with_progress(packages, start_time):
    """패키지 설치 및 진행률 표시"""
    if not isinstance(packages, list):
        packages = [packages]
    
    # 설치 진행 상황 추적기 생성
    tracker = PipProgressTracker(packages, start_time)
    
    # 설치 명령 준비
    install_cmd = [
        sys.executable, "-m", "pip", "install", 
        *packages, 
        "--verbose"
    ]
    
    # 실시간 출력 캡처를 위한 Popen 사용
    process = subprocess.Popen(
        install_cmd,
        stdout=subprocess.PIPE,
        stderr=subprocess.STDOUT,
        universal_newlines=True,
        bufsize=1
    )
    
    # 출력 처리
    for line in iter(process.stdout.readline, ''):
        tracker.update_from_output(line.strip())
    
    process.wait()
    
    # 설치 완료 확인
    if not tracker.completed:
        tracker.current_step = 100
        show_progress(f"{', '.join(packages)} 설치 완료", start_time, 100)
    
    return process.returncode == 0

def install_torch_cuda():
    """별도 프로세스에서 CUDA 지원 PyTorch 설치"""
    start_time = time.time()
    show_progress("CUDA 지원 PyTorch 설치 준비 중...", start_time, 0)
    
    # 스크립트 경로
    script_path = os.path.join(base_dir, "scripts", "install_pytorch_cuda.py")
    
    # 별도 프로세스에서 스크립트 실행
    show_progress("별도 프로세스에서 PyTorch 설치 실행 중...", start_time, 20)
    subprocess.run([sys.executable, script_path])
    
    # 결과 확인
    result_path = os.path.join(os.path.dirname(os.path.abspath(__file__)), "pytorch_install_result.txt")
    if os.path.exists(result_path):
        with open(result_path, "r") as f:
            result = f.read().strip()
        
        # 결과 파일 삭제
        try:
            os.remove(result_path)
        except:
            pass
        
        if result == "CUDA_SUCCESS":
            show_progress("PyTorch CUDA 설치 성공!", start_time, 100)
            return True, "cuda"
        elif result == "GPU_NOT_DETECTED":
            show_progress("PyTorch 설치는 성공했으나 GPU를 감지할 수 없습니다.", start_time, 100)
            return False, "cpu"
        else:
            show_progress(f"PyTorch 설치 중 오류 발생: {result}", start_time, 100)
            return False, "cpu"
    else:
        show_progress("PyTorch 설치 결과를 확인할 수 없습니다.", start_time, 100)
        return False, "cpu"

def download_dataset_with_progress(start_time):
    """서버에서 데이터셋 다운로드 및 진행률 표시"""
    # 데이터셋 저장 경로 설정, 덮어쓰기 
    dataset_dir = os.path.join(base_dir, "dataset", "tutorial_dataset")
    os.makedirs(dataset_dir, exist_ok=True)
    show_progress(f"데이터셋 기본 경로: {dataset_dir}", start_time, 70)

    # ZIP 파일 경로 정의
    zip_path = os.path.join(dataset_dir, "tutorial_dataset.zip")
    
    # ZIP 파일 압축 해제
    if os.path.exists(zip_path):
        try:
            with zipfile.ZipFile(zip_path, 'r') as zip_ref:
                file_list = zip_ref.namelist()
                total_files = len(file_list)
                show_progress(f"압축 파일 내 {total_files}개 파일 발견", start_time, 92)
                
                # 압축 해제 진행률 표시
                for i, file in enumerate(file_list):
                    zip_ref.extract(file, dataset_dir)  # tutorial_dataset 폴더에 압축 해제
                    if i % 50 == 0 or i == total_files - 1:  # 50개 파일마다 또는 마지막 파일에서 진행률 표시
                        extract_progress = 92 + (i / total_files) * 8  # 92% ~ 100% 범위
                        show_progress(f"압축 해제 중: {i+1}/{total_files} 파일", start_time, extract_progress)
            
            show_progress("압축 해제 완료", start_time, 100)
            
            # 임시 ZIP 파일 삭제
            try:
                os.remove(zip_path)
                show_progress("임시 ZIP 파일 삭제 완료", start_time, 100)
            except:
                show_progress("임시 ZIP 파일 삭제 실패", start_time, 100)
        except Exception as e:
            show_progress(f"ZIP 파일 압축 해제 오류: {e}", start_time, 95)
    else:
        show_progress("다운로드된 ZIP 파일을 찾을 수 없습니다.", start_time, 95)
    
    # 데이터셋 경로
    # 
    return type('obj', (), {'location': dataset_dir})

def find_yaml_file(dataset_dir, start_time):
    """데이터셋 디렉토리에서 data.yaml 파일 찾기"""
    show_progress(f"데이터 경로 확인: {dataset_dir}", start_time, 0)
    
    # 기본 data.yaml 경로
    yaml_path = os.path.join(dataset_dir, "data.yaml")
    
    # data.yaml 파일이 있는지 확인
    if (os.path.exists(yaml_path)):
        show_progress(f"데이터 파일 확인됨: {yaml_path}", start_time, 100)
        return yaml_path
    
    # 기본 위치에 없으면 모든 하위 디렉토리에서 검색
    show_progress("데이터 파일을 검색 중...", start_time, 50)
    
    for root, dirs, files in os.walk(dataset_dir):
        for file in files:
            if file == 'data.yaml':
                yaml_path = os.path.join(root, file)
                show_progress(f"데이터 파일 발견: {yaml_path}", start_time, 100)
                return yaml_path
    
    # 파일을 찾지 못했을 경우
    show_progress(f"data.yaml 파일을 찾을 수 없습니다: {yaml_path}", start_time, 100)
    return yaml_path

def check_gpu():
    """GPU 상태 확인 및 정보 반환"""
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
    
def find_latest_results_dir():
    """가장 최근에 생성된 results 디렉토리 찾기"""
    base_runs_dir = os.path.join(base_dir, "runs", "detect")
    
    if not os.path.exists(base_runs_dir):
        # 디렉토리가 없으면 생성
        os.makedirs(base_runs_dir, exist_ok=True)
        return os.path.join(base_dir, "runs", "detect", "train")
    
    # 'train'으로 시작하는 모든 폴더 찾기
    train_dirs = [d for d in os.listdir(base_runs_dir) if d.startswith('train')]
    if not train_dirs:
        return os.path.join(base_dir, "runs", "detect", "train")
    
    # 숫자 접미사가 있는 경우 가장 큰 숫자 찾기
    latest_dir = "train"
    max_num = 0
    for d in train_dirs:
        # train, train1, train2, ... 형식에서 숫자 추출
        match = re.match(r'train(\d*)', d)
        if match:
            num_str = match.group(1)
            num = int(num_str) if num_str else 0
            if num > max_num:
                max_num = num
                latest_dir = d
    
    return os.path.join(base_dir, "runs", "detect", latest_dir)
    
def visualize_training_results(results_path, start_time):
    """학습 결과 그래프 시각화"""
    try:
        # 결과 이미지 경로 확인
        if not os.path.exists(results_path):
            show_progress(f"결과 그래프 파일을 찾을 수 없습니다: {results_path}", start_time, 100)
            return False
        
        show_progress(f"학습 결과 그래프 확인: {results_path}", start_time, 100)
        
        # 여기서는 파일 경로만 반환 (실제 표시는 C# UI에서 수행)
        return results_path
    except Exception as e:
        show_progress(f"결과 시각화 오류: {e}", start_time, 100)
        return False    

def run_inference(model_path, image_path, start_time, conf_threshold=0.25, show=True):
    """모델을 사용해 이미지에서 객체 탐지 수행"""
    try:
        # 모델 경로 및 이미지 경로 확인
        if not os.path.exists(model_path):
            show_progress(f"모델 파일을 찾을 수 없습니다: {model_path}", start_time, 0)
            return None
        
        if not os.path.exists(image_path):
            show_progress(f"이미지 파일을 찾을 수 없습니다: {image_path}", start_time, 0)
            return None
        
        show_progress(f"모델 로드 중: {model_path}", start_time, 10)
        from ultralytics import YOLO
        model = YOLO(model_path)
        
        show_progress(f"이미지 추론 중: {image_path}", start_time, 30)
        results = model.predict(source=image_path, save=False, show=False, conf=conf_threshold)
        
        if not results or len(results) == 0:
            show_progress("추론 결과가 없습니다", start_time, 50)
            return None
        
        # 결과 처리
        show_progress("추론 결과 처리 중...", start_time, 70)
        
        # 결과 시각화
        result_img = results[0].plot()  # BGR 형식
        
        # 결과 이미지 저장
        output_dir = base_dir  # Python 폴더 사용
        output_path = os.path.join(output_dir, "inference_result.jpg")
        import cv2
        cv2.imwrite(output_path, result_img)

        import matplotlib
        matplotlib.use("TkAgg")

        # ✅ matplotlib 시각화 추가
        import matplotlib.pyplot as plt
        result_rgb = cv2.cvtColor(result_img, cv2.COLOR_BGR2RGB)
        plt.imshow(result_rgb)
        plt.axis("off")
        plt.title("YOLOv8 Prediction")
        plt.show()
        input("🔍 창이 떴나요? 아무 키나 눌러 종료하세요.")
        
        # 탐지 결과 추출 (JSON으로 반환하기 위함)
        detections = []
        if hasattr(results[0], 'boxes') and results[0].boxes is not None:
            for box in results[0].boxes:
                # 박스 좌표
                x1, y1, x2, y2 = box.xyxy[0].tolist()
                
                # 클래스 및 신뢰도
                cls = int(box.cls[0].item())
                conf = float(box.conf[0].item())
                
                # 클래스 이름
                cls_name = results[0].names[cls]
                
                detections.append({
                    "class": cls_name,
                    "confidence": conf,
                    "bbox": [x1, y1, x2, y2]
                })
        
        show_progress(f"추론 완료: {len(detections)}개 객체 감지됨", start_time, 100)
        
        return {
            "image_path": image_path,
            "result_image": output_path,
            "detections": detections
        }
        
    except Exception as e:
        show_progress(f"추론 오류: {e}", start_time, 100)
        return None

def main():
    """메인 실행 함수"""
    total_start_time = time.time()
    current_date = datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    show_progress(f"AI 블록 코딩 튜토리얼 모드 실행 시작 - {current_date}", total_start_time, 0)
    
    # 1. 필수 패키지 설치
    package_start_time = time.time()
    show_progress("필수 패키지 설치 중... (1/7)", total_start_time, 0)

    # NumPy 다운그레이드
    show_progress("NumPy 다운그레이드 중...", package_start_time, 0)
    install_packages_with_progress("numpy==1.24.3", package_start_time)
        
    # ultralytics 설치
    show_progress("ultralytics 패키지 설치 중...", package_start_time, 0)
    install_packages_with_progress("ultralytics", package_start_time)

    # OpenCV 설치 추가
    show_progress("opencv-python 패키지 설치 중...", package_start_time, 0)
    install_packages_with_progress("opencv-python", package_start_time)
    
    pkg_elapsed = time.time() - package_start_time
    show_progress(f"패키지 설치 완료 (소요 시간: {int(pkg_elapsed)}초)", total_start_time, 100)
   
    # 2. PyTorch 설치 확인 및 CUDA 지원 확인
    torch_start_time = time.time()
    show_progress("PyTorch 확인 중... (2/7)", total_start_time, 0)

    # 별도 프로세스에서 PyTorch 설치
    cuda_available, device = install_torch_cuda()  # 여기서 반환 값을 올바르게 받아야 함

    torch_elapsed = time.time() - torch_start_time
    show_progress(f"PyTorch 확인 완료 (소요 시간: {int(torch_elapsed)}초)", total_start_time, 100)
    
    # 3. GPU 정보 확인
    gpu_start_time = time.time()
    show_progress("GPU 정보 확인 중... (3/7)", total_start_time, 0)
    gpu_info = check_gpu()
    gpu_elapsed = time.time() - gpu_start_time
    show_progress(f"GPU 정보 확인 완료 (소요 시간: {int(gpu_elapsed)}초)", total_start_time, 100)
    
    # 4. YOLO 모델 로드
    model_start_time = time.time()
    show_progress("YOLOv8 모델 로드 중... (4/7)", total_start_time, 0)
    from ultralytics import YOLO
    model_path = os.path.join(base_dir, "yolov8n.pt")
    model = YOLO(model_path)
    model_elapsed = time.time() - model_start_time
    show_progress(f"YOLOv8 모델 로드 완료! (소요 시간: {int(model_elapsed)}초)", total_start_time, 100)
    
    # 5. 서버에서 데이터셋 다운로드
    data_start_time = time.time()
    show_progress("서버에서 데이터셋 다운로드 중... (5/7)", total_start_time, 0)
    dataset = download_dataset_with_progress(data_start_time)
    data_elapsed = time.time() - data_start_time
    show_progress(f"데이터셋 준비 완료 (총 소요 시간: {int(data_elapsed)}초)", total_start_time, 100)
    
   # 6. 데이터 경로 확인
    path_start_time = time.time()
    show_progress("데이터 경로 확인 중... (6/7)", total_start_time, 0)
    # dataset 객체의 location 속성을 사용
    tutorial_dataset_dataset_dir = dataset.location
    data_yaml_path = os.path.join(tutorial_dataset_dataset_dir, "data.yaml")
    path_elapsed = time.time() - path_start_time
    show_progress(f"데이터 경로 확인 완료 (소요 시간: {int(path_elapsed)}초)", total_start_time, 100)
    
    # 7. 학습 파라미터 설정 및 실행
    train_start_time = time.time()
    show_progress("모델 학습 준비 중... (7/7)", total_start_time, 0)
    
    batch_size = 16
    if device == "cuda" and gpu_info.get("available", False):
        # GPU 메모리에 따른 배치 크기 조정
        memory = gpu_info.get("memory_gb", [0])[0]
        if memory and memory < 6:
            batch_size = 8
            show_progress(f"GPU 메모리 제한으로 배치 크기 {batch_size}로 조정", total_start_time, 10)
    
    # 에폭 수
    epochs = 2 if device == "cuda" else 1
    
    show_progress(f"모델 학습 시작 (디바이스: {device}, 배치 크기: {batch_size}, 에폭: {epochs})", total_start_time, 20)
    show_progress("학습 중... (YOLOv8 진행 상황이 표시됩니다)", total_start_time, 30)
    show_progress("이 작업은 GPU 사용 시 약 10-30분, CPU 사용 시 1-3시간 소요될 수 있습니다", total_start_time, 40)
    
    try:
        # 학습 시작 시간 기록
        epoch_start_time = time.time()
        last_progress_update = time.time()
        
        # 학습 진행 상태를 모니터링할 변수들
        completed_epochs = 0
        total_epochs = epochs
        
        # 학습 진행률을 주기적으로 업데이트하는 함수
        def update_training_progress():
            nonlocal completed_epochs
            nonlocal last_progress_update
            
            while completed_epochs < total_epochs:
                time.sleep(5)  # 5초마다 확인
                
                # 현재 시간 기록
                current_time = time.time()
                
                # 10초마다 진행 상황 업데이트
                if current_time - last_progress_update >= 10:
                    progress = (completed_epochs / total_epochs) * 100
                    elapsed = current_time - epoch_start_time
                    minutes, seconds = divmod(elapsed, 60)
                    
                    # 잔여 시간 추정
                    if completed_epochs > 0:
                        time_per_epoch = elapsed / completed_epochs
                        remaining_epochs = total_epochs - completed_epochs
                        remaining_time = time_per_epoch * remaining_epochs
                        rem_minutes, rem_seconds = divmod(remaining_time, 60)
                        
                        show_progress(
                            f"학습 중: {completed_epochs}/{total_epochs} 에폭 완료 "
                            f"({int(minutes)}분 {int(seconds)}초 경과, 약 {int(rem_minutes)}분 {int(rem_seconds)}초 남음)", 
                            total_start_time, 
                            40 + (progress * 0.6)  # 40% ~ 100% 범위
                        )
                    else:
                        show_progress(
                            f"학습 중: {completed_epochs}/{total_epochs} 에폭 완료 "
                            f"({int(minutes)}분 {int(seconds)}초 경과)", 
                            total_start_time, 
                            40 + (progress * 0.6)
                        )
                    
                    last_progress_update = current_time
        
        # 별도 스레드에서 진행 상황 업데이트
        progress_thread = threading.Thread(target=update_training_progress)
        progress_thread.daemon = True
        progress_thread.start()
        
        # 학습 실행 (클래스 속성을 사용하여 진행 상황 업데이트)
        class ProgressCallback:
            def __init__(self):
                pass
            
            @staticmethod
            def on_train_epoch_end(trainer):
                nonlocal completed_epochs
                completed_epochs = trainer.epoch + 1  # 에폭은 0부터 시작하므로 +1
        
        # 콜백 객체 생성
        callbacks = [ProgressCallback()]

        
        # 학습 실행
        model.train(
            data=data_yaml_path,
            epochs=epochs,
            batch=batch_size,
            imgsz=640,
            device=device,
            project=os.path.join(base_dir, "runs"),
            name="detect/train",  # 하위 폴더 구조 지정
            exist_ok=True  # 기존 폴더가 있으면 덮어쓰기
        )
    
        # 진행 스레드 종료 신호
        completed_epochs = total_epochs
        
        # 스레드가 종료될 때까지 잠시 대기
        if progress_thread and progress_thread.is_alive():
            progress_thread.join(timeout=1)
        
        train_elapsed = time.time() - train_start_time
        min, sec = divmod(train_elapsed, 60)
        show_progress(f"모델 학습 완료! (소요 시간: {int(min)}분 {int(sec)}초)", total_start_time, 100)
    except Exception as e:
        show_progress(f"학습 중 오류 발생: {e}", total_start_time, 70)
        
        # 메모리 부족 오류 처리
        if "CUDA out of memory" in str(e):
            show_progress("GPU 메모리 부족. 배치 크기를 줄여서 다시 시도합니다.", total_start_time, 75)
            try:
                # 배치 크기 절반으로 줄임
                reduced_batch = max(1, batch_size // 2)
                retry_start = time.time()
                show_progress(f"줄어든 배치 크기로 재시도 중 (배치 크기: {reduced_batch})...", total_start_time, 80)
                
                # 진행 상황을 초기화하고 다시 시작
                completed_epochs = 0
                last_progress_update = time.time()
                
                # 재시도 진행률 업데이트를 위한 새 스레드
                retry_thread = threading.Thread(target=update_training_progress)
                retry_thread.daemon = True
                retry_thread.start()
                
                # 줄어든 배치 크기로 학습 재시도
                model.train(
                    data=data_yaml_path,
                    epochs=epochs,
                    batch=reduced_batch,
                    imgsz=640,
                    device=device,
                    project=os.path.join(base_dir, "runs"),
                    name="detect/train",  # 하위 폴더 구조 지정
                    exist_ok=True  # 기존 폴더가 있으면 덮어쓰기
                )
                
                # 진행 스레드 종료 신호
                completed_epochs = total_epochs
                
                # 스레드가 종료될 때까지 잠시 대기
                if retry_thread and retry_thread.is_alive():
                    retry_thread.join(timeout=1)
                
                retry_elapsed = time.time() - retry_start
                min, sec = divmod(retry_elapsed, 60)
                show_progress(f"배치 크기 {reduced_batch}로 학습 완료! (소요 시간: {int(min)}분 {int(sec)}초)", total_start_time, 100)
            except Exception as e2:
                show_progress(f"재시도도 실패: {e2}", total_start_time, 85)
                # CPU로 전환
                show_progress("CPU 모드로 전환합니다...", total_start_time, 90)
                cpu_start = time.time()
                show_progress("CPU로 학습 중 (이 작업은 1-3시간 소요될 수 있습니다)...", total_start_time, 93)
                
                # CPU로 전환하고 에폭 수 줄임
                completed_epochs = 0
                total_epochs = 50  # CPU에서는 에폭 수 줄임
                last_progress_update = time.time()
                
                # CPU 학습 진행률 업데이트를 위한 새 스레드
                cpu_thread = threading.Thread(target=update_training_progress)
                cpu_thread.daemon = True
                cpu_thread.start()
                
                model.train(
                    data=data_yaml_path,
                    epochs=total_epochs,
                    batch=4,
                    imgsz=640,
                    device="cpu",
                    project=os.path.join(base_dir, "runs"),
                    name="detect/train",  # 하위 폴더 구조 지정
                    exist_ok=True  # 기존 폴더가 있으면 덮어쓰기
                )
                
                # 진행 스레드 종료 신호
                completed_epochs = total_epochs
                
                # 스레드가 종료될 때까지 잠시 대기
                if cpu_thread and cpu_thread.is_alive():
                    cpu_thread.join(timeout=1)
                
                cpu_elapsed = time.time() - cpu_start
                hrs, remainder = divmod(cpu_elapsed, 3600)
                mins, secs = divmod(remainder, 60)
                show_progress(f"CPU로 학습 완료! (소요 시간: {int(hrs)}시간 {int(mins)}분 {int(secs)}초)", total_start_time, 100)
                
    # 8. 학습 결과 처리 중
    show_progress("학습 결과 처리 중...", total_start_time, 100)
    
   # 결과 저장 경로를 Python 폴더 내로 설정
    results_dir = os.path.join(base_dir, "runs", "detect", "train")
    
    # 9. 학습 결과 그래프 시각화
    results_image_path = os.path.join(results_dir, "results.png")
    visualize_result = visualize_training_results(results_image_path, total_start_time)

    # 10. 테스트 이미지로 추론 실행
    inference_result = None
    model_path = os.path.join(results_dir, "weights", "best.pt")
    
    # 테스트 이미지 경로 설정 (로컬 경로)
    # 데이터셋 폴더에서 test/images 폴더 내의 첫 번째 이미지 사용
    test_image_path = None
    # dataset_dir = os.path.join(base_dir, "dataset")
    # tutorial_dataset_dir = os.path.join(dataset_dir, "tutorial_dataset")
    
    # # 테스트 이미지 폴더 경로들 (여러 가능한 위치 검색)
    # possible_test_folders = [
    #     os.path.join(tutorial_dataset_dir, "dataset", "test", "images"),
    #     os.path.join(tutorial_dataset_dir, "test", "images"),
    #     os.path.join(tutorial_dataset_dir, "dataset", "valid", "images"),  # 검증 이미지도 시도
    #     os.path.join(tutorial_dataset_dir, "valid", "images"),
    #     os.path.join(tutorial_dataset_dir, "dataset", "train", "images"),  # 학습 이미지도 시도
    #     os.path.join(tutorial_dataset_dir, "train", "images")
    # ]
    
    # # 사용 가능한 테스트 이미지 찾기
    # for folder in possible_test_folders:
    #     if os.path.exists(folder):
    #         image_files = [f for f in os.listdir(folder) if f.lower().endswith(('.jpg', '.jpeg', '.png', '.bmp', '.webp'))]
    #         if image_files:
    #             test_image_path = os.path.join(folder, image_files[0])
    #             show_progress(f"테스트 이미지 발견: {test_image_path}", total_start_time, 100)
    #             break
    
    # 테스트 이미지로 추론 실행
    if test_image_path and os.path.exists(test_image_path):
        inference_start_time = time.time()
        show_progress(f"테스트 이미지 추론 중... ({test_image_path})", total_start_time, 100)
        inference_result = run_inference(model_path, test_image_path, inference_start_time)
    else:
        show_progress("테스트 이미지를 찾을 수 없어 추론을 건너뜁니다.", total_start_time, 100)
    
    # 11. 학습 완료 알림
    total_elapsed = time.time() - total_start_time
    hrs, remainder = divmod(total_elapsed, 3600)
    mins, secs = divmod(remainder, 60)
    
    show_progress(f"튜토리얼 모드 실행 완료! (총 소요 시간: {int(hrs)}시간 {int(mins)}분 {int(secs)}초)", total_start_time, 100)
    show_progress(f"학습된 모델 경로: {model_path}", total_start_time, 100)

    # 최신 결과 디렉토리에서 모델 경로 찾기
    results_dir = find_latest_results_dir()
    model_path = os.path.join(results_dir, "weights", "best.pt")
    
    show_progress(f"튜토리얼 모드 실행 완료! (총 소요 시간: {int(hrs)}시간 {int(mins)}분 {int(secs)}초)", total_start_time, 100)
    show_progress(f"학습된 모델 경로: {model_path}", total_start_time, 100)
    
    # 결과 정보
    result = {
        "success": True,
        "model_path": model_path,
        "results_path": results_image_path if visualize_result else None,
        "device_used": device,
        "gpu_info": gpu_info,
        "total_time_seconds": total_elapsed,
        "timestamp": datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    }
    
    # 추론 결과가 있으면 추가
    if inference_result:
        result["inference"] = {
            "image_path": inference_result["image_path"],
            "result_image": inference_result["result_image"],
            "detections_count": len(inference_result["detections"]),
            "detections": inference_result["detections"]
        }
    
    # JSON으로 결과 출력 (C# 프로그램에서 파싱)
    print(f"RESULT_JSON:{json.dumps(result)}")
    return result

# 추론 전용 함수
def infer_image(model_path, image_path, show=False):
    """모델을 사용해 개별 이미지 추론 (외부에서 호출용)"""
    start_time = time.time()
    show_progress(f"이미지 추론 요청: {image_path}", start_time, 0)
    
    # 추론 실행
    result = run_inference(model_path, image_path, start_time, show=show)
    
    if result:
        print(f"INFERENCE_RESULT:{json.dumps(result)}")
        return result
    else:
        error_result = {
            "success": False,
            "error": "추론 실패",
            "image_path": image_path,
            "timestamp": datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        }
        print(f"INFERENCE_RESULT:{json.dumps(error_result)}")
        return error_result

if __name__ == "__main__":
    # 명령행 인수 확인
    if len(sys.argv) > 2 and sys.argv[1] == "infer":
        # 추론 모드: python script.py infer <모델_경로> <이미지_경로>
        try:
            model_path = sys.argv[2]
            image_path = sys.argv[3]
            infer_image(model_path, image_path, show=True)
        except Exception as e:
            error_result = {
                "success": False,
                "error": str(e),
                "timestamp": datetime.now().strftime("%Y-%m-%d %H:%M:%S")
            }
            print(f"INFERENCE_RESULT:{json.dumps(error_result)}")
    else:
        # 일반 모드: 전체 학습 파이프라인 실행
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