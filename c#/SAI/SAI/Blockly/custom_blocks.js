// 1. 패키지 설치
Blockly.defineBlocksWithJsonArray([
    {
        "type": "pipInstall", // 블록 타입
        "message0": "패키지 설치", // 블록에 표시되는 문구
        "previousStatement": null,
        "nextStatement": null,
        "colour": 160,
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
        "colour": 210,
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
        `# 코드로 서버에 있는 데이터 땡겨오게 하기\n\n`
    );
};