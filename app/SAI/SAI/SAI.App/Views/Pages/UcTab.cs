//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Drawing.Drawing2D;
//using System.Windows.Forms;

//namespace SAI.SAI.App.Views.Pages
//{
//    public partial class UcTab : UserControl
//    {
//        private List<TabItem> tabs = new List<TabItem>();
//        private int selectedIndex = 0;
//        private const int TabHeight = 40;
//        private const int CloseButtonSize = 20;
//        private const int TabPadding = 15;
//        private bool showCloseButton = true;
//        private float cornerRadius = 19.45f; // 탭 모서리 반경 설정

//        // 이벤트 정의
//        public event EventHandler<TabEventArgs> TabSelected;
//        public event EventHandler<TabEventArgs> TabClosed;

//        // 생성자
//        public UcTab()
//        {
//            InitializeComponent();
//            this.DoubleBuffered = true;
//            this.Height = TabHeight;
//            this.BackColor = Color.White; // 흰색 배경색

//            // Load 이벤트 핸들러 등록 - 명시적으로 추가
//            this.Load += new EventHandler(UcTab_Load_1); // 명시적으로 UcTab_Load_1로 변경
//        }

//        // Load 이벤트 핸들러 - 이름 변경 (UcTab_Load_1로)
//        private void UcTab_Load_1(object sender, EventArgs e)
//        {
//            Console.WriteLine("[DEBUG] UcTab: UcTab_Load_1 이벤트 발생");
//            // 필요한 추가 초기화 코드
//        }

//        // 디자이너 자동 생성 이벤트 핸들러와 충돌 방지를 위한 빈 구현
//        private void UcTab_Load(object sender, EventArgs e)
//        {
//            // 디자이너에서 자동 생성되는 이벤트 핸들러용 빈 구현
//        }

//        protected override void OnPaint(PaintEventArgs e)
//        {
//            base.OnPaint(e);
//            Graphics g = e.Graphics;
//            g.SmoothingMode = SmoothingMode.AntiAlias;

//            // 이 부분이 변경됨 - 기존 직사각형 배경 대신 둥근 모서리 배경 그리기
//            using (GraphicsPath backgroundPath = new GraphicsPath())
//            {
//                // 전체 배경에 상단 모서리만 둥글게 처리
//                backgroundPath.AddArc(0, 0, cornerRadius * 2, cornerRadius * 2, 180, 90); // 왼쪽 상단
//                backgroundPath.AddArc(Width - (cornerRadius * 2), 0, cornerRadius * 2, cornerRadius * 2, 270, 90); // 오른쪽 상단
//                backgroundPath.AddLine(Width, cornerRadius, Width, Height); // 오른쪽 면
//                backgroundPath.AddLine(Width, Height, 0, Height); // 하단 면
//                backgroundPath.AddLine(0, Height, 0, cornerRadius); // 왼쪽 면
//                backgroundPath.CloseFigure();

//                // 배경 채우기
//                using (SolidBrush backBrush = new SolidBrush(BackColor))
//                {
//                    g.FillPath(backBrush, backgroundPath);
//                }

//                // 배경 테두리 그리기
//                using (Pen borderPen = new Pen(Color.Black, 1))
//                {
//                    g.DrawPath(borderPen, backgroundPath);
//                }
//            }
//            // 선택된 탭 표시선
//            using (Pen linePen = new Pen(Color.FromArgb(0, 122, 204), 2))
//            {
//                g.DrawLine(linePen, 0, Height - 2, Width, Height - 2);
//            }

//            if (tabs.Count == 0) return;

//            // 현재 x 위치 (그리기 시작점)
//            int x = 0;

//            // 각 탭 그리기
//            for (int i = 0; i < tabs.Count; i++)
//            {
//                TabItem tab = tabs[i];
//                bool isSelected = (i == selectedIndex);
//                bool isMainTab = tab.IsMainTab; // 메인 탭("전체 코드") 여부

//                // 탭 텍스트 크기 측정 - 메인 탭은 일반, 다른 탭은 볼드
//                Font tabFont = isMainTab ? Font : new Font(Font, FontStyle.Bold);
//                SizeF textSize = g.MeasureString(tab.Text, tabFont);
//                int tabWidth = (int)textSize.Width + (TabPadding * 2) + (showCloseButton && !tab.IsUndeletable ? CloseButtonSize : 0);
//                tab.Rectangle = new Rectangle(x, 0, tabWidth, TabHeight - 2);

//                // 탭 배경 색상 설정 - 메인 탭은 어두운 색, 다른 탭은 흰색
//                Color tabColor;
//                if (isMainTab)
//                {
//                    // 메인 탭("전체 코드")은 검정색 계열
//                    tabColor = isSelected ? Color.FromArgb(45, 45, 48) : Color.FromArgb(30, 30, 30);
//                }
//                else
//                {
//                    // 다른 탭들은 흰색 계열
//                    tabColor = isSelected ? Color.White : Color.FromArgb(240, 240, 240);
//                }

