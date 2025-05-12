using System;
using System.Drawing;
using System.Windows.Forms;
using SAI.SAI.App.Views.Interfaces;
using ScintillaNET;

namespace SAI.SAI.App.Views.Pages
{
    public partial class UcCode : UserControl, ICodeView
    {
        private Scintilla scintilla1;
        private bool accumulateCode = false; // 코드 누적 여부 (기본값: 누적하지 않음)

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

                // 모든 인디케이터 비활성화 (0-31)
                for (int i = 0; i < 32; i++)
                {
                    scintilla1.Indicators[i].Style = IndicatorStyle.Hidden;
                    scintilla1.Indicators[i].ForeColor = Color.Transparent;
                    scintilla1.Indicators[i].Alpha = 0;
                    scintilla1.Indicators[i].OutlineAlpha = 0;
                    scintilla1.Indicators[i].Under = true;
                }

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

        // 기본 업데이트 메서드 - 하이라이트 없이 코드만 업데이트
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

                // 모든 하이라이트 강제 제거
                ForceRemoveAllHighlights();

                // 읽기 전용 복원
                if (wasReadOnly)
                    scintilla1.ReadOnly = true;

                // 마지막 라인으로 스크롤
                scintilla1.GotoPosition(scintilla1.TextLength);

                Console.WriteLine("[DEBUG] UcCode: 코드 업데이트 완료");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] UcCode: 코드 업데이트 중 오류 발생 - {ex.Message}");
            }
        }

        // 모든 하이라이트 강제 제거 - 더 적극적인 방법 사용
        private void ForceRemoveAllHighlights()
        {
            try
            {
                if (scintilla1 == null)
                    return;

                // 모든 인디케이터 범위 제거 (0-31)
                for (int i = 0; i < 32; i++)
                {
                    scintilla1.IndicatorCurrent = i;
                    scintilla1.IndicatorClearRange(0, scintilla1.TextLength);
                }

                // 선택 영역 제거
                scintilla1.SetSelection(scintilla1.CurrentPosition, scintilla1.CurrentPosition);

                // 스타일 초기화 강제 적용
                scintilla1.StyleClearAll();

                // 파이썬 구문 강조 다시 적용
                scintilla1.Lexer = Lexer.Python;
                scintilla1.SetKeywords(0,
                    "and as assert break class continue def del elif else except " +
                    "False finally for from global if import in is lambda None " +
                    "not or pass print raise return True try while with yield");

                // 스타일 재설정
                // 주석
                scintilla1.Styles[1].ForeColor = Color.Green; // 주석
                scintilla1.Styles[12].ForeColor = Color.Green; // 블록 주석

                // 숫자 및 문자열
                scintilla1.Styles[2].ForeColor = Color.LightBlue; // 숫자
                scintilla1.Styles[3].ForeColor = Color.FromArgb(214, 157, 133); // 문자열
                scintilla1.Styles[7].ForeColor = Color.FromArgb(214, 157, 133); // 문자

                // 키워드 및 연산자
                scintilla1.Styles[5].ForeColor = Color.FromArgb(86, 156, 214); // 키워드
                scintilla1.Styles[10].ForeColor = Color.Blue; // 연산자

                // 기타 스타일
                scintilla1.Styles[11].ForeColor = Color.Blue; // 식별자

                // 라인 번호 스타일 복원 - 더 어두운 색상으로
                scintilla1.Styles[Style.LineNumber].BackColor = Color.FromArgb(255, 255, 255);
                scintilla1.Styles[Style.LineNumber].ForeColor = Color.Green;

                // 코드 다시 렌더링 강제화
                scintilla1.Refresh();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] UcCode: 하이라이트 제거 중 오류 - {ex.Message}");
            }
        }

        // 하이라이트 제거 메서드 - 강제 제거 호출
        public void ClearHighlight()
        {
            ForceRemoveAllHighlights();
        }

        // 특정 텍스트에 하이라이트 적용 - 빈 메서드로 대체 (아무 작업 안함)
        public void HighlightText(string text)
        {
            // 하이라이트 기능 비활성화됨 - 아무 작업 안함
            Console.WriteLine($"[INFO] UcCode: 하이라이트 기능이 비활성화되어 있습니다.");
        }

        // 선택한 줄에 하이라이트 적용 - 빈 메서드로 대체 (아무 작업 안함)
        public void HighlightLine(int lineIndex)
        {
            // 하이라이트 기능 비활성화됨 - 아무 작업 안함
            Console.WriteLine($"[INFO] UcCode: 하이라이트 기능이 비활성화되어 있습니다.");
        }

        // Text 속성 - 하이라이트 없이 텍스트만 설정
        // new 키워드 추가하여 경고 제거
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

                        // 모든 하이라이트 강제 제거
                        ForceRemoveAllHighlights();

                        // 읽기 전용 복원
                        if (wasReadOnly)
                            scintilla1.ReadOnly = true;
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

        // AppendCode 메서드 추가 - BlocklyPresenter에서 호출
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

                // 모든 하이라이트 강제 제거
                ForceRemoveAllHighlights();

                // 마지막 라인으로 스크롤
                scintilla1.GotoPosition(scintilla1.TextLength);

                // 읽기 전용 복원
                if (wasReadOnly)
                    scintilla1.ReadOnly = true;

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
            // 필요한 초기화 코드 추가
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

                // 모든 하이라이트 강제 제거
                ForceRemoveAllHighlights();

                if (wasReadOnly)
                    scintilla1.ReadOnly = true;

                Console.WriteLine("[DEBUG] UcCode: 모든 코드 지움");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] UcCode: 코드 지우기 중 오류 발생 - {ex.Message}");
            }
        }

        // 추가: 블록 ID 기반 하이라이트 함수 (비활성화됨)
        public void HighlightBlockByComments(string blockId)
        {
            // 하이라이트 기능 비활성화됨 - 아무 작업 안함
            Console.WriteLine($"[INFO] UcCode: 하이라이트 기능이 비활성화되어 있습니다.");

        }
    }
}