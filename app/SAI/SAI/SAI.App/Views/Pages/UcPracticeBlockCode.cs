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
using SAI.SAI.App.Models;
using static SAI.SAI.App.Models.BlocklyModel;
using System.Collections.Generic;


namespace SAI.SAI.App.Views.Pages
{
    public partial class UcPracticeBlockCode : UserControl, IUcShowDialogView, IBlocklyView
	{
		private BlocklyPresenter blocklyPresenter;
		private UcShowDialogPresenter ucShowDialogPresenter;
		
        private readonly IMainView mainView;

        private BlocklyModel blocklyModel;

		public event EventHandler HomeButtonClicked;

		public event EventHandler<BlockEventArgs> AddBlockButtonClicked;
		public event EventHandler AddBlockButtonDoubleClicked;
		private JsBridge jsBridge;

		private bool isInferPanelVisible = false;
        private int inferPanelWidth = 420; 
        private int originalCodePanelWidth;
        private int originalCodePanelLeft;
        private bool isMemoPanelVisible = false;

        private double currentThreshold = 0.5; // threshold 기본값 0.5

		private int undoCount = 0; // 뒤로가기 카운트

        private int currentZoomLevel = 60; // 현재 확대/축소 레벨 (기본값 60%)
        private readonly int[] zoomLevels = { 0, 20, 40, 60, 80, 100, 120, 140, 160, 180, 200 }; // 가능한 확대/축소 레벨

