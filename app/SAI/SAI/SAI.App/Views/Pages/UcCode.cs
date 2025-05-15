using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SAI.SAI.App.Models;
using SAI.SAI.App.Views.Interfaces;
using ScintillaNET;
using System.Text.RegularExpressions;
using System.Linq;

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
                scintilla1.Styles[Style.Default].Size = 6;
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

                // 주석
                scintilla1.Styles[1].ForeColor = Color.Green; // 주석
                scintilla1.Styles[12].ForeColor = Color.Green; // 블록 주석

                // 숫자 및 문자열
                scintilla1.Styles[2].ForeColor = Color.LightGreen; // 숫자
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

        // 특정 키워드 패턴을 전체 코드에서 하이라이트
        public void HighlightKeywords(string[] keywords)
        {
            try
            {
                if (scintilla1 == null || keywords == null || keywords.Length == 0)
                    return;

                foreach (string keyword in keywords)
                {
                    if (string.IsNullOrEmpty(keyword))
                        continue;

                    // 검색 범위 설정 (전체 문서)
                    scintilla1.TargetStart = 0;
                    scintilla1.TargetEnd = scintilla1.TextLength;
                    scintilla1.SearchFlags = ScintillaNET.SearchFlags.None;

                    // 문서 전체에서 키워드 모두 찾기
                    while (scintilla1.SearchInTarget(keyword) != -1)
                    {
                        // 스타일 설정 (키워드에 따라 다른 색상 사용 가능)
                        int start = scintilla1.TargetStart;
                        int end = scintilla1.TargetEnd;

                        // 다음 검색을 위해 타겟 위치 조정
                        scintilla1.TargetStart = end;
                        scintilla1.TargetEnd = scintilla1.TextLength;
                    }
                }

                scintilla1.Refresh();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] 키워드 하이라이트 오류: {ex.Message}");
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

                // 기존 하이라이트 지우기
                ClearHighlight();

                // 선택 색상 설정 - 더 명확한 색상으로 변경
                scintilla1.SetSelectionBackColor(true, Color.FromArgb(255, 100, 100)); // 밝은 빨간색 배경
                scintilla1.SetSelectionForeColor(true, Color.White); // 흰색 텍스트

                // 해당 라인 선택
                scintilla1.SelectionStart = lineStart;
                scintilla1.SelectionEnd = lineStart + lineLength;

                // 해당 라인으로 스크롤 - 라인이 보이도록 중앙에 배치
                scintilla1.GotoPosition(lineStart);
                scintilla1.ScrollCaret();

                // 화면 강제 갱신
                scintilla1.Refresh();
                this.Refresh();

                Console.WriteLine($"[DEBUG] 선택 완료: 시작={lineStart}, 끝={lineStart + lineLength}, 라인={lineIndex}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] 라인 하이라이트 오류: {ex.Message}");
            }
        }

        // 지정된 라인 범위를 하이라이트하는 메서드
        public void HighlightLineRange(int startLineIndex, int endLineIndex)
        {
            try
            {
                if (scintilla1 == null)
                    return;

                // 라인 인덱스 유효성 검사
                if (startLineIndex < 0)
                    startLineIndex = 0;

                if (scintilla1.Lines.Count == 0)
                    return;

                if (startLineIndex >= scintilla1.Lines.Count)
                    startLineIndex = 0;

                if (endLineIndex >= scintilla1.Lines.Count)
                    endLineIndex = scintilla1.Lines.Count - 1;

                if (endLineIndex < startLineIndex)
                    endLineIndex = startLineIndex;

                Console.WriteLine($"[DEBUG] 하이라이트: 라인 범위 선택, 시작: {startLineIndex}, 끝: {endLineIndex}");

                // 하이라이트 적용 전에 기존 선택 지우기
                ClearHighlight();

                // 시작 라인과 끝 라인 정보 가져오기
                int startPos = scintilla1.Lines[startLineIndex].Position;
                int endPos = scintilla1.Lines[endLineIndex].Position + scintilla1.Lines[endLineIndex].Length;

                // 해당 위치로 스크롤
                scintilla1.GotoPosition(startPos);

                // 선택 색상 설정
                scintilla1.SetSelectionBackColor(true, Color.FromArgb(180, 60, 60)); // 진한 빨간색 배경
                scintilla1.SetSelectionForeColor(true, Color.White); // 흰색 텍스트

                // 해당 라인 범위 선택
                scintilla1.SelectionStart = startPos;
                scintilla1.SelectionEnd = endPos;

                Console.WriteLine($"[DEBUG] 선택 완료: 시작={startPos}, 끝={endPos}");

                // 스타일 강제 적용 및 보이게 스크롤
                scintilla1.ScrollCaret();
                scintilla1.Refresh();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] 라인 범위 하이라이트 오류: {ex.Message}");
                Console.WriteLine($"[ERROR] 스택 트레이스: {ex.StackTrace}");
            }
        }

        // 전체 코드에서 개별 코드 세그먼트를 찾아 하이라이트
        public void HighlightCodeSegment(string codeSegment)
        {
            try
            {
                Console.WriteLine($"[DEBUG] UcCode: HighlightCodeSegment 호출됨 ({codeSegment?.Length ?? 0}자)");

                if (scintilla1 == null || string.IsNullOrEmpty(scintilla1.Text) || string.IsNullOrEmpty(codeSegment))
                {
                    Console.WriteLine("[DEBUG] UcCode: 하이라이트 불가능 - Scintilla null 또는 빈 텍스트");
                    return;
                }

                // 하이라이트 적용 전에 기존 선택 지우기
                ClearHighlight();

                // 전체 코드에서 코드 세그먼트 문자열 검색
                string fullCode = scintilla1.Text;
                int segmentIndex = fullCode.IndexOf(codeSegment);

                if (segmentIndex >= 0)
                {
                    Console.WriteLine($"[DEBUG] UcCode: 코드 세그먼트 직접 매칭 찾음 (위치: {segmentIndex})");

                    // 선택 스타일 설정
                    scintilla1.SetSelectionBackColor(true, Color.FromArgb(180, 60, 60)); // 진한 빨간색 배경
                    scintilla1.SetSelectionForeColor(true, Color.White); // 흰색 텍스트

                    // 세그먼트 선택
                    scintilla1.SelectionStart = segmentIndex;
                    scintilla1.SelectionEnd = segmentIndex + codeSegment.Length;

                    // 해당 위치로 스크롤
                    scintilla1.GotoPosition(segmentIndex);
                    scintilla1.ScrollCaret();

                    Console.WriteLine("[DEBUG] UcCode: 코드 세그먼트 하이라이트 완료");
                }
                else
                {
                    Console.WriteLine("[WARNING] UcCode: 코드 세그먼트를 찾지 못함");
                    // 코드 세그먼트를 찾지 못했을 때는 하이라이트 하지 않음
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] UcCode: 코드 세그먼트 하이라이트 중 오류 - {ex.Message}");
                Console.WriteLine($"[ERROR] 스택 트레이스: {ex.StackTrace}");
            }
        }

        // 새로운 메서드: Scintilla의 내장 검색 기능을 사용하여 텍스트를 찾고 하이라이트
        public void FindAndHighlightText(string searchText, bool caseSensitive = false)
        {
            try
            {
                Console.WriteLine($"[DEBUG] UcCode: FindAndHighlightText 호출됨 ({searchText?.Length ?? 0}자)");

                if (scintilla1 == null || string.IsNullOrEmpty(scintilla1.Text) || string.IsNullOrEmpty(searchText))
                {
                    Console.WriteLine("[DEBUG] UcCode: 검색 불가능 - Scintilla null 또는 빈 텍스트");
                    return;
                }

                // 하이라이트 적용 전에 기존 선택 지우기
                ClearHighlight();

                // 검색 범위 설정 (전체 문서)
                scintilla1.TargetStart = 0;
                scintilla1.TargetEnd = scintilla1.TextLength;

                // 검색 옵션 설정
                scintilla1.SearchFlags = caseSensitive ?
                    ScintillaNET.SearchFlags.MatchCase :
                    ScintillaNET.SearchFlags.None;

                // 검색 수행
                int pos = scintilla1.SearchInTarget(searchText);
                if (pos >= 0)
                {
                    // 검색된 텍스트 위치 가져오기
                    int startPos = scintilla1.TargetStart;
                    int endPos = scintilla1.TargetEnd;

                    // 하이라이트 적용
                    scintilla1.SetSelectionBackColor(true, Color.FromArgb(180, 60, 60)); // 진한 빨간색 배경
                    scintilla1.SetSelectionForeColor(true, Color.White); // 흰색 텍스트
                    scintilla1.SelectionStart = startPos;
                    scintilla1.SelectionEnd = endPos;

                    // 해당 위치로 스크롤
                    scintilla1.GotoPosition(startPos);
                    scintilla1.ScrollCaret();

                    // 찾은 위치의 라인 번호 계산 (로깅용, +1은 1-기반 라인 번호를 위해)
                    int line = scintilla1.LineFromPosition(startPos) + 1;
                    Console.WriteLine($"[DEBUG] 검색 문자열 '{searchText.Substring(0, Math.Min(20, searchText.Length))}...' 발견: 라인 {line}, 위치 {startPos}~{endPos}");
                    return;
                }

                // 정확한 문자열 매칭이 실패한 경우 줄별로 포함 여부 확인하는 대체 검색 방법
                Console.WriteLine($"[DEBUG] 정확한 문자열 매칭 실패, 대체 검색 방법 시도");
                string fullText = scintilla1.Text;
                string[] lines = fullText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                // 각 줄에 대해 검색 텍스트의 일부가 포함되어 있는지 확인
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];

                    // 텍스트의 처음 몇 단어만 검색 (주석의 경우 유용)
                    string[] words = searchText.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    if (words.Length > 0)
                    {
                        string firstFewWords = string.Join(" ", words.Take(Math.Min(3, words.Length))); // 처음 3단어
                        if (line.Contains(firstFewWords))
                        {
                            int lineStartPos = scintilla1.Lines[i].Position;
                            int lineEndPos = lineStartPos + scintilla1.Lines[i].Length;

                            // 하이라이트 적용
                            scintilla1.SetSelectionBackColor(true, Color.FromArgb(180, 60, 60));
                            scintilla1.SetSelectionForeColor(true, Color.White);
                            scintilla1.SelectionStart = lineStartPos;
                            scintilla1.SelectionEnd = lineEndPos;

                            // 해당 위치로 스크롤
                            scintilla1.GotoPosition(lineStartPos);
                            scintilla1.ScrollCaret();

                            Console.WriteLine($"[DEBUG] 대체 검색 방법: 라인 {i + 1}에서 일부 텍스트 '{firstFewWords}' 발견");
                            return;
                        }
                    }
                }

                Console.WriteLine($"[WARNING] 검색 문자열을 찾을 수 없습니다.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] UcCode: 문자열 검색 및 하이라이트 중 오류 - {ex.Message}");
                Console.WriteLine($"[ERROR] 스택 트레이스: {ex.StackTrace}");
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
            Console.WriteLine("[DEBUG] UcCode.OnLoad 호출됨");

            // 테스트 코드는 개발 중에만 필요하고 실제 사용 시에는 제거
            // 개발 환경에서만 활성화하려면 DEBUG 조건부 컴파일 지시문 사용
#if DEBUG
            if (scintilla1 != null && string.IsNullOrEmpty(scintilla1.Text))
            {
                scintilla1.Text = "# 코드가 여기에 표시됩니다";
                scintilla1.GotoPosition(0);
                scintilla1.ScrollCaret();
                scintilla1.Refresh();
            }
#endif
        }
    }
}