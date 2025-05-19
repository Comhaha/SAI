@echo off
chcp 65001 > nul
SETLOCAL ENABLEDELAYEDEXPANSION

REM 현재 배치 파일의 위치를 기준으로 상대 경로 설정
SET "TARGET_DIR=%~dp0app\SAI\SAI\SAI.Application\Python"
SET "VENV_DIR=%TARGET_DIR%\venv"

REM 가상환경 생성
IF EXIST "%VENV_DIR%\Scripts\activate" (
    echo ✅ 가상환경이 이미 존재합니다: "%VENV_DIR%"
) ELSE (
    echo 🐍 Python 3.9 가상환경 생성 중...
    py -3.9 -m venv "%VENV_DIR%"
    echo ✅ 생성 완료: "%VENV_DIR%"
)

REM 가상환경 활성화
echo 🔄 가상환경 활성화 중...
call "%VENV_DIR%\Scripts\activate"

REM 셸 유지
echo 💡 가상환경이 활성화되었습니다.
start "" "%VENV_DIR%"
cmd /k 