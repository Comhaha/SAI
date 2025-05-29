# ========== 기존 로컬 구조와 동일한 FastAPI 서버 ==========
from fastapi import FastAPI, UploadFile, File, HTTPException
from fastapi.middleware.cors import CORSMiddleware
from fastapi.responses import FileResponse, JSONResponse
import uvicorn
import os
import shutil
import zipfile
from pathlib import Path
import json
import time
import uuid
from datetime import datetime

# YOLO 관련 imports
try:
    from ultralytics import YOLO
    import torch
    YOLO_AVAILABLE = True
except ImportError:
    YOLO_AVAILABLE = False

app = FastAPI(title="AI Training Server", version="1.0.0")

# CORS 설정
app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

# 전역 변수들
TRAINING_STATUS = {}  # task_id별 상태 저장

@app.on_event("startup")
async def startup_event():
    """서버 시작시 기존 로컬 Python 구조와 동일한 디렉토리 생성"""
    directories = [
        "/app/scripts",                    # 기존: ./scripts/ (FastAPI 코드)
        "/app/dataset",                    # 기존: ./dataset/
        "/app/dataset/tutorial_dataset",   # 기존: ./dataset/tutorial_dataset/
        "/app/dataset/practice_dataset",   # 기존: ./dataset/practice_dataset/
        "/app/runs",                       # 기존: ./runs/
        "/app/runs/detect",               # 기존: ./runs/detect/
        "/app/runs/detect/train",         # 기존: ./runs/detect/train/
        "/app/runs/detect/train/weights", # 기존: ./runs/detect/train/weights/
        "/app/runs/result"                # 기존: ./runs/result/ (추론 결과)
    ]
    
    for directory in directories:
        Path(directory).mkdir(parents=True, exist_ok=True)
    
    print("✅ AI Training Server Started with local Python structure!")
    print(f"✅ YOLO Available: {YOLO_AVAILABLE}")
    if YOLO_AVAILABLE:
        print(f"✅ CUDA Available: {torch.cuda.is_available()}")

@app.get("/")
async def root():
    return {"message": "AI Training Server is running!", "yolo_available": YOLO_AVAILABLE}

@app.get("/health")
async def health_check():
    gpu_info = {}
    if YOLO_AVAILABLE:
        gpu_info = {
            "cuda_available": torch.cuda.is_available(),
            "gpu_count": torch.cuda.device_count() if torch.cuda.is_available() else 0,
        }
        if torch.cuda.is_available():
            gpu_info["gpu_name"] = torch.cuda.get_device_name(0)
    
    return {
        "status": "healthy",
        "timestamp": datetime.now().isoformat(),
        "yolo_available": YOLO_AVAILABLE,
        **gpu_info
    }

@app.post("/api/training/start")
async def start_training(dataset: UploadFile = File(...)):
    """학습 시작 - 기존 로컬 구조 사용"""
    if not YOLO_AVAILABLE:
        raise HTTPException(status_code=500, detail="YOLO not available")
    
    try:
        # 고유 task_id 생성
        task_id = str(uuid.uuid4())[:8]
        
        # 상태 초기화
        TRAINING_STATUS[task_id] = {
            "status": "preparing",
            "progress": 0,
            "message": "Preparing dataset...",
            "start_time": datetime.now().isoformat()
        }
        
        # 기존 구조: ./dataset/tutorial_dataset/ 에 저장
        dataset_dir = Path("/app/dataset/tutorial_dataset")
        dataset_dir.mkdir(parents=True, exist_ok=True)
        
        # 기존 tutorial 데이터셋 관련 파일만 삭제 (기존 로직과 동일)
        tutorial_specific_files = ["tutorial_dataset", "tutorial_dataset.zip", "tutorial_dataset_done.txt"]
        for filename in tutorial_specific_files:
            file_path = dataset_dir.parent / filename
            try:
                if os.path.exists(file_path):
                    if os.path.isfile(file_path) or os.path.islink(file_path):
                        os.unlink(file_path)
                    elif os.path.isdir(file_path):
                        shutil.rmtree(file_path)
            except Exception as e:
                print(f"파일 삭제 실패: {file_path} - {e}")
        
        # tutorial_dataset 디렉토리 새로 생성
        if dataset_dir.exists():
            shutil.rmtree(dataset_dir)
        dataset_dir.mkdir(parents=True, exist_ok=True)
        
        # ZIP 파일 저장
        zip_path = dataset_dir / "dataset.zip"
        with open(zip_path, "wb") as buffer:
            shutil.copyfileobj(dataset.file, buffer)
        
        # 압축 해제
        with zipfile.ZipFile(zip_path, 'r') as zip_ref:
            zip_ref.extractall(dataset_dir)
        
        # ZIP 파일 삭제
        os.remove(zip_path)
        
        # data.yaml 파일 찾기
        yaml_path = find_yaml_file(dataset_dir)
        if not yaml_path:
            raise HTTPException(status_code=400, detail="data.yaml not found in dataset")
        
        # 백그라운드에서 학습 시작
        import threading
        training_thread = threading.Thread(
            target=run_training, 
            args=(task_id, yaml_path)
        )
        training_thread.start()
        
        return {
            "task_id": task_id,
            "status": "started",
            "message": "Training started successfully"
        }
        
    except Exception as e:
        return JSONResponse(
            status_code=500,
            content={"error": str(e)}
        )

def find_yaml_file(dataset_dir):
    """data.yaml 파일 찾기 - tutorial_train_script.py와 동일 로직"""
    # 직접 경로에서 찾기
    yaml_path = dataset_dir / "data.yaml"
    if yaml_path.exists():
        return str(yaml_path)
    
    # 하