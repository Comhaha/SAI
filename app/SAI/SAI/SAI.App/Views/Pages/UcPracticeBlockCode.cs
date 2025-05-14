using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using SAI.SAI.App.Models.Events;
using SAI.SAI.App.Presenters;
using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.Application.Interop;
using System.Text.Json;
using SAI.SAI.App.Views.Common;
using SAI.SAI.App.Forms.Dialogs;


namespace SAI.SAI.App.Views.Pages
{
    public partial class UcPracticeBlockCode : UserControl, IUcShowDialogView, IBlocklyView
	{
		private BlocklyPresenter blocklyPresenter;
		private UcShowDialogPresenter ucShowDialogPresenter;
		private readonly IMainView mainView;

		public event EventHandler HomeButtonClicked;

		public event EventHandler<BlockEventArgs> AddBlockButtonClicked;
		public event EventHandler<BlockEventArgs> AddBlockButtonDoubleClicked;
		private JsBridge jsBridge;

		private bool isInferPanelVisible = false;
        private int inferPanelWidth = 420; 
        private int originalCodePanelWidth;
        private int originalCodePanelLeft;
        private bool isMemoPanelVisible = false;

        private double currentThreshold = 0.5; // threshold 기본값 0.5

        public UcPracticeBlockCode(IMainView view)
        {
            InitializeComponent();
			blocklyPresenter = new BlocklyPresenter(this);

			this.mainView = view;
			ucShowDialogPresenter = new UcShowDialogPresenter(this);

            // 홈페이지 이동
            ibtnHome.Click += (s, e) => {
                mainView.LoadPage(new UcSelectType(mainView));
            };

            ibtnHome.BackColor = Color.Transparent;
            ibtnInfer.BackColor = Color.Transparent;    
            ibtnMemo.BackColor = Color.Transparent;

            // 초기에는 숨기길 패널들
            pSideInfer.Visible = false;
            ibtnCloseInfer.Visible = false;
            pMemo.Visible = false;
            pboxInferAccuracy.Visible = false;
            // 추론사이드패널에서 '이미지 불러오기' 버튼 누르고 'pboxInferAccuracy'에 이미지 띄우고
            // pboxInferAccuracy.Visible = true 해주시면 됩니다.

            MemoUtils.ApplyStyle(tboxMemo);
            SetupThresholdControls();
            ScrollUtils.AdjustPanelScroll(pSideInfer);

			InitializeWebView2();

            // ToolTip 설정
            ToolTip toolTip = new ToolTip();
            toolTip.AutoPopDelay = 3000;
            toolTip.InitialDelay = 300;
            toolTip.ReshowDelay = 300;
            toolTip.ShowAlways = true;
            toolTip.OwnerDraw = true;
            toolTip.Draw += (s, e) =>
            {
                Font notoSans = new Font("Noto Sans KR", 9); // 원하는 폰트
                e.DrawBackground();                       
                e.DrawBorder();                              
                e.Graphics.DrawString(e.ToolTipText, notoSans, Brushes.Black, new PointF(2, 2)); // 텍스트 직접 그리기
            };
            // 크기 조절
            toolTip.Popup += (s, e) =>
            {
                Font notoSans = new Font("Noto Sans KR", 9);
                string text = toolTip.GetToolTip(pboxGraphe);
                Size size = TextRenderer.MeasureText(text, notoSans);
                e.ToolTipSize = new Size(size.Width + 8, size.Height + 4);
            };
            toolTip.SetToolTip(pboxGraphe, "자세히 보려면 클릭하세요.");

            // btnRunModel
            SetupButton(btnRunModel);
            btnRunModel.MouseEnter += (s, e) =>
            {
                btnRunModel.BackColor = Color.Transparent;
                btnRunModel.BackgroundImage = Properties.Resources.btnRunModel_clicked;
            };
            btnRunModel.MouseLeave += (s, e) =>
            {
                btnRunModel.BackgroundImage = Properties.Resources.btn_run_model;
            };

            // btnNextBlock
            SetupButton(btnNextBlock);
            btnNextBlock.MouseEnter += (s, e) =>
            {
                btnNextBlock.BackColor = Color.Transparent;
                btnNextBlock.BackgroundImage = Properties.Resources.btn_next_block_clicked;
            };
            btnNextBlock.MouseLeave += (s, e) =>
            {
                btnNextBlock.BackgroundImage = Properties.Resources.btn_next_block1;
            };

            // btnPreBlock
            SetupButton(btnPreBlock);
            btnPreBlock.MouseEnter += (s, e) =>
            {
                btnPreBlock.BackColor = Color.Transparent;
                btnPreBlock.BackgroundImage = Properties.Resources.btn_pre_block_clicked;
            };
            btnPreBlock.MouseLeave += (s, e) =>
            {
                btnPreBlock.BackgroundImage = Properties.Resources.btn_pre_block1;
            };

            // btnTrash
            SetupButton(btnTrash);
            btnTrash.MouseEnter += (s, e) =>
            {
                btnTrash.BackColor = Color.Transparent;
                btnTrash.BackgroundImage = Properties.Resources.btn_trash_clicked;
            };
            btnTrash.MouseLeave += (s, e) =>
            {
                btnTrash.BackgroundImage = Properties.Resources.btn_trash_block;
            };

            // btnQuestionMemo
            SetupButton(btnQuestionMemo);
            btnQuestionMemo.MouseEnter += (s, e) =>
            {
                btnQuestionMemo.BackColor = Color.Transparent;
                btnQuestionMemo.BackgroundImage = Properties.Resources.btn_question_memo_clicked;
            };
            btnQuestionMemo.MouseLeave += (s, e) =>
            {
                btnQuestionMemo.BackgroundImage = Properties.Resources.btn_question_memo;
            };

            // btnCloseMemo
            SetupButton(btnCloseMemo);
            btnCloseMemo.MouseEnter += (s, e) =>
            {
                btnCloseMemo.BackColor = Color.Transparent;
                btnCloseMemo.BackgroundImage = Properties.Resources.btn_close_25_clicked;
            };
            btnCloseMemo.MouseLeave += (s, e) =>
            {
                btnCloseMemo.BackgroundImage = Properties.Resources.btn_close_25;
            };
        }
        private void UcPraticeBlockCode_Load(object sender, EventArgs e)
        {
        }
        void SetupButton(Guna.UI2.WinForms.Guna2Button btn)
        {
            btn.BackColor = Color.Transparent;
            btn.PressedColor = Color.Transparent;
            btn.CheckedState.FillColor = Color.Transparent;
            btn.DisabledState.FillColor = Color.Transparent;
            btn.HoverState.FillColor = Color.Transparent;
        }
        private void ShowpSIdeInfer()
        {
            pSideInfer.Visible = true;
            ibtnCloseInfer.Visible = true;
            isInferPanelVisible = true;
        }