		public UcPracticeBlockCode(IMainView view)
        {
            InitializeComponent();

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

            // 정언이가 선언
			//생성자---------------
			blocklyPresenter = new BlocklyPresenter(this);
            this.mainView = view;
            blocklyModel = BlocklyModel.Instance;
			InitializeWebView2();

            // 여기에 UcCode 추가
            try
            {
                if (ucCode２ != null)
                {
                    // BlocklyPresenter에 기존 ucCode２ 설정
                    blocklyPresenter.SetCodeView(ucCode２);
                    Console.WriteLine("[DEBUG] UcPracticeBlockCode: ICodeView 설정 완료");

                    // BlocklyModel 이벤트 구독 확인
                    blocklyModel.BlockAllCodeChanged += (code) => {
                        Console.WriteLine($"[DEBUG] UcPracticeBlockCode: BlockAllCodeChanged 이벤트 발생 - 코드 길이: {code?.Length ?? 0}");
                    };
                }
                else
                {
                    Console.WriteLine("[ERROR] UcPracticeBlockCode: ucCode２가 null입니다");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] UcPracticeBlockCode: ICodeView 설정 중 오류 - {ex.Message}");
            }

            //ToolTipUtils.CustomToolTip(pboxGraphe, "자세히 보려면 클릭하세요.");
            ToolTipUtils.CustomToolTip(btnInfoThreshold,
              "AI의 분류 기준입니다. 예측 결과가 이 값보다 높으면 '맞다(1)'고 판단하고, 낮으면 '아니다(0)'로 처리합니다.");

            ToolTipUtils.CustomToolTip(btnInfoGraph,
              "AI 모델의 성능을 한눈에 확인할 수 있는 그래프입니다. 정확도, 재현율 등의 성능 지표가 포함되어 있습니다.");
            ToolTipUtils.CustomToolTip(btnSelectInferImage, "추론에 사용할 이미지를 가져오려면 클릭하세요.");

            ButtonUtils.SetupButton(btnRunModel,"btnRunModel_clicked", "btn_run_model");
            ButtonUtils.SetupButton(btnNextBlock, "btn_next_block_clicked", "btn_next_block1");
            ButtonUtils.SetupButton(btnPreBlock, "btn_pre_block_clicked", "btn_pre_block1");
            ButtonUtils.SetupButton(btnTrash, "btn_trash_clicked", "btn_trash_block");
            ButtonUtils.SetupButton(btnQuestionMemo, "btn_question_memo_clicked", "btn_question_memo");
            ButtonUtils.SetupButton(btnCloseMemo, "btn_close_25_clicked", "btn_close_25");
            ButtonUtils.SetupButton(btnSelectInferImage, "btn_selectinferimage_hover", "btn_selectinferimage");
            ButtonUtils.SetupButton(btnCopy, "btn_copy_hover", "btn_copy");

			undoCount = 0;
			btnNextBlock.Visible = false; // 처음에는 보이지 않게 설정
			// btnRunModel---------------
			btnRunModel.BackColor = Color.Transparent;
			btnRunModel.PressedColor = Color.Transparent;
			btnRunModel.CheckedState.FillColor = Color.Transparent;
			btnRunModel.DisabledState.FillColor = Color.Transparent;
			btnRunModel.HoverState.FillColor = Color.Transparent;
			// btnRunModel 마우스 입력 될 때
			btnRunModel.MouseEnter += (s, e) =>
			{
				btnRunModel.BackColor = Color.Transparent;
				btnRunModel.BackgroundImage = Properties.Resources.btnRunModel_clicked;
			};
			// btnRunModel 마우스 떠날때
			btnRunModel.MouseLeave += (s, e) =>
			{
				btnRunModel.BackgroundImage = Properties.Resources.btn_run_model;
			};
			// 스크롤바 설정-------------------
			var ucPracticeBlockList = new UcPracticeBlockList(this, AddBlockButtonClicked);
			pSelectBlock.Controls.Add(ucPracticeBlockList);
			pSelectBlock.AutoScroll = false;
			ucPracticeBlockList.AutoScroll = false;
			pSelectBlockvScrollBar.Scroll += (s, e) =>
			{
				if (!pSelectBlockvScrollBar.Visible) return; // ❗ 스크롤바 안 보이면 무시

				ucPracticeBlockList.content.Top = -pSelectBlockvScrollBar.Value;
			};
			pSelectBlockvScrollBar.Maximum = ucPracticeBlockList.content.Height - pSelectBlockvScrollBar.Height;
			ucPracticeBlockList.SizeChanged += (s,e) =>
			{
				int contentHeight = ucPracticeBlockList.content.Height;
				int viewportHeight = pSelectBlock.Size.Height;

				int newMax = contentHeight - viewportHeight;
				if(newMax <= 0)
				{
					pSelectBlockvScrollBar.Visible = false;
					pSelectBlockvScrollBar.Maximum = 0;
					pSelectBlockvScrollBar.Value = 0;
					ucPracticeBlockList.content.Top = 0;
				}
				else
				{
					pSelectBlockvScrollBar.Visible = true;
					pSelectBlockvScrollBar.Maximum = newMax;
				}
			};
			pSelectBlock.MouseEnter += (s, e) => pSelectBlock.Focus();
			// 마우스 휠 이벤트 수동 처리
			pSelectBlock.MouseWheel += (s, e) =>
			{
				if (!pSelectBlockvScrollBar.Visible) return; // ❗ 스크롤 안 보이면 스킵

				int newValue = pSelectBlockvScrollBar.Value - e.Delta / 5; // 120 → 한 칸, 반전 여부 조절 가능
				newValue = Math.Max(pSelectBlockvScrollBar.Minimum, Math.Min(pSelectBlockvScrollBar.Maximum, newValue));
				pSelectBlockvScrollBar.Value = newValue;
			};

            btnCopy.Click += (s, e) =>
            {
                try
                {
                    // BlocklyModel에서 전체 코드 가져오기
                    string codeToCopy = blocklyModel.blockAllCode;
                    
                    if (!string.IsNullOrEmpty(codeToCopy))
                    {
                        // 클립보드에 코드 복사
                        Clipboard.SetText(codeToCopy);
                        Console.WriteLine("[DEBUG] UcPracticeBlockCode: 코드가 클립보드에 복사됨");
                    }
                    else
                    {
                        Console.WriteLine("[WARNING] UcPracticeBlockCode: 복사할 코드가 없음");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] UcPracticeBlockCode: 코드 복사 중 오류 발생 - {ex.Message}");
                }
            };

            // 코드 확대/축소 버튼 및 퍼센트 표시 컨트롤 연결 (튜토리얼과 동일하게)
            guna2ImageButton2.Click += (s, e) =>
            {
                try
                {
                    int currentIndex = Array.IndexOf(zoomLevels, currentZoomLevel);
                    if (currentIndex < zoomLevels.Length - 1)
                    {
                        currentZoomLevel = zoomLevels[currentIndex + 1];
                        UpdateCodeZoom();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] UcPracticeBlockCode: 확대 중 오류 발생 - {ex.Message}");
                }
            };

            guna2ImageButton1.Click += (s, e) =>
            {
                try
                {
                    int currentIndex = Array.IndexOf(zoomLevels, currentZoomLevel);
                    if (currentIndex > 0)
                    {
                        currentZoomLevel = zoomLevels[currentIndex - 1];
                        UpdateCodeZoom();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] UcPracticeBlockCode: 축소 중 오류 발생 - {ex.Message}");
                }
            };

            // 초기 확대/축소 레벨 설정
            currentZoomLevel = 60;
            UpdateCodeZoom();

            // PercentUtils로 퍼센트 박스 스타일 일괄 적용
            PercentUtils.SetupPercentTextBox(guna2TextBox1, 0.5f, 0, 0);
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

		// webview에 blockly tutorial html 붙이기
		private async void InitializeWebView2()
		{
			jsBridge = new JsBridge((message, type) =>
			{
				blocklyPresenter.HandleJsMessage(message, type);
			});

			var baseDir = AppDomain.CurrentDomain.BaseDirectory;
			string localPath = Path.GetFullPath(Path.Combine(baseDir, @"..\\..\\Blockly\\TrainBlockly.html"));
			string uri = new Uri(localPath).AbsoluteUri;

			webViewblock.WebMessageReceived += async (s, e) =>
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

										blocklyModel.imgPath = filePath;

										string json = $@"{{
											""blockId"": {escapedBlockId},
											""filePath"": {escapedFilePath}
										}}";

