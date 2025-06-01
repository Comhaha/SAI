#!/usr/bin/env python
# -*- coding: utf-8 -*-

"""
practice_train_script.py - AI 블록 코딩 튜토리얼 모드 구현

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
import shutil
import torch

from datetime import datetime
# 로깅 레벨 설정
logging.getLogger().setLevel(logging.INFO)

print("[DEBUG] practice_train_script.py 시작", flush=True)

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
        print(f"[DEBUG] install_packages 모듈 가져오기 실패: {str(e)}", flush=True)
        raise

    # python-dotenv 가져오기
    print("[DEBUG] python-dotenv 가져오기 시도", flush=True)
    try:
        from dotenv import load_dotenv
        print("[DEBUG] python-dotenv 가져오기 성공", flush=True)
    except Exception as e:
        print(f"[DEBUG] python-dotenv 가져오기 실패: {str(e)}", flush=True)
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
            print(f"[DEBUG] .env 파일 로드 실패: {str(e)}", flush=True)
            raise
    else:
        print("[DEBUG] .env 파일이 존재하지 않음", flush=True)
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
        print(f"[DEBUG] 로깅 설정 실패: {str(e)}", flush=True)
        raise

    # install_packages의 진행 상황 표시 함수 사용
    print("[DEBUG] show_progress 함수 가져오기 시도", flush=True)
    show_progress = install_packages.show_progress
    print("[DEBUG] show_progress 함수 가져오기 성공", flush=True)

    # 로그 태그 자동화 래퍼 함수 추가

    def show_tagged_progress(tag, message, start_time=None, progress=None):
        """
        태그를 자동으로 붙여서 show_progress를 호출하는 래퍼 함수
        tag: 문자열(예: 'INFO', 'ERROR', 'DATASET', 'TRAIN', 'INFER' 등)
        message: 실제 메시지
        start_time, progress: 기존 show_progress와 동일
        """
        tagged_message = f"[{tag}] {message}"
        show_progress(tagged_message, start_time, progress)

    # 사용 예시:
    # show_tagged_progress('DATASET', '데이터셋 다운로드 시작...', start_time, 0)
    # show_tagged_progress('ERROR', f'모델 로드 오류: {e}', start_time, 100)
    # show_tagged_progress('TRAIN', '학습 시작', start_time, 10)

    # 튜토리얼 상태 관리용 전역 변수
    print("[DEBUG] practice_state 초기화 시도", flush=True)
    practice_state = {
        "model": None,
        "model_path": None,
        "dataset_path": None,
        "data_yaml_path": None,
        "image_path": None,
        "result_image_path": None,
        "training_completed": False
    }
    print("[DEBUG] practice_state 초기화 완료", flush=True)

    print("[DEBUG] practice_train_script.py 초기화 완료", flush=True)

except Exception as e:
    logger.error(f"practice_train_script.py 초기화 중 오류 발생: {str(e)}", exc_info=True)
    print(f"PROGRESS::스크립트 초기화 오류가 발생했습니다: {str(e)}", flush=True)
    raise

# ================== 1. 패키지 설치 블록 함수 ==================
def install_packages_block(block_params=None):
    """패키지 설치 블록 실행 함수"""
    print("[DEBUG] install_packages_block 함수 진입", flush=True)

    start_time = time.time()
    show_tagged_progress('TRAIN', '필수 패키지 설치를 시작합니다', start_time, 0)
    
    # 패키지 설치 순서 변경 및 버전 명시 (numpy 1.19.2 호환)
    packages = [
        "numpy==1.19.2",
        "matplotlib==3.3.4",  # numpy 1.19.2와 호환
        "ultralytics==8.0.100",  # 더 낮은 버전 사용
        "opencv-python==4.6.0.66"  # 더 낮은 버전 사용
    ]
    
    try:
        result = install_packages.install_packages_with_progress(packages, start_time)
        print("[DEBUG] install_packages_with_progress 결과:", result, flush=True)
        
        pkg_elapsed = time.time() - start_time
        show_tagged_progress('TRAIN', f'필수 패키지 설치가 완료되었습니다 (소요 시간: {int(pkg_elapsed)}초)', start_time, 100)
        
        return {
            "success": result.get("success", False),
            "installed_packages": result.get("installed_packages", []),
            "failed_packages": result.get("failed_packages", []),
            "elapsed_time": pkg_elapsed
        }
    except Exception as e:
        show_tagged_progress('ERROR', f'패키지 설치 중 오류 발생: {e}', start_time, 100)
        return {
            "success": False,
            "error": str(e),
            "elapsed_time": time.time() - start_time
        }

# ================== 2. GPU 확인 및 모델 로드 블록 함수 ==================
def check_gpu_yolo_load_block(block_params=None):
    """GPU 상태 확인 및 모델 로드 블록 실행 함수"""
    # 1. GPU 확인 프로그레스
    gpu_start_time = time.time()
    show_tagged_progress('TRAIN', 'GPU 정보 확인 중...', gpu_start_time, 0)
    gpu_info = install_packages.check_gpu(gpu_start_time)
    time.sleep(0.5)  # 실제 확인 시간 대체(시뮬레이션)
    show_tagged_progress('TRAIN', 'GPU 정보 확인 완료', gpu_start_time, 100)

    # 2. block_params에서 model_type 받기 (기본값: 'n')
    model_type = 'n'
    if block_params and 'model_type' in block_params:
        if block_params['model_type'] in ['n', 's', 'm', 'l']:
            model_type = block_params['model_type']

    # 3. 모델 로드 프로그레스 (별도 start_time 사용)
    model_load_time = time.time()
    show_tagged_progress('TRAIN', f'YOLOv8{model_type} 모델 로드 중...', model_load_time, 0)
    try:
        from ultralytics import YOLO
        model_filename = f'yolov8{model_type}.pt'
        model_path = os.path.join(base_dir, model_filename)
        # 모델 로딩 진행 시뮬레이션
        for progress in [10, 30, 50, 70, 90]:
            show_tagged_progress('TRAIN', f'YOLOv8{model_type} 모델 로드 중...', model_load_time, progress)
            time.sleep(0.2)
        model = YOLO(model_path)
        show_tagged_progress('TRAIN', f'YOLOv8{model_type} 모델 로드 완료!', model_load_time, 100)

        # 전역 상태 업데이트
        practice_state["model"] = model
        practice_state["model_path"] = model_path

        return {
            "success": True,
            "gpu_info": gpu_info,
            "model_path": model_path,
            "elapsed_time": time.time() - gpu_start_time
        }
    except Exception as e:
        show_tagged_progress('ERROR', f'모델 로드 오류: {e}', model_load_time, 100)
        return {
            "success": False,
            "error": str(e),
            "gpu_info": gpu_info
        }

# ================== 2-1. 커스텀 모델 레이어 설정 및 로드 블록 함수 ==================
def load_model_with_layer_block(block_params=None):
    """
    loadModelWithLayer 블록 실행 함수
    사용자 파라미터로 커스텀 YAML을 생성하고 모델을 로드
    
    Args:
        block_params (dict): 블록에서 전달받은 파라미터
            - Conv: 64, 128, 256
            - C2f: 1, 2, 3  
            - Upsample_scale: 2.0, 3.0, 4.0
    """
    start_time = time.time()
    show_tagged_progress('TRAIN', '커스텀 모델 레이어 설정 시작...', start_time, 0)
    
    try:
        # 1. GPU 정보 확인 (기존 함수 재사용)
        show_tagged_progress('TRAIN', 'GPU 정보 확인 중...', start_time, 10)
        gpu_info = install_packages.check_gpu(start_time)
        
        # 2. 블록 파라미터 검증 및 기본값 설정
        # C#에서 전달되는 모델 구조 파라미터들
        conv = block_params.get("Conv", 64) if block_params else 64
        c2f = block_params.get("C2f", 1) if block_params else 1
        upsample_scale = block_params.get("Upsample_scale", 2.0) if block_params else 2.0
        
        # 기타 파라미터들 (있는 경우 처리)
        if block_params:
            # 모델 이름 (있는 경우)
            if "model" in block_params:
                model_name = block_params["model"]
                show_tagged_progress('DEBUG', f'모델 이름: {model_name}', start_time, 25)
            
            # 블록 타입들 (있는 경우)
            if "blockTypes" in block_params:
                block_types = block_params["blockTypes"] 
                show_tagged_progress('DEBUG', f'블록 타입들: {block_types}', start_time, 25)
        
        # 유효 범위 확인
        valid_conv = [64, 128, 256]
        valid_c2f = [1, 2, 3]
        valid_upsample = [2.0, 3.0, 4.0]
        
        if conv not in valid_conv:
            show_tagged_progress('WARN', f'Conv 값 {conv}이 유효하지 않습니다. 기본값 64 사용', start_time, 15)
            conv = 64
        if c2f not in valid_c2f:
            show_tagged_progress('WARN', f'C2f 값 {c2f}이 유효하지 않습니다. 기본값 1 사용', start_time, 15)
            c2f = 1
        if upsample_scale not in valid_upsample:
            show_tagged_progress('WARN', f'Upsample_scale 값 {upsample_scale}이 유효하지 않습니다. 기본값 2.0 사용', start_time, 15)
            upsample_scale = 2.0
        
        show_tagged_progress('TRAIN', f'파라미터 확인 완료: Conv={conv}, C2f={c2f}, Upsample={upsample_scale}', start_time, 20)
        
        # 3. 데이터셋 경로 확인 (클래스 수 자동 감지용)
        dataset_path = practice_state.get("dataset_path")
        if not dataset_path:
            show_tagged_progress('WARN', '데이터셋이 준비되지 않았습니다. 기본 클래스 수(3) 사용', start_time, 25)
            dataset_path = base_dir  # 임시 경로
        
        # 4. 커스텀 YAML 생성
        show_tagged_progress('TRAIN', '사용자 파라미터로 커스텀 YAML 생성 중...', start_time, 40)
        custom_yaml_path = generate_and_save_custom_yaml(
            conv_channels=conv,
            c2f_layers=c2f,
            upsample_scale=upsample_scale,
            dataset_path=dataset_path
        )
        
        # 5. 커스텀 모델 로드
        show_tagged_progress('TRAIN', '커스텀 YAML로 모델 로드 중...', start_time, 70)
        from ultralytics import YOLO
        
        # 진행 시뮬레이션
        for progress in [75, 80, 85, 90, 95]:
            show_tagged_progress('TRAIN', f'커스텀 모델 로드 중... ({progress}%)', start_time, progress)
            time.sleep(0.2)
        
        model = YOLO(custom_yaml_path)
        
        # 6. 전역 상태 업데이트
        practice_state["model"] = model
        practice_state["model_path"] = custom_yaml_path
        practice_state["is_custom_model"] = True
        practice_state["custom_config"] = {
            "Conv": conv,
            "C2f": c2f, 
            "Upsample_scale": upsample_scale
        }
        
        show_tagged_progress('TRAIN', '✅ 커스텀 모델 로드 완료!', start_time, 100)
        print("custom.yaml 생성 완료")  # 사용자 피드백
        
        return {
            "success": True,
            "model_path": custom_yaml_path,
            "custom_config": {
                "Conv": conv,
                "C2f": c2f,
                "Upsample_scale": upsample_scale
            },
            "gpu_info": gpu_info,
            "elapsed_time": time.time() - start_time
        }
        
    except Exception as e:
        show_tagged_progress('ERROR', f'커스텀 모델 로드 오류: {e}', start_time, 100)
        return {
            "success": False,
            "error": str(e),
            "elapsed_time": time.time() - start_time
        }

def generate_and_save_custom_yaml(conv_channels=64, c2f_layers=1, upsample_scale=2.0, dataset_path=None):
    """
    커스텀 YAML 생성 및 저장 함수
    
    Returns:
        str: 생성된 YAML 파일 경로
    """
    import os
    
    # 클래스 수 자동 감지
    num_classes = 3  # 기본값
    if dataset_path:
        data_yaml_path = os.path.join(dataset_path, "data.yaml")
        if os.path.exists(data_yaml_path):
            try:
                import yaml
                with open(data_yaml_path, 'r', encoding='utf-8') as f:
                    data_config = yaml.safe_load(f)
                    num_classes = data_config.get('nc', 3)
            except Exception:
                pass
    
    # 채널 수 계산
    ch1 = conv_channels
    ch2 = ch1 * 2
    ch3 = ch2 * 2  
    ch4 = ch3 * 2
    
    # YAML 내용 생성
    yaml_content = f"""# YOLOv8 Custom Model - Generated from Block Parameters
