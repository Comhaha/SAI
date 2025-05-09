// 0. 시작 블록 -> 이 블록과 연결된 아이들만 code를 show할 예정
Blockly.defineBlocksWithJsonArray([
    {
        "type": "start", // 블록 타입
        "message0": "시작", // 블록에 표시되는 문구
        "nextStatement": null,
        "colour": 0,
        "tooltip": "관련 패키지(ultralytics)를 설치합니다.",
        "helpUrl": ""
    }
]);

Blockly.Python.forBlock['start'] = function (block) {
    return (
        `# ================================================\n` +
        `# 🔷 SAI AI 블록 코딩 튜토리얼 🔷\n` +
        `# ================================================\n\n\n\n`
    );
};

// 1. 패키지 설치
Blockly.defineBlocksWithJsonArray([
    {
        "type": "pipInstall", // 블록 타입
        "message0": "패키지 설치", // 블록에 표시되는 문구
        "previousStatement": null,
        "nextStatement": null,
        "colour": 50,
        "tooltip": "관련 패키지(ultralytics)를 설치합니다.",
        "helpUrl": ""
    }
]);

Blockly.Python.forBlock['pipInstall'] = function (block) {
    return (
        `# 패키지 설치\n` +
        `!pip install ultralytics\n\n\n`
    );
};

// 2. 모델 불러오기
Blockly.defineBlocksWithJsonArray([
    {
        "type": "loadModel", // 블록 타입
        "message0": "Yolov8 %1 모델 불러오기", // 블록에 표시되는 문구
        "args0": [
            {
                "type": "field_dropdown",
                "name": "MODEL_VERSION",
                "options": [
                    ["Nano", "yolov8n.pt"],
                    ["Small", "yolov8s.pt"],
                    ["Medium", "yolov8m.pt"],
                    ["Large", "yolov8l.pt"]
                ]
            }
        ],
        "previousStatement": null,
        "nextStatement": null,
        "colour": 100,
        "tooltip": "YOLOv8 모델을 불러옵니다.\nYOLOv8의 나노버전부터 Large버전까지 제공됩니다.",
        "helpUrl": ""
    }
]);

Blockly.Python.forBlock['loadModel'] = function (block) {
    const modelFile = block.getFieldValue('MODEL_VERSION');
    return (
        `# 모델 불러오기\n` +
        `from ultralytics import YOLO\n\n` +
        `model = YOLO("${modelFile}")   # YOLOv8 모델 불러오기\n` +
        `print("✅ YOLOv8 설치 및 (${modelFile}) 모델 로드 완료!")')\n\n\n`
    );
};

// 3. 데이터 불러오기
Blockly.defineBlocksWithJsonArray([
    {
        "type": "loadDataset", // 블록 타입
        "message0": "데이터 불러오기", // 블록에 표시되는 문구
        "previousStatement": null,
        "nextStatement": null,
        "colour": 150,
        "tooltip": "데이터셋을 불러옵니다.\n튜토리얼에서는 딸기와 바나나 데이터셋이 제공됩니다.",
        "helpUrl": ""
    }
]);

Blockly.Python.forBlock['loadDataset'] = function (block) {
    return (
        `# 데이터 불러오기\n` +
        `# 코드로 서버에 있는 데이터 땡겨오게 하기\n\n\n`
    );
};

// 4. 모델 학습
Blockly.defineBlocksWithJsonArray([
    {
        "type": "machineLearning", // 블록 타입
        "message0": "모델 학습하기\nepochs: %1\nimgsz: %2", // 블록에 표시되는 문구
        "args0": [
            {
                "type": "field_dropdown",
                "name": "epochs",
                "options": [
                    ["50", "50"],
                    ["100", "100"],
                    ["150", "150"],
                    ["200", "200"]
                ]
            },
            {
                "type": "field_dropdown",
                "name": "imgsz",
                "options": [
                    ["512", "512"],
                    ["640", "640"],
                    ["960", "960"],
                    ["1024", "1024"],
                    ["1280", "1280"]
                ]
            }
        ],
        "previousStatement": null,  
        "nextStatement": null,
        "colour": 200,
        "tooltip": "데이터셋을 불러옵니다.\n튜토리얼에서는 딸기와 바나나 데이터셋이 제공됩니다.",
        "helpUrl": ""
    }
]);

