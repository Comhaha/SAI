// 1. 패키지 설치
Blockly.defineBlocksWithJsonArray([
    {
        "type": "pipInstall", // 블록 타입
        "message0": "패키지 설치", // 블록에 표시되는 문구
        "previousStatement": null,
        "nextStatement": null,
        "colour": 0,
        "tooltip": "관련 패키지(ultralytics)를 설치합니다.",
        "helpUrl": ""
    }
]);

Blockly.Python.forBlock['pipInstall'] = function (block) {
    return (
        `# 패키지 설치\n` +
        `!pip install ultralytics\n\n`
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
        "colour": 50,
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
        `print("✅ YOLOv8 설치 및 (${modelFile}) 모델 로드 완료!")')\n\n`
    );
};

// 3. 데이터 불러오기
Blockly.defineBlocksWithJsonArray([
    {
        "type": "loadDataset", // 블록 타입
        "message0": "데이터 불러오기", // 블록에 표시되는 문구
        "previousStatement": null,
        "nextStatement": null,
        "colour": 100,
        "tooltip": "데이터셋을 불러옵니다.\n튜토리얼에서는 딸기와 바나나 데이터셋이 제공됩니다.",
        "helpUrl": ""
    }
]);

Blockly.Python.forBlock['loadDataset'] = function (block) {
    return (
        `# 데이터 불러오기\n` +
        `# 코드로 서버에 있는 데이터 땡겨오게 하기\n\n`
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
        "colour": 150,
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
        `)\n\n`
    );
};