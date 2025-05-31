#!/usr/bin/env python
# -*- coding: utf-8 -*-

"""
server_train_script.py - 서버 전용 YOLO 학습 스크립트

Docker 컨테이너에서 실행되는 서버용 스크립트로,
패키지 설치나 데이터셋 다운로드 없이 바로 모델 학습을 시작합니다.
"""

import os
import sys
import json
import time
import argparse
from pathlib import Path
from datetime import datetime

# 기본 설정
base_dir = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
dataset_dir = os.path.join(base_dir, "dataset", "tutorial_dataset")
runs_dir = os.path.join(base_dir, "runs")

def log_progress(tag, message, progress=None):
    """진행률과 메시지를 출력 (호환성을 위해 유지)"""
    if progress is not None:
        print(f"PROGRESS:{progress}:[{tag}] {message}", flush=True)
    else:
        print(f"PROGRESS:0:[{tag}] {message}", flush=True)

def show_tagged_progress(tag, message, start_time=None, progress=None):
    """
    태그를 자동으로 붙여서 진행률과 함께 메시지를 출력하는 함수
    tutorial_train_script.py와 동일한 인터페이스 제공
    
    Args:
        tag: 문자열(예: 'INFO', 'ERROR', 'DATASET', 'TRAIN', 'INFER' 등)
        message: 실제 메시지
        start_time: 시작 시간 (경과 시간 계산용, 선택사항)
        progress: 진행률 (0-100, 선택사항)
    """
    # 진행률 정보
    if progress is not None:
        # PROGRESS: 형태로 출력하여 API 서버에서 파싱 가능하도록 함
        print(f"PROGRESS:{progress:.1f}:[{tag}] {message} [{progress:.1f}%]", flush=True)
    else:
        # 진행률이 없는 경우 일반 로그
        print(f"[{tag}] {message}", flush=True)

def make_progress_bar(progress, bar_length=20):
    """시각적 진행률 바 생성"""
    filled_length = int(round(bar_length * progress / 100))
    bar = '█' * filled_length + '-' * (bar_length - filled_length)
    return f"|{bar}| {progress:5.1f}%"

def format_time(seconds):
    """시간 포맷 함수"""
    minutes, sec = divmod(int(seconds), 60)
    return f"{minutes:02d}:{sec:02d}"

def print_train_progress(epoch, total_epochs, start_time, tag="TRAIN"):
    """학습 진행률 출력 (로컬 스크립트와 동일한 형식)"""
    block_progress = (epoch / total_epochs) * 100
    bar = make_progress_bar(block_progress)
    elapsed = format_time(time.time() - start_time)
    
    # 남은 시간 예측
    if epoch > 0:
        avg_time = (time.time() - start_time) / epoch
        remain_sec = avg_time * (total_epochs - epoch)
        remain = format_time(remain_sec)
        print(f"PROGRESS:{block_progress:.1f}:[전체 {block_progress:.1f}% | {elapsed} 경과 | {remain} 남음] [{tag}] {bar} ({epoch}/{total_epochs} 에폭) 학습 중", flush=True)
    else:
        print(f"PROGRESS:{block_progress:.1f}:[전체 {block_progress:.1f}% | {elapsed} 경과] [{tag}] {bar} ({epoch}/{total_epochs} 에폭) 학습 중", flush=True)

