# 모델 학습하기
model.train(
   data="/home/j-k12d201/yolo8/bottle-2/data.yaml",    # 데이터셋의 정보를 담고 있는 YAML 파일 경로를 지정
   "epochs": 50,    # 학습 데이터를 몇 번 반복해서 학습할지를 결정
   "batch": 16,    # 한 번의 학습 단계에서 모델에 입력되는 이미지의 개수를 결정
   "imgsz": 512,  # 이미지의 크기(가로와 세로)를 지정
   "device": "cuda"    # CPU, GPU(cuda) 지정
)


img_path ='C:/Users/SSAFY/Pictures/Screenshots/스크린샷 2025-01-10 133459.png'))
