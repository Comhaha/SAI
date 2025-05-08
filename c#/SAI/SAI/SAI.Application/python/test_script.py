# 튜토리얼모드 테스트 py파일.
import os
import sys

import subprocess

# 필수 패키지 설치
subprocess.run([sys.executable, "-m", "pip", "install", "ultralytics"])
subprocess.run([sys.executable, "-m", "pip", "install", "roboflow"])
try:
    import torch
except ImportError:
    print(" torch 미설치. 사용자 환경에 맞게 설치 시도 중...")

    # GPU 사용 가능 여부 확인
    has_cuda = False
    try:
        import torch
        has_cuda = torch.cuda.is_available()
    except:
        pass  # torch가 없어서 예외 뜨는 건 무시

    # 설치 대상 설정
    if has_cuda:
        torch_url = "https://download.pytorch.org/whl/cu118"
        torch_pkg = "torch torchvision torchaudio"
    else:
        torch_url = "https://download.pytorch.org/whl/cpu"
        torch_pkg = "torch torchvision torchaudio"

    subprocess.run([
        sys.executable, "-m", "pip", "install", *torch_pkg.split(),
        "--index-url", torch_url
    ])
    import torch

print(" PyTorch 버전:", torch.__version__)
print(" CUDA 사용 가능:", torch.cuda.is_available())

# YOLO 모델 로드
from ultralytics import YOLO
print(" YOLOv8 설치 및 모델 로드 중...")
model = YOLO("yolov8n.pt")
print(" YOLOv8 모델 로드 완료!")

# Roboflow에서 데이터셋 다운로드
from roboflow import Roboflow
rf = Roboflow(api_key="ozRmezJLAsrdO3TrXlwo")
project = rf.workspace("ddd-1enry").project("strawberry-ds51b")
version = project.version(1)
dataset = version.download("yolov8")
print(" Roboflow 데이터 다운로드 완료!")

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

print(" 학습 완료!")
