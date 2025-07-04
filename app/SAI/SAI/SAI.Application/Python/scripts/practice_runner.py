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

class practiceRunner:
    def __init__(self, practice_train_script_path: str):
        self.practice_train_script_path = practice_train_script_path
        self.practice_train_script = self._load_script()

        self.block_mappings = {
            "start": None,
            "pipInstall": self.practice_train_script.install_packages_block,
            "loadModel": self.practice_train_script.check_gpu_yolo_load_block,
            "loadModelWithLayer": self.practice_train_script.load_model_with_layer_block,
            "loadDataset": self.practice_train_script.download_dataset_block,
            "machineLearning": self.practice_train_script.train_model_block,
            "resultGraph": self.practice_train_script.visualize_training_results_block,
            "imgPath": self.practice_train_script.set_image_path_block,
            "modelInference": self.practice_train_script.run_inference_block,
            "visualizeResult": self.practice_train_script.visualize_results_block
        }
        logger.debug("[Runner] 블록 함수 매핑 완료")
        print(f"[DEBUG] block_mappings.keys(): {list(self.block_mappings.keys())}", flush=True)


    def _load_script(self):
        """튜토리얼 스크립트 로드"""
        print(f"[DEBUG] 스크립트 로딩 시도: {self.practice_train_script_path}", flush=True)
        
        try:
            # 스크립트 파일 존재 확인
            if not os.path.exists(self.practice_train_script_path):
                print(f"[DEBUG] 스크립트 파일이 존재하지 않음: {self.practice_train_script_path}", flush=True)
                raise FileNotFoundError(f"튜토리얼 스크립트를 찾을 수 없습니다: {self.practice_train_script_path}")
            
            print(f"[DEBUG] 스크립트 파일 존재 확인됨", flush=True)
            
            # 스크립트 디렉토리를 sys.path에 추가
            script_dir = os.path.dirname(self.practice_train_script_path)
            if script_dir not in sys.path:
                sys.path.insert(0, script_dir)
                print(f"[DEBUG] sys.path에 추가됨: {script_dir}", flush=True)
            
            # 모듈 사양 생성
            print(f"[DEBUG] 모듈 사양 생성 시도", flush=True)
            spec = importlib.util.spec_from_file_location(
                "practice_train_script",
                self.practice_train_script_path
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
                        block_params = params.copy() if params else {}
                        print(f"[DEBUG] 전달할 파라미터: {block_params}", flush=True)
                        logger.debug(f"[Runner] 전달할 파라미터: {block_params}")
                        
                        try:
                            print(f"[DEBUG] 함수 실행 시작: {block_type}", flush=True)
                            result = func(block_params=block_params)
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
    parser = argparse.ArgumentParser(description='실습 스크립트 실행기')
    parser.add_argument('--train-script', required=True, help='트레이닝 스크립트 경로')
    parser.add_argument('--blocks', nargs='+', help='실행할 블록 타입 (공백으로 구분)')
    parser.add_argument('--image-path', help='추론용 이미지 경로')
    parser.add_argument('--epochs', type=int, help='에폭 수')
    parser.add_argument('--imgsz', type=int, help='이미지 사이즈')
    parser.add_argument('--params', type=str, help='모든 파라미터를 담은 JSON 문자열')

    args = parser.parse_args()
    logger.debug(f"[Runner] 받은 인자: {args}")

    try:
        runner = practiceRunner(args.train_script)

        # 파라미터 초기화
        params = {}
        blocks = args.blocks

        # 개별 인자들 처리
        if args.image_path:
            params["image_path"] = args.image_path
        if args.epochs:
            params["epochs"] = args.epochs
        if args.imgsz:
            params["imgsz"] = args.imgsz

        # JSON 파라미터 처리
        print(f"[DEBUG] args.params: {args.params}")
        if args.params:
            json_params = json.loads(args.params)
            params.update(json_params)
            
            # JSON에서 blocks를 추출 (개별 인자보다 우선)
            if "blocks" in json_params and not blocks:
                blocks = json_params["blocks"]
                print(f"[DEBUG] JSON에서 blocks 추출: {blocks}")
        else:
            print("[DEBUG] --params 인자가 비어있음")

        # blocks가 여전히 None이면 오류
        if not blocks:
            raise ValueError("blocks 파라미터가 제공되지 않았습니다.")

        logger.debug(f"[Runner] 실행할 블록: {blocks}")
        logger.debug(f"[Runner] 전달할 최종 파라미터: {params}")

        results = runner.execute_blocks(blocks, params)
        print(f"TRAINING_RESULT:{json.dumps(results)}")

        return 0 if results["success"] else 1

    except Exception as e:
        logger.error(f"[Runner] 전체 실행 중 오류 발생: {e}", exc_info=True)
        print(f"PROGRESS:0:오류 발생: {str(e)}", flush=True)
        return 1

if __name__ == "__main__":
    sys.exit(main())
