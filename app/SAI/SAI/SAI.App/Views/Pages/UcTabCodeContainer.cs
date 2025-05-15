//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Windows.Forms;
//using SAI.SAI.App.Views.Interfaces;

//namespace SAI.SAI.App.Views.Pages
//{
//    public partial class UcTabCodeContainer : UserControl
//    {
//        private UcTab tabControl;
//        private Panel contentPanel;
//        private Dictionary<int, UcCode> codeEditors = new Dictionary<int, UcCode>();
//        private int lastTabIndex = 0;

//        // "전체 코드" 탭과 "모델생성" 탭의 인덱스를 저장
//        private const int MAIN_TAB_INDEX = 0;
//        private const int MODEL_TAB_INDEX = 1;

//        public UcTabCodeContainer()
//        {
//            InitializeComponent();

//            // Load 이벤트 핸들러 명시적 등록
//            this.Load += new EventHandler(UcTabCodeContainer_Load);

//            // 둥근 모서리를 위한 설정
//            this.ResizeRedraw = true;

//            SetupUI();


//            // 둥근 모서리 적용
//            ApplyRoundedCorners(19.45f);
//        }

//        // 둥근 모서리를 적용하는 메서드 추가
//        private void ApplyRoundedCorners(float radius)
//        {
//            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();

//            // 상단 모서리만 둥글게 처리
//            path.AddArc(0, 0, radius * 2, radius * 2, 180, 90); // 왼쪽 상단
//            path.AddArc(this.Width - radius * 2, 0, radius * 2, radius * 2, 270, 90); // 오른쪽 상단

//            // 나머지 부분은 직선으로
//            path.AddLine(this.Width, radius, this.Width, this.Height); // 오른쪽 면
//            path.AddLine(this.Width, this.Height, 0, this.Height); // 하단 면
//            path.AddLine(0, this.Height, 0, radius); // 왼쪽 면

//            // 경로 닫기
//            path.CloseAllFigures();

//            this.Region = new Region(path);
//        }

//        // 크기가 변경될 때 둥근 모서리를 다시 적용
//        protected override void OnResize(EventArgs e)
//        {
//            base.OnResize(e);
//            ApplyRoundedCorners(19.45f);
//        }

//        // Load 이벤트 핸들러 추가
//        private void UcTabCodeContainer_Load(object sender, EventArgs e)
//        {
//            Console.WriteLine("[DEBUG] UcTabCodeContainer: UcTabCodeContainer_Load 이벤트 발생");
//            // 추가 초기화 코드가 필요하면 여기에 추가
//        }

//        private void SetupUI()
//        {
//            // 레이아웃 설정
//            this.Padding = new Padding(0);
//            this.BackColor = Color.White; // 배경을 흰색으로 변경

//            // 탭 컨트롤 생성
//            tabControl = new UcTab();
//            tabControl.Dock = DockStyle.Top;
//            tabControl.TabSelected += TabControl_TabSelected;
//            tabControl.TabClosed += TabControl_TabClosed;
//            tabControl.CornerRadius = 19.45f; // 모서리 둥글기 설정
//            tabControl.BackColor = Color.White; // 탭 컨트롤 배경을 흰색으로 설정

//            // 콘텐츠 패널 생성
//            contentPanel = new Panel();
//            contentPanel.Dock = DockStyle.Fill;
//            contentPanel.BackColor = Color.FromArgb(45, 45, 48); // 코드 에디터 배경은 어두운 색상 유지

//            // 컨트롤 추가
//            this.Controls.Add(contentPanel);
//            this.Controls.Add(tabControl);

//            // 기본 탭 추가 - "전체 코드" 탭은 삭제 불가능하게 설정, 메인 탭으로 설정
//            AddCodeTab("전체 코드", true, true);

//            // "모델생성" 탭 기본 추가 - 일반 탭으로 설정 (삭제 가능)
//            //AddCodeTab("모델생성");

//            // 기본 탭("전체 코드")에서 코드 내용을 "모델생성" 탭으로 복사
//            try
//            {
//                UcCode mainEditor = GetMainCodeEditor();
//                UcCode modelEditor = codeEditors[MODEL_TAB_INDEX];

//                if (mainEditor != null && modelEditor != null)
//                {
//                    string code = mainEditor.Text;
//                    if (!string.IsNullOrEmpty(code))
//                    {
//                        modelEditor.Text = code;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"[ERROR] 초기 코드 복사 오류: {ex.Message}");
//            }

//            // 기본적으로 "전체 코드" 탭 선택
//            tabControl.SelectTab(MAIN_TAB_INDEX);
//        }

//        // 새 코드 탭 추가 - 메인 탭 여부 매개변수 추가
//        public UcCode AddCodeTab(string title, bool undeletable = false, bool isMainTab = false)
//        {
//            int tabIndex = lastTabIndex++;
//            tabControl.AddTab(title, tabIndex, undeletable, isMainTab);

