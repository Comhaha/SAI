# ================================================
# 🔷 SAI AI 블록 코딩 튜토리얼 🔷
# ================================================


# 패키지 설치
!pip install ultralytics


# 모델 불러오기
from ultralytics import YOLO

model = YOLO("yolov8n.pt")   # YOLOv8 모델 불러오기
print("✅ YOLOv8 설치 및 yolov8n.pt 모델 로드 완료!")')


# 데이터 불러오기
# 코드로 서버에 있는 데이터 땡겨오게 하기
