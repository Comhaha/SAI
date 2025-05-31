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
import re
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

# 서버 상태 확인
# yolo 라이브러리 설치 여부, cuda 사용여부, cuda 이름, gpu 개수
@app.get("/api/system/status")
async def get_system_status():
    gpu_info = {}
    if YOLO_AVAILABLE:
        gpu_info = {
            "cuda_available": torch.cuda.is_available(),
            "gpu_count": torch.cuda.device_count() if torch.cuda.is_available() else 0,
        }
        if torch.cuda.is_available():
            gpu_info["gpu_name"] = torch.cuda.get_device_name(0)
    
    return {
        "status": "ready",
        "timestamp": datetime.now().isoformat(),
        "yolo_available": YOLO_AVAILABLE,
        **gpu_info
    }

# =============== 튜토리얼 모드 =====================
@app.post("/api/training/tutorial/start")
async def start_tutorial_training(dataset: UploadFile = File(...)):
    """튜토리얼 모드 학습 시작"""
    if not YOLO_AVAILABLE:
        raise HTTPException(status_code=500, detail="YOLO 사용 불가")
    
    try:
        task_id = str(uuid.uuid4())[:8] # 비동기 처리를 위한 task id. 진행상황을 task id에 저장
        
        TRAINING_STATUS[task_id] = {
            "status": "preparing",
            "progress": 0,
            "message": "tutorial 데이터셋 준비중...",
            "mode": "tutorial",
            "start_time": datetime.now().isoformat()
        }
        
        # Tutorial 전용 디렉토리
        dataset_dir = Path("/app/dataset/tutorial_dataset")
        dataset_dir.mkdir(parents=True, exist_ok=True)
        
        # Tutorial 전용 파일 정리
        tutorial_files = ["tutorial_dataset", "tutorial_dataset.zip", "tutorial_dataset_done.txt"]
        for filename in tutorial_files:
            file_path = dataset_dir.parent / filename
            if file_path.exists():
                if file_path.is_file():
                    file_path.unlink()
                elif file_path.is_dir():
                    shutil.rmtree(file_path)
        
        if dataset_dir.exists():
            shutil.rmtree(dataset_dir)
        dataset_dir.mkdir(parents=True, exist_ok=True)
        
        # ZIP 파일 저장 및 압축 해제
        zip_path = dataset_dir / "dataset.zip"
        with open(zip_path, "wb") as buffer:
            shutil.copyfileobj(dataset.file, buffer)
        
        with zipfile.ZipFile(zip_path, 'r') as zip_ref:
            zip_ref.extractall(dataset_dir)
        
        os.remove(zip_path)
        
        # data.yaml 파일 찾기
        yaml_path = find_yaml_file(dataset_dir, mode="tutorial")
        if not yaml_path:
            raise HTTPException(status_code=400, detail="[튜토리얼 모드] data.yaml를 못찾음")
        
        # 백그라운드에서 학습 시작
        import threading
        training_thread = threading.Thread(
            target=run_training, 
            args=(task_id, yaml_path, "tutorial")
        )
        training_thread.start()
        
        return {
            "task_id": task_id,
            "status": "started",
            "mode": "tutorial",
            "message": "[튜토리얼 모드] 학습 실행 성공"
        }
        
    except Exception as e:
        return JSONResponse(
            status_code=500,
            content={"error": str(e)}
        )

@app.post("/api/training/practice/start")
async def start_practice_training(dataset: UploadFile = File(...)):
    """실습 모드 학습 시작"""
    if not YOLO_AVAILABLE:
        raise HTTPException(status_code=500, detail="YOLO not available")
    
    try:
        task_id = str(uuid.uuid4())[:8]
        
        TRAINING_STATUS[task_id] = {
            "status": "preparing",
            "progress": 0,
            "message": "Preparing practice dataset...",
            "mode": "practice",
            "start_time": datetime.now().isoformat()
        }
        
        # Practice 전용 디렉토리
        dataset_dir = Path("/app/dataset/practice_dataset")
        dataset_dir.mkdir(parents=True, exist_ok=True)
        
        # Practice 전용 파일 정리
        practice_files = ["practice_dataset", "practice_dataset.zip", "practice_dataset_done.txt"]
        for filename in practice_files:
            file_path = dataset_dir.parent / filename
            if file_path.exists():
                if file_path.is_file():
                    file_path.unlink()
                elif file_path.is_dir():
                    shutil.rmtree(file_path)
        
        if dataset_dir.exists():
            shutil.rmtree(dataset_dir)
        dataset_dir.mkdir(parents=True, exist_ok=True)
        
        # ZIP 파일 저장 및 압축 해제
        zip_path = dataset_dir / "dataset.zip"
        with open(zip_path, "wb") as buffer:
            shutil.copyfileobj(dataset.file, buffer)
        
        with zipfile.ZipFile(zip_path, 'r') as zip_ref:
            zip_ref.extractall(dataset_dir)
        
        os.remove(zip_path)
        
        # data.yaml 파일 찾기
        yaml_path = find_yaml_file(dataset_dir, mode="practice")
        if not yaml_path:
            raise HTTPException(status_code=400, detail="data.yaml not found in practice dataset")
        
        # 백그라운드에서 학습 시작
        import threading
        training_thread = threading.Thread(
            target=run_training, 
            args=(task_id, yaml_path, "practice")
        )
        training_thread.start()
        
        return {
            "task_id": task_id,
            "status": "started",
            "mode": "practice",
            "message": "Practice training started successfully"
        }
        
    except Exception as e:
        return JSONResponse(
            status_code=500,
            content={"error": str(e)}
        )

