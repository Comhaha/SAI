#!/usr/bin/env python
# -*- coding: utf-8 -*-

import os
import sys
import re
import logging
import time
import importlib.util
import json
import argparse
from datetime import datetime
from typing import Dict, List, Any

# 로깅 설정
logging.basicConfig(
    level=logging.DEBUG,
    format='%(asctime)s - %(levelname)s - %(message)s',
    datefmt='%Y-%m-%d %H:%M:%S'
)
logger = logging.getLogger(__name__)

base_dir = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))

class TutorialRunner:
    def __init__(self, tutorial_train_script_path: str):
        self.tutorial_train_script_path = tutorial_train_script_path
        self.tutorial_train_script = self._load_script()

        self.block_mappings = {
            "start": None,
            "pipInstall": self.tutorial_train_script.install_packages_block,
            "loadModel": self.tutorial_train_script.check_gpu_yolo_load_block,
            "loadDataset": self.tutorial_train_script.download_dataset_block,
            "machineLearning": self.tutorial_train_script.train_model_block,
            "resultGraph": self.tutorial_train_script.visualize_training_results_block,
            "imgPath": self.tutorial_train_script.set_image_path_block,
            "modelInference": self.tutorial_train_script.run_inference_block,
            "visualizeResult": self.tutorial_train_script.visualize_results_block
        }
        logger.debug("[Runner] 블록 함수 매핑 완료")
        print(f"[DEBUG] block_mappings.keys(): {list(self.block_mappings.keys())}", flush=True)


    def _load_script(self):
        """튜토리얼 스크립트 로드"""
        print(f"[DEBUG] 스크립트 로딩 시도: {self.tutorial_train_script_path}", flush=True)
        
        try:
            # 스크립트 파일 존재 확인
            if not os.path.exists(self.tutorial_train_script_path):
                print(f"[DEBUG] 스크립트 파일이 존재하지 않음: {self.tutorial_train_script_path}", flush=True)
                raise FileNotFoundError(f"튜토리얼 스크립트를 찾을 수 없습니다: {self.tutorial_train_script_path}")
            
            print(f"[DEBUG] 스크립트 파일 존재 확인됨", flush=True)
            
            # 스크립트 디렉토리를 sys.path에 추가
            script_dir = os.path.dirname(self.tutorial_train_script_path)
            if script_dir not in sys.path:
                sys.path.insert(0, script_dir)
                print(f"[DEBUG] sys.path에 추가됨: {script_dir}", flush=True)
            
            # 모듈 사양 생성
            print(f"[DEBUG] 모듈 사양 생성 시도", flush=True)
            spec = importlib.util.spec_from_file_location(
                "tutorial_train_script",
                self.tutorial_train_script_path
            )
            print(f"[DEBUG] 모듈 사양 생성됨", flush=True)
            
            # 모듈 객체 생성
            print(f"[DEBUG] 모듈 객체 생성 시도", flush=True)
            module = importlib.util.module_from_spec(spec)
            print(f"[DEBUG] 모듈 객체 생성됨", flush=True)
            
            # 모듈 실행
            print(f"[DEBUG] 모듈 실행 시도", flush=True)
            spec.loader.exec_module(module)
            print(f"[DEBUG] 모듈 실행 완료", flush=True)
            
            print(f"[DEBUG] 튜토리얼 스크립트 로드 완료", flush=True)
            return module
            
        except Exception as e:
            print(f"[DEBUG] 스크립트 로드 중 오류 발생: {e}", flush=True)
            raise

    def execute_blocks(self, block_types: List[str], params: Dict[str, Any] = None) -> Dict[str, Any]:
        if params is None:
            params = {}

        results = {
            "success": True,
            "blocks_executed": [],
            "results": {},
            "execution_time": 0,
            "timestamp": datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        }
        
        print(f"[DEBUG] execute_blocks 시작 - 블록 목록: {block_types}", flush=True)
        logger.debug(f"[Runner] 실행할 블록 목록: {block_types}")
        start_time = time.time()

        try:
            for block_type in block_types:
                print(f"[DEBUG] 블록 실행 시도: {block_type}", flush=True)
                logger.debug(f"[Runner] 현재 블록: {block_type}")

                if block_type == "start":
                    print(f"[DEBUG] 'start' 블록은 생략", flush=True)
                    logger.debug(f"[Runner] 'start' 블록은 생략")
                    continue

                if block_type in self.block_mappings:
                    func = self.block_mappings[block_type]
                    print(f"[DEBUG] 매핑된 함수: {func}", flush=True)
                    logger.debug(f"[Runner] 매핑된 함수: {func}")

                    if func:
                        block_params = {}
                        if block_type == "imgPath" and "image_path" in params:
                            block_params["image_path"] = params["image_path"]
                        if block_type == "machineLearning":
                            if "epochs" in params:
                                block_params["epochs"] = params["epochs"]
                            if "imgsz" in params:
                                block_params["imgsz"] = params["imgsz"]

                        print(f"[DEBUG] 전달할 파라미터: {block_params}", flush=True)
                        logger.debug(f"[Runner] 전달할 파라미터: {block_params}")
                        
                        try:
                            print(f"[DEBUG] 함수 실행 시작: {block_type}", flush=True)
                            result = func(**block_params) if block_params else func()
                            print(f"[DEBUG] 함수 실행 완료: {block_type}", flush=True)
                            logger.debug(f"[Runner] 실행 결과: {result}")

                            results["blocks_executed"].append(block_type)
                            results["results"][block_type] = result

                            if not result.get("success", False):
                                error_msg = f"블록 '{block_type}' 실행 실패: {result.get('error')}"
                                print(f"[DEBUG] {error_msg}", flush=True)
                                logger.warning(f"[Runner] {error_msg}")
                                results["success"] = False
                                break
                        except Exception as e:
                            error_msg = f"블록 '{block_type}' 실행 중 예외 발생: {e}"
                            print(f"[DEBUG] {error_msg}", flush=True)
                            logger.error(error_msg, exc_info=True)
                            results["success"] = False
                            results["error"] = str(e)
                            break
                    else:
                        error_msg = f"블록 '{block_type}'는 실행 함수가 없습니다."
                        print(f"[DEBUG] {error_msg}", flush=True)
                        logger.warning(f"[Runner] {error_msg}")
                else:
                    error_msg = f"알 수 없는 블록 타입: {block_type}"
                    print(f"[DEBUG] {error_msg}", flush=True)
                    logger.warning(f"[Runner] {error_msg}")

        except Exception as e:
            error_msg = f"블록 실행 중 오류: {e}"
            print(f"[DEBUG] {error_msg}", flush=True)
            logger.error(error_msg, exc_info=True)
            results["success"] = False
            results["error"] = str(e)

        results["execution_time"] = time.time() - start_time
        print(f"[DEBUG] 전체 실행 소요 시간: {results['execution_time']}초", flush=True)
        logger.debug(f"[Runner] 전체 실행 소요 시간: {results['execution_time']}초")
        return results

