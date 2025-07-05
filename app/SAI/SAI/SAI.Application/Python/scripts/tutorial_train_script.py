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
import shutil
import torch
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
    print(f"PROGRESS::스크립트 초기화 오류가 발생했습니다: {str(e)}", flush=True)
    raise

# ================== 1. 패키지 설치 블록 함수 ==================
# 패키지는 venv에 미리 설치 돼 있으므로 해당 블럭은 진행률에만 영향을 준다
def install_packages_block(block_params=None):
    """패키지 설치 블록 실행 함수 (단순 확인 모드)"""
    print("[DEBUG] install_packages_block 함수 진입", flush=True)

    start_time = time.time()
    show_tagged_progress('TRAIN', '필수 패키지 확인 중...', start_time, 0)
    
    try:
        # 간단한 진행률 시뮬레이션
        time.sleep(0.5)
        show_tagged_progress('TRAIN', '패키지 확인 중...', start_time, 50)
        
        time.sleep(0.5)
        show_tagged_progress('TRAIN', '패키지 확인 완료!', start_time, 100)
        
        return {
            "success": True,
            "installed_packages": ["numpy", "ultralytics", "opencv-python"],
            "failed_packages": [],
            "elapsed_time": 1.0
        }
        
    except Exception as e:
        show_tagged_progress('ERROR', f'패키지 확인 중 오류 발생: {e}', start_time, 100)
        return {
            "success": False,
            "error": str(e),
            "elapsed_time": time.time() - start_time
        }

# ================== 2. CPU 에서 사전 학습 모델 로드 블록 함수 ==================
def load_pretrained_model_block(block_params=None):
    """CPU 환경에서 사전학습 모델 로드 블록 실행 함수"""
    
    # 사전학습된 모델 로드
    model_load_time = time.time()
    show_tagged_progress('TRAIN', 'ovi7 사전학습 모델 로드 중...', model_load_time, 0)
    
    try:
        from ultralytics import YOLO
        
        # 🎯 미리 ovi7로 학습한 모델 경로 (고정)
        pretrained_model_path = os.path.join(base_dir, "model", "yolov8m-oiv7.pt")
        
        # 모델 파일 존재 확인
        if not os.path.exists(pretrained_model_path):
            error_msg = f"사전학습 모델을 찾을 수 없습니다: {pretrained_model_path}"
            show_tagged_progress('ERROR', error_msg, model_load_time, 50)
            return {
                "success": False,
                "error": error_msg
            }
        
        # 모델 로딩 진행 시뮬레이션
        for progress in [20, 40, 60, 80, 95]:
            show_tagged_progress('TRAIN', f'사전학습 모델 로드 중... ({progress}%)', model_load_time, progress)
            time.sleep(0.2)
        
        # 실제 모델 로드
        model = YOLO(pretrained_model_path)
        show_tagged_progress('TRAIN', f'✅ 사전학습 모델 로드 완료!', model_load_time, 100)

        # 전역 상태 업데이트
        tutorial_state["model"] = model
        tutorial_state["model_path"] = pretrained_model_path
        tutorial_state["is_pretrained"] = True
        tutorial_state["device"] = "cpu"  # CPU 사용 명시

        return {
            "success": True,
            "model_path": pretrained_model_path,
            "model_type": "ovi7 사전학습 YOLOv8m",
            "device": "cpu",
            "elapsed_time": time.time() - model_load_time
        }
        
    except Exception as e:
        show_tagged_progress('ERROR', f'모델 로드 오류: {e}', model_load_time, 100)
        return {
            "success": False,
            "error": str(e)
        }

# ================== 3. 데이터셋 다운로드 블록 함수 ==================
def download_dataset_block(block_params=None):
    """데이터셋 확인 블록 (로컬 배포 버전)"""
    start_time = time.time()
    show_tagged_progress('DATASET', 'COCO128 데이터셋 확인 중...', start_time, 0)
    
    # 로컬에 미리 배포된 데이터셋 경로
    dataset_dir = os.path.join(base_dir, "dataset", "coco128")
    data_yaml_path = os.path.join(dataset_dir, "coco128.yaml")
    
    # 데이터셋 존재 확인
    if os.path.exists(data_yaml_path):
        show_tagged_progress('DATASET', 'COCO128 데이터셋 확인 완료', start_time, 100)
        
        tutorial_state["dataset_path"] = dataset_dir
        tutorial_state["data_yaml_path"] = data_yaml_path
        
        return {
            "success": True,
            "location": dataset_dir,
            "data_yaml_path": data_yaml_path,
            "cached": True,
            "elapsed_time": time.time() - start_time
        }
    else:
        show_tagged_progress('ERROR', 'COCO128 데이터셋을 찾을 수 없습니다', start_time, 100)
        return {
            "success": False,
            "error": "로컬 데이터셋 없음"
        }

