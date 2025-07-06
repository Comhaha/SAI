#!/usr/bin/env python
# -*- coding: utf-8 -*-

"""
inference.py - 단일 이미지 추론 스크립트

이 스크립트는 C# WinForms 애플리케이션에서 호출하여 YOLOv8 모델로
단일 이미지 객체 탐지 추론을 실행하고 결과 이미지를 저장합니다.

필수 패키지: ultralytics, opencv-python (튜토리얼에서 미리 설치됨)

사용법:
    python inference.py --image path/to/image.jpg --conf 0.25
"""

import os
import sys
import json
import time
import argparse
import cv2
from pathlib import Path
import subprocess
import re
import uuid

def log_message(message):
    """로그 메시지를 출력하고 즉시 버퍼를 비웁니다."""
    print(f"[INFERENCE] {message}", flush=True)

# base_dir은 현재 스크립트 기준에서 Python 폴더
base_dir = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
log_message(f"Base directory: {base_dir}")

# model_dir은 YOLOv8 best.pt 파일이 있는 폴더
default_model_path = os.path.join(base_dir, "model", "yolov8m-oiv7.pt")
log_message(f"Default model path: {default_model_path}")


# 추론 함수
def run_inference(model_path, image_path, conf=0.25):
    """
    YOLOv8 모델을 사용하여 이미지 추론 실행
    """
    start_time = time.time()
    log_message(f"모델 로드 중: {model_path}")
    
    # 파일 존재 확인
    if not os.path.exists(model_path):
        error_msg = f"오류: 모델 파일을 찾을 수 없습니다: {model_path}"
        log_message(error_msg)
        return {"success": False, "error": error_msg}
    
    if not os.path.exists(image_path):
        error_msg = f"오류: 이미지 파일을 찾을 수 없습니다: {image_path}"
        log_message(error_msg)
        return {"success": False, "error": error_msg}
    
    # 결과 저장 디렉토리 설정
    result_dir = os.path.join(base_dir, "runs", "result")
    os.makedirs(result_dir, exist_ok=True)
    
    # ✅ 안전한 이전 결과 파일 삭제
    try:
        import time as time_module
        log_message("이전 결과 파일 정리 시작...")
        
        deleted_count = 0
        for existing_file in os.listdir(result_dir):
            if existing_file.lower().endswith(('.png', '.jpg', '.jpeg', '.bmp', '.gif')):
                old_file_path = os.path.join(result_dir, existing_file)
                try:
                    # 파일이 사용 중인지 확인하고 안전하게 삭제
                    os.remove(old_file_path)
                    deleted_count += 1
                    log_message(f"삭제됨: {existing_file}")
                except PermissionError:
                    log_message(f"삭제 실패 (사용 중): {existing_file}")
                except Exception as file_error:
                    log_message(f"삭제 실패: {existing_file} - {file_error}")
        
        if deleted_count > 0:
            log_message(f"이전 결과 파일 {deleted_count}개 삭제 완료")
            # ✅ 삭제 후 잠시 대기 (파일 시스템 안정화)
            time_module.sleep(0.1)
        else:
            log_message("삭제할 이전 결과 파일이 없음")
            
    except Exception as e:
        log_message(f"이전 결과 파일 정리 중 오류 (무시하고 계속): {e}")
    
    log_message(f"결과 저장 디렉토리: {result_dir}")
    
    try:
        # YOLO 모델 로드 (삭제 작업과 분리)
        log_message("ultralytics 패키지 임포트 중...")
        from ultralytics import YOLO
        log_message("YOLO 모델 로드 중...")
        model = YOLO(model_path)
        log_message("YOLO 모델 로드 완료")
        
        # 이미지 파일명 추출 (확장자 제외)
        image_basename = os.path.basename(image_path)
        image_name, image_ext = os.path.splitext(image_basename)
        
        # 결과 이미지 경로 정의 - 한글 파일명 문제 해결
        # 한글이 포함된 경로는 C#에서 읽어오지 못하는 문제를 해결하기 위해 경로 처리
        original_name = image_name  # 원본 파일명 보존
        
        # 한글이 포함된 경로인지 확인
        if any(ord(char) > 127 for char in image_name):
            # 한글이 포함된 경우 고유한 영문 파일명 생성
            safe_name = f"result_{uuid.uuid4().hex[:8]}"
            log_message(f"한글 파일명 감지: {image_name} → {safe_name}로 변환")
        else:
            safe_name = image_name
            
        result_image_name = f"{safe_name}_result{image_ext}"
        result_image_path = os.path.join(result_dir, result_image_name)
        
        # 추론 실행 (save=True로 저장)
        log_message(f"이미지 추론 시작: {image_path}")
        log_message(f"신뢰도 임계값: {conf}")
        
        # 모델 추론 실행
        results = model.predict(
            source=image_path,
            conf=conf,
            save=False,  # 자체 저장 비활성화 (직접 저장)
            show=False
        )
        
        # 결과 이미지 직접 저장
        if results and len(results) > 0:
            # 첫 번째 결과에서 시각화 이미지 가져오기
            result_img = results[0].plot()
            
            # OpenCV로 이미지 저장
            cv2.imwrite(result_image_path, result_img)
            log_message(f"결과 이미지 저장 완료: {result_image_path}")

            time.sleep(0.2)  # 파일 시스템 안정화 대기
            
            # 감지된 객체 정보
            detections = []
            if hasattr(results[0], 'boxes') and results[0].boxes is not None:
                for box in results[0].boxes:
                    # 박스 좌표
                    x1, y1, x2, y2 = box.xyxy[0].tolist()
                    
                    # 클래스 및 신뢰도
                    cls = int(box.cls[0].item())
                    conf_val = float(box.conf[0].item())
                    
                    # 클래스 이름
                    cls_name = results[0].names[cls]
                    
                    detections.append({
                        "class": cls_name,
                        "confidence": conf_val,
                        "bbox": [x1, y1, x2, y2]
                    })
            
            log_message(f"감지된 객체 수: {len(detections)}")
        else:
            # 결과가 없는 경우 원본 이미지 복사
            log_message("객체가 감지되지 않았습니다. 원본 이미지를 복사합니다.")
            img = cv2.imread(image_path)
            cv2.imwrite(result_image_path, img)
            detections = []
        
        # 추론 시간 계산
        inference_time = time.time() - start_time
        log_message(f"추론 완료 (소요 시간: {inference_time:.3f}초)")
        
        # 결과 반환
        result = {
            "success": True,
            "result_image": result_image_path,
            "original_name": original_name,  # 원본 한글 파일명 추가
            "detections": detections,
            "inference_time": inference_time
        }
    except Exception as e:
        import traceback
        error_msg = f"추론 오류: {str(e)}"
        log_message(error_msg)
        log_message(traceback.format_exc())  # 상세 오류 추적
        result = {"success": False, "error": error_msg}
    finally:
        print(f"INFERENCE_RESULT:{json.dumps(result, ensure_ascii=True)}")
        return result


def main():
    """명령행에서 실행될 메인 함수"""
    log_message("스크립트 시작")
    
    parser = argparse.ArgumentParser(description='YOLOv8 이미지 추론 도구')
    
    # 필수 인자
    parser.add_argument('--image', required=True, help='추론할 이미지 파일 경로')
    
    # 선택적 인자
    parser.add_argument('--model', help='학습된 모델 파일 경로 (.pt)')
    parser.add_argument('--conf', type=float, default=0.25, help='객체 탐지 신뢰도 임계값 (기본값: 0.25)')
    
    args = parser.parse_args()
    log_message(f"받은 인자: image={args.image}, model={args.model}, conf={args.conf}")
    
    # 모델 경로가 지정되지 않은 경우 기본 경로 사용
    model_path = args.model if args.model else default_model_path
    log_message(f"사용할 모델 경로: {model_path}")
    
    # 추론 실행
    result = run_inference(
        model_path=model_path,
        image_path=args.image,
        conf=args.conf
    )
    
    # 성공 여부에 따른 종료 코드
    sys.exit(0 if result["success"] else 1)


if __name__ == "__main__":
    main()