        private void HidepSideInfer()
        {
            pSideInfer.Visible = false;
            ibtnCloseInfer.Visible = false;
            isInferPanelVisible = false;
        }

        private void SetupThresholdControls()
        {
            ThresholdUtils.Setup(tbarThreshold, tboxThreshold, (newValue) =>
            {
                currentThreshold = newValue;
            });
        }

        private void ibtnHome_Click(object sender, EventArgs e)
        {
            HomeButtonClicked?.Invoke(this, EventArgs.Empty); // Presenter에게 알림
        }

        private void ibtnInfer_Click(object sender, EventArgs e)
        {
            if (!isInferPanelVisible)
            {
                ShowpSIdeInfer();
            }
            else
            {
                HidepSideInfer();
            }
        }

        private void ibtnCloseInfer_Click(object sender, EventArgs e)
        {
            HidepSideInfer();
        }

        private void ibtnMemo_Click(object sender, EventArgs e)
        {
            isMemoPanelVisible = !isMemoPanelVisible;
            pMemo.Visible = isMemoPanelVisible;
        }

        private void btnCloseMemo_Click(object sender, EventArgs e)
        {
            isMemoPanelVisible = !isMemoPanelVisible;
            pMemo.Visible = isMemoPanelVisible;
        }

        private void ibtnGoNotion_Click(object sender, EventArgs e)
        {
            using (var dialog = new DialogNotion())
            {
                dialog.ShowDialog();
            }
        }
		private void ibtnDone_Click(object sender, EventArgs e)
		{
			ucShowDialogPresenter.clickFinish();
		}