//                // 탭 배경 그리기 (메인 탭은 left-top만 둥글게, 다른 탭은 각진 직사각형)
//                if (isMainTab)
//                {
//                    // 메인 탭 - left-top만 둥글게
//                    using (GraphicsPath path = new GraphicsPath())
//                    {
//                        int rightX = x + tabWidth;

//                        // 왼쪽 상단만 둥글게
//                        path.AddArc(x, 0, cornerRadius * 2, cornerRadius * 2, 180, 90); // 왼쪽 상단
//                        path.AddLine(x + cornerRadius, 0, rightX, 0); // 상단 테두리
//                        path.AddLine(rightX, 0, rightX, TabHeight - 2); // 오른쪽 테두리
//                        path.AddLine(rightX, TabHeight - 2, x, TabHeight - 2); // 하단 테두리
//                        path.AddLine(x, TabHeight - 2, x, cornerRadius); // 왼쪽 테두리
//                        path.CloseFigure();

//                        // 탭 배경 채우기
//                        using (SolidBrush tabBrush = new SolidBrush(tabColor))
//                        {
//                            g.FillPath(tabBrush, path);
//                        }

//                        // 탭 테두리 그리기
//                        using (Pen borderPen = new Pen(Color.Black, 1))
//                        {
//                            g.DrawPath(borderPen, path);
//                        }
//                    }
//                }
//                else
//                {
//                    // 일반 탭 - 직사각형
//                    using (SolidBrush tabBrush = new SolidBrush(tabColor))
//                    {
//                        g.FillRectangle(tabBrush, tab.Rectangle);
//                    }

//                    // 탭 테두리 그리기
//                    using (Pen borderPen = new Pen(Color.Black, 1))
//                    {
//                        g.DrawRectangle(borderPen, tab.Rectangle);
//                    }
//                }

//                // 선택된 탭 하단에 강조선 그리기
//                if (isSelected)
//                {
//                    using (SolidBrush highlightBrush = new SolidBrush(Color.FromArgb(0, 122, 204)))
//                    {
//                        Rectangle highlightRect = new Rectangle(x, Height - 2, tabWidth, 2);
//                        g.FillRectangle(highlightBrush, highlightRect);
//                    }
//                }

//                // 탭 텍스트 그리기 - 메인 탭은 흰색, 다른 탭은 검정색
//                using (SolidBrush textBrush = new SolidBrush(isMainTab ? Color.White : Color.Black))
//                {
//                    StringFormat sf = new StringFormat
//                    {
//                        Alignment = StringAlignment.Center,
//                        LineAlignment = StringAlignment.Center
//                    };

//                    // 닫기 버튼 공간 감안하여 텍스트 위치 조정
//                    Rectangle textRect = showCloseButton && !tab.IsUndeletable
//                        ? new Rectangle(x, 0, tabWidth - CloseButtonSize, TabHeight - 2)
//                        : tab.Rectangle;

//                    g.DrawString(tab.Text, tabFont, textBrush, textRect, sf);
//                }

//                // 닫기 버튼 그리기 (필요한 경우만)
//                if (showCloseButton && !tab.IsUndeletable)
//                {
//                    int btnX = x + tabWidth - CloseButtonSize - 5;
//                    int btnY = (TabHeight - CloseButtonSize) / 2;
//                    Rectangle closeRect = new Rectangle(btnX, btnY, CloseButtonSize, CloseButtonSize);
//                    tab.CloseButtonRect = closeRect;

//                    // X 아이콘 그리기 - 탭 종류에 따라 색상 변경
//                    Color closeColor = isMainTab ? Color.LightGray : Color.DarkGray;
//                    using (Pen closePen = new Pen(closeColor, 1.5f))
//                    {
//                        g.DrawLine(closePen, btnX + 5, btnY + 5, btnX + CloseButtonSize - 5, btnY + CloseButtonSize - 5);
//                        g.DrawLine(closePen, btnX + 5, btnY + CloseButtonSize - 5, btnX + CloseButtonSize - 5, btnY + 5);
//                    }
//                }

//                // 다음 탭의 x 위치 업데이트
//                x += tabWidth;
//            }
//        }
//        protected override void OnMouseClick(MouseEventArgs e)
//        {
//            base.OnMouseClick(e);

//            for (int i = 0; i < tabs.Count; i++)
//            {
//                TabItem tab = tabs[i];

//                // 탭 영역 클릭 확인
//                if (tab.Rectangle.Contains(e.Location))
//                {
//                    // 닫기 버튼 클릭 확인 (삭제 불가능한 탭은 닫기 버튼 처리 안함)
//                    if (showCloseButton && !tab.IsUndeletable && tab.CloseButtonRect.Contains(e.Location))
//                    {
//                        // 닫기 버튼 클릭
//                        if (i == tabs.Count - 1 && i > 0)
//                        {
//                            // 마지막 탭이 닫히면 이전 탭 선택
//                            selectedIndex = i - 1;
//                        }
//                        else if (i < selectedIndex)
//                        {
//                            // 선택된 탭보다 앞에 있는 탭이 닫히면 선택 인덱스 조정
//                            selectedIndex--;
//                        }

