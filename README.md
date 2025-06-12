# <img src="/docs/icon192.png" width="20" alt="로고"> SAI(SSAFY AI Interface)

[<img src="/docs/스플래시_로고_바꾼ver.png">](https://k12d201.p.ssafy.io/)

#### 👆 위 이미지 클릭하면 `랜딩페이지로` 이동합니다 👆

<br/>

# 목차

1. [**개요**](#✨-개요)
1. [**기획 배경**](#-기획-배경)
1. [**주요 기능**](#-주요-기능)
1. [**서비스 화면**](#-서비스-화면)
1. [**기술 스택**](#-기술-스택)
1. [**프로젝트 진행 및 산출물**](#-프로젝트-진행-및-산출물)
1. [**개발 멤버 및 회고**](#-개발-멤버-및-역할분담)
1. [**메뉴얼 및 상세문서**](#-메뉴얼-및-상세-문서)

<br/>

<div id="1"></div>

# ✨ 개요

#### 🥇 SSAFY 자율 PJT 1등 🥇 | 🏆 SSAFY 자율 PJT 전시팀 선정 🏆 | [영상 포트폴리오 보러가기 ▶️](https://youtu.be/ww6OEHa8NBc)

#### 서비스명 : SAI

#### 한줄 설명 : AI 모델 학습 과정을 블록 코딩 방식으로 구현하여 누구나 쉽게 실습할 수 있는 교육용 플랫폼

#### 프로젝트 기간 : 2025.04.14 ~ 2025.05.22

<br/>

<div id="2"></div>

# ✨ 기획 배경

-   **진입 장벽 완화**  
    기존 AI 실습 환경은 설치·구성 난이도가 높아 초보자가 쉽게 접근하기 어려웠습니다.

-   **실습 중심 학습**  
    이론 강의만으로 실습으로 넘어가기 어려운 학습자들을 위해, 블록 인터페이스로 단계별 흐름을 시각화하고 실전 경험을 제공하고자 했습니다.

-   **SSAFY 명칭 변경 및 커리큘럼 확대**  
    ‘SSAFY’가 ‘SSAFY Software AI Academy’로 변경되면서 AI 활용 역량 강화를 목표로, 프로젝트에서 자주 사용하는 AI를 직접 실습하는 과정이 교육과정에 추가되었습니다.

-   **다리 역할 수행**  
    이론 강의와 실제 실습 사이에 놓인 ‘다리’ 역할을 하여, 학습자들이 부담 없이 AI 모델 학습 과정을 체험할 수 있도록 지원합니다.

-   **로컬 기반 실습 환경**  
    GPU 서버 비용 문제와 동시 접속 한계를 해소하기 위해, 모든 AI 학습·추론 과정을 사용자의 로컬 PC에서 실행하도록 설계했습니다.

<div id="3"></div>

# ✨ 주요 기능

#### 서비스 설명

-   SAI는 AI 학습에 대한 진입 장벽을 낮추기 위해 설계된 SSAFY 교육생 맞춤형 블록 코딩 플랫폼입니다.
-   실제 AI 모델 학습 흐름(pip install, 데이터 로딩, 학습, 추론 등)을 시각적으로 구성하여 실습 중심의 학습을 지원합니다.

#### 주요 기능

-   **AI 블록 코딩**

    -   pip 설치, 모델 불러오기, 데이터셋 불러오기, 파라미터 설정 등 학습 과정을 블록으로 구성
    -   Run 버튼 클릭만으로 실제 학습 코드 실행 가능

-   **라벨링 체험 가능**

    -   이미지 탐지 모델 학습에 가장중요한 라벨링 (Classification, BoundingBox, Segmentation) 체험 가능
    -   라벨링에서 사용한 데이터는 실제 학습에 포함되지 않음

-   **실시간 파이썬 코드 변환**

    -   사용자가 블록을 조립하는 즉시 해당 흐름이 파이썬 코드로 변환되어 표시됨
    -   코드 구조를 직관적으로 이해하고 실전 환경에도 대비 가능

-   **학습 결과 기반 AI 피드백 시스템**

    -   오류 블록 원인 분석 및 해결 방법 제시
    -   학습 결과(지표/그래프)에 대한 해석 및 성능 개선을 위한 파라미터 추천

-   **튜토리얼/실습 모드 지원**

    -   튜토리얼: 라벨링부터 추론까지 고정된 파라미터로 단계별 학습 가이드
    -   실습: 사용자가 직접 파라미터를 설정하여 반복 실습 가능

-   **학습 결과 Export 기능**
    -   학습 완료 후 생성된 코드와 피드백을 Notion에 내보내기 지원
    -   학습 모델 다운로드 및 코드 복사, 자동 주석 기능 포함

<br/>

<div id="4"></div>

# ✨ 서비스 화면

### [서비스 상세 화면 보러가기↗️](https://github.com/breadbirds/SAI/wiki/%EC%84%9C%EB%B9%84%EC%8A%A4-%ED%99%94%EB%A9%B4)

<br/>

<div id="5"></div>

# 📚 기술 스택

### Backend

<div align=left> 
  <img src="https://img.shields.io/badge/java-007396?style=for-the-badge&logo=java&logoColor=white">
  <img src="https://img.shields.io/badge/postgresql-4169E1?style=for-the-badge&logo=postgresql&logoColor=white"> 
  <img src="https://img.shields.io/badge/swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=white">
  <img src="https://img.shields.io/badge/postman-FF6C37?style=for-the-badge&logo=postman&logoColor=white">
  <img src="https://img.shields.io/badge/intellijidea-000000?style=for-the-badge&logo=intellijidea&logoColor=white">
  <img src="https://img.shields.io/badge/spring-6DB33F?style=for-the-badge&logo=spring&logoColor=white">
  <img src="https://img.shields.io/badge/springboot-6DB33F?style=for-the-badge&logo=springboot&logoColor=white">
   <img src="https://img.shields.io/badge/springsecurity-6DB33F?style=for-the-badge&logo=springsecurity&logoColor=white">
  <img src="https://img.shields.io/badge/openjdk-000000?style=for-the-badge&logo=openjdk&logoColor=white">

</div>

### Frontend

<div align=left> 
  <img src="https://img.shields.io/badge/gradle-02303A?style=for-the-badge&logo=gradle&logoColor=white">
  <img src="https://img.shields.io/badge/openjdk-000000?style=for-the-badge&logo=openjdk&logoColor=white">
  <img src="https://img.shields.io/badge/lottiefiles-00DDB3?style=for-the-badge&logo=lottiefiles&logoColor=white">
  <img src="https://img.shields.io/badge/react-61DAFB?style=for-the-badge&logo=react&logoColor=white">
</div>

### Desktop Application

<div align=left> 
  <img src="https://img.shields.io/badge/Csharp-00599C?style=for-the-badge&logo=&logoColor=white">
  <img src="https://img.shields.io/badge/VisualStudio-663399?style=for-the-badge&logo=&logoColor=white">
  <img src="https://img.shields.io/badge/winforms-00DDB3?style=for-the-badge&logo=&logoColor=white">
  <img src="https://img.shields.io/badge/blockly-61DAFB?style=for-the-badge&logo=&logoColor=white">
  <img src="https://img.shields.io/badge/Guna-663399?style=for-the-badge&logo=&logoColor=white">

</div>

### AI

<div align=left> 
  <img src="https://img.shields.io/badge/python-3776AB?style=for-the-badge&logo=python&logoColor=white">
  <img src="https://img.shields.io/badge/yolo-111F68?style=for-the-badge&logo=yolo&logoColor=white">
  <img src="https://img.shields.io/badge/opencv-5C3EE8?style=for-the-badge&logo=opencv&logoColor=white">
  <img src="https://img.shields.io/badge/pytorch-EE4C2C?style=for-the-badge&logo=pytorch&logoColor=white">
  <img src="https://img.shields.io/badge/jupyter-F37626?style=for-the-badge&logo=jupyter&logoColor=white">
  <img src="https://img.shields.io/badge/googlecolab-F9AB00?style=for-the-badge&logo=googlecolab&logoColor=white">

</div>

### Infra

<div align=left> 
  <img src="https://img.shields.io/badge/docker-2496ED?style=for-the-badge&logo=docker&logoColor=white">
  <img src="https://img.shields.io/badge/jenkins-D24939?style=for-the-badge&logo=jenkins&logoColor=white">
  <img src="https://img.shields.io/badge/nginx-009639?style=for-the-badge&logo=nginx&logoColor=white">
  <img src="https://img.shields.io/badge/letsencrypt-003A70?style=for-the-badge&logo=letsencrypt&logoColor=white">
  <img src="https://img.shields.io/badge/amazonec2-FF9900?style=for-the-badge&logo=amazonec2&logoColor=white">
  <img src="https://img.shields.io/badge/amazons3-569A31?style=for-the-badge&logo=amazons3&logoColor=white">

</div>

### Project Management & DevOps

<div align=left> 
  <img src="https://img.shields.io/badge/git-F05032?style=for-the-badge&logo=git&logoColor=white">
  <img src="https://img.shields.io/badge/gitlab-FC6D26?style=for-the-badge&logo=gitlab&logoColor=white">
  <img src="https://img.shields.io/badge/mattermost-0058CC?style=for-the-badge&logo=mattermost&logoColor=white">
  <img src="https://img.shields.io/badge/notion-000000?style=for-the-badge&logo=notion&logoColor=white">
  <img src="https://img.shields.io/badge/jira-0052CC?style=for-the-badge&logo=jira&logoColor=white">
  <img src="https://img.shields.io/badge/discord-5865F2?style=for-the-badge&logo=discord&logoColor=white">
  <img src="https://img.shields.io/badge/googledrive-4285F4?style=for-the-badge&logo=googledrive&logoColor=white">
 
</div>

## 기술적 특징

### 1. JS <-> C# <-> Python 데이터 흐름 자동화

-   사용자가 블럭을 조립하면 학습 및 추론이 가능한 직관적인 구조

    <img src="/docs/자율프로젝트_최종발표.jpg" width="800">

### 2. 간단한 파라미터 조정으로 모델 구조와 성능을 체감할 수 있는 데이터셋 제공

-   튜토리얼 모드는 `바나나` 데이터셋, 실습 모드는 `가위바위보` 데이터셋을 제공

    <img src="/docs/자율프로젝트_기술설명.jpg" width="800">

### 3. 서비스 내에서 로컬 GPU를 사용한 학습 및 추론 가능

- 로컬 GPU를 사용한 학습 및 추론이 가능
- 로컬 GPU 뿐만 아니라 서버 GPU도 선택 가능

### 4. 가상환경 기반으로 별도의 환경 설정 없이 즉시 실행 가능

- 프로젝트 내 Python 가상환경에 모든 라이브러리 및 버전이 사전 설정되어 있음
- 추가 설치 과정 없이 코드 실행 및 모델 학습 가능

## 시스템 아키텍처

<img src="/docs/시스템아키텍처.png" width="800">

<br/>

<div id="6"></div>

# ✨ 프로젝트 진행 및 산출물

## 화면 설계서

#### [피그마 바로가기 ↗️](https://www.figma.com/design/ku5Sdq7QepQOk2uL4EeMZr/%EC%9E%90%EC%9C%A8-PJT--%EA%B0%80%EC%A0%9C-?node-id=450-9380)

<img src="/docs/화면설계서1.webp" width="800">

<img src="/docs/화면설계서2.webp" width="800">

## API 명세서

<img src="/docs/api 설계서.png" width="800">

## ERD

<img src="/docs/ERD.png" width="800">

## Git
<!-- -   [Git Convention 노션](https://thinkable-bear-51d.notion.site/1a4c2f3f4a77815299c7feb0724d372c?pvs=4) -->
<!-- -   [Git Wiki](https://lab.ssafy.com/s12-ai-image-sub1/S12P21D101/-/wikis/Git) -->

<br/>

<div id="7"></div>

## 👥 Team Members

<table>
  <tr>
    <td align="center" width="200">
      <a href="https://github.com/breadbirds">
        <img src="https://github.com/breadbirds.png" width="100" style="border-radius: 50%;">
        <br>
        <strong>정유진</strong>
      </a>
    </td>
    <td align="center" width="200">
      <a href="https://github.com/DonghyeonKwon">
        <img src="https://github.com/DonghyeonKwon.png" width="100" style="border-radius: 50%;">
        <br>
        <strong>권동현</strong>
      </a>
    </td>
    <td align="center" width="200">
      <a href="https://github.com/JeongEon8">
        <img src="https://github.com/JeongEon8.png" width="100" style="border-radius: 50%;">
        <br>
        <strong>김정언</strong>
      </a>
    </td>
  </tr>
  <tr>
    <td align="center">
      <sub>AI 학습 블럭 설계<br>학습, 추론 로직 구현<br>로컬 GPU 사용 및 가상환경 세팅</sub>
    </td>
    <td align="center">
      <sub>백엔드 서버 개발<br>AI 모델 성능 보고서<br>노션 export</sub>
    </td>
    <td align="center">
      <sub>블록 UI/UX 구현<br>실시간 Python코드 변환<br>MVP패턴 설계</sub>
    </td>
  </tr>
  <tr>
    <td align="center" width="200">
      <a href="https://github.com/DoSeungGuk">
        <img src="https://github.com/DoSeungGuk.png" width="100" style="border-radius: 50%;">
        <br>
        <strong>도승국</strong>
      </a>
    </td>
    <td align="center" width="200">
      <a href="https://github.com/estel2005">
        <img src="https://github.com/estel2005.png" width="100" style="border-radius: 50%;">
        <br>
        <strong>박재영</strong>
      </a>
    </td>
    <td align="center" width="200">
      <a href="https://github.com/joehyejeong">
        <img src="https://github.com/joehyejeong.png" width="100" style="border-radius: 50%;">
        <br>
        <strong>조혜정</strong>
      </a>
    </td>
  </tr>
  <tr>
    <td align="center">
      <sub>라벨링 기능 및 UI/UX 구현<br>GPU 서버 연동, api 설계</sub>
    </td>
    <td align="center">
      <sub>WinForms UI 구현<br>재사용 가능한 컴포넌트 개발<br>UI/UX 디자인</sub>
    </td>
    <td align="center">
      <sub>메모장, 코드 하이라이팅 구현<br>랜딩페이지 UI 디자인</sub>
    </td>
  </tr>
</table>

<img src="/docs/역할분담.jpg">

<div id="8"></div>

# 📒 메뉴얼 및 상세 문서

-   [포팅메뉴얼](https://github.com/breadbirds/SAI/wiki/%ED%8F%AC%ED%8C%85-%EB%A9%94%EB%89%B4%EC%96%BC)
-   [Git]()
-   [서비스 화면](https://github.com/breadbirds/SAI/wiki/%EC%84%9C%EB%B9%84%EC%8A%A4-%ED%99%94%EB%A9%B4)