Blockly.Python.forBlock['machineLearning'] = function (block) {
    const epochs = block.getFieldValue('epochs');
    const imgsz = block.getFieldValue('imgsz');
    return (
        `# 모델 학습하기\n` +
        `model.train(\n` +
        `   data="/home/j-k12d201/yolo8/bottle-2/data.yaml",    # 데이터셋의 정보를 담고 있는 YAML 파일 경로를 지정\n` +
        `   "epochs": ${epochs},    # 학습 데이터를 몇 번 반복해서 학습할지를 결정\n` +
        `   "batch": 16,    # 한 번의 학습 단계에서 모델에 입력되는 이미지의 개수를 결정\n` +
        `   "imgsz": ${imgsz},  # 이미지의 크기(가로와 세로)를 지정\n`+
        `   "device": "cuda"    # CPU, GPU(cuda) 지정\n` +
        `)\n\n\n`
    );
};

// 5. 결과 확인
Blockly.defineBlocksWithJsonArray([
    {
        "type": "resultGraph", // 블록 타입
        "message0": "학습 결과 그래프 출력하기", // 블록에 표시되는 문구
        "previousStatement": null,
        "nextStatement": null,
        "colour": 250,
        "tooltip": "학습 결과 그래프를 출력합니다.",
        "helpUrl": ""
    }
]);

Blockly.Python.forBlock['resultGraph'] = function (block) {
    return (
        `# 학습 결과 그래프 출력\n` +
        `from IPython.display import Image, display\n\n` +
        `display(Image(filename = 'runs/detect/train/results.png'))\n\n\n`
    );
};

// 6. 이미지 경로 지정
Blockly.defineBlocksWithJsonArray([
    {
        "type": "imgPath",
        "message0": "이미지 불러오기\n이미지: %1",
        "args0": [
            {
                "type": "field_filepicker",
                "name": "FILE_PATH",
                "value": "파일 선택"
            }
        ],
        "previousStatement": null,
        "nextStatement": null,
        "colour": 300,
        "tooltip": "추론을 위한 이미지 한 장을 불러옵니다.",
        "helpUrl": ""
    }
]);

Blockly.Python.forBlock['imgPath'] = function (block) {
    const filePath = block.getFieldValue('FILE_PATH');
    return (
        `img_path ='${filePath}'))\n\n\n`
    );
};

// 7. 추론 실행
Blockly.defineBlocksWithJsonArray([
    {
        "type": "modelInference", // 블록 타입
        "message0": "추론 실행하기", // 블록에 표시되는 문구
        "previousStatement": null,
        "nextStatement": null,
        "colour": 350,
        "tooltip": "학습한 모델의 추론을 실행합니다.\n",
        "helpUrl": ""
    }
]);

Blockly.Python.forBlock['modelInference'] = function (block) {
    return (
        `# 추론 실행\n` +
        `model = YOLO("/home/.../best.pt")\n` +
        `results = model.predict(source=img_path, save=False, show=False, conf=0.25)\n\n\n`
    );
};

// 8. 결과 시각화
Blockly.defineBlocksWithJsonArray([
    {
        "type": "visualizeResult", // 블록 타입
        "message0": "결과 시각화하기", // 블록에 표시되는 문구
        "previousStatement": null,
        "nextStatement": null,
        "colour": 120,
        "tooltip": "학습한 결과를 시각화합니다.\n",
        "helpUrl": ""
    }
]);

Blockly.Python.forBlock['visualizeResult'] = function (block) {
    return (
        `# 결과 시각화\n` +
        `import cv2\n` +
        `import matplotlib.pyplot as plt\n\n` +
        `# bounding box 그려진 이미지 추출 (BGR)\n` +
        `result_img = results[0].plot()\n` +
        `result_img = cv2.cvtColor(result_img, cv2.COLOR_BGR2RGB)   # matplotlib용 RGB로 변환\n\n` +
        `plt.imshow(result_img) # 출력\n` +
        `plt.axis("off")\n` +
        `plt.title("YOLOv8 Prediction")\n` +
        `plt.show()\n\n\n`
    );
};