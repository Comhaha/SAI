using System;
using System.Drawing;
using System.Windows.Forms;
using SAI.SAI.App.Models;
using SAI.SAI.App.Views.Interfaces;
using ScintillaNET;

namespace SAI.SAI.App.Views.Pages
{
    public partial class UcCode : UserControl, ICodeView
    {
        private Scintilla scintilla1;
        private bool accumulateCode = false; // 코드 누적 여부 (기본값: 누적하지 않음)
        private string lastAddedCode = string.Empty; // 마지막으로 추가된 코드 저장

        public UcCode()
        {
            InitializeComponent();
            InitializeScintilla();
            Console.WriteLine("[DEBUG] UcCode: 생성자 호출됨");
        }

        private void InitializeScintilla()
        {
            try
            {
                Console.WriteLine("[DEBUG] UcCode: InitializeScintilla 시작");

                // Scintilla 에디터 생성
                scintilla1 = new Scintilla();
                scintilla1.Dock = DockStyle.Fill;
                scintilla1.Name = "scintilla1";

                // 기본 설정
                scintilla1.StyleResetDefault();
                scintilla1.Styles[Style.Default].Font = "Consolas";
                scintilla1.Styles[Style.Default].Size = 10;
                scintilla1.Styles[Style.Default].BackColor = Color.FromArgb(255, 255, 255);
                scintilla1.Styles[Style.Default].ForeColor = Color.Black; // 기본 텍스트 색상: 검정

                // 라인 번호 설정 - 더 어두운 배경색 적용
                scintilla1.Margins[0].Width = 30;
                scintilla1.Styles[Style.LineNumber].BackColor = Color.FromArgb(255, 255, 255); // 밝은 색상으로 변경
                scintilla1.Styles[Style.LineNumber].ForeColor = Color.Green;

                // 마진 배경 색상 변경 (추가)
                scintilla1.SetFoldMarginColor(true, Color.FromArgb(255, 255, 255));
                scintilla1.SetFoldMarginHighlightColor(true, Color.FromArgb(255, 255, 255));

                // 모든 마진 배경색 변경
                for (int i = 0; i < 5; i++)
                {
                    scintilla1.Margins[i].BackColor = Color.FromArgb(255, 255, 255);
                }

                // 파이썬 구문 강조 설정
                scintilla1.Lexer = Lexer.Python;

                // Python 키워드 설정
                scintilla1.SetKeywords(0,
                    "and as assert break class continue def del elif else except " +
                    "False finally for from global if import in is lambda None " +
                    "not or pass print raise return True try while with yield");

                // 스타일 설정 - 기본 텍스트 색상은 흰색으로 통일
                // 주석
                scintilla1.Styles[1].ForeColor = Color.Green; // 주석
                scintilla1.Styles[12].ForeColor = Color.Green; // 블록 주석

                // 숫자 및 문자열
                scintilla1.Styles[2].ForeColor = Color.LightBlue; // 숫자
                scintilla1.Styles[3].ForeColor = Color.FromArgb(214, 157, 133); // 문자열
                scintilla1.Styles[7].ForeColor = Color.FromArgb(214, 157, 133); // 문자

                // 키워드 및 연산자
                scintilla1.Styles[5].ForeColor = Color.FromArgb(86, 156, 214); // 키워드 (예: print)
                scintilla1.Styles[10].ForeColor = Color.Blue; // 연산자

                // 기타 스타일
                scintilla1.Styles[11].ForeColor = Color.Blue; // 식별자

                // 여백 스타일 설정 (추가)
                scintilla1.SetWhitespaceForeColor(true, Color.FromArgb(255, 255, 255));
                scintilla1.SetWhitespaceBackColor(true, Color.FromArgb(255, 255, 255));

                // 읽기 전용으로 시작 (필요에 따라 수정 가능)
                scintilla1.ReadOnly = true;

                // 컨트롤에 추가
                this.Controls.Add(scintilla1);
                Console.WriteLine("[DEBUG] UcCode: Scintilla가 Controls에 추가됨");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] UcCode: Scintilla 초기화 오류 - {ex.Message}");
                // 실패 시 일반 TextBox 사용
                CreateFallbackTextBox();
            }
        }