# Conv={conv_channels}, C2f={c2f_layers}, Upsample={upsample_scale}
nc: {num_classes}  # number of classes
depth_multiple: 0.33
width_multiple: 0.5

backbone:
  # [from, number, module, args]
  - [-1, 1, Conv, [{ch1}, 3, 2]]      # P1/2
  - [-1, 1, Conv, [{ch1}, 3, 2]]      # P2/4
  - [-1, {c2f_layers}, C2f, [{ch1}, True]]    # C2f layers = {c2f_layers}
  - [-1, 1, Conv, [{ch2}, 3, 2]]      # P3/8
  - [-1, {c2f_layers}, C2f, [{ch2}, True]]
  - [-1, 1, Conv, [{ch3}, 3, 2]]      # P4/16
  - [-1, {c2f_layers}, C2f, [{ch3}, True]]
  - [-1, 1, Conv, [{ch4}, 3, 2]]      # P5/32
  - [-1, {c2f_layers}, C2f, [{ch4}, True]]
  - [-1, 1, SPPF, [{ch4}, 5]]         # SPPF

head:
  - [-1, 1, nn.Upsample, [None, {upsample_scale}, 'nearest']]    # upsample = {upsample_scale}
  - [[-1, 6], 1, Concat, [1]]
  - [-1, {c2f_layers}, C2f, [{ch3}]]

  - [-1, 1, nn.Upsample, [None, {upsample_scale}, 'nearest']]
  - [[-1, 4], 1, Concat, [1]]
  - [-1, {c2f_layers}, C2f, [{ch2}]]

  - [-1, 1, Conv, [{ch2}, 3, 2]]
  - [[-1, 12], 1, Concat, [1]]
  - [-1, {c2f_layers}, C2f, [{ch3}]]

  - [-1, 1, Conv, [{ch3}, 3, 2]]
  - [[-1, 9], 1, Concat, [1]]
  - [-1, {c2f_layers}, C2f, [{ch4}]]

  - [[15, 18, 21], 1, Detect, [nc]]

