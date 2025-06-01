# ================================================
# 🔷 SAI AI 블록 코딩 튜토리얼 🔷
# ================================================

# 패키지 설치
!pip install ultralytics


# 레이어 수정
Conv = 64 # Conv (합성곱 계층): 이미지에서 특징을 찾는 기본적인 눈 역할을 하는 층
C2f = 1 # C2f (병목 현상을 개선한 Conv): Conv 보다 조금 더 똑똑하게 특징을 추출해서 효율을 높이는 층
Upsample = 2.0 # Upsample (업샘플링): 더 넓은 영역의 정보를 볼 수 있게 확대하는 층
						      # Upsample 레이어의 scale 값을 수정합니다.

# custom.yaml 파일 생성
# 입력한 Conv, C2f, Upsample_scale으로 SAI가 custom.yaml 파일을 생성해드려요!
 print("custom.yaml 생성 완료")

# 커스텀 모델 생성
from ultralytics import YOLO

# 사용자 레이어 설정으로 커스텀 YAML 생성됨
model = YOLO("custom_model.yaml")  # 동적 생성된 커스텀 구조
print("✅ 커스텀 YOLOv8 모델 생성 완료!")
print("custom.yaml 생성 완료")


# 데이터 불러오기
# 코드로 서버에 있는 데이터 땡겨오게 하기


# 모델 학습하기
model.train(
   data="/home/j-k12d201/yolo8/bottle-2/data.yaml",    # 데이터셋의 정보를 담고 있는 YAML 파일 경로를 지정
   "epochs": 1,    # 학습 데이터를 몇 번 반복해서 학습할지를 결정
   "batch": 16,    # 한 번의 학습 단계에서 모델에 입력되는 이미지의 개수를 결정
   "imgsz": 640,  # 이미지의 크기(가로와 세로)를 지정
   "device": "cuda"    # CPU, GPU(cuda) 지정
)


# 학습 결과 그래프 출력
from IPython.display import Image, display

display(Image(filename = 'runs/detect/train/results.png'))


img_path ='C:\Users\SSAFY\Desktop\1598542999444.jpg'))


# 추론 실행
model = YOLO("/home/.../best.pt")
results = model.predict(source=img_path, save=False, show=False, conf=0.25)


# 결과 시각화
import cv2
import matplotlib.pyplot as plt

# bounding box 그려진 이미지 추출 (BGR)
result_img = results[0].plot()
result_img = cv2.cvtColor(result_img, cv2.COLOR_BGR2RGB)   # matplotlib용 RGB로 변환

plt.imshow(result_img) # 출력
plt.axis("off")
plt.title("YOLOv8 Prediction")
plt.show()
