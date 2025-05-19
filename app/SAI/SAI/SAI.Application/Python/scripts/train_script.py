# ================================================
# 🔷 SAI AI 블록 코딩 튜토리얼 🔷
# ================================================

# 패키지 설치
!pip install ultralytics


# 모델 불러오기
from ultralytics import YOLO

model = YOLO("custom.yaml") # 커스텀 레이어 적용
model.load("yolov8n.pt") # Yolov8 모델 불러오기
# YOLOv8 모델 불러오기
print("✅ YOLOv8 설치 및 yolov8n.pt 모델 로드 완료!")')


# 레이어 수정
Conv = 64 # Conv (합성곱 계층): 이미지에서 특징을 찾는 기본적인 눈 역할을 하는 층
C2f = 1 # C2f (병목 현상을 개선한 Conv): Conv 보다 조금 더 똑똑하게 특징을 추출해서 효율을 높이는 층
Upsample = 1.5 # Upsample (업샘플링): 더 넓은 영역의 정보를 볼 수 있게 확대하는 층
						      # Upsample 레이어의 scale 값을 수정합니다.

# custom.yaml 파일 생성
# 입력한 Conv, C2f, Upsample_scale으로 SAI가 custom.yaml 파일을 생성해드려요!
 print("custom.yaml 생성 완료")