        private void CreateFallbackTextBox()
        {
            // Scintilla 초기화 실패시 일반 TextBox 사용
            TextBox codeTextBox = new TextBox();
            codeTextBox.Dock = DockStyle.Fill;
            codeTextBox.Multiline = true;
            codeTextBox.ScrollBars = ScrollBars.Both;
            codeTextBox.Font = new Font("Consolas", 10F);
            codeTextBox.BackColor = Color.FromArgb(45, 45, 48);
            codeTextBox.ForeColor = Color.White;
            codeTextBox.WordWrap = false;

            this.Controls.Add(codeTextBox);
            Console.WriteLine("[INFO] UcCode: 대체 TextBox가 Controls에 추가됨");
        }

        // 기본 업데이트 메서드 - 코드만 업데이트
        public void UpdateCode(string code)
        {
            try
            {
                Console.WriteLine($"[DEBUG] UcCode: UpdateCode 메서드 호출됨 ({code?.Length ?? 0}자)");

                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new Action(() => UpdateCode(code)));
                    return;
                }

                if (scintilla1 == null)
                    return;

                // 읽기 전용 상태 저장 및 해제
                bool wasReadOnly = scintilla1.ReadOnly;
                if (wasReadOnly)
                    scintilla1.ReadOnly = false;

                // 이전 내용 유지 여부 확인
                if (!accumulateCode)
                {
                    // 비누적 모드: 이전 내용 지우기
                    scintilla1.Text = string.Empty;
                }

                // 새 코드 추가
                scintilla1.Text = code ?? string.Empty;

                // 마지막으로 추가된 코드 저장
                lastAddedCode = code ?? string.Empty;

                // 읽기 전용 복원
                if (wasReadOnly)
                    scintilla1.ReadOnly = true;

                // 항상 전체 코드 하이라이트
                HighlightBlockByComment(null);

                // 마지막 라인으로 스크롤
                scintilla1.GotoPosition(scintilla1.TextLength);

