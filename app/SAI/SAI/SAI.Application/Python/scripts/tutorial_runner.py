#!/usr/bin/env python
# -*- coding: utf-8 -*-

"""
runner.py - 튜토리얼 모드 실행기

이 스크립트는 c#의 blockly모델 배열을 파싱하여 
해당 부분만 tutorial_script.py에서 실행하는 기능을 담당합니다.

사용법:
    python runner.py --tutorial tutorial_script.py --tutorial-script tutorial_script.py
"""

import os
import sys
import re
import logging
import time
import importlib.util
import subprocess
import json
import argparse
from datetime import datetime
from typing import Dict, List, Any, Tuple, Optional, Union

# 로깅 설정
logging.basicConfig(
    level=logging.INFO,
    format='%(asctime)s - %(levelname)s - %(message)s',
    datefmt='%Y-%m-%d %H:%M:%S'
)
logger = logging.getLogger(__name__)

# 기본 경로 설정
# base_dir = "C:\Users\SSAFY\Desktop\3rd PJT\S12P31D201\app\SAI\SAI\SAI.Application\Python"
base_dir = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))

class TutorialRunner:
    """튜토리얼 스크립트 실행기"""
    
    def __init__(self, tutorial_train_script_path: str):
        self.tutorial_train_script_path = tutorial_train_script_path
        self.test_script = None
        self._load_script()
        
        # 블록 타입과 함수 매핑
        self.block_mappings = {
            "start": None,  # 시작 블록은 실행하지 않음
            "pipInstall": self.test_script.install_packages_with_progress,
            "loadModel": self.test_script.check_gpu,
            "loadDataset": self.test_script.download_dataset_with_progress,
            "machineLearning": self.test_script.train_model_block,  # 수정된 함수명
            "resultGraph": self.test_script.visualize_training_results,
            "imgPath": self.test_script.set_image_path_block,
            "modelInference": self.test_script.run_inference_block,
            "visualizeResult": self.test_script.visualize_results
        }
    
    def _load_script(self) -> None:
        """스크립트 로드"""
        try:
            # 튜토리얼 스크립트를 모듈로 로드
            if os.path.exists(self.tutorial_train_script_path):
                spec = importlib.util.spec_from_file_location("test_script", self.tutorial_train_script_path)
                self.test_script = importlib.util.module_from_spec(spec)
                spec.loader.exec_module(self.test_script)
                logger.info(f"테스트 스크립트 로드 완료: {self.tutorial_train_script_path}")
            else:
                raise FileNotFoundError(f"테스트 스크립트를 찾을 수 없습니다: {self.tutorial_train_script_path}")
        except Exception as e:
            logger.error(f"스크립트 로드 중 오류: {e}")
            raise
    
    def execute_blocks(self, block_types: List[str], params: Dict[str, Any] = None) -> Dict[str, Any]:
        """
        블록 배열을 받아서 순서대로 실행
        
        Args:
            block_types (List[str]): 블록 타입 배열 (예: ["start", "pipInstall", "loadModel"])
            params (Dict[str, Any], optional): 블록에 전달할 파라미터
            
        Returns:
            Dict[str, Any]: 실행 결과
        """
        if params is None:
            params = {}
        
        results = {
            "success": True,
            "blocks_executed": [],
            "results": {},
            "execution_time": 0,
            "timestamp": datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        }
        
        start_time = time.time()
        
        try:
            for block_type in block_types:
                if block_type == "start":
                    continue  # 시작 블록은 건너뜀
                    
                if block_type in self.block_mappings:
                    func = self.block_mappings[block_type]
                    if func:
                        # 블록 타입에 맞는 파라미터 추출
                        block_params = {}
                        
                        # 이미지 경로 파라미터 처리
                        if block_type == "imgPath" and "image_path" in params:
                            block_params["image_path"] = params["image_path"]
                        
                        # 학습 파라미터 처리
                        if block_type == "machineLearning":
                            if "epochs" in params:
                                block_params["epochs"] = params["epochs"]
                            if "imgsz" in params:
                                block_params["imgsz"] = params["imgsz"]
                        
                        # 블록 함수 실행
                        if block_params:
                            result = func(**block_params)
                        else:
                            result = func()
                            
                        results["blocks_executed"].append(block_type)
                        results["results"][block_type] = result
                    else:
                        logger.warning(f"블록 타입 {block_type}에 대한 함수가 정의되지 않았습니다.")
                else:
                    logger.warning(f"알 수 없는 블록 타입: {block_type}")
                    
        except Exception as e:
            results["success"] = False
            results["error"] = str(e)
            logger.error(f"블록 실행 중 오류 발생: {e}")
            
        results["execution_time"] = time.time() - start_time
        return results

def main():
    """메인 함수"""
    parser = argparse.ArgumentParser(description='튜토리얼 스크립트 실행기')
    parser.add_argument('--tutorial-train-script', required=True, help='튜토리얼 트레이닝 스크립트 경로')
    parser.add_argument('--blocks', nargs='+', help='실행할 블록 타입 (공백으로 구분, 예: start pipInstall loadModel)')
    parser.add_argument('--image-path', help='추론에 사용할 이미지 경로')
    parser.add_argument('--epochs', type=int, help='학습 에폭 수')
    parser.add_argument('--imgsz', type=int, help='이미지 크기')
    
    args = parser.parse_args()
    
    try:
        # 실행기 초기화
        runner = TutorialRunner(args.tutorial_train_script)
        
        # 파라미터 설정
        params = {}
        if args.image_path:
            params["image_path"] = args.image_path
        if args.epochs:
            params["epochs"] = args.epochs
        if args.imgsz:
            params["imgsz"] = args.imgsz
        
        # 블록 실행
        results = runner.execute_blocks(args.blocks, params)
        
        # JSON 결과 출력
        print(f"RESULT_JSON:{json.dumps(results)}")
        
        # 성공 여부에 따라 종료 코드 반환
        return 0 if results["success"] else 1
        
    except Exception as e:
        logger.error(f"실행 중 오류: {e}", exc_info=True)
        print(f"ERROR:{str(e)}")
        return 1


if __name__ == "__main__":
    sys.exit(main())