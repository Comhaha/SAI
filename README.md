# <img src="https://lab.ssafy.com/s12-final/S12P31D201/-/wikis/uploads/a1754d0a91b9c80ddc92d37114703c71/icon192.png" width="20" alt="로고"> SAI(SSAFY AI Interface)

[<img src="https://lab.ssafy.com/s12-final/S12P31D201/-/wikis/uploads/136ab697f8e86a4bf4d8bd7b0484de54/%EC%8A%A4%ED%94%8C%EB%9E%98%EC%8B%9C_%EB%A1%9C%EA%B3%A0_%EB%B0%94%EA%BE%BCver.png">](https://k12d201.p.ssafy.io/)

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

### [서비스 상세 화면 보러가기↗️](https://lab.ssafy.com/s12-final/S12P31D201/-/wikis/%EC%84%9C%EB%B9%84%EC%8A%A4-%ED%99%94%EB%A9%B4)

<br/>

<div id="5"></div>

# 📚 기술 스택

### Backend

<div align=left> 
  <img src="https://img.shields.io/badge/java-007396?style=flat-square&logo=java&logoColor=white">
  <img src="https://img.shields.io/badge/postgresql-4169E1?style=flat-square&logo=postgresql&logoColor=white"> 
  <img src="https://img.shields.io/badge/swagger-85EA2D?style=flat-square&logo=swagger&logoColor=white">
  <img src="https://img.shields.io/badge/postman-FF6C37?style=flat-square&logo=postman&logoColor=white">
  <img src="https://img.shields.io/badge/intellijidea-000000?style=flat-square&logo=intellijidea&logoColor=white">
  <img src="https://img.shields.io/badge/spring-6DB33F?style=flat-square&logo=spring&logoColor=white">
  <img src="https://img.shields.io/badge/springboot-6DB33F?style=flat-square&logo=springboot&logoColor=white">
   <img src="https://img.shields.io/badge/springsecurity-6DB33F?style=flat-square&logo=springsecurity&logoColor=white">
  <img src="https://img.shields.io/badge/openjdk-000000?style=flat-square&logo=openjdk&logoColor=white">

</div>

### Frontend

<div align=left> 
  <img src="https://img.shields.io/badge/gradle-02303A?style=flat-square&logo=gradle&logoColor=white">
  <img src="https://img.shields.io/badge/openjdk-000000?style=flat-square&logo=openjdk&logoColor=white">
  <img src="https://img.shields.io/badge/lottiefiles-00DDB3?style=flat-square&logo=lottiefiles&logoColor=white">
  <img src="https://img.shields.io/badge/react-61DAFB?style=flat-square&logo=react&logoColor=white">
</div>

### Desktop Application

<div align=left> 
  <img src="https://img.shields.io/badge/Csharp-00599C?style=flat-square&logo=&logoColor=white">
  <img src="https://img.shields.io/badge/VisualStudio-663399?style=flat-square&logo=&logoColor=white">
  <img src="https://img.shields.io/badge/winforms-00DDB3?style=flat-square&logo=&logoColor=white">
  <img src="https://img.shields.io/badge/blockly-61DAFB?style=flat-square&logo=&logoColor=white">
  <img src="https://img.shields.io/badge/Guna-663399?style=flat-square&logo=&logoColor=white">

</div>

### AI

<div align=left> 
  <img src="https://img.shields.io/badge/python-3776AB?style=flat-square&logo=python&logoColor=white">
  <img src="https://img.shields.io/badge/yolo-111F68?style=flat-square&logo=yolo&logoColor=white">
  <img src="https://img.shields.io/badge/opencv-5C3EE8?style=flat-square&logo=opencv&logoColor=white">
  <img src="https://img.shields.io/badge/pytorch-EE4C2C?style=flat-square&logo=pytorch&logoColor=white">
  <img src="https://img.shields.io/badge/jupyter-F37626?style=flat-square&logo=jupyter&logoColor=white">
  <img src="https://img.shields.io/badge/googlecolab-F9AB00?style=flat-square&logo=googlecolab&logoColor=white">

</div>

### Infra

<div align=left> 
  <img src="https://img.shields.io/badge/docker-2496ED?style=flat-square&logo=docker&logoColor=white">
  <img src="https://img.shields.io/badge/jenkins-D24939?style=flat-square&logo=jenkins&logoColor=white">
  <img src="https://img.shields.io/badge/nginx-009639?style=flat-square&logo=nginx&logoColor=white">
  <img src="https://img.shields.io/badge/letsencrypt-003A70?style=flat-square&logo=letsencrypt&logoColor=white">
  <img src="https://img.shields.io/badge/amazonec2-FF9900?style=flat-square&logo=amazonec2&logoColor=white">
  <img src="https://img.shields.io/badge/amazons3-569A31?style=flat-square&logo=amazons3&logoColor=white">

</div>

### Project Management & DevOps

<div align=left> 
  <img src="https://img.shields.io/badge/git-F05032?style=flat-square&logo=git&logoColor=white">
  <img src="https://img.shields.io/badge/gitlab-FC6D26?style=flat-square&logo=gitlab&logoColor=white">
  <img src="https://img.shields.io/badge/mattermost-0058CC?style=flat-square&logo=mattermost&logoColor=white">
  <img src="https://img.shields.io/badge/notion-000000?style=flat-square&logo=notion&logoColor=white">
  <img src="https://img.shields.io/badge/jira-0052CC?style=flat-square&logo=jira&logoColor=white">
  <img src="https://img.shields.io/badge/discord-5865F2?style=flat-square&logo=discord&logoColor=white">
  <img src="https://img.shields.io/badge/googledrive-4285F4?style=flat-square&logo=googledrive&logoColor=white">
 
</div>

## 기술적 특징

### 1. JS <-> C# <-> Python 데이터 흐름 자동화

-   사용자가 블럭을 조립하면 학습 및 추론이 가능한 직관적인 구조

    <img src="https://lab.ssafy.com/s12-final/S12P31D201/-/wikis/uploads/db1f1801a131d82a33aedb8b54addeb0/%EC%9E%90%EC%9C%A8%ED%94%84%EB%A1%9C%EC%A0%9D%ED%8A%B8_%EC%B5%9C%EC%A2%85%EB%B0%9C%ED%91%9C.jpg" width="800">

### 2. 간단한 파라미터 조정으로 모델 구조와 성능을 체감할 수 있는 데이터셋 제공

-   튜토리얼 모드는 `바나나` 데이터셋, 실습 모드는 `가위바위보` 데이터셋을 제공

    <img src="https://lab.ssafy.com/s12-final/S12P31D201/-/wikis/uploads/4d8ac409b7820af96324ba68bccc8b81/%EC%9E%90%EC%9C%A8%ED%94%84%EB%A1%9C%EC%A0%9D%ED%8A%B8_%EA%B8%B0%EC%88%A0%EC%84%A4%EB%AA%85.jpg" width="800">

## 시스템 아키텍처

<img src="https://lab.ssafy.com/s12-final/S12P31D201/-/wikis/uploads/5ed9f4335e896c6d611bc5199bbf0022/image__6_.png" width="800">

<br/>

<div id="6"></div>

# ✨ 프로젝트 진행 및 산출물

## 화면 설계서

#### [피그마 바로가기 ↗️](https://www.figma.com/design/ku5Sdq7QepQOk2uL4EeMZr/%EC%9E%90%EC%9C%A8-PJT--%EA%B0%80%EC%A0%9C-?node-id=450-9380)

<img src="https://lab.ssafy.com/s12-final/S12P31D201/-/wikis/uploads/a75912d77c3294adb49049475234fefb/%ED%99%94%EB%A9%B4%EC%84%A4%EA%B3%84%EC%84%9C.webp" width="800">

## API 명세서

<img src="https://lab.ssafy.com/s12-final/S12P31D201/-/wikis/uploads/5fdbef1b691bf64fd0c1f9acfdc263c6/image.png" width="800">

## ERD

<img src="https://lab.ssafy.com/s12-final/S12P31D201/-/wikis/uploads/b25d73d3d9c65fd1369ac4b61c47d0dd/image.png" width="800">

## Git

<!-- -   [Git Convention 노션](https://thinkable-bear-51d.notion.site/1a4c2f3f4a77815299c7feb0724d372c?pvs=4) -->
<!-- -   [Git Wiki](https://lab.ssafy.com/s12-ai-image-sub1/S12P21D101/-/wikis/Git) -->

<br/>

<div id="7"></div>

<table>
  <tr>
    <!-- 1 -->
    <td align="center" width="260">
      <img src="https://lab.ssafy.com/s12-final/S12P31D201/-/wikis/uploads/5ebc5118139ebdecb3f59ae732f3213d/%EC%A0%95%EC%9C%A0%EC%A7%84.png" width="100"><br>
      <a href="https://github.com/breadbirds"><strong>정유진</strong></a>
    </td>
    <!-- 2 -->
    <td align="center" width="260">
      <img src="https://lab.ssafy.com/s12-final/S12P31D201/-/wikis/uploads/1cb66373687b47ae6e04f15a4490b9ef/%EA%B6%8C%EB%8F%99%ED%98%84.png" width="100"><br>
      <a href="https://github.com/DonghyeonKwon"><strong>권동현</strong></a>
    </td>
    <!-- 3 -->
    <td align="center" width="260">
      <img src="https://lab.ssafy.com/s12-final/S12P31D201/-/wikis/uploads/6428dd61a1f361ad6eb05efc7cfbc5ae/%EA%B9%80%EC%A0%95%EC%96%B8.png" width="100"><br>
      <a href="https://github.com/JeongEon8"><strong>김정언</strong></a>
    </td>
  </tr>
  <tr>
    <td>
      - AI model design<br>
      - Training pipeline implementation
    </td>
    <td>
      - Backend server development<br>
      - Prompt-based Notion export implementation
    </td>
    <td>
      - Block coding UI/UX design<br>
      - Real-time code conversion
    </td>
  </tr>
  <tr>
    <!-- 4 -->
    <td align="center">
      <img src="https://lab.ssafy.com/s12-final/S12P31D201/-/wikis/uploads/67548002f143ad004f061e3d9def8fb2/%EB%8F%84%EC%8A%B9%EA%B5%AD.png" width="100"><br>
      <a href="https://github.com/DoSeungGuk"><strong>도승국</strong></a>
    </td>
    <!-- 5 -->
    <td align="center">
      <img src="https://lab.ssafy.com/s12-final/S12P31D201/-/wikis/uploads/1cd61ace7afbc6dbe7938376030633fe/%EB%B0%95%EC%9E%AC%EC%98%81.png" width="100"><br>
      <a href="https://github.com/estel2005"><strong>박재영</strong></a>
    </td>
    <!-- 6 -->
    <td align="center">
      <img src="https://lab.ssafy.com/s12-final/S12P31D201/-/wikis/uploads/72da216584bd09d0c07c74f2fb186c16/%EC%A1%B0%ED%98%9C%EC%A0%95.png" width="100"><br>
      <a href="https://github.com/joehyejeong"><strong>조혜정</strong></a>
    </td>
  </tr>
  <tr>
    <td>
      - Labeling interface UI/UX<br>
      - Full labeling feature development
    </td>
    <td>
      - WinForms UI screen implementation<br>
      - Reusable component implementation
    </td>
    <td>
      - Memo editor development<br>
      - Code highlighting feature<br>
      - Landing page UI design
    </td>
  </tr>
</table>

<br/>

<img src="https://lab.ssafy.com/s12-final/S12P31D201/-/wikis/uploads/669e161633a410171f1cee4b2e1c22be/%EC%97%AD%ED%95%A0%EB%B6%84%EB%8B%B4.jpg" width="800">

<div id="8"></div>

# 📒 메뉴얼 및 상세 문서

-   [포팅메뉴얼](https://lab.ssafy.com/s12-final/S12P31D201/-/wikis/%ED%8F%AC%ED%8C%85-%EB%A9%94%EB%89%B4%EC%96%BC)
-   [Git]()
-   [서비스 화면](https://lab.ssafy.com/s12-final/S12P31D201/-/wikis/%EC%84%9C%EB%B9%84%EC%8A%A4-%ED%99%94%EB%A9%B4)
