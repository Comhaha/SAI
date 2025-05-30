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

# 기본 설정
base_dir = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
dataset_dir = os.path.join(base_dir, "dataset", "tutorial_dataset")
runs_dir = os.path.join(base_dir, "runs")

def log_progress(tag, message, progress=None):
    """진행률과 메시지를 출력"""
    if progress is not None:
        print(f"PROGRESS:{progress}:[{tag}] {message}", flush=True)
    else:
        # progress가 없어도 PROGRESS 형태로 출력하여 클라이언트에서 캐치할 수 있도록 함
        # 마지막 진행률을 유지하거나 기본값 사용
        print(f"PROGRESS:0:[{tag}] {message}", flush=True)

def check_dataset():
    """로컬 데이터셋 존재 확인 및 경로 수정"""
    yaml_path = os.path.join(dataset_dir, "data.yaml")
    
    if os.path.exists(yaml_path):
        log_progress("INFO", "바나나 데이터셋 확인 완료", 3)
        
        # data.yaml 파일 내용을 Docker 환경에 맞게 수정
        try:
            with open(yaml_path, 'r') as f:
                content = f.read()
            
            # 상대 경로를 절대 경로로 변경
            content = content.replace('../train/images', f'{dataset_dir}/train/images')
            content = content.replace('../valid/images', f'{dataset_dir}/valid/images') 
            content = content.replace('../test/images', f'{dataset_dir}/test/images')
            
            with open(yaml_path, 'w') as f:
                f.write(content)
                
            log_progress("INFO", "데이터셋 경로 수정 완료", 5)
            return True
        except Exception as e:
            log_progress("ERROR", f"데이터셋 경로 수정 실패: {str(e)}", 5)
            return False
    else:
        log_progress("ERROR", f"바나나 데이터셋을 찾을 수 없습니다: {yaml_path}", 2)
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
    try:
        log_progress("TRAIN", "YOLO 모델 학습 시작", 0)
        
        # 데이터셋 확인
        if not check_dataset():
            return {"success": False, "error": "데이터셋 준비 실패"}
        
        # YOLO 임포트
        from ultralytics import YOLO
        
        # 모델 로드
        model_file = f"yolov8{model_type}.pt"
        model_path = os.path.join(base_dir, model_file)
        
        if not os.path.exists(model_path):
            log_progress("ERROR", f"모델 파일을 찾을 수 없습니다: {model_path}")
            return {"success": False, "error": f"모델 파일 없음: {model_path}"}
        
        log_progress("TRAIN", f"YOLOv8{model_type} 모델 로드 중...", 10)
        model = YOLO(model_path)
        
        # 데이터셋 YAML 파일 확인
        yaml_path = os.path.join(dataset_dir, "data.yaml")
        if not os.path.exists(yaml_path):
            log_progress("ERROR", f"데이터셋 YAML 파일을 찾을 수 없습니다: {yaml_path}")
            return {"success": False, "error": f"데이터셋 YAML 파일 없음: {yaml_path}"}
        
        log_progress("TRAIN", f"데이터셋 확인 완료: {yaml_path}", 20)
        
        # GPU 확인
        import torch
        device = "0" if torch.cuda.is_available() else "cpu"
        log_progress("TRAIN", f"학습 디바이스: {device}", 25)
        
        # 학습 시작
        log_progress("TRAIN", f"모델 학습 시작 (에폭: {epochs}, 이미지 크기: {imgsz})", 30)
        
        # 학습 실행 (콜백 없이 단순하게)
        results = model.train(
            data=yaml_path,
            epochs=epochs,
            imgsz=imgsz,
            device=device,
            project=runs_dir,
            name="detect/train",
            exist_ok=True,
            verbose=False,
            # 성능 최적화 파라미터 (공유 메모리 문제 해결)
            workers=0,    # 워커 비활성화 (공유 메모리 문제 해결)
            batch=8,      # 배치 크기 감소 (속도 향상)
            cache='disk', # 디스크 캐싱 사용 (메모리 절약)
            amp=True,     # Mixed precision 활성화
            patience=10   # Early stopping patience 감소
        )
        
        log_progress("TRAIN", "모델 학습 완료", 90)
        
        # 결과 저장 경로
        weights_dir = os.path.join(runs_dir, "detect", "train", "weights")
        best_model_path = os.path.join(weights_dir, "best.pt")
        
        log_progress("DEBUG", f"가중치 디렉토리 확인: {weights_dir}", 91)
        log_progress("DEBUG", f"모델 파일 경로: {best_model_path}", 91)
        log_progress("DEBUG", f"모델 파일 존재 여부: {os.path.exists(best_model_path)}", 91)
        
        if os.path.exists(best_model_path):
            log_progress("TRAIN", f"학습된 모델 저장: {best_model_path}", 95)
            
            # CSV 결과 파일 확인 및 처리
            csv_path = os.path.join(runs_dir, "detect", "train", "results.csv")
            csv_base64 = None
            
            log_progress("DEBUG", f"CSV 파일 경로: {csv_path}", 95)
            log_progress("DEBUG", f"CSV 파일 존재 여부: {os.path.exists(csv_path)}", 95)
            
            if os.path.exists(csv_path):
                import base64
                with open(csv_path, 'rb') as f:
                    csv_base64 = base64.b64encode(f.read()).decode('utf-8')
                log_progress("TRAIN", "학습 결과 CSV 파일 처리 완료", 96)
            else:
                log_progress("WARNING", "CSV 파일을 찾을 수 없음", 96)
            
            # 결과 그래프 이미지 처리
            results_img_path = os.path.join(runs_dir, "detect", "train", "results.png")
            results_img_base64 = None
            
            log_progress("DEBUG", f"이미지 파일 경로: {results_img_path}", 96)
            log_progress("DEBUG", f"이미지 파일 존재 여부: {os.path.exists(results_img_path)}", 96)
            
            if os.path.exists(results_img_path):
                import base64
                with open(results_img_path, 'rb') as f:
                    results_img_base64 = base64.b64encode(f.read()).decode('utf-8')
                log_progress("TRAIN", "결과 그래프 이미지 처리 완료", 97)
            else:
                log_progress("WARNING", "이미지 파일을 찾을 수 없음", 97)
            
            # 학습된 모델 파일을 base64로 인코딩
            model_base64 = None
            if os.path.exists(best_model_path):
                log_progress("DEBUG", "모델 파일 base64 인코딩 시작", 97)
                with open(best_model_path, 'rb') as f:
                    model_base64 = base64.b64encode(f.read()).decode('utf-8')
                log_progress("TRAIN", "학습된 모델 파일 인코딩 완료", 98)
            else:
                log_progress("WARNING", "모델 파일을 다시 찾을 수 없음", 98)
            
            log_progress("TRAIN", "모든 학습 과정 완료", 100)
            
            log_progress("DEBUG", "결과 딕셔너리 생성 시작", 100)
            result = {
                "success": True,
                "model_path": best_model_path,
                "csv_path": csv_path,
                "csv_base64": csv_base64,
                "results_img_base64": results_img_base64,
                "model_base64": model_base64,
                "epochs": epochs,
                "device": device,
                "final_metrics": {
                    "precision": getattr(results, 'results_dict', {}).get('metrics/precision(B)', 0),
                    "recall": getattr(results, 'results_dict', {}).get('metrics/recall(B)', 0),
                    "mAP50": getattr(results, 'results_dict', {}).get('metrics/mAP50(B)', 0),
                    "mAP50_95": getattr(results, 'results_dict', {}).get('metrics/mAP50-95(B)', 0)
                }
            }
            log_progress("DEBUG", "결과 딕셔너리 생성 완료", 100)
            
            log_progress("DEBUG", "TRAINING_RESULT 출력 시작", 100)
            print(f"TRAINING_RESULT:{json.dumps(result, ensure_ascii=False)}")
            log_progress("DEBUG", "TRAINING_RESULT 출력 완료", 100)
            return result
        else:
            error_msg = f"학습된 모델 파일을 찾을 수 없습니다: {best_model_path}"
            log_progress("ERROR", error_msg)
            return {"success": False, "error": error_msg}
            
    except Exception as e:
        error_msg = f"학습 중 오류 발생: {str(e)}"
        log_progress("ERROR", error_msg)
        return {"success": False, "error": error_msg}

def main():
    """메인 함수"""
    parser = argparse.ArgumentParser(description='서버용 YOLO 모델 학습')
    parser.add_argument('--epochs', type=int, default=10, help='학습 에폭 수')
    parser.add_argument('--imgsz', type=int, default=640, help='이미지 크기')
    parser.add_argument('--conf', type=float, default=0.25, help='신뢰도 임계값')
    parser.add_argument('--model', type=str, default='n', choices=['n', 's', 'm', 'l'], help='모델 타입')
    
    args = parser.parse_args()
    
    log_progress("INFO", "서버용 YOLO 학습 스크립트 시작")
    log_progress("INFO", f"학습 파라미터: epochs={args.epochs}, imgsz={args.imgsz}, conf={args.conf}, model={args.model}")
    
    # 학습 실행
    result = train_model(
        epochs=args.epochs,
        imgsz=args.imgsz,
        conf=args.conf,
        model_type=args.model
    )
    
    # 종료 코드 설정
    sys.exit(0 if result["success"] else 1)

if __name__ == "__main__":
    main() 