//            // 새 코드 에디터 생성
//            UcCode codeEditor = new UcCode();
//            codeEditor.Dock = DockStyle.Fill;
//            codeEditor.Visible = false; // 처음에는 숨김
//            contentPanel.Controls.Add(codeEditor);

//            // 사전에 에디터 저장
//            codeEditors[tabIndex] = codeEditor;

//            // 방금 추가한 탭 선택
//            if (tabControl.TabCount == 1)
//            {
//                ShowCodeEditor(tabIndex);
//            }

//            return codeEditor;
//        }



//        // 메인 코드 복사하여 새 탭에 표시
//        public UcCode CopyMainCodeToNewTab(string title)
//        {
//            // 기본 탭의 코드 에디터 내용 가져오기
//            UcCode mainEditor = GetMainCodeEditor();
//            string code = mainEditor?.Text ?? string.Empty;

//            // 새 탭 추가
//            UcCode newEditor = AddCodeTab(title);

//            // 코드 복사
//            if (newEditor != null && !string.IsNullOrEmpty(code))
//            {
//                newEditor.UpdateCode(code);
//            }

//            return newEditor;
//        }

//        // 탭 선택 이벤트 처리
//        private void TabControl_TabSelected(object sender, TabEventArgs e)
//        {
//            if (e.TabItem.Tag is int tabIndex)
//            {
//                ShowCodeEditor(tabIndex);
//            }
//        }

//        // 탭 닫기 이벤트 처리
//        private void TabControl_TabClosed(object sender, TabEventArgs e)
//        {
//            if (e.TabItem.Tag is int tabIndex && codeEditors.ContainsKey(tabIndex))
//            {
//                // "전체 코드" 탭은 삭제 처리하지 않음 (추가 안전장치)
//                if (tabIndex == MAIN_TAB_INDEX)
//                    return;

//                UcCode codeEditor = codeEditors[tabIndex];
//                contentPanel.Controls.Remove(codeEditor);
//                codeEditor.Dispose();
//                codeEditors.Remove(tabIndex);

//                // 다른 탭이 선택되도록 처리하는 로직은 UcTab 내부에서 이미 처리됨
//                if (tabControl.SelectedTab != null && tabControl.SelectedTab.Tag is int selectedIndex)
//                {
//                    ShowCodeEditor(selectedIndex);
//                }
//            }
//        }

//        // 코드 에디터 표시
//        private void ShowCodeEditor(int tabIndex)
//        {
//            // 모든 에디터 숨기기
//            foreach (var editor in codeEditors.Values)
//            {
//                editor.Visible = false;
//            }

//            // 선택된 에디터만 표시
//            if (codeEditors.ContainsKey(tabIndex))
//            {
//                codeEditors[tabIndex].Visible = true;
//            }
//        }

//        // 현재 선택된 코드 에디터 가져오기
//        public UcCode GetCurrentCodeEditor()
//        {
//            if (tabControl.SelectedTab != null && tabControl.SelectedTab.Tag is int tabIndex &&
//                codeEditors.ContainsKey(tabIndex))
//            {
//                return codeEditors[tabIndex];
//            }

//            return null;
//        }

//        // 메인 코드 에디터("전체 코드" 탭) 가져오기
//        public UcCode GetMainCodeEditor()
//        {
//            if (codeEditors.ContainsKey(MAIN_TAB_INDEX))
//            {
//                return codeEditors[MAIN_TAB_INDEX];
//            }

//            return null;
//        }

//        // 코드 업데이트 (현재 선택된 탭)
//        public void UpdateCurrentCode(string code)
//        {
//            UcCode currentEditor = GetCurrentCodeEditor();
//            if (currentEditor != null)
//            {
//                currentEditor.UpdateCode(code);
//            }
//        }

//        // 특정 탭의 코드 업데이트
//        public void UpdateCodeByTabIndex(int tabIndex, string code)
//        {
//            if (codeEditors.ContainsKey(tabIndex))
//            {
//                codeEditors[tabIndex].UpdateCode(code);
//            }
//        }

//        // 메인 코드("전체 코드" 탭) 업데이트
//        public void UpdateMainCode(string code)
//        {
//            if (codeEditors.ContainsKey(MAIN_TAB_INDEX))
//            {
//                codeEditors[MAIN_TAB_INDEX].UpdateCode(code);
//            }
//        }
//        // 모델 생성 탭 가져오기
//        public UcCode GetModelEditor()
//        {
//            if (codeEditors.ContainsKey(MODEL_TAB_INDEX))
//            {
//                return codeEditors[MODEL_TAB_INDEX];
//            }

//            return null;
//        }

//        // 모델 생성 탭 업데이트
//        public void UpdateModelCode(string code)
//        {
//            if (codeEditors.ContainsKey(MODEL_TAB_INDEX))
//            {
//                codeEditors[MODEL_TAB_INDEX].UpdateCode(code);
//            }
//        }
//    }
//}
