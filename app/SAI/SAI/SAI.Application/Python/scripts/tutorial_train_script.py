#!/usr/bin/env python
# -*- coding: utf-8 -*-

"""
tutorial_train_script.py - AI 블록 코딩 튜토리얼 모드 구현

이 스크립트는 AI 블록 코딩 튜토리얼 모드를 위한 기능을 구현합니다.
install_packages.py의 유틸리티 함수를 활용하여 패키지 설치, GPU 확인 등을 수행합니다.
"""

import os
import sys
import logging
import json
import time
import threading
import zipfile
import glob
import io
import re
from datetime import datetime
# 로깅 레벨 설정
logging.getLogger().setLevel(logging.INFO)

print("[DEBUG] tutorial_train_script.py 시작", flush=True)

try:
    # 기본 디렉토리 설정
    base_dir = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
    print(f"[DEBUG] base_dir 설정됨: {base_dir}", flush=True)

    # 현재 파일 경로 기준으로 scripts 디렉토리 얻기
    current_dir = os.path.dirname(os.path.abspath(__file__))
    print(f"[DEBUG] current_dir: {current_dir}", flush=True)

    # scripts 디렉토리를 sys.path에 추가
    if current_dir not in sys.path:
        sys.path.insert(0, current_dir)
        print(f"[DEBUG] sys.path에 추가된 경로: {current_dir}", flush=True)

    # install_packages 가져오기
    print("[DEBUG] install_packages 모듈 가져오기 시도", flush=True)
    try:
        import install_packages
        print("[DEBUG] install_packages 모듈 가져오기 성공", flush=True)
    except Exception as e:
        print(f"[ERROR] install_packages 모듈 가져오기 실패: {str(e)}", flush=True)
        raise

    # python-dotenv 가져오기
    print("[DEBUG] python-dotenv 가져오기 시도", flush=True)
    try:
        from dotenv import load_dotenv
        print("[DEBUG] python-dotenv 가져오기 성공", flush=True)
    except Exception as e:
        print(f"[ERROR] python-dotenv 가져오기 실패: {str(e)}", flush=True)
        raise
    
    # .env 파일 로드
    env_path = os.path.join(current_dir, '.env')
    print(f"[DEBUG] .env 파일 경로: {env_path}", flush=True)
    
    if os.path.exists(env_path):
        print("[DEBUG] .env 파일 존재함", flush=True)
        try:
            load_dotenv(env_path)
            print("[DEBUG] .env 파일 로드 완료", flush=True)
            api_url = os.getenv('API_SERVER_URL')
            print(f"[DEBUG] API_SERVER_URL: {api_url}", flush=True)
        except Exception as e:
            print(f"[ERROR] .env 파일 로드 실패: {str(e)}", flush=True)
            raise
    else:
        print("[ERROR] .env 파일이 존재하지 않음", flush=True)
        raise FileNotFoundError(".env 파일을 찾을 수 없습니다.")

    # 표준 출력 스트림 설정
    print("[DEBUG] 표준 출력 스트림 설정 시도", flush=True)
    print("[DEBUG] 표준 출력 스트림 설정 생략", flush=True)

    # 로깅 설정
    print("[DEBUG] 로깅 설정 시도", flush=True)
    try:
        logging.basicConfig(
            level=logging.DEBUG,
            format='%(asctime)s - %(levelname)s - %(message)s',
            handlers=[
                logging.StreamHandler(sys.stdout)
            ]
        )
        print("[DEBUG] 로깅 설정 완료", flush=True)
    except Exception as e:
        print(f"[ERROR] 로깅 설정 실패: {str(e)}", flush=True)
        raise

    # install_packages의 진행 상황 표시 함수 사용
    print("[DEBUG] show_progress 함수 가져오기 시도", flush=True)
    show_progress = install_packages.show_progress
    print("[DEBUG] show_progress 함수 가져오기 성공", flush=True)

    # 튜토리얼 상태 관리용 전역 변수
    print("[DEBUG] tutorial_state 초기화 시도", flush=True)
    tutorial_state = {
        "model": None,
        "model_path": None,
        "dataset_path": None,
        "data_yaml_path": None,
        "image_path": None,
        "result_image_path": None,
        "training_completed": False
    }
    print("[DEBUG] tutorial_state 초기화 완료", flush=True)

    print("[DEBUG] tutorial_train_script.py 초기화 완료", flush=True)

except Exception as e:
    logger.error(f"tutorial_train_script.py 초기화 중 오류 발생: {str(e)}", exc_info=True)
    print(f"PROGRESS::초기화 오류 발생: {str(e)}", flush=True)
    raise

# 1. 패키지 설치 블록 함수
def install_packages_block():
    """패키지 설치 블록 실행 함수"""
    print("[DEBUG] install_packages_block 함수 진입", flush=True)

    start_time = time.time()
    show_progress("필수 패키지 설치 시작... (1/8)", start_time, 0)
    
    # 패키지 설치 순서 변경 및 버전 명시
    packages = [
        "numpy==1.24.3",
        "ultralytics==8.0.196",
        "opencv-python==4.8.0.76"
    ]
    
    try:
        result = install_packages.install_packages_with_progress(packages, start_time)
        print("[DEBUG] install_packages_with_progress 결과:", result, flush=True)
        
        pkg_elapsed = time.time() - start_time
        show_progress(f"패키지 설치 완료 (소요 시간: {int(pkg_elapsed)}초)", start_time, 100)
        
        return {
            "success": result.get("success", False),
            "installed_packages": result.get("installed_packages", []),
            "failed_packages": result.get("failed_packages", []),
            "elapsed_time": pkg_elapsed
        }
    except Exception as e:
        print(f"[DEBUG] 패키지 설치 중 오류 발생: {e}", flush=True)
        return {
            "success": False,
            "error": str(e),
            "elapsed_time": time.time() - start_time
        }

