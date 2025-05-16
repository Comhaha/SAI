#!/usr/bin/env python
# -*- coding: utf-8 -*-

"""
inference.py - 단일 이미지 추론 스크립트

이 스크립트는 C# WinForms 애플리케이션에서 호출하여 YOLOv8 모델로
단일 이미지 객체 탐지 추론을 실행하고 결과 이미지를 저장합니다.

필수 패키지: ultralytics, opencv-python (튜토리얼에서 미리 설치됨)

사용법:
    python inference.py --model path/to/model.pt --image path/to/image.jpg --conf 0.25 --output-dir results
"""

import os
import sys
import json
import time
import argparse
from pathlib import Path

# base_dir은 현재 스크립트 기준에서 Python 폴더
# "C:\Users\SSAFY\Desktop\3rd PJT\S12P31D201\app\SAI\SAI\SAI.Application\Python"
base_dir = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
print(f"Base directory: {base_dir}")

# model_dir은 YOLOv8 best.pt 파일이 있는 폴더
# "C:\Users\SSAFY\Desktop\3rd PJT\S12P31D201\app\SAI\SAI\SAI.Application\Python\runs\detect\train\weights
# best.pt"
model_dir = os.path.join(base_dir, "runs", "detect", "train", "weights", "best.pt")


# 추론 함수
def run_inference(model_dir, image_path, conf=0.25, output_dir=None):
    """
    YOLOv8 모델을 사용하여 이미지 추론 실행
    
    Args:
        model_dir (str): 모델 파일 경로
        image_path (str): 이미지 파일 경로
        conf (float): 신뢰도 임계값
        output_dir (str): 결과 저장 디렉토리
        
    Returns:
        dict: 추론 결과 정보
    """
    start_time = time.time()
    print(f"모델 로드 중: {model_dir}")
    
    # 파일 존재 확인
    if not os.path.exists(model_dir):
        print(f"오류: 모델 파일을 찾을 수 없습니다: {model_dir}")
        return {"success": False, "error": f"모델 파일을 찾을 수 없습니다: {model_dir}"}
    
    if not os.path.exists(image_path):
        print(f"오류: 이미지 파일을 찾을 수 없습니다: {image_path}")
        return {"success": False, "error": f"이미지 파일을 찾을 수 없습니다: {image_path}"}
    
    # 출력 디렉토리 설정
    # "C:\Users\SSAFY\Desktop\3rd PJT\S12P31D201\app\SAI\SAI\SAI.Application\Python\runs\result"
    if not output_dir:
        output_dir = os.path.join(base_dir, "runs", "result")
    os.makedirs(output_dir, exist_ok=True)
    
    try:
        # YOLO 모델 로드
        from ultralytics import YOLO
        model = YOLO(model_dir)
        
        # 추론 실행
        print(f"이미지 추론 중: {image_path}")
        results = model.predict(
            source=image_path,
            conf=conf,
            save=True,
            save_dir=output_dir,
            show=False
        )
        
        # 결과 처리
        if not results or len(results) == 0:
            print("추론 결과가 없습니다")
            return {
                "success": True,
                "has_detections": False,
                "image_path": image_path,
                "inference_time": time.time() - start_time
            }
        
        # 결과 이미지 경로 
        image_name = os.path.basename(image_path)
        result_image_path = os.path.join(output_dir, image_name)
        
        # 탐지 결과 추출
        detections = []
        if hasattr(results[0], 'boxes') and results[0].boxes is not None:
            boxes = results[0].boxes
            for i, box in enumerate(boxes):
                # 박스 좌표
                x1, y1, x2, y2 = box.xyxy[0].tolist()
                
                # 클래스 및 신뢰도
                cls = int(box.cls[0].item())
                conf = float(box.conf[0].item())
                
                # 클래스 이름
                cls_name = results[0].names.get(cls, f"class_{cls}")
                
                detections.append({
                    "id": i,
                    "class_id": cls,
                    "class_name": cls_name,
                    "confidence": conf,
                    "bbox": [x1, y1, x2, y2]
                })
        
        # 추론 시간 계산
        inference_time = time.time() - start_time
        print(f"추론 완료: {len(detections)}개 객체 감지 (소요 시간: {inference_time:.3f}초)")
        
        # 결과 반환
        return {
            "success": True,
            "has_detections": len(detections) > 0,
            "detections": detections,
            "detection_count": len(detections),
            "result_image": result_image_path,
            "input_image": image_path,
            "inference_time": inference_time
        }
    
    except Exception as e:
        print(f"추론 오류: {str(e)}")
        return {"success": False, "error": str(e)}


def main():
    """명령행에서 실행될 메인 함수"""
    parser = argparse.ArgumentParser(description='YOLOv8 이미지 추론 도구')
    
    # 필수 인자
    parser.add_argument('--model', required=True, help='학습된 모델 파일 경로 (.pt)')
    parser.add_argument('--image', required=True, help='추론할 이미지 파일 경로')
    
    # 선택적 인자
    parser.add_argument('--conf', type=float, default=0.25, help='객체 탐지 신뢰도 임계값 (기본값: 0.25)')
    parser.add_argument('--output-dir', help='결과 저장 디렉토리')
    
    args = parser.parse_args()
    
    # 추론 실행
    result = run_inference(
        model_dir=args.model,
        image_path=args.image,
        conf=args.conf,
        output_dir=args.output_dir
    )
    
    # 결과 출력 (C# 애플리케이션에서 파싱하는 형식)
    print(f"INFERENCE_RESULT:{json.dumps(result)}")
    
    # 성공 여부에 따른 종료 코드 반환
    return 0 if result["success"] else 1


if __name__ == "__main__":
    sys.exit(main())