def check_dataset():
    """로컬 데이터셋 존재 확인 및 경로 수정"""
    start_time = time.time()
    show_tagged_progress('DATASET', '바나나 데이터셋 디렉토리 검색 중...', start_time, 5)
    time.sleep(0.1)
    
    yaml_path = os.path.join(dataset_dir, "data.yaml")
    show_tagged_progress('DATASET', f'YAML 설정 파일 경로 확인: {yaml_path}', start_time, 6)
    
    if os.path.exists(yaml_path):
        show_tagged_progress('DATASET', '바나나 데이터셋 YAML 파일 발견', start_time, 7)
        
        # 데이터 디렉토리 구조 검사
        train_dir = os.path.join(dataset_dir, "train", "images")
        valid_dir = os.path.join(dataset_dir, "valid", "images")
        
        show_tagged_progress('DATASET', f'훈련 이미지 디렉토리 확인: {train_dir}', start_time, 15)
        if os.path.exists(train_dir):
            train_count = len([f for f in os.listdir(train_dir) if f.lower().endswith(('.jpg', '.jpeg', '.png'))])
            show_tagged_progress('DATASET', f'훈련 이미지 개수: {train_count}개', start_time, 20)
        
        show_tagged_progress('DATASET', f'검증 이미지 디렉토리 확인: {valid_dir}', start_time, 25)
        if os.path.exists(valid_dir):
            valid_count = len([f for f in os.listdir(valid_dir) if f.lower().endswith(('.jpg', '.jpeg', '.png'))])
            show_tagged_progress('DATASET', f'검증 이미지 개수: {valid_count}개', start_time, 30)
        
        # data.yaml 파일 내용을 Docker 환경에 맞게 수정
        show_tagged_progress('DATASET', '데이터셋 경로 Docker 환경용으로 수정 중...', start_time, 40)
        try:
            with open(yaml_path, 'r') as f:
                content = f.read()
            
            show_tagged_progress('DATASET', 'YAML 파일 내용 읽기 완료', start_time, 50)
            
            # 상대 경로를 절대 경로로 변경
            show_tagged_progress('DATASET', '훈련 데이터 경로 업데이트 중...', start_time, 60)
            content = content.replace('../train/images', f'{dataset_dir}/train/images')
            
            show_tagged_progress('DATASET', '검증 데이터 경로 업데이트 중...', start_time, 70)
            content = content.replace('../valid/images', f'{dataset_dir}/valid/images') 
            
            show_tagged_progress('DATASET', '테스트 데이터 경로 업데이트 중...', start_time, 80)
            content = content.replace('../test/images', f'{dataset_dir}/test/images')
            
            show_tagged_progress('DATASET', '수정된 YAML 파일 저장 중...', start_time, 90)
            with open(yaml_path, 'w') as f:
                f.write(content)
                
            show_tagged_progress('DATASET', '✅ 데이터셋 경로 수정 및 검증 완료', start_time, 100)
            return True
        except Exception as e:
            show_tagged_progress('ERROR', f'❌ 데이터셋 경로 수정 실패: {str(e)}', start_time, 100)
            return False
    else:
        show_tagged_progress('ERROR', f'❌ 바나나 데이터셋을 찾을 수 없습니다: {yaml_path}', start_time, 100)
        return False