# 2. GPU 확인 및 모델 로드 블록 함수
def check_gpu_yolo_load_block():
    """GPU 상태 확인 및 모델 로드 블록 실행 함수"""
    start_time = time.time()
    show_progress("GPU 정보 확인 중... (2/8)", start_time, 0)
    
    # install_packages 모듈의 GPU 확인 함수 사용
    gpu_info = install_packages.check_gpu(start_time)
    
    # PyTorch CUDA 설치 확인
    if not gpu_info.get("available", False):
        show_progress("GPU를 감지할 수 없습니다. PyTorch CUDA 설치를 시도합니다...", start_time, 30)
        cuda_success, device = install_packages.install_torch_cuda(start_time)
        if cuda_success:
            show_progress("PyTorch CUDA 설치 성공", start_time, 40)
            # GPU 정보 다시 확인
            gpu_info = install_packages.check_gpu(start_time)
        else:
            show_progress("PyTorch CUDA 설치 실패, CPU 모드로 계속합니다", start_time, 40)
    
    # YOLO 모델 로드
    show_progress("YOLOv8 모델 로드 중...", start_time, 50)
    try:
        from ultralytics import YOLO
        model_path = os.path.join(base_dir, "yolov8n.pt")
        model = YOLO(model_path)
        
        # 전역 상태 업데이트
        tutorial_state["model"] = model
        tutorial_state["model_path"] = model_path
        
        model_elapsed = time.time() - start_time
        show_progress(f"YOLOv8 모델 로드 완료! (소요 시간: {int(model_elapsed)}초)", start_time, 100)
    except Exception as e:
        show_progress(f"모델 로드 오류: {e}", start_time, 100)
        return {
            "success": False,
            "error": str(e),
            "gpu_info": gpu_info
        }
    
    return {
        "success": True,
        "gpu_info": gpu_info,
        "model_path": model_path,
        "elapsed_time": time.time() - start_time
    }

