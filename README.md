# <img src="/docs/icon192.png" width="20" alt="로고"> SAI

[<img src="/docs/스플래시_로고_바꾼ver.png">](https://k12d201.p.ssafy.io/)

**👆 위 이미지 클릭하면 `랜딩페이지로` 이동합니다 👆**

<br/>

## ✨ 개요

### 🥇 SSAFY 12기 자율 PJT 1등 🥇 | 🏆 SSAFY 12기 자율 PJT 전시회 입상 🏆

**서비스명** : SAI

**한줄 설명** : AI 모델 학습 과정을 블록 코딩 방식으로 구현하여 누구나 쉽게 실습할 수 있는 교육용 플랫폼

**도메인** : 오픈소스

**팀원** : FE 명 / BE 2명 / AI 1명 (6명)

**프로젝트 기간** : 2025.04.14 ~ 2025.05.22 (6주)

[▶️영상 포트폴리오 보러가기](https://youtu.be/ww6OEHa8NBc)

<br/>

<div id="2"></div>

## 목차

1. [**기획 배경**](#-기획-배경)
1. [**주요 기능**](#-주요-기능)
1. [**주요 기능별 화면**](#-주요-기능별-화면)
1. [**기술적 특징**](#-기술적-특징)
1. [**기술 스택**](#-기술-스택)
1. [**프로젝트 진행 및 산출물**](#-프로젝트-진행-및-산출물)
1. [**개발 멤버 및 회고**](#-개발-멤버-및-역할분담)
1. [**메뉴얼 및 상세문서**](#-메뉴얼-및-상세-문서)

<br/>

<div id="2"></div>


## ✨ 기획 배경

- **AI 실습의 진입 장벽 해소**  
  복잡한 설치·환경 구성을 줄이고, 초보자도 쉽게 시작할 수 있도록 설계

- **이론과 실습을 연결하는 구조 제공**  
  블록 기반 인터페이스로 AI 학습 흐름을 시각화해, 실습 중심의 학습 경험 지원

- **SSAFY AI 커리큘럼 확대에 따른 실습 환경 구축**  
  SSAFY 내 강화된 AI 교육과정에 맞춰, 교육생이 직접 모델을 실습해볼 수 있는 플랫폼으로 기획

- **GPU 비용 및 인프라 부담 최소화**  
  서버 없이 로컬 GPU에서도 학습·추론이 가능하도록 구현해 비용 및 접근성 문제 해결

<br/>

<div id="3"></div>

## ✨ 주요 기능

### 서비스 설명

-   SAI는 AI 학습에 대한 진입 장벽을 낮추기 위해 설계된 SSAFY 교육생 맞춤형 블록 코딩 플랫폼입니다.
-   실제 AI 모델 학습 흐름(pip install, 데이터 로딩, 학습, 추론 등)을 시각적으로 구성하여 실습 중심의 학습을 지원합니다.

### 주요 기능

**1. AI 블록 코딩**
    
-   pip 설치, 모델 불러오기, 데이터셋 로딩, 파라미터 설정 등 AI 학습 과정을 블록 단위로 시각화하여 구성
  - 각 블록은 모듈화되어 개별 실행이 가능하며, 추가 및 수정이 용이한 확장성 있는 구조로 구현
  - 로컬 또는 서버 GPU를 선택하여 학습 및 추론을 서비스 내에서 직접 수행 가능

**2. 튜토리얼 / 실습 모드 분리**

- 튜토리얼 모드: 라벨링부터 추론까지 흐름을 따라가며, 고정된 파라미터로 학습 과정을 익힘
- 실습 모드: 사용자가 자유롭게 파라미터를 수정하며 모델을 반복 학습

 **3. 라벨링 체험 기능**

- `Classification`, `Bounding Box`, `Segmentation` 방식의 라벨링 실습을 직접 경험 가능
- 정확도(Accuracy) 90% 이상 달성 시에만 다음 단계 진행 가능
- 정답 기준을 스스로 검증하며 정확한 라벨링 감각을 체득할 수 있는 구조

**4. 실시간 파이썬 코드 변환**

- 블록을 조립하는 즉시 해당 로직이 Python 코드로 실시간 변환되어 사용자에게 표시
- 학습 흐름을 코드 구조로 직관적으로 이해하고, 실전 환경에도 대비 가능

**5. AI 피드백 시스템**

- 블록 조립 중 발생한 오류에 대해 원인 분석 및 해결 가이드 자동 제공
- 학습 완료 후 성능 지표를 분석하고, OpenAI 기반 성능 개선 피드백 및 파라미터 추천

**6. 학습 결과 Export 기능**
- 학습이 완료되면 Python 코드, 지표, 메모를 Notion 페이지로 내보내기 지원
- 모델 다운로드, 코드 복사, 자동 주석 기능 제공으로 실습 확장 가능

<br/>

<div id="4"></div>

## ✨ 주요 기능별 화면

[↗️전체 서비스 화면 보러가기](https://github.com/breadbirds/SAI/wiki/%EC%84%9C%EB%B9%84%EC%8A%A4-%ED%99%94%EB%A9%B4)

### 1. 라벨링 체험 기능

-   `Classification` `Bounding Box` `Segmentation` 3가지 라벨링 체험 가능
- 라벨링 정확도(Accuracy)가 90% 이상일 경우에만 다음 단계로 진행 
- 툴팁과 가이드라인 제공

<img src="/docs/gif/라벨링툴팁.gif" width="800">

### 2. 블록 코딩 기능

- 튜토리얼 / 실습 모드에서 블록 조립으로 모델 학습 파이프라인 구성
- 실시간으로 Python 코드 변환, 전체 코드 복사 가능
- 각 블록에 대한 사용 가이드 및 툴팁 제공

<img src="/docs/gif/블록 기능.gif" width="800">

### 3. 학습 결과 피드백 기능

- 블록 조립 중 오류 발생 시, 원인과 해결 방법을 시각적으로 안내
- 실행 결과 및 오류 상황을 토대로 실시간 피드백 제공

<img src="/docs/gif/튜토리얼 복사&오류에러.gif" width="800">

### 4. 성능 지표 분석 및 Notion Export

- 학습 결과를 바탕으로 OpenAI 기반 성능 분석 및 개선 피드백 제공
- 필요 시 Notion으로 Export (관리자 코드 입력 필요)

<img src="/docs/gif/노션 export.gif" width="800">
<br/>
<div id="5"></div>

## ✨ 기술적 특징

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

<br/>

## 📚 기술 스택

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
  <img src="https://img.shields.io/badge/fastapi-009688?style=for-the-badge&logo=python&logoColor=white">


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
  <img src="https://img.shields.io/badge/javascript-F7DF1E?style=for-the-badge&logo=&logoColor=white">


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

<br/>

<div id="6"></div>

## ✨ 프로젝트 진행 및 산출물

- [API 명세서](https://github.com/breadbirds/SAI/wiki/%ED%94%84%EB%A1%9C%EC%A0%9D%ED%8A%B8-%EC%82%B0%EC%B6%9C%EB%AC%BC#1-api-%EB%AA%85%EC%84%B8%EC%84%9C)
- [ERD](https://github.com/breadbirds/SAI/wiki/%ED%94%84%EB%A1%9C%EC%A0%9D%ED%8A%B8-%EC%82%B0%EC%B6%9C%EB%AC%BC#2-erd)
- [시스템 아키텍쳐](https://github.com/breadbirds/SAI/wiki/%ED%94%84%EB%A1%9C%EC%A0%9D%ED%8A%B8-%EC%82%B0%EC%B6%9C%EB%AC%BC#3-%EC%8B%9C%EC%8A%A4%ED%85%9C-%EC%95%84%ED%82%A4%ED%85%8D%EC%B3%90)
- [화면 설계서](https://github.com/breadbirds/SAI/wiki/%ED%94%84%EB%A1%9C%EC%A0%9D%ED%8A%B8-%EC%82%B0%EC%B6%9C%EB%AC%BC#4-%ED%99%94%EB%A9%B4-%EC%84%A4%EA%B3%84%EC%84%9C)
<!-- - [GIT]()-->

<br/>

<div id="7"></div>

## 👥 Team Members

<table>
  <tr>
    <td align="center" width="300">
      <a href="https://github.com/breadbirds">
        <img src="https://github.com/breadbirds.png" width="300" style="border-radius: 50%;">
        <br>
        <strong>정유진</strong>
      </a>
    </td>
    <td align="center" width="300">
      <a href="https://github.com/DonghyeonKwon">
        <img src="https://github.com/DonghyeonKwon.png" width="300" style="border-radius: 50%;">
        <br>
        <strong>권동현</strong>
      </a>
    </td>
    <td align="center" width="300">
      <a href="https://github.com/JeongEon8">
        <img src="https://github.com/JeongEon8.png" width="300" style="border-radius: 50%;">
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
    <td align="center" width="300">
      <a href="https://github.com/DoSeungGuk">
        <img src="https://github.com/DoSeungGuk.png" width="300" style="border-radius: 50%;">
        <br>
        <strong>도승국</strong>
      </a>
    </td>
    <td align="center" width="300">
      <a href="https://github.com/estel2005">
        <img src="https://github.com/estel2005.png" width="300" style="border-radius: 50%;">
        <br>
        <strong>박재영</strong>
      </a>
    </td>
    <td align="center" width="300">
      <a href="https://github.com/joehyejeong">
        <img src="https://github.com/joehyejeong.png" width="300" style="border-radius: 50%;">
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

## 📒 메뉴얼 및 상세 문서

-   [포팅메뉴얼](https://github.com/breadbirds/SAI/wiki/%ED%8F%AC%ED%8C%85-%EB%A9%94%EB%89%B4%EC%96%BC)
-   [전체 서비스 화면](https://github.com/breadbirds/SAI/wiki/%EC%84%9C%EB%B9%84%EC%8A%A4-%ED%99%94%EB%A9%B4)
- [프로젝트 산출물](https://github.com/breadbirds/SAI/wiki/%ED%94%84%EB%A1%9C%EC%A0%9D%ED%8A%B8-%EC%82%B0%EC%B6%9C%EB%AC%BC)