def find_yaml_file(dataset_dir, mode="tutorial"):
    """data.yaml 파일 찾기 - 모드별 처리"""
    # 직접 경로에서 찾기
    yaml_path = dataset_dir / "data.yaml"
    if yaml_path.exists():
        return str(yaml_path)
    
    # 하위 폴더에서 찾기
    for root, dirs, files in os.walk(dataset_dir):
        for file in files:
            if file.endswith('.yaml') or file.endswith('.yml'):
                return os.path.join(root, file)
    
    return None

def run_training(task_id: str, yaml_path: str, mode: str = "tutorial"):
    """실제 학습 실행 함수 - 모드별 설정 적용"""
    try:
        # 상태 업데이트
        TRAINING_STATUS[task_id]["status"] = "training"
        TRAINING_STATUS[task_id]["message"] = f"Loading {mode} model..."
        TRAINING_STATUS[task_id]["progress"] = 10
        
        # 기존 results.csv 삭제
        results_csv = Path("/app/runs/detect/train/results.csv")
        if results_csv.exists():
            os.remove(results_csv)
        
        # YOLO 모델 로드
        model = YOLO("yolov8n.pt")
        
        TRAINING_STATUS[task_id]["message"] = f"{mode.capitalize()} training started..."
        TRAINING_STATUS[task_id]["progress"] = 20
        
        # GPU 정보 확인
        device = "cuda" if torch.cuda.is_available() else "cpu"
        
        # 모드별 학습 파라미터 설정
        if mode == "tutorial":
            # Tutorial: 고정 파라미터 (빠른 데모)
            epochs = 5
            batch_size = 16
            if device == "cuda" and torch.cuda.is_available():
                gpu_memory = torch.cuda.get_device_properties(0).total_memory / 1024**3
                if gpu_memory < 6:
                    batch_size = 8
        else:  # practice
            # Practice: 더 많은 에폭, 더 정교한 학습
            epochs = 10
            batch_size = 8
            if device == "cuda" and torch.cuda.is_available():
                gpu_memory = torch.cuda.get_device_properties(0).total_memory / 1024**3
                if gpu_memory < 6:
                    batch_size = 4
        
        # 학습 실행
        results = model.train(
            data=yaml_path,
            epochs=epochs,
            batch=batch_size,
            imgsz=640,
            device=device,
            project="/app/runs",
            name="detect/train",
            exist_ok=True,
            workers=0 if device == "cpu" else 4
        )
        
        # 결과 경로 설정
        results_dir = find_latest_results_dir()
        model_path = results_dir / "weights" / "best.pt"
        
        # 완료 상태 업데이트
        TRAINING_STATUS[task_id]["status"] = "completed"
        TRAINING_STATUS[task_id]["progress"] = 100
        TRAINING_STATUS[task_id]["message"] = f"{mode.capitalize()} training completed!"
        TRAINING_STATUS[task_id]["model_path"] = str(model_path)
        TRAINING_STATUS[task_id]["results_dir"] = str(results_dir)
        TRAINING_STATUS[task_id]["epochs"] = epochs
        TRAINING_STATUS[task_id]["batch_size"] = batch_size
        TRAINING_STATUS[task_id]["end_time"] = datetime.now().isoformat()
        
    except Exception as e:
        TRAINING_STATUS[task_id]["status"] = "failed"
        TRAINING_STATUS[task_id]["message"] = f"{mode.capitalize()} training failed: {str(e)}"
        TRAINING_STATUS[task_id]["error"] = str(e)

def find_latest_results_dir():
    """가장 최근에 생성된 results 디렉토리 찾기 - 기존 로직과 동일"""
    base_runs_dir = Path("/app/runs/detect")
    
    if not base_runs_dir.exists():
        base_runs_dir.mkdir(parents=True, exist_ok=True)
        return Path("/app/runs/detect/train")
    
    # 'train'으로 시작하는 모든 폴더 찾기
    train_dirs = [d for d in base_runs_dir.iterdir() if d.is_dir() and d.name.startswith('train')]
    if not train_dirs:
        return Path("/app/runs/detect/train")
    
    # 숫자 접미사가 있는 경우 가장 큰 숫자 찾기
    latest_dir = "train"
    max_num = 0
    for d in train_dirs:
        match = re.match(r'train(\d*)', d.name)
        if match:
            num_str = match.group(1)
            num = int(num_str) if num_str else 0
            if num > max_num:
                max_num = num
                latest_dir = d.name
    
    return base_runs_dir / latest_dir