# 3. 데이터셋 다운로드 블록 함수
def download_dataset_block():
    """데이터셋 다운로드 블록 실행 함수"""
    start_time = time.time()
    show_progress("서버에서 데이터셋 다운로드 중... (3/8)", start_time, 0)
    
    # API 서버에서 데이터셋 다운로드
    try:
        import requests
        from tqdm import tqdm
    except ImportError:
        show_progress("필요한 패키지 설치 중...", start_time, 5)
        install_packages.install_packages_with_progress(["requests", "tqdm"], start_time)
        import requests
        from tqdm import tqdm
    
    # 데이터셋 저장 경로 설정
    dataset_dir = os.path.join(base_dir, "dataset")
    os.makedirs(dataset_dir, exist_ok=True)
    show_progress(f"데이터셋 기본 경로: {dataset_dir}", start_time, 10)
    
    # 환경 변수에서 서버 주소 가져오기
    server_url = os.environ.get("API_SERVER_URL")
    if not server_url:
        show_progress("API_SERVER_URL 환경 변수가 설정되지 않았습니다.", start_time, 15)
        
        # 테스트용 더미 데이터 생성
        tutorial_state["dataset_path"] = dataset_dir
        return {
            "success": True,
            "message": "테스트용 더미 데이터 사용",
            "location": dataset_dir
        }
    
    # 슬래시로 끝나지 않는지 확인
    if server_url.endswith('/'):
        server_url = server_url[:-1]
    
    # API 엔드포인트 URL 구성
    api_url = f"{server_url}/api/download/tutorial"
    show_progress("API에서 다운로드 URL 요청 중...", start_time, 20)
    
    zip_path = os.path.join(dataset_dir, "tutorial_dataset.zip")
    
    # API 호출하여 presigned URL 받기
    try:
        response = requests.get(api_url)
        if response.status_code == 200:
            data = response.json()
            download_url = data['result']
            show_progress("다운로드 URL 획득 성공", start_time, 30)
        else:
            show_progress(f"API 호출 실패: 상태 코드 {response.status_code}", start_time, 30)
            tutorial_state["dataset_path"] = dataset_dir
            return {
                "success": False,
                "error": f"API 응답 오류: {response.text}",
                "location": dataset_dir
            }
    except Exception as e:
        show_progress(f"API 호출 중 오류 발생: {e}", start_time, 30)
        tutorial_state["dataset_path"] = dataset_dir
        return {
            "success": False,
            "error": str(e),
            "location": dataset_dir
        }
    
    # 파일 다운로드 (진행률 표시)
    show_progress("데이터셋 다운로드 시작...", start_time, 40)
    try:
        response = requests.get(download_url, stream=True)
        total_size = int(response.headers.get('content-length', 0))
        
        # 다운로드 진행률 표시 및 파일 저장
        with open(zip_path, 'wb') as f:
            downloaded = 0
            for chunk in response.iter_content(chunk_size=1024*1024):  # 1MB 단위로 청크 다운로드
                if chunk:
                    f.write(chunk)
                    downloaded += len(chunk)
                    progress = min(40 + (downloaded / total_size * 30), 70)  # 40% ~ 70% 범위
                    show_progress(f"다운로드 중: {downloaded//(1024*1024)}MB/{total_size//(1024*1024)}MB", start_time, progress)
        
        show_progress("데이터셋 다운로드 완료", start_time, 70)
    except Exception as e:
        show_progress(f"다운로드 중 오류 발생: {e}", start_time, 70)
        tutorial_state["dataset_path"] = dataset_dir
        return {
            "success": False,
            "error": str(e),
            "location": dataset_dir
        }
    
    # ZIP 파일 압축 해제
    extracted_dir = dataset_dir  # 기본값 설정
    
    if os.path.exists(zip_path):
        try:
            with zipfile.ZipFile(zip_path, 'r') as zip_ref:
                file_list = zip_ref.namelist()
                total_files = len(file_list)
                show_progress(f"압축 파일 내 {total_files}개 파일 발견", start_time, 75)
                
                # 첫 번째 파일의 경로에서 최상위 디렉토리 확인 (있는 경우)
                if file_list and '/' in file_list[0]:
                    top_dir = file_list[0].split('/')[0]
                    # 최상위 디렉토리가 있는 경우, 이를 추출 디렉토리로 설정
                    potential_extracted_dir = os.path.join(dataset_dir, top_dir)
                else:
                    potential_extracted_dir = dataset_dir
                
                # 압축 해제 진행률 표시
                for i, file in enumerate(file_list):
                    zip_ref.extract(file, dataset_dir)
                    if i % 50 == 0 or i == total_files - 1:  # 50개 파일마다 또는 마지막 파일에서 진행률 표시
                        extract_progress = 75 + (i / total_files) * 20  # 75% ~ 95% 범위
                        show_progress(f"압축 해제 중: {i+1}/{total_files} 파일", start_time, extract_progress)
                
                # 압축 해제 후 실제 추출 디렉토리 확인
                if os.path.exists(potential_extracted_dir) and os.path.isdir(potential_extracted_dir):
                    extracted_dir = potential_extracted_dir
                    show_progress(f"데이터셋이 하위 디렉토리에 압축 해제됨: {extracted_dir}", start_time, 95)
            
            show_progress("압축 해제 완료", start_time, 95)
            
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
    
    # 데이터셋 경로 저장
    tutorial_state["dataset_path"] = extracted_dir
    
    # data.yaml 파일 찾기
    data_yaml_path = find_yaml_file(dataset_dir, extracted_dir, start_time)
    tutorial_state["data_yaml_path"] = data_yaml_path
    
    show_progress("데이터셋 준비 완료", start_time, 100)
    return {
        "success": True,
        "location": extracted_dir,
        "extracted_dir": extracted_dir,
        "data_yaml_path": data_yaml_path,
        "elapsed_time": time.time() - start_time
    }

# data.yaml 파일 찾기 도우미 함수
def find_yaml_file(dataset_dir, extracted_dir, start_time):
    """데이터셋 디렉토리에서 data.yaml 파일 찾기"""
    show_progress(f"데이터 경로 확인: {extracted_dir}", start_time, 95)
    
    # 압축 해제된 디렉토리에서 data.yaml 찾기
    yaml_path = os.path.join(extracted_dir, "data.yaml")
    
    # data.yaml 파일이 있는지 확인
    if os.path.exists(yaml_path):
        show_progress(f"데이터 파일 확인됨: {yaml_path}", start_time, 98)
        return yaml_path
    
    # 압축 해제된 디렉토리의 하위 폴더들에서 data.yaml 찾기
    for root, dirs, files in os.walk(extracted_dir):
        for file in files:
            if file == "data.yaml":
                yaml_path = os.path.join(root, file)
                show_progress(f"데이터 파일 확인됨: {yaml_path}", start_time, 98)
                return yaml_path
    
    # 기본 dataset 디렉토리에서 데이터 찾기 (압축 해제 경로에서 찾지 못했을 경우)
    if dataset_dir != extracted_dir:
        yaml_path = os.path.join(dataset_dir, "data.yaml")
        if os.path.exists(yaml_path):
            show_progress(f"데이터 파일 확인됨: {yaml_path}", start_time, 98)
            return yaml_path
    
    # 파일을 찾지 못했을 경우
    show_progress(f"data.yaml 파일을 찾을 수 없습니다: {yaml_path}", start_time, 98)
    return None

