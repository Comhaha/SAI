import os
import subprocess

# 필수 패키지 설치
subprocess.run(["pip", "install", "ultralytics"])
subprocess.run(["pip", "install", "roboflow"])

# YOLO 모델 로드
from ultralytics import YOLO
print("✅ YOLOv8 설치 및 모델 로드 중...")
model = YOLO("yolov8n.pt")
print("✅ YOLOv8 모델 로드 완료!")

# Roboflow에서 데이터셋 다운로드
from roboflow import Roboflow
rf = Roboflow(api_key="ozRmezJLAsrdO3TrXlwo")
project = rf.workspace("ddd-1enry").project("strawberry-ds51b")
version = project.version(1)
dataset = version.download("yolov8")
print("✅ Roboflow 데이터 다운로드 완료!")

# 데이터 경로 출력 (확인용)
print(f"데이터 경로: {dataset.location}/data.yaml")

# 학습 실행
model.train(
    data=os.path.join(dataset.location, "data.yaml"),
    epochs=100,
    batch=16,
    imgsz=640,
    device="cuda"
)

print("✅ 학습 완료!")