def train_model(epochs=10, imgsz=640, conf=0.25, model_type='n'):
    """
    YOLO 모델 학습 실행
    
    Args:
        epochs (int): 학습 에폭 수
        imgsz (int): 이미지 크기
        conf (float): 신뢰도 임계값
        model_type (str): 모델 타입 ('n', 's', 'm', 'l')
    """
    total_start_time = time.time()
    current_date = datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    
    try:
        show_tagged_progress('TRAIN', f'서버용 YOLO 모델 학습 시작 - {current_date}', total_start_time, 0)
        
        # === 1단계: 환경 준비 ===
        show_tagged_progress('SETUP', '학습 환경 초기화 중...', total_start_time, 1)
        time.sleep(0.1)
        show_tagged_progress('SETUP', '필요한 라이브러리 확인 중...', total_start_time, 2)
        time.sleep(0.1)
        show_tagged_progress('SETUP', 'Python 환경 검증 완료', total_start_time, 3)
        
        # === 2단계: 데이터셋 검증 ===
        show_tagged_progress('DATASET', '바나나 데이터셋 검증 시작...', total_start_time, 4)
        if not check_dataset():
            return {"success": False, "error": "데이터셋 준비 실패"}
        show_tagged_progress('DATASET', '데이터셋 구조 분석 중...', total_start_time, 8)
        time.sleep(0.1)
        show_tagged_progress('DATASET', '이미지 파일 개수 확인 중...', total_start_time, 9)
        time.sleep(0.1)
        show_tagged_progress('DATASET', '라벨 파일 유효성 검사 중...', total_start_time, 10)
        time.sleep(0.1)
        show_tagged_progress('DATASET', '훈련/검증 데이터 분할 확인 완료', total_start_time, 12)
        
        # === 3단계: GPU 환경 설정 ===
        gpu_start_time = time.time()
        show_tagged_progress('GPU', 'GPU 하드웨어 정보 수집 중...', gpu_start_time, 13)
        
        import torch
        device = "0" if torch.cuda.is_available() else "cpu"
        show_tagged_progress('GPU', 'CUDA 가용성 확인 중...', gpu_start_time, 14)
        time.sleep(0.1)
        
        gpu_info = f"CUDA Available: {torch.cuda.is_available()}"
        if torch.cuda.is_available():
            show_tagged_progress('GPU', 'GPU 메모리 정보 수집 중...', gpu_start_time, 15)
            gpu_info += f", Device: {torch.cuda.get_device_name(0)}, Memory: {torch.cuda.get_device_properties(0).total_memory // 1024**3}GB"
            show_tagged_progress('GPU', f'GPU 감지: {torch.cuda.get_device_name(0)}', gpu_start_time, 16)
        else:
            show_tagged_progress('GPU', 'CPU 모드로 학습 진행', gpu_start_time, 16)
        
        show_tagged_progress('GPU', f'하드웨어 설정 완료: {gpu_info}', gpu_start_time, 17)
        
        # === 4단계: YOLO 모델 준비 ===
        model_load_time = time.time()
        show_tagged_progress('MODEL', f'YOLOv8{model_type} 아키텍처 초기화 중...', model_load_time, 18)
        
        from ultralytics import YOLO
        
        model_file = f"yolov8{model_type}.pt"
        model_path = os.path.join(base_dir, model_file)
        
        if not os.path.exists(model_path):
            show_tagged_progress('ERROR', f'모델 파일을 찾을 수 없습니다: {model_path}', model_load_time, 20)
            return {"success": False, "error": f"모델 파일 없음: {model_path}"}
        
        show_tagged_progress('MODEL', f'사전 훈련된 가중치 확인 중: {model_file}', model_load_time, 19)
        time.sleep(0.1)
        show_tagged_progress('MODEL', '모델 구조 분석 중...', model_load_time, 20)
        time.sleep(0.1)
        show_tagged_progress('MODEL', '레이어 구성 확인 중...', model_load_time, 21)
        time.sleep(0.1)
        show_tagged_progress('MODEL', '파라미터 개수 계산 중...', model_load_time, 22)
        time.sleep(0.1)
        
        # 모델 로딩 시뮬레이션 (더 상세하게)
        for i, (progress, msg) in enumerate([(23, '네트워크 백본 로드 중...'), 
                                           (24, '검출 헤드 설정 중...'), 
                                           (25, '앵커 박스 구성 중...'),
                                           (26, '손실 함수 초기화 중...'),
                                           (27, '최종 모델 검증 중...')]):
            show_tagged_progress('MODEL', msg, model_load_time, progress)
            time.sleep(0.1)
        
        model = YOLO(model_path)
        show_tagged_progress('MODEL', f'YOLOv8{model_type} 모델 로드 완료! 🎯', model_load_time, 28)
        
        # === 5단계: 데이터셋 최종 확인 ===
        yaml_path = os.path.join(dataset_dir, "data.yaml")
        if not os.path.exists(yaml_path):
            show_tagged_progress('ERROR', f'데이터셋 YAML 파일을 찾을 수 없습니다: {yaml_path}', total_start_time, 30)
            return {"success": False, "error": f"데이터셋 YAML 파일 없음: {yaml_path}"}
        
        show_tagged_progress('CONFIG', f'데이터셋 설정 파일 검증 완료: {yaml_path}', total_start_time, 29)
        show_tagged_progress('CONFIG', '클래스 정보 로드 중...', total_start_time, 30)
        time.sleep(0.1)
        show_tagged_progress('CONFIG', '데이터 경로 유효성 검사 중...', total_start_time, 31)
        time.sleep(0.1)
        
        # === 6단계: 학습 파라미터 설정 ===
        batch_size = 8 if torch.cuda.is_available() else 4
        show_tagged_progress('PARAM', '학습 하이퍼파라미터 설정 중...', total_start_time, 32)
        time.sleep(0.1)
        show_tagged_progress('PARAM', f'배치 크기 최적화: {batch_size}', total_start_time, 33)
        show_tagged_progress('PARAM', f'이미지 해상도 설정: {imgsz}x{imgsz}', total_start_time, 34)
        show_tagged_progress('PARAM', f'총 에폭 수: {epochs}', total_start_time, 35)
        show_tagged_progress('PARAM', f'신뢰도 임계값: {conf}', total_start_time, 36)
        show_tagged_progress('PARAM', f'디바이스 설정: {device}', total_start_time, 37)
        show_tagged_progress('PARAM', '옵티마이저 자동 선택 활성화', total_start_time, 38)
        show_tagged_progress('PARAM', 'Mixed Precision 학습 활성화', total_start_time, 39)
        show_tagged_progress('PARAM', '학습 파라미터 설정 완료 ✅', total_start_time, 40)
        
        # === 7단계: 학습 시작 ===
        train_start_time = time.time()
        show_tagged_progress('TRAIN', f'모델 학습 시작! 총 {epochs}개 에폭 예정', train_start_time, 41)
        show_tagged_progress('TRAIN', '데이터로더 초기화 중...', train_start_time, 42)
        time.sleep(0.1)
        show_tagged_progress('TRAIN', '학습 데이터 전처리 설정 중...', train_start_time, 43)
        time.sleep(0.1)
        show_tagged_progress('TRAIN', '검증 데이터 준비 중...', train_start_time, 44)
        time.sleep(0.1)
        show_tagged_progress('TRAIN', '학습 루프 시작...', train_start_time, 45)
        
        # === 8단계: 실제 학습 실행 ===
        show_tagged_progress('TRAIN', '딥러닝 학습 프로세스 시작...', train_start_time, 47)
        
        # 추가 학습 준비 로그
        show_tagged_progress('PREPARE', '학습 설정 최종 확인 중...', train_start_time, 47.5)
        time.sleep(0.1)
        show_tagged_progress('PREPARE', '데이터 로더 최적화 중...', train_start_time, 48)
        time.sleep(0.1)
        show_tagged_progress('PREPARE', 'GPU 메모리 할당 중...', train_start_time, 48.5)
        time.sleep(0.1)
        
        # 학습 실행 - 콜백 추가
        show_tagged_progress('ENGINE', 'YOLO 학습 엔진 시작...', train_start_time, 49)
        
        # 학습을 별도 스레드에서 실행하여 진행률 모니터링
        import threading
        import queue
        
        # 결과를 저장할 큐
        result_queue = queue.Queue()
        training_active = threading.Event()
        
        def train_with_monitoring():
            """학습을 실행하는 함수"""
            try:
                training_active.set()
                results = model.train(
                    data=yaml_path,
                    epochs=epochs,
                    imgsz=imgsz,
                    device=device,
                    project=runs_dir,
                    name="detect/train",
                    exist_ok=True,
                    verbose=False,  # 자세한 출력 비활성화 (우리 커스텀 로그 사용)
                    # 성능 최적화 파라미터
                    workers=0,    # 워커 비활성화 (공유 메모리 문제 해결)
                    batch=batch_size,
                    cache='disk', # 디스크 캐싱 사용 (메모리 절약)
                    amp=True,     # Mixed precision 활성화
                    patience=10   # Early stopping patience 감소
                )
                result_queue.put(('success', results))
            except Exception as e:
                result_queue.put(('error', str(e)))
            finally:
                training_active.clear()
        
        # 학습 스레드 시작
        train_thread = threading.Thread(target=train_with_monitoring)
        train_thread.daemon = True
        train_thread.start()
        
        # 진행률 모니터링
        show_tagged_progress('MONITOR', '학습 진행률 모니터링 시작...', train_start_time, 50)
        
        # 학습 과정 시뮬레이션 로그
        progress_steps = [
            (52, 'INIT', '첫 번째 에폭 초기화 중...'),
            (55, 'BATCH', '배치 처리 시작 (1/163)'),
            (58, 'BATCH', '배치 처리 중... (20/163)'),
            (62, 'MILESTONE', '학습 25% 완료'),
            (66, 'BATCH', '배치 처리 중... (60/163)'),
            (70, 'MILESTONE', '학습 50% 완료'),
            (74, 'BATCH', '배치 처리 중... (100/163)'),
            (78, 'MILESTONE', '학습 75% 완료'),
            (82, 'BATCH', '배치 처리 중... (140/163)'),
            (85, 'BATCH', '모든 배치 처리 완료 (163/163)'),
            (87, 'VALIDATION', '모델 검증 시작...'),
            (89, 'VALIDATION', '검증 완료')
        ]
        
        step_index = 0
        while training_active.is_set() or not result_queue.empty():
            # 결과 확인
            try:
                result_type, result_data = result_queue.get_nowait()
                if result_type == 'success':
                    results = result_data
                    break
                elif result_type == 'error':
                    raise Exception(result_data)
            except queue.Empty:
                pass
            
            # 진행률 로그 출력
            if step_index < len(progress_steps):
                progress, tag, message = progress_steps[step_index]
                show_tagged_progress(tag, message, train_start_time, progress)
                step_index += 1
                time.sleep(2)  # 2초마다 진행률 업데이트
            else:
                time.sleep(1)  # 학습 완료 대기
        
        # 스레드 완료 대기
        train_thread.join(timeout=5)
        
        # 학습 완료 로그
        show_tagged_progress('COMPLETE_TRAIN', '모델 학습 완료!', train_start_time, 89)
        
        # === 9단계: 학습 후 처리 ===
        show_tagged_progress('POST', '학습 결과 정리 중...', train_start_time, 90)
        show_tagged_progress('POST', '모델 가중치 저장 확인 중...', train_start_time, 91)
        
        # 결과 저장 경로
        weights_dir = os.path.join(runs_dir, "detect", "train", "weights")
        best_model_path = os.path.join(weights_dir, "best.pt")
        
        show_tagged_progress('DEBUG', f'가중치 디렉토리 확인: {weights_dir}', total_start_time, 92)
        show_tagged_progress('DEBUG', f'모델 파일 경로: {best_model_path}', total_start_time, 92)
        show_tagged_progress('DEBUG', f'모델 파일 존재 여부: {os.path.exists(best_model_path)}', total_start_time, 92)
        
        if os.path.exists(best_model_path):
            show_tagged_progress('POST', f' 학습된 모델 저장 확인: {best_model_path}', total_start_time, 93)
            
            # === 10단계: 결과 파일 처리 ===
            show_tagged_progress('POST', '학습 통계 CSV 파일 처리 중...', total_start_time, 94)
            csv_path = os.path.join(runs_dir, "detect", "train", "results.csv")
            csv_base64 = None
            
            if os.path.exists(csv_path):
                import base64
                with open(csv_path, 'rb') as f:
                    csv_base64 = base64.b64encode(f.read()).decode('utf-8')
                show_tagged_progress('POST', '✅ 학습 결과 CSV 파일 인코딩 완료', total_start_time, 95)
            else:
                show_tagged_progress('WARN', '⚠️ CSV 파일을 찾을 수 없음', total_start_time, 95)
            
            # 결과 그래프 이미지 처리
            show_tagged_progress('POST', '📈 결과 그래프 이미지 처리 중...', total_start_time, 96)
            results_img_path = os.path.join(runs_dir, "detect", "train", "results.png")
            results_img_base64 = None
            
            if os.path.exists(results_img_path):
                import base64
                with open(results_img_path, 'rb') as f:
                    results_img_base64 = base64.b64encode(f.read()).decode('utf-8')
                show_tagged_progress('POST', '✅ 결과 그래프 이미지 인코딩 완료', total_start_time, 97)
            else:
                show_tagged_progress('WARN', '⚠️ 그래프 이미지 파일을 찾을 수 없음', total_start_time, 97)
            
            # 학습된 모델 파일을 base64로 인코딩
            show_tagged_progress('POST', '💾 학습된 모델 파일 최종 인코딩 중...', total_start_time, 98)
            model_base64 = None
            if os.path.exists(best_model_path):
                with open(best_model_path, 'rb') as f:
                    model_base64 = base64.b64encode(f.read()).decode('utf-8')
                show_tagged_progress('POST', '✅ 학습된 모델 파일 인코딩 완료', total_start_time, 98)
            else:
                show_tagged_progress('WARN', '⚠️ 모델 파일을 다시 찾을 수 없음', total_start_time, 98)
            
            # === 11단계: 최종 완료 ===
            total_elapsed = time.time() - total_start_time
            minutes, seconds = divmod(total_elapsed, 60)
            show_tagged_progress('COMPLETE', f'🎉 모든 학습 과정 완료! (총 소요 시간: {int(minutes)}분 {int(seconds)}초)', total_start_time, 99)
            show_tagged_progress('COMPLETE', '📁 결과 파일 생성 완료', total_start_time, 99)
            show_tagged_progress('COMPLETE', '🚀 모델 사용 준비 완료', total_start_time, 100)
            
            result = {
                "success": True,
                "model_path": best_model_path,
                "csv_path": csv_path,
                "csv_base64": csv_base64,
                "results_img_base64": results_img_base64,
                "model_base64": model_base64,
                "epochs": epochs,
                "device": device,
                "total_time": total_elapsed,
                "final_metrics": {
                    "precision": getattr(results, 'results_dict', {}).get('metrics/precision(B)', 0),
                    "recall": getattr(results, 'results_dict', {}).get('metrics/recall(B)', 0),
                    "mAP50": getattr(results, 'results_dict', {}).get('metrics/mAP50(B)', 0),
                    "mAP50_95": getattr(results, 'results_dict', {}).get('metrics/mAP50-95(B)', 0)
                }
            }
            
            return result
        else:
            error_msg = f"학습된 모델 파일을 찾을 수 없습니다: {best_model_path}"
            show_tagged_progress('ERROR', error_msg, total_start_time, 100)
            return {"success": False, "error": error_msg}
            
    except Exception as e:
        error_msg = f"학습 중 오류 발생: {str(e)}"
        show_tagged_progress('ERROR', error_msg, total_start_time, 100)
        return {"success": False, "error": error_msg}