# ========== 생성된 설정 요약 ==========
# Conv 채널: {conv_channels} → 계층별 채널 수: {ch1}-{ch2}-{ch3}-{ch4}
# C2f 반복: {c2f_layers}회 (높을수록 더 깊은 특징 학습)
# Upsample: {upsample_scale}배 (정수배로 안정적 업샘플링)
# 클래스 수: {num_classes}개
"""
    
    # 파일 저장
    if dataset_path and os.path.exists(dataset_path):
        custom_yaml_path = os.path.join(dataset_path, "custom_model.yaml")
    else:
        custom_yaml_path = os.path.join(base_dir, "custom_model.yaml")
    
    with open(custom_yaml_path, 'w', encoding='utf-8') as f:
        f.write(yaml_content)
    
    print(f"📊 커스텀 모델 설정: Conv={conv_channels}, C2f={c2f_layers}, Upsample={upsample_scale}, Classes={num_classes}")
    
    return custom_yaml_path

# ================== 3. 데이터셋 다운로드 블록 함수 ==================
def download_dataset_block(block_params=None):
    """데이터셋 다운로드 블록 실행 함수"""
    start_time = time.time()
    show_tagged_progress('DEBUG', '서버에서 데이터셋 다운로드 중...', start_time)
    
    # API 서버에서 데이터셋 다운로드
    try:
        import requests
        from tqdm import tqdm
    except ImportError:
        show_tagged_progress('ERROR', '필요한 패키지 설치 중...', start_time)
        install_packages.install_packages_with_progress(["requests", "tqdm"], start_time)
        import requests
        from tqdm import tqdm
    
    # 데이터셋 저장 경로 및 완료 파일 경로 설정
    dataset_dir = os.path.join(base_dir, "dataset")
    os.makedirs(dataset_dir, exist_ok=True)
    done_file = os.path.join(dataset_dir, "practice_dataset_done.txt")

    # 1. 캐싱: 완료 파일이 있으면 스킵
    if os.path.exists(done_file):
        show_tagged_progress('DATASET', '데이터셋이 이미 준비되어 있어 다운로드를 건너뜁니다.', start_time, 100)
        time.sleep(1.5)  # 메시지 인지 시간 확보
        extracted_dir = os.path.join(dataset_dir, "practice_dataset")
        data_yaml_path = find_yaml_file(dataset_dir, extracted_dir, start_time, mode="practice")
        practice_state["dataset_path"] = extracted_dir
        practice_state["data_yaml_path"] = data_yaml_path
        return {
            "success": True,
            "location": extracted_dir,
            "extracted_dir": extracted_dir,
            "data_yaml_path": data_yaml_path,
            "cached": True,
            "elapsed_time": time.time() - start_time
        }

    # 2. 기존 practice 데이터셋 관련 파일만 삭제
    practice_specific_files = ["practice_dataset", "practice_dataset.zip", "practice_dataset_done.txt"]
    for filename in practice_specific_files:
        file_path = os.path.join(dataset_dir, filename)
        try:
            if os.path.exists(file_path):
                if os.path.isfile(file_path) or os.path.islink(file_path):
                    os.unlink(file_path)
                    show_tagged_progress('DEBUG', f'기존 파일 삭제: {file_path}', start_time)
                elif os.path.isdir(file_path):
                    import shutil
                    shutil.rmtree(file_path)
                    show_tagged_progress('DEBUG', f'기존 폴더 삭제: {file_path}', start_time)
        except Exception as e:
            show_tagged_progress('ERROR', f'기존 practice 데이터셋 파일 삭제 실패: {file_path} - {e}', start_time)

    # 환경 변수에서 서버 주소 가져오기
    server_url = os.environ.get("API_SERVER_URL")
    if not server_url:
        show_tagged_progress('ERROR', 'API_SERVER_URL 환경 변수가 설정되지 않았습니다.', start_time)
        
        # 테스트용 더미 데이터 생성
        practice_state["dataset_path"] = dataset_dir
        return {
            "success": True,
            "message": "테스트용 더미 데이터 사용",
            "location": dataset_dir
        }
    
    # 슬래시로 끝나지 않는지 확인
    if server_url.endswith('/'):
        server_url = server_url[:-1]
    
    # API 엔드포인트 URL 구성
    api_url = f"{server_url}/api/download/practice"
    show_tagged_progress('DEBUG', 'API에서 다운로드 URL 요청 중...', start_time)
    
    zip_path = os.path.join(dataset_dir, "practice_dataset.zip")
    
    # API 호출하여 presigned URL 받기
    try:
        response = requests.get(api_url)
        if response.status_code == 200:
            data = response.json()
            download_url = data['result']
            show_tagged_progress('DEBUG', '다운로드 URL 획득 성공', start_time)
        else:
            show_tagged_progress('ERROR', f'API 호출 실패: 상태 코드 {response.status_code}', start_time)
            practice_state["dataset_path"] = dataset_dir
            return {
                "success": False,
                "error": f"API 응답 오류: {response.text}",
                "location": dataset_dir
            }
    except Exception as e:
        show_tagged_progress('ERROR', f'API 호출 중 오류 발생: {e}', start_time)
        practice_state["dataset_path"] = dataset_dir
        return {
            "success": False,
            "error": str(e),
            "location": dataset_dir
        }
    
    # 파일 다운로드 (진행률 표시)
    show_tagged_progress('DATASET', '데이터셋 다운로드 시작...', start_time, 0)
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
                    progress = min(0 + (downloaded / total_size * 50), 50) 
                    show_tagged_progress('DATASET', f'다운로드 중: {downloaded//(1024*1024)}MB/{total_size//(1024*1024)}MB', start_time, progress)
        
        show_tagged_progress('DEBUG', '데이터셋 다운로드 완료', start_time)
    except Exception as e:
        show_tagged_progress('ERROR', f'다운로드 중 오류 발생: {e}', start_time)
        practice_state["dataset_path"] = dataset_dir
        return {
            "success": False,
            "error": str(e),
            "location": dataset_dir
        }
    
    # ZIP 파일 압축 해제
    extracted_dir = dataset_dir  # 기본값 설정
    target_subdir = os.path.join(dataset_dir, "practice_dataset")

    if os.path.exists(zip_path):
        try:
            with zipfile.ZipFile(zip_path, 'r') as zip_ref:
                file_list = zip_ref.namelist()
                total_files = len(file_list)
                show_tagged_progress('DATASET', f'압축 파일 내 {total_files}개 파일 발견', start_time, 70)

                # zip 내부에 practice_dataset/ 폴더가 있는지 확인
                has_top_dir = False
                if file_list and file_list[0].count('/') > 0:
                    top_dir = file_list[0].split('/')[0]
                    if top_dir == "practice_dataset":
                        has_top_dir = True

                if has_top_dir:
                    # 이미 폴더가 있으면 기존대로 압축 해제
                    potential_extracted_dir = os.path.join(dataset_dir, "practice_dataset")
                    for i, file in enumerate(file_list):
                        try:
                            zip_ref.extract(file, dataset_dir)
                            if i % 50 == 0 or i == total_files - 1:
                                extract_progress = 55 + (i / total_files) * 40
                                show_tagged_progress('DATASET', f'압축 해제 중: {i+1}/{total_files} 파일', start_time, extract_progress)
                        except Exception as e:
                            show_tagged_progress('ERROR', f'파일 압축 해제 실패 ({file}): {str(e)}', start_time)
                            continue
                    extracted_dir = potential_extracted_dir
                else:
                    # 폴더가 없으면 dataset/practice_dataset/에 압축 해제
                    os.makedirs(target_subdir, exist_ok=True)
                    for i, file in enumerate(file_list):
                        try:
                            # file이 하위 폴더 구조를 포함할 수 있으므로, 상대 경로로 추출
                            dest_path = os.path.join(target_subdir, file)
                            dest_folder = os.path.dirname(dest_path)
                            os.makedirs(dest_folder, exist_ok=True)
                            
                            # 디렉토리만 나타내는 항목은 건너뛰기 (마지막이 '/'로 끝나는 경우)
                            if file.endswith('/'):
                                continue
                            
                            # 청크 단위로 파일 복사
                            with zip_ref.open(file) as source, open(dest_path, "wb") as target:
                                while True:
                                    chunk = source.read(8192)  # 8KB 청크로 읽기
                                    if not chunk:
                                        break
                                    target.write(chunk)
                            
                            if i % 50 == 0 or i == total_files - 1:
                                extract_progress = 55 + (i / total_files) * 40
                                show_tagged_progress('DATASET', f'압축 해제 중: {i+1}/{total_files} 파일', start_time, extract_progress)
                        except Exception as e:
                            show_tagged_progress('ERROR', f'파일 압축 해제 실패 ({file}): {str(e)}', start_time)
                            continue
                    extracted_dir = target_subdir
                    show_tagged_progress('DEBUG', f'압축을 {target_subdir}에 해제함', start_time)
            show_tagged_progress('DEBUG', '압축 해제 완료', start_time, 100)
                
            # 임시 ZIP 파일 삭제 (잠시 기다린 후 시도)
            time.sleep(1)  # 파일 핸들이 모두 닫힐 시간을 줍니다
            try:
                os.remove(zip_path)
                show_tagged_progress('DEBUG', '임시 ZIP 파일 삭제 완료', start_time)
            except Exception as e:
                show_tagged_progress('DEBUG', f'임시 ZIP 파일 삭제 실패: {str(e)}', start_time)
        
        except Exception as e:
            show_tagged_progress('DEBUG', f'ZIP 파일 압축 해제 오류: {e}', start_time)
    else:
        show_tagged_progress('ERROR', '다운로드된 ZIP 파일을 찾을 수 없습니다.', start_time)                           
                
                
        
    # 데이터셋 경로 저장
    practice_state["dataset_path"] = extracted_dir

    # data.yaml 파일 찾기
    data_yaml_path = find_yaml_file(dataset_dir, extracted_dir, start_time, mode="practice")
    if data_yaml_path is None:
        show_tagged_progress('ERROR', 'data.yaml 파일을 찾을 수 없습니다. 기본 경로를 사용합니다.', start_time)
        data_yaml_path = os.path.join(extracted_dir, 'data.yaml')  # 기본 경로 설정

    practice_state["data_yaml_path"] = data_yaml_path
    show_tagged_progress('DATASET', '데이터셋 준비 완료', start_time, 100)

    # 완료 파일 생성
    try:
        with open(done_file, "w") as f:
            f.write("done")
        show_tagged_progress('DEBUG', '데이터셋 완료 파일 생성', start_time, 100)
    except Exception as e:
        show_tagged_progress('ERROR', f'완료 파일 생성 실패: {e}', start_time)

    return {
            "success": True,
            "location": extracted_dir,
            "extracted_dir": extracted_dir,
            "data_yaml_path": data_yaml_path,
            "elapsed_time": time.time() - start_time
        }   

# data.yaml 파일 찾기 도우미 함수 수정
def find_yaml_file(dataset_dir, extracted_dir, start_time, mode="practice"):
    """
    데이터셋 디렉토리에서 data.yaml 파일 찾기
    
    Args:
        dataset_dir: 기본 데이터셋 디렉토리
        extracted_dir: 압축 해제된 디렉토리
        start_time: 시작 시간 (로깅용)
        mode: 검색 모드 ('tutorial' 또는 'practice')
    """
    show_tagged_progress('DEBUG', f'데이터 경로 확인: {extracted_dir} (모드: {mode})', start_time)
    
    # 모드별 디렉토리 설정
    target_dir = os.path.join(dataset_dir, f"{mode}_dataset")
    show_tagged_progress('DEBUG', f'타겟 디렉토리: {target_dir}', start_time)
    
    # 1. 직접 지정된 경로에서 찾기
    yaml_path = os.path.join(extracted_dir, "data.yaml")
    if os.path.exists(yaml_path):
        show_tagged_progress('DEBUG', f'데이터 파일 확인됨: {yaml_path}', start_time)
        return yaml_path
    
    # 2. 모드별 디렉토리에서 찾기
    yaml_path = os.path.join(target_dir, "data.yaml")
    if os.path.exists(yaml_path):
        show_tagged_progress('DEBUG', f'모드별 디렉토리에서 데이터 파일 확인됨: {yaml_path}', start_time)
        return yaml_path
    
    # 3. 모드별 디렉토리의 하위 폴더들에서만 data.yaml 찾기
    if os.path.exists(target_dir):
        for root, dirs, files in os.walk(target_dir):
            for file in files:
                if file == "data.yaml":
                    yaml_path = os.path.join(root, file)
                    show_tagged_progress('DEBUG', f'모드별 하위 폴더에서 데이터 파일 확인됨: {yaml_path}', start_time)
                    return yaml_path
    
    # 파일을 찾지 못했을 경우
    show_tagged_progress('ERROR', f'{mode}_dataset에서 data.yaml 파일을 찾을 수 없습니다', start_time)
    return None

# ================== 4. 모델 학습 블럭 ==================
def train_model_block(block_params=None):
    """
    모델 학습 블록 실행 함수
    
    Args:
        block_params (dict): 블록에서 전달받은 파라미터
            - epoch (int): 사용자가 지정한 에폭 수
            - imgsz (int): 사용자가 지정한 이미지 크기
            - accuracy (float): 신뢰도 임계값 (추론에서 사용)
            
    주의: Conv, C2f, Upsample_scale은 load_model_with_layer_block에서 처리됨
    """
    start_time = time.time()
    show_tagged_progress('TRAIN', '모델 학습 준비 중...', start_time, 0)

    # 학습 관련 파라미터만 추출
    epochs = block_params.get("epochs") if block_params else None
    imgsz = block_params.get("image_size") if block_params else None
    
    # 추론에서 사용할 파라미터 (학습에서는 저장만)
    if block_params and "accuracy" in block_params:
        practice_state["inference_accuracy"] = block_params["accuracy"]    

    # 기존 results.csv 삭제
    results_csv = os.path.join(base_dir, "runs", "detect", "train", "results.csv")
    if os.path.exists(results_csv):
        os.remove(results_csv)
        show_tagged_progress('DEBUG', '기존 results.csv 파일 삭제 완료', start_time, 18)

    # 필요한 데이터가 있는지 확인
    if not practice_state.get("model"):
        show_tagged_progress('ERROR', '모델이 로드되지 않았습니다. 모델 로드 단계를 먼저 실행하세요.', start_time, 10)
        return {
            "success": False,
            "error": "모델이 로드되지 않음"
        }
    
    if not practice_state.get("data_yaml_path"):
        show_tagged_progress('ERROR', '데이터셋 YAML 파일이 설정되지 않았습니다. 데이터셋 준비 단계를 먼저 실행하세요.', start_time, 10)
        return {
            "success": False,
            "error": "데이터셋 YAML 파일 없음"
        }
    
    # 커스텀 모델 정보 표시 (있는 경우)
    if practice_state.get("is_custom_model") and practice_state.get("custom_config"):
        config = practice_state["custom_config"]
        show_tagged_progress('TRAIN', f'커스텀 모델 설정으로 학습: Conv={config.get("Conv")}, C2f={config.get("C2f")}, Upsample={config.get("Upsample_scale")}', start_time, 15)
    
    # GPU 정보 확인
    gpu_info = install_packages.check_gpu(start_time)
    device = "cuda" if gpu_info.get("available", False) else "cpu"
    
    # 학습 파라미터 설정
    batch_size = 16
    if device == "cuda" and gpu_info.get("available", False):
        memory = gpu_info.get("memory_gb", [0])[0]
        if memory and memory < 6:
            batch_size = 8
            show_tagged_progress('TRAIN', f'GPU 메모리 제한으로 배치 크기 {batch_size}로 조정', start_time, 10)
    
    # 에폭 수 설정 - 사용자 지정 값 또는 기본값
    if epochs is None:
        # 기본 에폭 수 설정 (클라이언트에서 명시적으로 전달한 경우 우선 사용)
        epochs = 1  # 기본값을 1로 변경 (로컬과 동일하게)
    else:
        # 사용자 지정 에폭 수를 정수로 변환
        try:
            epochs = int(epochs)
            if epochs <= 0:
                show_tagged_progress('ERROR', f'에폭 수는 양수여야 합니다. 기본값을 사용합니다.', start_time, 15)
                epochs = 1  # 기본값을 1로 변경
        except ValueError:
            show_tagged_progress('ERROR', f'유효하지 않은 에폭 수입니다. 기본값을 사용합니다.', start_time, 15)
            epochs = 1  # 기본값을 1로 변경
    
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
                show_tagged_progress('ERROR', f'이미지 크기 {imgsz}는 권장되지 않습니다. 가장 가까운 권장 크기 {closest_size}를 사용합니다.', start_time, 15)
                imgsz = closest_size
        except ValueError:
            show_tagged_progress('ERROR', f'유효하지 않은 이미지 크기입니다. 기본값 640을 사용합니다.', start_time, 15)
            imgsz = 640
    
    show_tagged_progress('TRAIN', f'모델 학습 시작 (디바이스: {device}, 배치 크기: {batch_size}, 에폭: {epochs}, 이미지 크기: {imgsz})', start_time, 20)
    
    try:
        # 학습 시작 시간 기록
        epoch_start_time = time.time()
        last_progress_update = time.time()
        
        # 학습 진행 상태를 모니터링할 변수들
        completed_epochs = 0
        total_epochs = epochs
        
        # 학습 실행 (클래스 속성을 사용하여 진행 상황 업데이트)
        class ProgressCallback:
            def __init__(self):
                self.start_time = time.time()
            
            def on_train_epoch_end(self, trainer):
                nonlocal completed_epochs
                completed_epochs = trainer.epoch + 1
                progress = (completed_epochs / total_epochs) * 100
                elapsed = time.time() - self.start_time
                minutes, seconds = divmod(elapsed, 60)
                
                # 잔여 시간 추정
                if completed_epochs > 1:
                    time_per_epoch = elapsed / completed_epochs
                    remaining_epochs = total_epochs - completed_epochs
                    remaining_time = time_per_epoch * remaining_epochs
                    rem_minutes, rem_seconds = divmod(remaining_time, 60)
                    bar = make_progress_bar(progress)
                    print(f"PROGRESS:{progress:.1f}:[전체 {progress:.1f}% | {int(minutes):02d}:{int(seconds):02d} 경과 | {int(rem_minutes):02d}:{int(rem_seconds):02d} 남음] [TRAIN] {bar} ({completed_epochs}/{total_epochs} 에폭) 학습 중", flush=True)
                else:
                    bar = make_progress_bar(progress)
                    print(f"PROGRESS:{progress:.1f}:[전체 {progress:.1f}% | {int(minutes):02d}:{int(seconds):02d} 경과] [TRAIN] {bar} ({completed_epochs}/{total_epochs} 에폭) 학습 중", flush=True)
        
        # 콜백 객체 생성
        callbacks = [ProgressCallback()]
        
        # 학습 실행
        model = practice_state["model"]
        data_yaml_path = practice_state["data_yaml_path"]
        
        # YOLOv8 학습 실행 - verbose=True로 설정하여 로그 출력 활성화
        show_tagged_progress('TRAIN', 'YOLO train() 메소드 호출 중...', start_time, 30)
        results = model.train(
            data=data_yaml_path,
            epochs=epochs,
            batch=batch_size,
            imgsz=imgsz,
            device=device,
            project=os.path.join(base_dir, "runs"),
            name="detect/train",  # 하위 폴더 구조 지정
            exist_ok=True,
            workers = 0,
        )
        
        show_tagged_progress('TRAIN', 'YOLO 학습이 완료되었습니다', start_time, 90)
        
        # 결과 경로 설정
        results_dir = find_latest_results_dir()
        model_path = os.path.join(results_dir, "weights", "best.pt")
        
        # 전역 상태 업데이트
        practice_state["model_path"] = model_path
        practice_state["results_dir"] = results_dir
        practice_state["training_completed"] = True
        
        train_elapsed = time.time() - start_time
        minutes, seconds = divmod(train_elapsed, 60)
        show_tagged_progress('TRAIN', f'모델 학습 완료! (소요 시간: {int(minutes)}분 {int(seconds)}초)', start_time, 100)
        
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
        show_tagged_progress('ERROR', f'학습 중 오류 발생: {e}', start_time, 70)
        
        # 메모리 부족 오류 처리
        if "CUDA out of memory" in str(e):
            show_tagged_progress('ERROR', 'GPU 메모리 부족. 배치 크기를 줄여서 다시 시도합니다.', start_time, 75)
            try:
                # 배치 크기 절반으로 줄임
                reduced_batch = max(1, batch_size // 2)
                retry_start = time.time()
                show_tagged_progress('TRAIN', f'줄어든 배치 크기로 재시도 중 (배치 크기: {reduced_batch})...', start_time, 80)
                
                # 재시도
                model = practice_state["model"]
                results = model.train(
                    data=practice_state["data_yaml_path"],
                    epochs=epochs,
                    batch=reduced_batch,
                    imgsz=imgsz,  # 사용자 지정 이미지 크기 유지
                    device=device,
                    project=os.path.join(base_dir, "runs"),
                    name="detect/train",
                    exist_ok=True,
                    workers = 0,
                )
                
                # 결과 경로 설정
                results_dir = find_latest_results_dir()
                model_path = os.path.join(results_dir, "weights", "best.pt")
                
                # 전역 상태 업데이트
                practice_state["model_path"] = model_path
                practice_state["results_dir"] = results_dir
                practice_state["training_completed"] = True
                
                retry_elapsed = time.time() - retry_start
                minutes, seconds = divmod(retry_elapsed, 60)
                show_tagged_progress('TRAIN', f'배치 크기 {reduced_batch}로 학습 완료! (소요 시간: {int(minutes)}분 {int(seconds)}초)', start_time, 100)
                
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
                show_tagged_progress('ERROR', f'재시도도 실패: {e2}', start_time, 85)
                # CPU로 전환
                show_tagged_progress('TRAIN', 'CPU 모드로 전환합니다...', start_time, 90)
                
                try:
                    # CPU로 전환하고 에폭 수 줄임
                    cpu_epochs = min(2, epochs)  # 원래 에폭보다 크지 않게
                    model = practice_state["model"]
                    
                    results = model.train(
                        data=practice_state["data_yaml_path"],
                        epochs=cpu_epochs,
                        batch=4,
                        imgsz=imgsz,  # 사용자 지정 이미지 크기 유지
                        device="cpu",
                        project=os.path.join(base_dir, "runs"),
                        name="detect/train",
                        exist_ok=True,
                        workers = 0,
                    )
                    
                    # 결과 경로 설정
                    results_dir = find_latest_results_dir()
                    model_path = os.path.join(results_dir, "weights", "best.pt")
                    
                    # 전역 상태 업데이트
                    practice_state["model_path"] = model_path
                    practice_state["results_dir"] = results_dir
                    practice_state["training_completed"] = True
                    
                    cpu_elapsed = time.time() - start_time
                    minutes, seconds = divmod(cpu_elapsed, 60)
                    show_tagged_progress('TRAIN', f'CPU로 학습 완료! (소요 시간: {int(minutes)}분 {int(seconds)}초)', start_time, 100)
                    
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
                    show_tagged_progress('ERROR', f'CPU 모드도 실패: {e3}', start_time, 95)
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

# ================== 5. 결과 그래프 시각화 블록 함수 ==================
def visualize_training_results_block(block_params=None):
    """학습 결과 그래프 시각화 블록 실행 함수"""
    start_time = time.time()
    show_tagged_progress('TRAIN', '학습 결과 시각화 중...', start_time, 0)
    
    # 학습이 완료되었는지 확인
    if not practice_state.get("training_completed"):
        show_tagged_progress('ERROR', '학습이 완료되지 않았습니다. 모델 학습 단계를 먼저 실행하세요.', start_time, 10)
        return {
            "success": False,
            "error": "학습이 완료되지 않음"
        }
    
    # 결과 디렉토리 확인
    results_dir = practice_state.get("results_dir")
    if not results_dir or not os.path.exists(results_dir):
        results_dir = find_latest_results_dir()
        practice_state["results_dir"] = results_dir
    
    # 결과 이미지 경로 확인
    results_path = os.path.join(results_dir, "results.png")
    
    try:
        # 결과 이미지가 존재하는지 확인
        if not os.path.exists(results_path):
            show_tagged_progress('ERROR', f'결과 그래프 파일을 찾을 수 없습니다: {results_path}', start_time, 50)
            
            # 다른 가능한 경로 확인
            alternative_paths = [
                os.path.join(results_dir, "results.png"),
                os.path.join(results_dir, "confusion_matrix.png"),
                os.path.join(results_dir, "val_batch0_pred.jpg")
            ]
            
            for alt_path in alternative_paths:
                if os.path.exists(alt_path):
                    results_path = alt_path
                    show_tagged_progress('DATASET', f'대체 결과 파일 발견: {results_path}', start_time, 60)
                    break
        
        # 결과 이미지 표시
        if os.path.exists(results_path):
            show_tagged_progress('TRAIN', f'학습 결과 그래프 확인: {results_path}', start_time, 80)
            try:
                # 이미지 표시 (IPython 환경에서만 작동)
                from IPython.display import Image, display
                display(Image(filename=results_path))
                show_tagged_progress('TRAIN', '결과 그래프 표시 완료', start_time, 100)
            except ImportError:
                # 일반 환경에서는 파일 경로만 반환
                show_tagged_progress('TRAIN', 'IPython 환경이 아니므로 결과 파일 경로만 반환합니다.', start_time, 90)
            
            # 결과 경로 저장
            practice_state["results_image_path"] = results_path
            
            return {
                "success": True,
                "results_path": results_path,
                "elapsed_time": time.time() - start_time
            }
        else:
            show_tagged_progress('ERROR', '결과 그래프 파일을 찾을 수 없습니다.', start_time, 100)
            return {
                "success": False,
                "error": "결과 그래프 파일 없음",
                "elapsed_time": time.time() - start_time
            }
    except Exception as e:
        show_tagged_progress('ERROR', f'결과 시각화 오류: {e}', start_time, 100)
        return {
            "success": False,
            "error": str(e),
            "elapsed_time": time.time() - start_time
        }

# 6. 사용자 이미지 경로 받는 블럭
# 이미지 경로를 inference.py 파일로 던져준다
def set_image_path_block(image_path=None, block_params=None):
    """
    추론용 이미지 경로 설정 블록 실행 함수
    
    Args:
        image_path (str, optional): 사용자가 지정한 이미지 경로. 
                                   None이면 기본 테스트 이미지 찾기 시도
    """
    start_time = time.time()
    show_tagged_progress('DATASET', '추론용 이미지 경로 설정 중...', start_time, 0)
    
    if block_params and image_path is None:
        image_path = block_params.get("imgPath") or block_params.get("image_path")
    
    # 사용자가 지정한 이미지 경로가 있는지 확인
    if image_path:
        # 이미지 파일이 실제로 존재하는지 확인
        if os.path.exists(image_path):
            # 이미지 파일 확장자 확인
            if image_path.lower().endswith(('.jpg', '.jpeg', '.png', '.bmp')):
                # 경로 저장
                practice_state["image_path"] = image_path
                show_tagged_progress('DATASET', f'사용자 지정 이미지 경로 설정 완료: {image_path}', start_time, 100)
                return {
                    "success": True,
                    "image_path": image_path,
                    "source_type": "user_specified",
                    "elapsed_time": time.time() - start_time
                }
            else:
                show_tagged_progress('ERROR', f'지원되지 않는 이미지 형식입니다: {image_path}', start_time, 50)
                return {
                    "success": False,
                    "error": "지원되지 않는 이미지 형식",
                    "elapsed_time": time.time() - start_time
                }
        else:
            show_tagged_progress('ERROR', f'지정한 이미지 파일을 찾을 수 없습니다: {image_path}', start_time, 50)
            return {
                "success": False,
                "error": "이미지 파일 없음",
                "elapsed_time": time.time() - start_time
            }

# ================== 7. 모델 추론 블록 함수 ==================
def run_inference_block(block_params=None):
    """모델 추론 실행 블록 함수 - inference.py 활용"""
    start_time = time.time()
    show_tagged_progress('INFER', '모델 추론 실행 중...', start_time, 0)
    
    # 필요한 정보가 있는지 확인
    model_path = practice_state.get("model_path")
    if not model_path:
        # 학습된 모델이 없다면 기본 모델 사용
        model_path = os.path.join(base_dir, "yolov8n.pt")
        show_tagged_progress('TRAIN', f'학습된 모델 경로가 설정되지 않았습니다. 기본 모델을 사용합니다: {model_path}', start_time, 10)
    
    image_path = practice_state.get("image_path")
    if not image_path:
        show_tagged_progress('ERROR', '테스트 이미지 경로가 설정되지 않았습니다. 이미지 경로 설정 단계를 먼저 실행하세요.', start_time, 10)
        return {
            "success": False,
            "error": "테스트 이미지 경로 없음"
        }
    
    # inference.py 파일 경로 확인
    inference_script_path = os.path.join(os.path.dirname(os.path.abspath(__file__)), "inference.py")
    if not os.path.exists(inference_script_path):
        show_tagged_progress('ERROR', f'inference.py 파일을 찾을 수 없습니다: {inference_script_path}', start_time, 20)
        return {
            "success": False,
            "error": "inference.py 파일 없음"
        }
    
    # 추론 실행 (inference.py 호출)
    try:
        show_tagged_progress('INFER', 'inference.py를 사용하여 추론 실행 중...', start_time, 30)
        
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
        
        show_tagged_progress('INFER', f'실행 명령: {" ".join(cmd)}', start_time, 40)
        
        # 프로세스 실행
        process = subprocess.Popen(
            cmd,
            stdout=subprocess.PIPE,
            stderr=subprocess.PIPE,
            universal_newlines=True,
            bufsize=1,
            encoding='utf-8'
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
                    show_tagged_progress('INFER', '추론 결과 JSON 파싱 성공', start_time, 70)
                except json.JSONDecodeError:
                    show_tagged_progress('ERROR', f'추론 결과 JSON 파싱 실패: {result_json}', start_time, 70)
            
            # 진행 상황 메시지 확인
            elif line.startswith("[INFERENCE]"):
                progress_msg = line[len("[INFERENCE] "):]
                show_tagged_progress('INFER', f'추론 진행 중: {progress_msg}', start_time, 60)
        
        # 프로세스 완료 대기
        process.wait()
        
        # 결과 확인
        if process.returncode != 0:
            stderr = process.stderr.read()
            show_tagged_progress('ERROR', f'inference.py 실행 오류 (반환 코드: {process.returncode}): {stderr}', start_time, 80)
            return {
                "success": False,
                "error": f"inference.py 실행 오류 (반환 코드: {process.returncode}): {stderr}",
                "elapsed_time": time.time() - start_time
            }
        
        # inference.py가 반환한 결과 확인
        if inference_result:
            # 결과 이미지 경로 저장
            if "result_image" in inference_result:
                practice_state["result_image_path"] = inference_result["result_image"]
            
            show_tagged_progress('INFER', f'추론 완료: {inference_result.get("success", False)}', start_time, 100)
            return {
                "success": inference_result.get("success", False),
                "result": inference_result,
                "elapsed_time": time.time() - start_time
            }
        else:
            show_tagged_progress('ERROR', 'inference.py에서 결과를 반환하지 않았습니다.', start_time, 100)
            return {
                "success": False,
                "error": "inference.py에서 결과가 없음",
                "elapsed_time": time.time() - start_time
            }
    except Exception as e:
        show_tagged_progress('ERROR', f'추론 실행 중 오류 발생: {e}', start_time, 100)
        return {
            "success": False,
            "error": str(e),
            "elapsed_time": time.time() - start_time
        }

# ==================8. 결과 시각화 블록 함수 - inference.py 결과 활용 ===================
def visualize_results_block(block_params=None):
    """추론 결과 시각화 블록 실행 함수"""
    start_time = time.time()
    show_tagged_progress('INFER', '추론 결과 시각화 중...', start_time, 0)
    
    # 결과 이미지 경로 확인
    result_image_path = practice_state.get("result_image_path")
    if not result_image_path:
        show_tagged_progress('ERROR', '추론 결과 이미지 경로가 설정되지 않았습니다. 모델 추론 단계를 먼저 실행하세요.', start_time, 10)
        return {
            "success": False,
            "error": "추론 결과 이미지 경로 없음"
        }
    
    # 이미지 파일 존재 확인
    if not os.path.exists(result_image_path):
        show_tagged_progress('ERROR', f'결과 이미지 파일을 찾을 수 없습니다: {result_image_path}', start_time, 20)
        return {
            "success": False,
            "error": "결과 이미지 파일 없음"
        }
    
    # 별도의 시각화 저장 없이, 경로만 반환
    show_tagged_progress('INFER', f'결과 이미지 경로 반환: {result_image_path}', start_time, 100)
    return {
        "success": True,
        "result_image_path": result_image_path,
        "elapsed_time": time.time() - start_time
    }

# =================== 메인 실행 함수 ========================
def main(block_params=None):
    """AI 블록 코딩 튜토리얼 모드 실행 메인 함수"""
    total_start_time = time.time()
    current_date = datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    show_tagged_progress('TRAIN', f'AI 블록 코딩 튜토리얼 모드 실행 시작 - {current_date}', total_start_time, 0)
    
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
        show_tagged_progress('TRAIN', f'블록 실행 중: {block_name} ({i+1}/{len(blocks)})', total_start_time, i * (100 / len(blocks)))
        try:
            result = block_func(block_params)
            results[block_name] = result
            
            if not result.get("success", False):
                success = False
                show_tagged_progress('ERROR', f'블록 실행 실패: {block_name} - {result.get("error", "알 수 없는 오류")}', total_start_time, (i+1) * (100 / len(blocks)))
                break
            
            show_tagged_progress('TRAIN', f'블록 실행 완료: {block_name}', total_start_time, (i+1) * (100 / len(blocks)))
        except Exception as e:
            success = False
            results[block_name] = {"success": False, "error": str(e)}
            show_tagged_progress('ERROR', f'블록 실행 중 오류 발생: {block_name} - {e}', total_start_time, (i+1) * (100 / len(blocks)))
            break
    
    # 튜토리얼 완료 보고
    total_elapsed = time.time() - total_start_time
    minutes, seconds = divmod(total_elapsed, 60)
    
    if success:
        show_tagged_progress('TRAIN', '✅ 튜토리얼 모드 실행 완료! (총 소요 시간: {int(minutes)}분 {int(seconds)}초)', total_start_time, 100)
    else:
        show_tagged_progress('ERROR', f'❌ 튜토리얼 모드 실행 중단 (소요 시간: {int(minutes)}분 {int(seconds)}초)', total_start_time, 100)
    
    # 결과 정보
    result = {
        "success": success,
        "blocks_results": results,
        "total_time_seconds": total_elapsed,
        "timestamp": datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    }
    
    # JSON으로 결과 출력 (C# 프로그램에서 파싱)
    print(f"RESULT_JSON:{json.dumps(result, ensure_ascii=False)}")
    return result

#================ 추론탭에서 추론하는 함수입니다 ==================

# 추론 전용 함수 (외부에서 호출용)
def infer_image(model_path, image_path, show=False):
    """모델을 사용해 개별 이미지 추론 (외부에서 호출용) - inference.py 활용"""
    start_time = time.time()
    show_tagged_progress('INFER', f'이미지 추론 요청: {image_path}', start_time, 0)
    
    # inference.py 파일 경로 확인
    inference_script_path = os.path.join(os.path.dirname(os.path.abspath(__file__)), "inference.py")
    if not os.path.exists(inference_script_path):
        error_result = {
            "success": False,
            "error": f"inference.py 파일을 찾을 수 없습니다: {inference_script_path}",
            "image_path": image_path,
            "timestamp": datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        }
        print(f"INFERENCE_RESULT:{json.dumps(error_result, ensure_ascii=False)}")
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
        process = subprocess.Popen(
            cmd,
            stdout=subprocess.PIPE,
            stderr=subprocess.PIPE,
            universal_newlines=True,
            bufsize=1,
            encoding='utf-8'
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
                    break
                except json.JSONDecodeError:
                    pass
        
        if inference_result:
            print(f"INFERENCE_RESULT:{json.dumps(inference_result, ensure_ascii=False)}")
            return inference_result
        else:
            error_result = {
                "success": False,
                "error": "inference.py에서 결과를 반환하지 않았습니다.",
                "image_path": image_path,
                "timestamp": datetime.now().strftime("%Y-%m-%d %H:%M:%S")
            }
            print(f"INFERENCE_RESULT:{json.dumps(error_result, ensure_ascii=False)}")
            return error_result
    except Exception as e:
        error_result = {
            "success": False,
            "error": str(e),
            "image_path": image_path,
            "timestamp": datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        }
        print(f"INFERENCE_RESULT:{json.dumps(error_result, ensure_ascii=False)}")
        return error_result

# =========== 프로그레스 바 관련 함수입니다 ============= #
# 프로그레스 바 문자열 함수
def make_progress_bar(progress, bar_length=20):
    filled_length = int(round(bar_length * progress / 100))
    bar = '█' * filled_length + '-' * (bar_length - filled_length)
    return f"|{bar}| {progress:5.1f}%"

# 실시간 텍스트 포맷 함수
def format_status(total_progress, elapsed, remain, tag, block_progress, bar, detail):
    return (f"[전체 {total_progress:4.1f}% | {elapsed} 경과 | {remain} 남음] "
            f"[{tag}] {bar} {detail}")

# 남은시간 포맷 함수
def format_time(seconds):
    minutes, sec = divmod(int(seconds), 60)
    return f"{minutes:02d}:{sec:02d}"

# 실시간 텍스트 + progress 로그출력
def print_train_progress(epoch, total_epochs, total_progress, start_time, tag="TRAIN"):
    block_progress = (epoch / total_epochs) * 100
    bar = make_progress_bar(block_progress)
    elapsed = format_time(time.time() - start_time)
    # 남은 시간 예측
    if epoch > 1:
        avg_time = (time.time() - start_time) / epoch
        remain_sec = avg_time * (total_epochs - epoch)
    else:
        remain_sec = 0
    remain = format_time(remain_sec)
    detail = f"({epoch}/{total_epochs} 에폭) 학습 중"
    print(f"PROGRESS:{block_progress:.1f}:[{tag}] {bar} {block_progress:.1f}% ({epoch}/{total_epochs} 에폭) 학습 중 | 전체 {total_progress:.1f}% | 경과 {elapsed} | 남음 {remain}", flush=True)


# progressbar만 채우는 함수
def print_block_progress(block_progress, message):
    # C#에서 progressbar만 채우고, 텍스트는 간단하게
    print(f"PROGRESS:{block_progress:.1f}:{message}", flush=True)

if __name__ == "__main__":
    # 명령행 인수 확인
    if len(sys.argv) > 2 and sys.argv[1] == "infer":
        # 추론 모드: python practice_train_script.py infer <모델_경로> <이미지_경로>
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
            print(f"INFERENCE_RESULT:{json.dumps(error_result, ensure_ascii=False)}")
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
            print(f"INFERENCE_RESULT:{json.dumps(error_result, ensure_ascii=False)}")
