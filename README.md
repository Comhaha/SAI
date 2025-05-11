# <img src="https://lab.ssafy.com/s12-final/S12P31D201/-/wikis/uploads/a1754d0a91b9c80ddc92d37114703c71/icon192.png" width="20" alt="로고"> SAI(SSAFY AI Interface)

[<img src="https://lab.ssafy.com/s12-final/S12P31D201/-/wikis/uploads/1158c1d86fd8f9aa5db790cc2160a5a8/%EC%8A%A4%ED%94%8C%EB%9E%98%EC%8B%9C.png">]()

#### 👆 위 이미지 클릭하면 `랜딩페이지로` 이동합니다 👆

<br/>

# 목차

1. [**개요**](#✨-개요)
1. [**주요 기능**](#-주요-기능)
1. [**서비스 화면 (유저 / 관리자)**](<#-꼼대-서비스-화면-(유저)>)
1. [**기술 스택**](#-기술-스택)
1. [**프로젝트 진행 및 산출물**](#-프로젝트-진행-및-산출물)
1. [**개발 멤버 및 회고**](#-개발-멤버-및-역할분담)
1. [**메뉴얼 및 상세문서**](#-메뉴얼-및-상세-문서)

<br/>

<div id="1"></div>

# ✨ 개요

#### 서비스명 : SAI

#### 한줄 설명 : AI 모델 학습 과정을 블록 코딩 방식으로 구현하여 누구나 쉽게 실습할 수 있는 교육용 플랫폼

#### 프로젝트 기간 : 2025.04.14 ~ 2025.05.22

<br/>

<div id="2"></div>

# ✨ 주요 기능

#### 서비스 설명

-   SAI는 AI 학습에 대한 진입 장벽을 낮추기 위해 설계된 SSAFY 교육생 맞춤형 블록 코딩 플랫폼입니다.
-   실제 AI 모델 학습 흐름(pip install, 데이터 로딩, 학습, 추론 등)을 시각적으로 구성하여 실습 중심의 학습을 지원합니다.

#### 주요 기능

-   **AI 블록 코딩**

    -   pip 설치, 모델 불러오기, 데이터셋 불러오기, 파라미터 설정 등 학습 과정을 블록으로 구성
    -   Run 버튼 클릭만으로 실제 학습 코드 실행 가능

-   **실시간 파이썬 코드 변환**

    -   사용자가 블록을 조립하는 즉시 해당 흐름이 파이썬 코드로 변환되어 표시됨
    -   코드 구조를 직관적으로 이해하고 실전 환경에도 대비 가능

-   **지능형 피드백 시스템**

    -   오류 블록 원인 분석 및 해결 방법 제시
    -   학습 결과(지표/그래프)에 대한 해석 및 성능 개선을 위한 파라미터 추천

-   **튜토리얼/실습 모드 지원**

    -   튜토리얼: 라벨링부터 추론까지 고정된 파라미터로 단계별 학습 가이드
    -   실습: 사용자가 직접 파라미터를 설정하여 반복 실습 가능

-   **학습 결과 Export 기능**
    -   학습 완료 후 생성된 코드와 피드백을 Notion에 내보내기 지원
    -   학습 모델 다운로드 및 코드 복사, 자동 주석 기능 포함

<br/>

<div id="3"></div>

# 서비스 화면

<br/>

<div id="4"></div>

# 📚 기술 스택 (수정 예정)

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

## 서비스 아키텍처

<img src="https://lab.ssafy.com/s12-final/S12P31D201/-/wikis/uploads/f4c4ef8fdbc0ef323a034dd45d7cd662/%EC%8B%9C%EC%8A%A4%ED%85%9C_%EC%95%84%ED%82%A4%ED%85%8D%EC%B2%98.png">

<br/>

<div id="5"></div>

# ✨ 프로젝트 진행 및 산출물

## 화면 설계서

## API 명세서

<img src="https://lab.ssafy.com/s12-final/S12P31D201/-/wikis/uploads/5fdbef1b691bf64fd0c1f9acfdc263c6/image.png">

## ERD

<img src="https://lab.ssafy.com/s12-final/S12P31D201/-/wikis/uploads/b25d73d3d9c65fd1369ac4b61c47d0dd/image.png">

## Git

<!-- -   [Git Convention 노션](https://thinkable-bear-51d.notion.site/1a4c2f3f4a77815299c7feb0724d372c?pvs=4) -->
<!-- -   [Git Wiki](https://lab.ssafy.com/s12-ai-image-sub1/S12P21D101/-/wikis/Git) -->

<br/>

<div id="6"></div>

# 👨‍👩‍👧‍👦 개발 멤버 및 역할분담

<table width="100%">
  <tr>
    <td width="16.6%" align="center"><a href="https://github.com/breadbirds"><strong>정유진</strong></a></td>
    <td width="16.6%" align="center"><strong>권동현</strong></td>
    <td width="16.6%" align="center"><strong>김정언</strong></td>
    <td width="16.6%" align="center"><strong>도승국</strong></td>
    <td width="16.6%" align="center"><strong>박재영</strong></td>
    <td width="16.6%" align="center"><strong>조혜정</strong></td>
  </tr>
  <tr>
    <td width="16.6%" align="center"><img src="https://lab.ssafy.com/s12-final/S12P31D201/-/wikis/uploads/5ebc5118139ebdecb3f59ae732f3213d/%EC%A0%95%EC%9C%A0%EC%A7%84.png" width="150"></td>
    <td width="16.6%" align="center"><img src="https://lab.ssafy.com/s12-final/S12P31D201/-/wikis/uploads/1cb66373687b47ae6e04f15a4490b9ef/%EA%B6%8C%EB%8F%99%ED%98%84.png" width="150"></td>
    <td width="16.6%" align="center"><img src="https://lab.ssafy.com/s12-final/S12P31D201/-/wikis/uploads/6428dd61a1f361ad6eb05efc7cfbc5ae/%EA%B9%80%EC%A0%95%EC%96%B8.png" width="150"></td>
    <td width="16.6%" align="center"><img src="https://lab.ssafy.com/s12-final/S12P31D201/-/wikis/uploads/67548002f143ad004f061e3d9def8fb2/%EB%8F%84%EC%8A%B9%EA%B5%AD.png" width="150"></td>
    <td width="16.6%" align="center"><img src="https://lab.ssafy.com/s12-final/S12P31D201/-/wikis/uploads/1cd61ace7afbc6dbe7938376030633fe/%EB%B0%95%EC%9E%AC%EC%98%81.png" width="150"></td>
    <td width="16.6%" align="center"><img src="https://lab.ssafy.com/s12-final/S12P31D201/-/wikis/uploads/72da216584bd09d0c07c74f2fb186c16/%EC%A1%B0%ED%98%9C%EC%A0%95.png" width="150"></td>
  </tr>
  <tr>
    <td width="16.6%" align="center">Leader & AI & Training</td>
    <td width="16.6%" align="center">Backend & Infra & Inference</td>
    <td width="16.6%" align="center">UI/UX & Block UI<br>& Real-time Code View</td>
    <td width="16.6%" align="center">UI/UX & Labeling</td>
    <td width="16.6%" align="center">WinForms UI Structure<br>& Component Implementation</td>
    <td width="16.6%" align="center">UI/UX & Memo Editor<br>& Code Highlighting & Landing Page</td>
  </tr>
</table>

<br/>

<div id="7"></div>

# 📒 메뉴얼 및 상세 문서

-   [포팅메뉴얼](https://lab.ssafy.com/s12-final/S12P31D201/-/wikis/%ED%8F%AC%ED%8C%85-%EB%A9%94%EB%89%B4%EC%96%BC)
-   [Git]()
-   [화면 설계서]