def main():
    parser = argparse.ArgumentParser(description='튜토리얼 스크립트 실행기')
    parser.add_argument('--tutorial-train-script', required=True, help='튜토리얼 트레이닝 스크립트 경로')
    parser.add_argument('--blocks', nargs='+', help='실행할 블록 타입 (공백으로 구분)')
    parser.add_argument('--image-path', help='추론용 이미지 경로')
    parser.add_argument('--epochs', type=int, help='에폭 수')
    parser.add_argument('--imgsz', type=int, help='이미지 사이즈')

    args = parser.parse_args()
    logger.debug(f"[Runner] 받은 인자: {args}")

    try:
        runner = TutorialRunner(args.tutorial_train_script)

        params = {}
        if args.image_path:
            params["image_path"] = args.image_path
        if args.epochs:
            params["epochs"] = args.epochs
        if args.imgsz:
            params["imgsz"] = args.imgsz

        logger.debug(f"[Runner] 전달할 최종 파라미터: {params}")

        results = runner.execute_blocks(args.blocks, params)
        print(f"RESULT_JSON:{json.dumps(results)}")

        return 0 if results["success"] else 1

    except Exception as e:
        logger.error(f"[Runner] 전체 실행 중 오류 발생: {e}", exc_info=True)
        print(f"ERROR:{str(e)}")
        return 1

if __name__ == "__main__":
    sys.exit(main())