		public void showDialog(Form dialog)
		{
			dialog.Owner = mainView as Form;
			dialog.ShowDialog();
		}

		private async void InitializeWebView2()
		{
			jsBridge = new JsBridge((message, type) =>
			{
				blocklyPresenter.HandleJsMessage(message, type);
			});

			var baseDir = AppDomain.CurrentDomain.BaseDirectory;
			string localPath = Path.GetFullPath(Path.Combine(baseDir, @"..\\..\\Blockly\\TrainBlockly.html"));
			string uri = new Uri(localPath).AbsoluteUri;

			webViewBlock.WebMessageReceived += async (s, e) =>
			{
				try
				{
					// 먼저 시도: 객체 기반 JSON 메시지 처리
					var doc = JsonDocument.Parse(e.WebMessageAsJson);
					var root = doc.RootElement;

					if (root.ValueKind == JsonValueKind.Object &&
						root.TryGetProperty("type", out var typeElem))
					{
						string type = typeElem.GetString();

						switch (type)
						{
							case "openFile":
								using (OpenFileDialog dialog = new OpenFileDialog())
								{
									dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
									dialog.Multiselect = false;
									string blockId = root.GetProperty("blockId").GetString(); // blockId를 가져옴
									if (dialog.ShowDialog() == DialogResult.OK)
									{
										string filePath = dialog.FileName.Replace("\\", "/");
										string escapedFilePath = JsonSerializer.Serialize(filePath);
										string escapedBlockId = JsonSerializer.Serialize(blockId); // 이건 위에서 받은 blockId

										string json = $@"{{
											""blockId"": {escapedBlockId},
											""filePath"": {escapedFilePath}
										}}";

										await webViewBlock.ExecuteScriptAsync(
											$"window.dispatchEvent(new MessageEvent('message', {{ data: {json} }}));"
										);
									}
								}
								break;

							case "blockAllCode":
								string blockAllCode = root.GetProperty("code").GetString();
								jsBridge.receiveMessageFromJs(blockAllCode, type);
								break;

							case "blockCode":
								string blockCode = root.GetProperty("code").GetString();
								jsBridge.receiveMessageFromJs(blockCode, type);
								break;
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show($"WebView2 메시지 처리 오류: {ex.Message}");
				}
			};

			webViewBlock.ZoomFactor = 0.7; // 줌 비율 설정

			await webViewBlock.EnsureCoreWebView2Async();
			webViewBlock.Source = new Uri(uri);
		}

		// Presenter가 호출할 메서드(UI에 있는 웹뷰에 명령을 내리는 UI 행위) : 블록 생성
		public void addBlock(string blockType)
		{
			webViewBlock.ExecuteScriptAsync($"addBlock('{blockType}')");
		}

		// 개별 블록 코드를 받아오기위한 JS 코드 호출 함수
		public void getPythonCodeByType(string blockType)
		{
			webViewBlock.ExecuteScriptAsync($"getPythonCodeByType('{blockType}')");
		}

        private void webViewBlock_ZoomFactorChanged(object sender, EventArgs e)
		{
			//webViewBlock.ZoomFactor = 0.7;
		}

        private void ibtnAiFeedback_Click(object sender, EventArgs e)
        {
            using (var dialog = new DialogNotion())
            {
                dialog.ShowDialog();
            }
        }

        private void pboxGraphe_Click(object sender, EventArgs e)
        {
            using (var dialog = new DialogModelPerformance())
            {
                dialog.ShowDialog();
            }
        }
    }
}
