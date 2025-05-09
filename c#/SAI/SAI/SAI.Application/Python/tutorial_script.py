# 패키지 설치
!pip install ultralytics


# 모델 불러오기
from ultralytics import YOLO

model = YOLO("yolov8n.pt")   # YOLOv8 모델 불러오기
print("✅ YOLOv8 설치 및 (yolov8n.pt) 모델 로드 완료!")')


img_path ='파일 선택'))


# 학습 결과 그래프 출력
from IPython.display import Image, display

display(Image(filename = 'runs/detect/train/results.png'))