                Console.WriteLine("[DEBUG] UcCode: 코드 업데이트 완료");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] UcCode: 코드 업데이트 중 오류 발생 - {ex.Message}");
            }
        }

        // 하이라이트 제거 메서드
        public void ClearHighlight()
        {
            try
            {
                if (scintilla1 == null)
                    return;

                // 선택 영역 제거
                scintilla1.SetSelection(scintilla1.CurrentPosition, scintilla1.CurrentPosition);

                // 선택 색상 초기화
                scintilla1.SetSelectionBackColor(false, Color.White);
                scintilla1.SetSelectionForeColor(false, Color.Black);

                // 화면 갱신
                scintilla1.Refresh();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] UcCode: 하이라이트 제거 중 오류 - {ex.Message}");
            }
        }

        // 선택한 줄에 하이라이트 적용
        public void HighlightLine(int lineIndex)
        {
            try
            {
                if (scintilla1 == null)
                    return;

                // 라인 인덱스 유효성 검사
                if (lineIndex < 0)
                    lineIndex = 0;

                if (scintilla1.Lines.Count == 0)
                    return;

                if (lineIndex >= scintilla1.Lines.Count)
                    lineIndex = 0;

                Console.WriteLine($"[DEBUG] 하이라이트: 선택 방식 시도, 라인 {lineIndex}");

                // 라인 정보 가져오기
                int lineStart = scintilla1.Lines[lineIndex].Position;
                int lineLength = scintilla1.Lines[lineIndex].Length;

                // 해당 라인으로 스크롤
                scintilla1.GotoPosition(lineStart);

                // 선택 색상 설정
                scintilla1.SetSelectionBackColor(true, Color.Red);
                scintilla1.SetSelectionForeColor(true, Color.White);

                // 해당 라인 선택
                scintilla1.SelectionStart = lineStart;
                scintilla1.SelectionEnd = lineStart + lineLength;

                Console.WriteLine($"[DEBUG] 선택 완료: 시작={lineStart}, 끝={lineStart + lineLength}");

                // 스타일 강제 적용
                scintilla1.Refresh();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] 라인 하이라이트 오류: {ex.Message}");
            }
        }

        // 블록 하이라이트 함수 - 개선된 버전
        public void HighlightBlockByComment(string comment)
        {
            try
            {
                Console.WriteLine($"[DEBUG] 코드 하이라이트 시도");

                if (scintilla1 == null || string.IsNullOrEmpty(scintilla1.Text))
                    return;

                // 하이라이트 적용 전에 기존 선택 지우기
                ClearHighlight();

                // comment가 null이거나 비어있으면 전체 코드 하이라이트
                if (string.IsNullOrEmpty(comment))
                {
                    // 전체 코드 하이라이트
                    Console.WriteLine($"[DEBUG] 전체 코드 하이라이트 적용 시도");

                    // 선택 스타일 설정 (검정색 배경, 흰색 텍스트)
                    scintilla1.SetSelectionBackColor(true, Color.Black); // 검정색 배경
                    scintilla1.SetSelectionForeColor(true, Color.White); // 흰색 텍스트

                    // 전체 코드 선택
                    scintilla1.SelectionStart = 0;
                    scintilla1.SelectionEnd = scintilla1.TextLength;

                    // 스크롤 시작 부분으로 이동
                    scintilla1.GotoPosition(0);
                    scintilla1.ScrollCaret();

                    Console.WriteLine($"[DEBUG] 전체 코드 하이라이트 완료: {scintilla1.TextLength}자");
                    return;
                }

                // 주석 기반 블록 하이라이트
                string cleanComment = comment.Replace("#", "").Trim();

                // 주요 블록 주석 패턴
                string[] possibleKeywords = new string[] {
                    "패키지 설치", "모델 불러오기", "데이터 불러오기",
                    "모델 학습", "학습 결과", "이미지 경로", "추론 실행", "결과 시각화"
                };

                // 가장 가능성 높은 키워드 찾기
                string targetKeyword = "";
                foreach (string keyword in possibleKeywords)
                {
                    if (cleanComment.Contains(keyword))
                    {
                        targetKeyword = keyword;
                        break;
                    }
                }

                // 키워드가 없으면 원본 주석 사용
                if (string.IsNullOrEmpty(targetKeyword))
                {
                    targetKeyword = cleanComment;
                }

                Console.WriteLine($"[DEBUG] 검색할 키워드: '{targetKeyword}'");

                // 모든 라인을 순회하며 주석 검색
                int startLine = -1;

                // 주석 찾기
                for (int i = 0; i < scintilla1.Lines.Count; i++)
                {
                    string line = scintilla1.Lines[i].Text.Trim();
                    if (line.StartsWith("#") && line.Contains(targetKeyword))
                    {
                        startLine = i;
                        Console.WriteLine($"[DEBUG] 주석 찾음: 라인 {i}, 내용: '{line}'");
                        break;
                    }
                }

                int endLine = -1;

                // 시작 라인을 찾은 경우에만 끝 라인 찾기
                if (startLine >= 0)
                {
                    // 해당 블록의 끝 찾기
                    for (int i = startLine + 1; i < scintilla1.Lines.Count; i++)
                    {
                        string line = scintilla1.Lines[i].Text.Trim();

                        // 빈 줄이 나오면 블록 경계로 간주
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            endLine = i - 1;
                            break;
                        }

                        // 새로운 주석(#으로 시작)을 만나면 블록 끝으로 간주
                        if (line.StartsWith("#") && i > startLine + 1)
                        {
                            endLine = i - 1;
                            break;
                        }

                        // 마지막 라인인 경우
                        if (i == scintilla1.Lines.Count - 1)
                        {
                            endLine = i;
                        }
                    }
                }
                else
                {
                    // 주석을 찾지 못한 경우 첫 번째 라인부터 시작
                    startLine = 0;
                    endLine = Math.Min(5, scintilla1.Lines.Count - 1);
                }

                Console.WriteLine($"[DEBUG] 블록 범위: 시작 라인 {startLine}, 끝 라인 {endLine}");

                // 블록 선택
                if (startLine >= 0 && endLine >= startLine)
                {
                    int blockStart = scintilla1.Lines[startLine].Position;
                    int blockEnd = scintilla1.Lines[endLine].EndPosition;

                    // 선택 스타일 설정
                    scintilla1.SetSelectionBackColor(true, Color.FromArgb(180, 60, 60)); // 진한 빨간색 배경
                    scintilla1.SetSelectionForeColor(true, Color.White); // 흰색 텍스트

                    // 블록 선택
                    scintilla1.SelectionStart = blockStart;
                    scintilla1.SelectionEnd = blockEnd;

                    // 해당 위치로 스크롤
                    scintilla1.GotoPosition(blockStart);
                    scintilla1.ScrollCaret();

                    Console.WriteLine($"[DEBUG] 블록 선택 완료: 시작 위치 {blockStart}, 끝 위치 {blockEnd}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] 블록 하이라이트 중 오류: {ex.Message}");
                try
                {
                    // 오류 발생시 첫 번째 라인 하이라이트
                    HighlightLine(0);
                }
                catch { } // 추가 오류 무시
            }
        }

        // Text 속성
        public new string Text
        {
            get { return scintilla1?.Text ?? string.Empty; }
            set
            {
                try
                {
                    if (scintilla1 != null)
                    {
                        // 읽기 전용 일시 해제
                        bool wasReadOnly = scintilla1.ReadOnly;
                        if (wasReadOnly)
                            scintilla1.ReadOnly = false;

                        scintilla1.Text = value ?? string.Empty;

                        // 마지막으로 추가된 코드 저장
                        lastAddedCode = value ?? string.Empty;

                        // 읽기 전용 복원
                        if (wasReadOnly)
                            scintilla1.ReadOnly = true;

                        // 전체 코드 하이라이트
                        HighlightBlockByComment(null);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] UcCode: Text 속성 설정 중 오류 - {ex.Message}");
                }
            }
        }

        // 코드 누적 모드 설정
        public void SetAccumulateMode(bool accumulate)
        {
            accumulateCode = accumulate;
            Console.WriteLine($"[INFO] UcCode: 코드 누적 모드 설정 - {(accumulate ? "활성화" : "비활성화")}");
        }

        // AppendCode 메서드
        public void AppendCode(string code, string blockId = null)
        {
            try
            {
                if (string.IsNullOrEmpty(code))
                    return;

                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new Action(() => AppendCode(code, blockId)));
                    return;
                }

                if (scintilla1 == null)
                    return;

                // 현재 읽기 전용 상태 저장
                bool wasReadOnly = scintilla1.ReadOnly;

                // 읽기 전용 해제
                if (wasReadOnly)
                    scintilla1.ReadOnly = false;

                // 코드 누적 여부에 따라 처리
                if (accumulateCode)
                {
                    // 누적 모드: 현재 위치 저장
                    int currentPos = scintilla1.TextLength;

                    // 새 라인 추가 (필요한 경우)
                    if (currentPos > 0 && !scintilla1.Text.EndsWith("\n\n"))
                    {
                        scintilla1.AppendText("\n\n");
                    }

                    // 새 코드 추가
                    scintilla1.AppendText(code);
                }
                else
                {
                    // 비누적 모드: 기존 코드 지우고 새 코드만 표시
                    scintilla1.Text = code;
                }

                // 마지막으로 추가된 코드 저장
                lastAddedCode = code;

                // 읽기 전용 복원
                if (wasReadOnly)
                    scintilla1.ReadOnly = true;

                // 항상 전체 코드 하이라이트
                HighlightBlockByComment(null);

                Console.WriteLine("[DEBUG] UcCode: 코드 추가 완료");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] UcCode: 코드 추가 중 오류 발생 - {ex.Message}");
            }
        }

        // UcCode_Load 이벤트 핸들러 추가
        private void UcCode_Load(object sender, EventArgs e)
        {
            Console.WriteLine("[DEBUG] UcCode: UcCode_Load 이벤트 발생");
        }

        // 모든 코드 지우기
        public void ClearCode()
        {
            try
            {
                if (scintilla1 == null)
                    return;

                bool wasReadOnly = scintilla1.ReadOnly;
                if (wasReadOnly)
                    scintilla1.ReadOnly = false;

                scintilla1.Text = string.Empty;

                // 마지막 코드 초기화
                lastAddedCode = string.Empty;

                // 하이라이트도 제거
                ClearHighlight();

                if (wasReadOnly)
                    scintilla1.ReadOnly = true;

                Console.WriteLine("[DEBUG] UcCode: 모든 코드 지움");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] UcCode: 코드 지우기 중 오류 발생 - {ex.Message}");
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (scintilla1 != null)
            {
                scintilla1.Text = "테스트 코드\nprint('Hello')";
                scintilla1.SetSelectionBackColor(true, Color.Red);
                scintilla1.SetSelectionForeColor(true, Color.White);
                scintilla1.SelectionStart = 0;
                scintilla1.SelectionEnd = scintilla1.TextLength;
                scintilla1.GotoPosition(0);
                scintilla1.ScrollCaret();
                scintilla1.Refresh();
            }
        }
    }
}