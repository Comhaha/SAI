  # 레이어 수정
  Conv = 128 # Conv (합성곱 계층): 이미지에서 특징을 찾는 기본적인 눈 역할을 하는 층
  C2f = 1 # C2f (병목 현상을 개선한 Conv): Conv 보다 조금 더 똑똑하게 특징을 추출해서 효율을 높이는 층
  Upsample = 2.5 # Upsample (업샘플링): 더 넓은 영역의 정보를 볼 수 있게 확대하는 층
  						      # Upsample 레이어의 scale 값을 수정합니다.

  # custom.yaml 파일 생성
  # 입력한 Conv, C2f, Upsample_scale으로 SAI가 custom.yaml 파일을 생성해드려요!
   print("custom.yaml 생성 완료")

  # 레이어 수정
  Conv = 64 # Conv (합성곱 계층): 이미지에서 특징을 찾는 기본적인 눈 역할을 하는 층
  C2f = 1 # C2f (병목 현상을 개선한 Conv): Conv 보다 조금 더 똑똑하게 특징을 추출해서 효율을 높이는 층
  Upsample = 1.5 # Upsample (업샘플링): 더 넓은 영역의 정보를 볼 수 있게 확대하는 층
  						      # Upsample 레이어의 scale 값을 수정합니다.

  # custom.yaml 파일 생성
  # 입력한 Conv, C2f, Upsample_scale으로 SAI가 custom.yaml 파일을 생성해드려요!
   print("custom.yaml 생성 완료")

# 모델 불러오기
from ultralytics import YOLO

model = YOLO("custom.yaml") # 커스텀 레이어 적용
model.load("yolov8m.pt") # Yolov8 모델 불러오기
# YOLOv8 모델 불러오기
print("✅ YOLOv8 설치 및 yolov8m.pt 모델 로드 완료!")')


# 모델 학습하기
model.train(
   data="/home/j-k12d201/yolo8/bottle-2/data.yaml",    # 데이터셋의 정보를 담고 있는 YAML 파일 경로를 지정
   "epochs": 50,    # 학습 데이터를 몇 번 반복해서 학습할지를 결정
   "batch": 16,    # 한 번의 학습 단계에서 모델에 입력되는 이미지의 개수를 결정
   "imgsz": 640,  # 이미지의 크기(가로와 세로)를 지정
   "device": "cuda"    # CPU, GPU(cuda) 지정
)