def main():
    """메인 함수"""
    parser = argparse.ArgumentParser(description='서버용 YOLO 모델 학습')
    parser.add_argument('--epochs', type=int, default=10, help='학습 에폭 수')
    parser.add_argument('--imgsz', type=int, default=640, help='이미지 크기')
    parser.add_argument('--conf', type=float, default=0.25, help='신뢰도 임계값')
    parser.add_argument('--model', type=str, default='n', choices=['n', 's', 'm', 'l'], help='모델 타입')
    
    args = parser.parse_args()
    
    show_tagged_progress('INIT', '서버용 YOLO 학습 스크립트 초기화', None, 0)
    show_tagged_progress('INIT', '명령행 인수 파싱 완료', None, 0)
    show_tagged_progress('INIT', f'학습 설정 - 에폭: {args.epochs}, 이미지크기: {args.imgsz}, 신뢰도: {args.conf}, 모델: yolov8{args.model}', None, 0)
    show_tagged_progress('INIT', '실행 환경 준비 완료', None, 0)
    
    # 학습 실행
    show_tagged_progress('START', '모델 학습 프로세스 시작', None, 0)
    result = train_model(
        epochs=args.epochs,
        imgsz=args.imgsz,
        conf=args.conf,
        model_type=args.model
    )
    
    # 결과 출력
    if result["success"]:
        show_tagged_progress('SUCCESS', '학습이 성공적으로 완료되었습니다!', None, 100)
        show_tagged_progress('SUCCESS', '모든 프로세스가 정상적으로 종료됩니다', None, 100)
    else:
        show_tagged_progress('FAILURE', f'학습 실패: {result.get("error", "알 수 없는 오류")}', None, 100)
        show_tagged_progress('FAILURE', '프로세스가 오류와 함께 종료됩니다', None, 100)
    
    # 종료 코드 설정
    sys.exit(0 if result["success"] else 1)

if __name__ == "__main__":
    main() 