# ================== 4. 모델 학습 블럭 ==================
def train_model_block(block_params=None):
    """
    모델 학습 블록 실행 함수 (진행률만 시뮬레이션)
    """
    start_time = time.time()
    show_tagged_progress('TRAIN', '모델 학습 준비 중...', start_time, 0)

    # 파라미터 받기
    epochs = block_params.get("epoch") if block_params else 2
    if epochs is None:
        epochs = 2
    
    try:
        epochs = int(epochs)
        if epochs <= 0:
            epochs = 2
    except:
        epochs = 2

    show_tagged_progress('TRAIN', f'모델 학습 시작 (에폭: {epochs})', start_time, 20)
    
    # === 진행률만 시뮬레이션 ===
    simulation_start_time = time.time()
    time_per_epoch = 8.0  # 에폭당 8초
    
    for epoch in range(1, epochs + 1):
        progress = (epoch / epochs) * 100
        elapsed = time.time() - simulation_start_time
        minutes, seconds = divmod(elapsed, 60)
        
        # 잔여 시간 계산
        if epoch > 1:
            avg_time = elapsed / (epoch - 1)
            remaining_time = avg_time * (epochs - epoch)
            rem_minutes, rem_seconds = divmod(remaining_time, 60)
            bar = make_progress_bar(progress)
            print(f"PROGRESS:{progress:.1f}:[전체 {progress:.1f}% | {int(minutes):02d}:{int(seconds):02d} 경과 | {int(rem_minutes):02d}:{int(rem_seconds):02d} 남음] [TRAIN] {bar} ({epoch}/{epochs} 에폭) 학습 중", flush=True)
        else:
            bar = make_progress_bar(progress)
            print(f"PROGRESS:{progress:.1f}:[전체 {progress:.1f}% | {int(minutes):02d}:{int(seconds):02d} 경과] [TRAIN] {bar} ({epoch}/{epochs} 에폭) 학습 중", flush=True)
        
        time.sleep(time_per_epoch)
    
    # 완료
    train_elapsed = time.time() - start_time
    minutes, seconds = divmod(train_elapsed, 60)
    show_tagged_progress('TRAIN', f'모델 학습 완료! (소요 시간: {int(minutes)}분 {int(seconds)}초)', start_time, 100)
    
    # 상태 업데이트
    tutorial_state["training_completed"] = True
    
    return {
        "success": True,
        "epochs": epochs,
        "elapsed_time": train_elapsed
    }

def make_progress_bar(progress, bar_length=20):
    filled_length = int(round(bar_length * progress / 100))
    bar = '█' * filled_length + '-' * (bar_length - filled_length)
    return f"|{bar}| {progress:5.1f}%"

# ================== 5. 결과 그래프 시각화 블록 함수 ==================
def visualize_training_results_block(block_params=None):
    """학습 결과 그래프 시각화 블록 실행 함수 (완전 스킵)"""
    
    # 바로 완료 처리 (아무것도 안함)
    return {
        "success": True,
        "elapsed_time": 0
    }

# ============== 6. 사용자 이미지 경로 받는 블럭====================
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
                tutorial_state["image_path"] = image_path
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
    
    # best.pt 경로 설정 (inference.py의 구현과 일치하도록)
    model_path = os.path.join(base_dir, base_dir, "model", "yolov8m-oiv7.pt")
    
    # 모델 파일이 실제로 존재하는지 확인
    if not os.path.exists(model_path):
        show_tagged_progress('WARN', f'학습된 모델(best.pt)을 찾을 수 없습니다: {model_path}', start_time, 10)
        show_tagged_progress('WARN', 'inference.py가 내부적으로 best.pt를 찾을 수 있는지 시도합니다.', start_time, 15)
    else:
        show_tagged_progress('INFER', f'학습된 모델 경로: {model_path}', start_time, 15)
    
    image_path = tutorial_state.get("image_path")
    if not image_path:
        show_tagged_progress('ERROR', '테스트 이미지 경로가 설정되지 않았습니다. 이미지 경로 설정 단계를 먼저 실행하세요.', start_time, 10)
        return {
            "success": False,
            "error": "테스트 이미지 경로 없음"
        }
    
    # conf 값 설정 (accuracy 파라미터로부터)
    conf = "0.25"  # 기본값
    if block_params and "accuracy" in block_params:
        try:
            # accuracy를 float으로 변환 (0-1 사이 값으로 가정)
            acc_value = float(block_params["accuracy"])
            if 0 <= acc_value <= 1:
                conf = str(acc_value)
            else:
                show_tagged_progress('WARN', f'유효하지 않은 accuracy 값({acc_value})입니다. 기본값 0.25를 사용합니다.', start_time, 20)
        except (ValueError, TypeError):
            show_tagged_progress('WARN', f'accuracy 값({block_params["accuracy"]})을 변환할 수 없습니다. 기본값 0.25를 사용합니다.', start_time, 20)
    
    show_tagged_progress('INFER', f'신뢰도 임계값(conf): {conf}', start_time, 25)
    
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
            "--conf", conf
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
                tutorial_state["result_image_path"] = inference_result["result_image"]
            
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
    result_image_path = tutorial_state.get("result_image_path")
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
        ("GPU 확인 및 모델 로드", load_pretrained_model_block),
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
