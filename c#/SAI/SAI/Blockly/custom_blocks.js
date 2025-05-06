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

Blockly.Python['pipInstall'] = function (block) {
    return '!pip install ultralytics\n';
};