# 4. 모델 학습 블럭
def train_model_block(epochs=None, imgsz=None):
    """
    모델 학습 블록 실행 함수
    
    Args:
        epochs (int, optional): 사용자가 지정한 에폭 수. None이면 기본값 사용
        imgsz (int, optional): 사용자가 지정한 이미지 크기. None이면 기본값 사용
    """
    start_time = time.time()
    show_progress("모델 학습 준비 중... (4/8)", start_time, 0)

    # 기존 results.csv 삭제
    results_csv = os.path.join(base_dir, "runs", "detect", "train", "results.csv")
    if os.path.exists(results_csv):
        os.remove(results_csv)
        show_progress("기존 results.csv 파일 삭제 완료", start_time, 18)

    
    # 필요한 데이터가 있는지 확인
    if not tutorial_state.get("model"):
        show_progress("모델이 로드되지 않았습니다. 모델 로드 단계를 먼저 실행하세요.", start_time, 10)
        return {
            "success": False,
            "error": "모델이 로드되지 않음"
        }
    
    if not tutorial_state.get("data_yaml_path"):
        show_progress("데이터셋 YAML 파일이 설정되지 않았습니다. 데이터셋 준비 단계를 먼저 실행하세요.", start_time, 10)
        return {
            "success": False,
            "error": "데이터셋 YAML 파일 없음"
        }
    
    # GPU 정보 확인 (이미 check_gpu 함수에서 확인됨)
    gpu_info = install_packages.check_gpu(start_time)
    device = "cuda" if gpu_info.get("available", False) else "cpu"
    
    # 학습 파라미터 설정
    batch_size = 16
    if device == "cuda" and gpu_info.get("available", False):
        # GPU 메모리에 따른 배치 크기 조정
        memory = gpu_info.get("memory_gb", [0])[0]
        if memory and memory < 6:
            batch_size = 8
            show_progress(f"GPU 메모리 제한으로 배치 크기 {batch_size}로 조정", start_time, 10)
    
    # 에폭 수 설정 - 사용자 지정 값 또는 기본값
    if epochs is None:
        # 기본 에폭 수 설정
        epochs = 5 if device == "cuda" else 2
    else:
        # 사용자 지정 에폭 수를 정수로 변환
        try:
            epochs = int(epochs)
            if epochs <= 0:
                show_progress(f"에폭 수는 양수여야 합니다. 기본값을 사용합니다.", start_time, 15)
                epochs = 5 if device == "cuda" else 2
        except ValueError:
            show_progress(f"유효하지 않은 에폭 수입니다. 기본값을 사용합니다.", start_time, 15)
            epochs = 5 if device == "cuda" else 2
    
    # 이미지 크기 설정 - 사용자 지정 값 또는 기본값
    if imgsz is None:
        # 기본 이미지 크기 설정
        imgsz = 640
    else:
        # 사용자 지정 이미지 크기를 정수로 변환
        try:
            imgsz = int(imgsz)
            # 유효한 이미지 크기 범위 확인 (YOLO 권장 크기)
            valid_sizes = [512, 640, 960, 1024, 1280]
            if imgsz not in valid_sizes:
                # 가장 가까운 유효 크기 찾기
                closest_size = min(valid_sizes, key=lambda x: abs(x - imgsz))
                show_progress(f"이미지 크기 {imgsz}는 권장되지 않습니다. 가장 가까운 권장 크기 {closest_size}를 사용합니다.", start_time, 15)
                imgsz = closest_size
        except ValueError:
            show_progress(f"유효하지 않은 이미지 크기입니다. 기본값 640을 사용합니다.", start_time, 15)
            imgsz = 640
    
    show_progress(f"모델 학습 시작 (디바이스: {device}, 배치 크기: {batch_size}, 에폭: {epochs}, 이미지 크기: {imgsz})", start_time, 20)
    show_progress("학습 중... (YOLOv8 진행 상황이 표시됩니다)", start_time, 30)
    show_progress("이 작업은 GPU 사용 시 약 5-15분, CPU 사용 시 20-60분 소요될 수 있습니다", start_time, 40)
    
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
                            start_time, 
                            40 + (progress * 0.5)  # 40% ~ 90% 범위
                        )
                    else:
                        show_progress(
                            f"학습 중: {completed_epochs}/{total_epochs} 에폭 완료 "
                            f"({int(minutes)}분 {int(seconds)}초 경과)", 
                            start_time, 
                            40 + (progress * 0.5)
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
        model = tutorial_state["model"]
        data_yaml_path = tutorial_state["data_yaml_path"]
        
        # YOLOv8 학습 실행
        results = model.train(
            data=data_yaml_path,
            epochs=epochs,
            batch=batch_size,
            imgsz=imgsz,
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
        
        # 결과 경로 설정
        results_dir = find_latest_results_dir()
        model_path = os.path.join(results_dir, "weights", "best.pt")
        
        # 전역 상태 업데이트
        tutorial_state["model_path"] = model_path
        tutorial_state["results_dir"] = results_dir
        tutorial_state["training_completed"] = True
        
        train_elapsed = time.time() - start_time
        min, sec = divmod(train_elapsed, 60)
        show_progress(f"모델 학습 완료! (소요 시간: {int(min)}분 {int(sec)}초)", start_time, 100)
        
        return {
            "success": True,
            "model_path": model_path,
            "results_dir": results_dir,
            "epochs": epochs,
            "imgsz": imgsz,
            "device": device,
            "elapsed_time": train_elapsed
        }
    except Exception as e:
        show_progress(f"학습 중 오류 발생: {e}", start_time, 70)
        
        # 메모리 부족 오류 처리
        if "CUDA out of memory" in str(e):
            show_progress("GPU 메모리 부족. 배치 크기를 줄여서 다시 시도합니다.", start_time, 75)
            try:
                # 배치 크기 절반으로 줄임
                reduced_batch = max(1, batch_size // 2)
                retry_start = time.time()
                show_progress(f"줄어든 배치 크기로 재시도 중 (배치 크기: {reduced_batch})...", start_time, 80)
                
                # 재시도
                model = tutorial_state["model"]
                results = model.train(
                    data=tutorial_state["data_yaml_path"],
                    epochs=epochs,
                    batch=reduced_batch,
                    imgsz=imgsz,  # 사용자 지정 이미지 크기 유지
                    device=device,
                    project=os.path.join(base_dir, "runs"),
                    name="detect/train",
                    exist_ok=True
                )
                
                # 결과 경로 설정
                results_dir = find_latest_results_dir()
                model_path = os.path.join(results_dir, "weights", "best.pt")
                
                # 전역 상태 업데이트
                tutorial_state["model_path"] = model_path
                tutorial_state["results_dir"] = results_dir
                tutorial_state["training_completed"] = True
                
                retry_elapsed = time.time() - retry_start
                min, sec = divmod(retry_elapsed, 60)
                show_progress(f"배치 크기 {reduced_batch}로 학습 완료! (소요 시간: {int(min)}분 {int(sec)}초)", start_time, 100)
                
                return {
                    "success": True,
                    "model_path": model_path,
                    "results_dir": results_dir,
                    "epochs": epochs,
                    "imgsz": imgsz,
                    "device": device,
                    "elapsed_time": time.time() - start_time,
                    "note": "배치 크기 감소로 재시도 성공"
                }
            except Exception as e2:
                show_progress(f"재시도도 실패: {e2}", start_time, 85)
                # CPU로 전환
                show_progress("CPU 모드로 전환합니다...", start_time, 90)
                
                try:
                    # CPU로 전환하고 에폭 수 줄임
                    cpu_epochs = min(2, epochs)  # 원래 에폭보다 크지 않게
                    model = tutorial_state["model"]
                    
                    results = model.train(
                        data=tutorial_state["data_yaml_path"],
                        epochs=cpu_epochs,
                        batch=4,
                        imgsz=imgsz,  # 사용자 지정 이미지 크기 유지
                        device="cpu",
                        project=os.path.join(base_dir, "runs"),
                        name="detect/train",
                        exist_ok=True
                    )
                    
                    # 결과 경로 설정
                    results_dir = find_latest_results_dir()
                    model_path = os.path.join(results_dir, "weights", "best.pt")
                    
                    # 전역 상태 업데이트
                    tutorial_state["model_path"] = model_path
                    tutorial_state["results_dir"] = results_dir
                    tutorial_state["training_completed"] = True
                    
                    cpu_elapsed = time.time() - start_time
                    hrs, mins = divmod(cpu_elapsed, 3600)
                    mins, secs = divmod(mins, 60)
                    show_progress(f"CPU로 학습 완료! (소요 시간: {int(hrs)}시간 {int(mins)}분 {int(secs)}초)", start_time, 100)
                    
                    return {
                        "success": True,
                        "model_path": model_path,
                        "results_dir": results_dir,
                        "epochs": cpu_epochs,
                        "imgsz": imgsz,
                        "device": "cpu",
                        "elapsed_time": cpu_elapsed,
                        "note": "CPU 모드로 전환하여 완료"
                    }
                except Exception as e3:
                    show_progress(f"CPU 모드도 실패: {e3}", start_time, 95)
                    return {
                        "success": False,
                        "error": str(e3),
                        "original_error": str(e)
                    }
        
        return {
            "success": False,
            "error": str(e)
        }

# 최신 결과 디렉토리 찾기 도우미 함수
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

# 5. 결과 그래프 시각화 블록 함수
def visualize_training_results_block():
    """학습 결과 그래프 시각화 블록 실행 함수"""
    start_time = time.time()
    show_progress("학습 결과 시각화 중... (5/8)", start_time, 0)
    
    # 학습이 완료되었는지 확인
    if not tutorial_state.get("training_completed"):
        show_progress("학습이 완료되지 않았습니다. 모델 학습 단계를 먼저 실행하세요.", start_time, 10)
        return {
            "success": False,
            "error": "학습이 완료되지 않음"
        }
    
    # 결과 디렉토리 확인
    results_dir = tutorial_state.get("results_dir")
    if not results_dir or not os.path.exists(results_dir):
        results_dir = find_latest_results_dir()
        tutorial_state["results_dir"] = results_dir
    
    # 결과 이미지 경로 확인
    results_path = os.path.join(results_dir, "results.png")
    
    try:
        # 결과 이미지가 존재하는지 확인
        if not os.path.exists(results_path):
            show_progress(f"결과 그래프 파일을 찾을 수 없습니다: {results_path}", start_time, 50)
            
            # 다른 가능한 경로 확인
            alternative_paths = [
                os.path.join(results_dir, "results.png"),
                os.path.join(results_dir, "confusion_matrix.png"),
                os.path.join(results_dir, "val_batch0_pred.jpg")
            ]
            
            for alt_path in alternative_paths:
                if os.path.exists(alt_path):
                    results_path = alt_path
                    show_progress(f"대체 결과 파일 발견: {results_path}", start_time, 60)
                    break
        
        # 결과 이미지 표시
        if os.path.exists(results_path):
            show_progress(f"학습 결과 그래프 확인: {results_path}", start_time, 80)
            try:
                # 이미지 표시 (IPython 환경에서만 작동)
                from IPython.display import Image, display
                display(Image(filename=results_path))
                show_progress("결과 그래프 표시 완료", start_time, 100)
            except ImportError:
                # 일반 환경에서는 파일 경로만 반환
                show_progress("IPython 환경이 아니므로 결과 파일 경로만 반환합니다.", start_time, 90)
            
            # 결과 경로 저장
            tutorial_state["results_image_path"] = results_path
            
            return {
                "success": True,
                "results_path": results_path,
                "elapsed_time": time.time() - start_time
            }
        else:
            show_progress("결과 그래프 파일을 찾을 수 없습니다.", start_time, 100)
            return {
                "success": False,
                "error": "결과 그래프 파일 없음",
                "elapsed_time": time.time() - start_time
            }
    except Exception as e:
        show_progress(f"결과 시각화 오류: {e}", start_time, 100)
        return {
            "success": False,
            "error": str(e),
            "elapsed_time": time.time() - start_time
        }

# 6. 사용자 이미지 경로 받는 블럭
# 이미지 경로를 inference.py 파일로 던져준다
def set_image_path_block(image_path=None):
    """
    추론용 이미지 경로 설정 블록 실행 함수
    
    Args:
        image_path (str, optional): 사용자가 지정한 이미지 경로. 
                                   None이면 기본 테스트 이미지 찾기 시도
    """
    start_time = time.time()
    show_progress("추론용 이미지 경로 설정 중... (6/8)", start_time, 0)
    
    # 사용자가 지정한 이미지 경로가 있는지 확인
    if image_path:
        # 이미지 파일이 실제로 존재하는지 확인
        if os.path.exists(image_path):
            # 이미지 파일 확장자 확인
            if image_path.lower().endswith(('.jpg', '.jpeg', '.png', '.bmp')):
                # 경로 저장
                tutorial_state["image_path"] = image_path
                show_progress(f"사용자 지정 이미지 경로 설정 완료: {image_path}", start_time, 100)
                return {
                    "success": True,
                    "image_path": image_path,
                    "source_type": "user_specified",
                    "elapsed_time": time.time() - start_time
                }
            else:
                show_progress(f"지원되지 않는 이미지 형식입니다: {image_path}", start_time, 50)
                return {
                    "success": False,
                    "error": "지원되지 않는 이미지 형식",
                    "elapsed_time": time.time() - start_time
                }
        else:
            show_progress(f"지정한 이미지 파일을 찾을 수 없습니다: {image_path}", start_time, 50)
            return {
                "success": False,
                "error": "이미지 파일 없음",
                "elapsed_time": time.time() - start_time
            }

# 7. 모델 추론 블록 함수
def run_inference_block():
    """모델 추론 실행 블록 함수 - inference.py 활용"""
    start_time = time.time()
    show_progress("모델 추론 실행 중... (7/8)", start_time, 0)
    
    # 필요한 정보가 있는지 확인
    model_path = tutorial_state.get("model_path")
    if not model_path:
        # 학습된 모델이 없다면 기본 모델 사용
        model_path = os.path.join(base_dir, "yolov8n.pt")
        show_progress(f"학습된 모델 경로가 설정되지 않았습니다. 기본 모델을 사용합니다: {model_path}", start_time, 10)
    
    image_path = tutorial_state.get("image_path")
    if not image_path:
        show_progress("테스트 이미지 경로가 설정되지 않았습니다. 이미지 경로 설정 단계를 먼저 실행하세요.", start_time, 10)
        return {
            "success": False,
            "error": "테스트 이미지 경로 없음"
        }
    
    # inference.py 파일 경로 확인
    inference_script_path = os.path.join(os.path.dirname(os.path.abspath(__file__)), "inference.py")
    if not os.path.exists(inference_script_path):
        show_progress(f"inference.py 파일을 찾을 수 없습니다: {inference_script_path}", start_time, 20)
        return {
            "success": False,
            "error": "inference.py 파일 없음"
        }
    
    # 추론 실행 (inference.py 호출)
    try:
        show_progress(f"inference.py를 사용하여 추론 실행 중...", start_time, 30)
        
        # subprocess를 사용하여 inference.py 실행
        import subprocess
        
        # 명령 구성 - inference.py의 명령행 인자 형식에 맞춤
        cmd = [
            sys.executable,
            inference_script_path,
            "--model", model_path,
            "--image", image_path,
            "--conf", "0.25"
        ]
        
        show_progress(f"실행 명령: {' '.join(cmd)}", start_time, 40)
        
        # 프로세스 실행
        process = subprocess.Popen(
            cmd,
            stdout=subprocess.PIPE,
            stderr=subprocess.PIPE,
            universal_newlines=True,
            bufsize=1
        )
        
        # 출력 처리
        inference_result = None
        for line in iter(process.stdout.readline, ''):
            line = line.strip()
            print(line)  # 로그 확인용
            
            # 결과 JSON 찾기
            if line.startswith("INFERENCE_RESULT:"):
                result_json = line[len("INFERENCE_RESULT:"):]
                try:
                    inference_result = json.loads(result_json)
                    show_progress("추론 결과 JSON 파싱 성공", start_time, 70)
                except json.JSONDecodeError:
                    show_progress(f"추론 결과 JSON 파싱 실패: {result_json}", start_time, 70)
            
            # 진행 상황 메시지 확인
            elif line.startswith("[INFERENCE]"):
                progress_msg = line[len("[INFERENCE] "):]
                show_progress(f"추론 진행 중: {progress_msg}", start_time, 60)
        
        # 프로세스 완료 대기
        process.wait()
        
        # 결과 확인
        if process.returncode != 0:
            stderr = process.stderr.read()
            show_progress(f"inference.py 실행 오류 (반환 코드: {process.returncode}): {stderr}", start_time, 80)
            return {
                "success": False,
                "error": f"inference.py 실행 오류 (반환 코드: {process.returncode}): {stderr}",
                "elapsed_time": time.time() - start_time
            }
        
        # inference.py가 반환한 결과 확인
        if inference_result:
            # 결과 이미지 경로 저장
            if "result_image" in inference_result:
                tutorial_state["result_image_path"] = inference_result["result_image"]
            
            show_progress(f"추론 완료: {inference_result.get('success', False)}", start_time, 100)
            return {
                "success": inference_result.get("success", False),
                "result": inference_result,
                "elapsed_time": time.time() - start_time
            }
        else:
            show_progress("inference.py에서 결과를 반환하지 않았습니다.", start_time, 100)
            return {
                "success": False,
                "error": "inference.py에서 결과가 없음",
                "elapsed_time": time.time() - start_time
            }
    except Exception as e:
        show_progress(f"추론 실행 중 오류 발생: {e}", start_time, 100)
        return {
            "success": False,
            "error": str(e),
            "elapsed_time": time.time() - start_time
        }

# 8. 결과 시각화 블록 함수 - inference.py 결과 활용
def visualize_results_block():
    """추론 결과 시각화 블록 실행 함수"""
    start_time = time.time()
    show_progress("추론 결과 시각화 중... (8/8)", start_time, 0)
    
    # 결과 이미지 경로 확인
    result_image_path = tutorial_state.get("result_image_path")
    if not result_image_path:
        show_progress("추론 결과 이미지 경로가 설정되지 않았습니다. 모델 추론 단계를 먼저 실행하세요.", start_time, 10)
        return {
            "success": False,
            "error": "추론 결과 이미지 경로 없음"
        }
    
    # 이미지 파일 존재 확인
    if not os.path.exists(result_image_path):
        show_progress(f"결과 이미지 파일을 찾을 수 없습니다: {result_image_path}", start_time, 20)
        return {
            "success": False,
            "error": "결과 이미지 파일 없음"
        }
    
    try:
        # 이미지 표시 (IPython 환경에서만 작동)
        try:
            import cv2
            import matplotlib.pyplot as plt
            
            # 이미지 읽기
            img = cv2.imread(result_image_path)
            img_rgb = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)
            
            # matplotlib으로 시각화
            plt.figure(figsize=(10, 8))
            plt.imshow(img_rgb)
            plt.axis('off')
            plt.title("YOLOv8 Prediction")
            
            # IPython 환경인지 확인
            try:
                from IPython import get_ipython
                if get_ipython() is not None:
                    plt.show()
                    show_progress("결과 이미지 표시 완료", start_time, 100)
                else:
                    # 일반 환경에서는 이미지 저장
                    output_path = os.path.join(base_dir, "visualization_result.png")
                    plt.savefig(output_path)
                    plt.close()
                    show_progress(f"결과 이미지 저장 완료: {output_path}", start_time, 100)
            except ImportError:
                # IPython이 없으면 이미지 저장
                output_path = os.path.join(base_dir, "visualization_result.png")
                plt.savefig(output_path)
                plt.close()
                show_progress(f"결과 이미지 저장 완료: {output_path}", start_time, 100)
            
        except ImportError:
            show_progress("matplotlib 또는 OpenCV를 가져올 수 없습니다. 이미지 경로만 반환합니다.", start_time, 70)
            
        return {
            "success": True,
            "result_image_path": result_image_path,
            "elapsed_time": time.time() - start_time
        }
    except Exception as e:
        show_progress(f"결과 시각화 오류: {e}", start_time, 100)
        return {
            "success": False,
            "error": str(e),
            "elapsed_time": time.time() - start_time
        }

# 메인 실행 함수
def main():
    """AI 블록 코딩 튜토리얼 모드 실행 메인 함수"""
    total_start_time = time.time()
    current_date = datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    show_progress(f"AI 블록 코딩 튜토리얼 모드 실행 시작 - {current_date}", total_start_time, 0)
    
    # 전체 블록 순차 실행
    blocks = [
        ("패키지 설치", install_packages_block),
        ("GPU 확인 및 모델 로드", check_gpu_yolo_load_block),
        ("데이터셋 다운로드", download_dataset_block),
        ("모델 학습", train_model_block),
        ("학습 결과 시각화", visualize_training_results_block),
        ("테스트 이미지 경로 설정", set_image_path_block),
        ("모델 추론", run_inference_block),
        ("추론 결과 시각화", visualize_results_block)
    ]
    
    results = {}
    success = True
    
    for i, (block_name, block_func) in enumerate(blocks):
        show_progress(f"블록 실행 중: {block_name} ({i+1}/{len(blocks)})", total_start_time, i * (100 / len(blocks)))
        try:
            result = block_func()
            results[block_name] = result
            
            if not result.get("success", False):
                success = False
                show_progress(f"블록 실행 실패: {block_name} - {result.get('error', '알 수 없는 오류')}", total_start_time, (i+1) * (100 / len(blocks)))
                break
            
            show_progress(f"블록 실행 완료: {block_name}", total_start_time, (i+1) * (100 / len(blocks)))
        except Exception as e:
            success = False
            results[block_name] = {"success": False, "error": str(e)}
            show_progress(f"블록 실행 중 오류 발생: {block_name} - {e}", total_start_time, (i+1) * (100 / len(blocks)))
            break
    
    # 튜토리얼 완료 보고
    total_elapsed = time.time() - total_start_time
    hrs, remainder = divmod(total_elapsed, 3600)
    mins, secs = divmod(remainder, 60)
    
    if success:
        show_progress(f"✅ 튜토리얼 모드 실행 완료! (총 소요 시간: {int(hrs)}시간 {int(mins)}분 {int(secs)}초)", total_start_time, 100)
    else:
        show_progress(f"❌ 튜토리얼 모드 실행 중단 (소요 시간: {int(hrs)}시간 {int(mins)}분 {int(secs)}초)", total_start_time, 100)
    
    # 결과 정보
    result = {
        "success": success,
        "blocks_results": results,
        "total_time_seconds": total_elapsed,
        "timestamp": datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    }
    
    # JSON으로 결과 출력 (C# 프로그램에서 파싱)
    print(f"RESULT_JSON:{json.dumps(result)}")
    return result

# 추론 전용 함수 (외부에서 호출용)
def infer_image(model_path, image_path, show=False):
    """모델을 사용해 개별 이미지 추론 (외부에서 호출용) - inference.py 활용"""
    start_time = time.time()
    show_progress(f"이미지 추론 요청: {image_path}", start_time, 0)
    
    # inference.py 파일 경로 확인
    inference_script_path = os.path.join(os.path.dirname(os.path.abspath(__file__)), "inference.py")
    if not os.path.exists(inference_script_path):
        error_result = {
            "success": False,
            "error": f"inference.py 파일을 찾을 수 없습니다: {inference_script_path}",
            "image_path": image_path,
            "timestamp": datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        }
        print(f"INFERENCE_RESULT:{json.dumps(error_result)}")
        return error_result
    
    # inference.py 실행
    try:
        import subprocess
        
        # 명령 구성
        cmd = [
            sys.executable,
            inference_script_path,
            "--model", model_path,
            "--image", image_path,
            "--conf", "0.25"
        ]
        
        # 프로세스 실행
        process = subprocess.run(
            cmd,
            stdout=subprocess.PIPE,
            stderr=subprocess.PIPE,
            universal_newlines=True,
            check=False
        )
        
        # 결과 확인
        if process.returncode != 0:
            error_result = {
                "success": False,
                "error": f"inference.py 실행 오류 (반환 코드: {process.returncode}): {process.stderr}",
                "image_path": image_path,
                "timestamp": datetime.now().strftime("%Y-%m-%d %H:%M:%S")
            }
            print(f"INFERENCE_RESULT:{json.dumps(error_result)}")
            return error_result
        
        # inference.py 출력에서 결과 JSON 찾기
        inference_result = None
        for line in process.stdout.splitlines():
            if line.startswith("INFERENCE_RESULT:"):
                result_json = line[len("INFERENCE_RESULT:"):]
                try:
                    inference_result = json.loads(result_json)
                    break
                except json.JSONDecodeError:
                    pass
        
        if inference_result:
            print(f"INFERENCE_RESULT:{json.dumps(inference_result)}")
            return inference_result
        else:
            error_result = {
                "success": False,
                "error": "inference.py에서 결과를 반환하지 않았습니다.",
                "image_path": image_path,
                "timestamp": datetime.now().strftime("%Y-%m-%d %H:%M:%S")
            }
            print(f"INFERENCE_RESULT:{json.dumps(error_result)}")
            return error_result
    except Exception as e:
        error_result = {
            "success": False,
            "error": str(e),
            "image_path": image_path,
            "timestamp": datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        }
        print(f"INFERENCE_RESULT:{json.dumps(error_result)}")
        return error_result

if __name__ == "__main__":
    # 명령행 인수 확인
    if len(sys.argv) > 2 and sys.argv[1] == "infer":
        # 추론 모드: python tutorial_train_script.py infer <모델_경로> <이미지_경로>
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
        # 일반 모드: 전체 튜토리얼 파이프라인 실행
        try:
            main()
        except Exception as e:
            logger.error(f"프로그램 실행 중 오류 발생: {e}", exc_info=True)
            print(f"PROGRESS::프로그램 실행 중 오류 발생: {str(e)}", flush=True)
            error_result = {
                "success": False,
                "error": str(e),
                "timestamp": datetime.now().strftime("%Y-%m-%d %H:%M:%S")
            }
            # print(f"RESULT_JSON:{json.dumps(error_result)}") # 이 부분은 주석처리 또는 삭제