@app.get("/api/training/status/{task_id}")
async def get_training_status(task_id: str):
    """학습 상태 조회"""
    if task_id not in TRAINING_STATUS:
        raise HTTPException(status_code=404, detail="Task not found")
    
    return TRAINING_STATUS[task_id]

@app.post("/api/inference/predict")
async def predict_image(
    image: UploadFile = File(...),
    task_id: str = None  # 특정 모델 사용시
):
    """이미지 추론 - 기존 inference.py와 동일한 로직"""
    if not YOLO_AVAILABLE:
        raise HTTPException(status_code=500, detail="YOLO not available")
    
    try:
        # 이미지 저장 (기존 구조: ./runs/result/)
        temp_id = str(uuid.uuid4())[:8]
        result_dir = Path("/app/runs/result")
        result_dir.mkdir(parents=True, exist_ok=True)
        
        image_path = result_dir / f"image_{temp_id}.jpg"
        
        with open(image_path, "wb") as buffer:
            shutil.copyfileobj(image.file, buffer)
        
        # 모델 경로 결정 (기존 로직과 동일)
        if task_id and task_id in TRAINING_STATUS:
            model_path = TRAINING_STATUS[task_id].get("model_path")
            if model_path and os.path.exists(model_path):
                model = YOLO(model_path)  # 학습된 모델 사용
            else:
                # 기본 경로에서 best.pt 찾기
                default_model_path = Path("/app/runs/detect/train/weights/best.pt")
                if default_model_path.exists():
                    model = YOLO(str(default_model_path))
                else:
                    model = YOLO("yolov8n.pt")  # 기본 모델
        else:
            # 가장 최근 학습된 모델 찾기
            latest_results_dir = find_latest_results_dir()
            latest_model_path = latest_results_dir / "weights" / "best.pt"
            if latest_model_path.exists():
                model = YOLO(str(latest_model_path))
            else:
                model = YOLO("yolov8n.pt")  # 기본 모델
        
        # 추론 실행 (기존 inference.py와 동일!)
        results = model.predict(
            source=str(image_path),
            conf=0.25,
            save=True,
            save_dir=str(result_dir),
            show=False
        )
        
        # 결과 이미지 경로
        result_image_path = result_dir / f"image_{temp_id}.jpg"
        
        # 결과 데이터 추출
        detections = []
        if results and len(results) > 0:
            result = results[0]
            if result.boxes is not None:
                for box in result.boxes:
                    detections.append({
                        "class": int(box.cls.item()),
                        "confidence": float(box.conf.item()),
                        "bbox": box.xyxy.tolist()[0]  # [x1, y1, x2, y2]
                    })
        
        return {
            "success": True,
            "detections": detections,
            "result_image_id": temp_id,
            "total_detections": len(detections),
            "inference_time": time.time() - time.time()  # 실제로는 시간 측정 로직 추가
        }
        
    except Exception as e:
        return JSONResponse(
            status_code=500,
            content={"success": False, "error": str(e)}
        )

@app.get("/api/inference/result/{result_id}")
async def get_result_image(result_id: str):
    """추론 결과 이미지 다운로드 - 기존 구조 사용"""
    result_path = Path(f"/app/runs/result/image_{result_id}.jpg")
    
    if not result_path.exists():
        raise HTTPException(status_code=404, detail="Result image not found")
    
    return FileResponse(path=str(result_path), media_type="image/jpeg")

@app.get("/api/training/download/{task_id}")
async def download_model(task_id: str):
    """학습된 모델 다운로드 - 기존 best.pt 경로 사용"""
    if task_id not in TRAINING_STATUS:
        raise HTTPException(status_code=404, detail="Task not found")
    
    model_path = TRAINING_STATUS[task_id].get("model_path")
    if not model_path or not os.path.exists(model_path):
        # 기본 경로에서 찾기
        default_model_path = Path("/app/runs/detect/train/weights/best.pt")
        if default_model_path.exists():
            model_path = str(default_model_path)
        else:
            raise HTTPException(status_code=404, detail="Model file not found")
    
    return FileResponse(
        path=model_path,
        filename=f"trained_model_{task_id}.pt",
        media_type="application/octet-stream"
    )

@app.get("/api/training/results/{task_id}")
async def get_training_results_image(task_id: str):
    """학습 결과 그래프 다운로드 - 기존 results.png 경로 사용"""
    if task_id not in TRAINING_STATUS:
        raise HTTPException(status_code=404, detail="Task not found")
    
    results_dir = TRAINING_STATUS[task_id].get("results_dir", "/app/runs/detect/train")
    results_path = Path(results_dir) / "results.png"
    
    if not results_path.exists():
        # 대체 경로들 확인
        alternative_paths = [
            Path(results_dir) / "confusion_matrix.png",
            Path(results_dir) / "val_batch0_pred.jpg",
            Path("/app/runs/detect/train/results.png")  # 기본 경로
        ]
        
        for alt_path in alternative_paths:
            if alt_path.exists():
                results_path = alt_path
                break
        else:
            raise HTTPException(status_code=404, detail="Results image not found")
    
    return FileResponse(path=str(results_path), media_type="image/png")

if __name__ == "__main__":
    uvicorn.run(app, host="0.0.0.0", port=8000)