										await webViewblock.ExecuteScriptAsync(
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

							case "blockDoubleClick":
								string eventCode = root.GetProperty("code").GetString();
								blocklyPresenter.OnAddBlockDoubleClicked(eventCode);
								break;

							case "blockTypes":
								var jsonTypes = root.GetProperty("types");
								var blockTypes = JsonSerializer.Deserialize<List<BlockInfo>>(jsonTypes.GetRawText());
								blocklyPresenter.setBlockTypes(blockTypes);
								break;
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show($"WebView2 메시지 처리 오류: {ex.Message}");
				}
			};

			webViewblock.ZoomFactor = 0.5; // 줌 비율 설정

			await webViewblock.EnsureCoreWebView2Async();
			webViewblock.Source = new Uri(uri);
		}

		// JS 함수 호출 = 블럭 넣기
		public void addBlock(string blockType)
		{
			webViewblock.ExecuteScriptAsync($"addBlock('{blockType}')");
		}

		// JS 함수호출 = 하나의 블럭의 코드 가져오기
		public void getPythonCodeByType(string blockType)
		{
			webViewblock.ExecuteScriptAsync($"getPythonCodeByType('{blockType}')");
		}

		// blockly 웹뷰 확대 조절 함수
		private void webViewblock_ZoomFactorChanged(object sender, EventArgs e)
		{
			webViewblock.ZoomFactor = 0.5;
		}

		// JS 함수 호출 = 다시 실행하기
		private void ibtnNextBlock_Click(object sender, EventArgs e)
		{
			undoCount--;
			webViewblock.ExecuteScriptAsync($"redo()");

			if (undoCount == 0)
			{
				btnNextBlock.Visible = false;
				btnPreBlock.Visible = true;
			}
			else
			{
				btnNextBlock.Visible = true;
				btnPreBlock.Visible = true;
			}
		}

		// JS 함수 호출 = 되돌리기
		private void ibtnPreBlock_Click(object sender, EventArgs e)
		{
			if (undoCount <= 10)
			{
				undoCount++;
				webViewblock.ExecuteScriptAsync($"undo()");
				btnNextBlock.Visible = true;
				btnPreBlock.Visible = true;
			}
			else
			{
				btnNextBlock.Visible = true;
				btnPreBlock.Visible = false;
			}
		}

		// JS 함수 호출 - 블럭 모두 삭제
		private void btnTrashBlock_Click(object sender, EventArgs e)
		{
			webViewblock.ExecuteScriptAsync($"clear()");
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

        private void UcPracticeBlockCode_Load(object sender, EventArgs e)
        {

        }

        private void webViewCode_Click(object sender, EventArgs e)
        {

        }

        private void ucCode1_Load(object sender, EventArgs e)
        {
            // ucCode２ 로드 이벤트 처리
        }

        private void UpdateCodeZoom()
        {
            try
            {
                if (ucCode２ != null)
                {
                    // Scintilla 에디터의 폰트 크기 업데이트
                    ucCode２.UpdateFontSize(currentZoomLevel);
                    // 확대/축소 레벨 표시 업데이트
                    guna2TextBox1.Text = $"{currentZoomLevel}%";
                    Console.WriteLine($"[DEBUG] UcPracticeBlockCode: 코드 확대/축소 레벨 변경 - {currentZoomLevel}%");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] UcPracticeBlockCode: 확대/축소 레벨 업데이트 중 오류 발생 - {ex.Message}");
            }
        }
    }
}