//                        // 탭 닫기 이벤트 발생
//                        TabEventArgs args = new TabEventArgs { TabIndex = i, TabItem = tab };
//                        TabClosed?.Invoke(this, args);

//                        // 탭 제거
//                        tabs.RemoveAt(i);

//                        if (tabs.Count == 0)
//                        {
//                            selectedIndex = -1;
//                        }
//                        else if (selectedIndex >= tabs.Count)
//                        {
//                            selectedIndex = tabs.Count - 1;
//                        }

//                        Invalidate();
//                        return;
//                    }
//                    else
//                    {
//                        // 탭 선택
//                        if (selectedIndex != i)
//                        {
//                            selectedIndex = i;
//                            TabEventArgs args = new TabEventArgs { TabIndex = i, TabItem = tab };
//                            TabSelected?.Invoke(this, args);
//                            Invalidate();
//                        }
//                        return;
//                    }
//                }
//            }
//        }

//        // 탭 추가 메서드 - 메인 탭 여부 추가
//        public void AddTab(string text, object tag = null, bool undeletable = false, bool isMainTab = false)
//        {
//            TabItem newTab = new TabItem
//            {
//                Text = text,
//                Tag = tag,
//                IsUndeletable = undeletable,
//                IsMainTab = isMainTab
//            };

//            tabs.Add(newTab);

//            // 첫 번째 추가된 탭이면 선택
//            if (tabs.Count == 1)
//            {
//                selectedIndex = 0;
//                TabEventArgs args = new TabEventArgs { TabIndex = 0, TabItem = newTab };
//                TabSelected?.Invoke(this, args);
//            }

//            Invalidate();
//        }

//        // 탭 삭제 메서드
//        public void RemoveTab(int index)
//        {
//            if (index < 0 || index >= tabs.Count)
//                return;

//            // 삭제 불가능한 탭인 경우 무시
//            if (tabs[index].IsUndeletable)
//                return;

//            tabs.RemoveAt(index);

//            // 선택된 탭 조정
//            if (tabs.Count == 0)
//            {
//                selectedIndex = -1;
//            }
//            else if (selectedIndex >= tabs.Count)
//            {
//                selectedIndex = tabs.Count - 1;
//                TabEventArgs args = new TabEventArgs { TabIndex = selectedIndex, TabItem = tabs[selectedIndex] };
//                TabSelected?.Invoke(this, args);
//            }

//            Invalidate();
//        }

//        // 선택된 탭 변경 메서드
//        public void SelectTab(int index)
//        {
//            if (index < 0 || index >= tabs.Count || index == selectedIndex)
//                return;

//            selectedIndex = index;
//            TabEventArgs args = new TabEventArgs { TabIndex = selectedIndex, TabItem = tabs[selectedIndex] };
//            TabSelected?.Invoke(this, args);
//            Invalidate();
//        }

//        // 선택된 탭 인덱스 속성
//        public int SelectedIndex
//        {
//            get { return selectedIndex; }
//            set { SelectTab(value); }
//        }

//        // 선택된 탭 아이템 속성
//        public TabItem SelectedTab
//        {
//            get { return (selectedIndex >= 0 && selectedIndex < tabs.Count) ? tabs[selectedIndex] : null; }
//        }

//        // 탭 수 속성
//        public int TabCount
//        {
//            get { return tabs.Count; }
//        }

//        // 닫기 버튼 표시 여부 속성
//        public bool ShowCloseButton
//        {
//            get { return showCloseButton; }
//            set
//            {
//                if (showCloseButton != value)
//                {
//                    showCloseButton = value;
//                    Invalidate();
//                }
//            }
//        }

//        // 모서리 둥글기 속성
//        public float CornerRadius
//        {
//            get { return cornerRadius; }
//            set
//            {
//                if (cornerRadius != value)
//                {
//                    cornerRadius = value;
//                    Invalidate();
//                }
//            }
//        }

//        // 탭 가져오기 메서드
//        public TabItem GetTab(int index)
//        {
//            if (index < 0 || index >= tabs.Count)
//                return null;

//            return tabs[index];
//        }
//    }

//    // 탭 아이템 클래스 - IsMainTab 속성 추가
//    public class TabItem
//    {
//        public string Text { get; set; }
//        public object Tag { get; set; }
//        public Rectangle Rectangle { get; set; }
//        public Rectangle CloseButtonRect { get; set; }
//        public bool IsUndeletable { get; set; } = false; // 삭제 불가능 여부
//        public bool IsMainTab { get; set; } = false; // 메인 탭("전체 코드") 여부
//    }

//    // 탭 이벤트 인자 클래스
//    public class TabEventArgs : EventArgs
//    {
//        public int TabIndex { get; set; }
//        public TabItem TabItem { get; set; }
//    }
//}