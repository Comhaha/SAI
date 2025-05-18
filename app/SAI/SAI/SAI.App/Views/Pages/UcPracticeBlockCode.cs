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
using System.Threading.Tasks;
using System.Threading;
using Timer = System.Windows.Forms.Timer;


namespace SAI.SAI.App.Views.Pages
{
    public partial class UcPracticeBlockCode : UserControl, IUcShowDialogView, IBlocklyView, IPracticeInferenceView
    {
        private BlocklyPresenter blocklyPresenter;
        private UcShowDialogPresenter ucShowDialogPresenter;
        private DialogInferenceLoading dialogLoadingInfer;

        private readonly IMainView mainView;

        private BlocklyModel blocklyModel;

        public event EventHandler HomeButtonClicked;

        public event EventHandler<BlockEventArgs> AddBlockButtonClicked;
        public event EventHandler AddBlockButtonDoubleClicked;
        private JsBridge jsBridge;

        private bool isInferPanelVisible = false;
        private int inferPanelWidth = 420;
        private bool isMemoPanelVisible = false;

        private double currentThreshold = 0.5; // threshold 기본값 0.5
        private string selectedImagePath = string.Empty; // 추론 이미지 경로를 저장할 변수

		private int undoCount = 0; // 뒤로가기 카운트
		private int blockCount = 0; // 블럭 개수

		private string errorMessage = "";
		private string missingType = "";
		private string errorType = "";

		private CancellationTokenSource _toastCancellationSource;

        private int currentZoomLevel = 60; // 현재 확대/축소 레벨 (기본값 60%)
        private readonly int[] zoomLevels = { 0, 20, 40, 60, 80, 100, 120, 140, 160, 180, 200 }; // 가능한 확대/축소 레벨

        public UcPracticeBlockCode(IMainView view)
        {
            InitializeComponent();

            ucShowDialogPresenter = new UcShowDialogPresenter(this);

            // 홈페이지 이동
            ibtnHome.Click += (s, e) =>
            {
                mainView.LoadPage(new UcSelectType(mainView));
            };

            ibtnHome.BackColor = Color.Transparent;
            ibtnInfer.BackColor = Color.Transparent;
            ibtnMemo.BackColor = Color.Transparent;
            ButtonUtils.SetTransparentStyle(btnCopy);

            // 초기에는 숨기길 패널들
            pSideInfer.Visible = false;
            ibtnCloseInfer.Visible = false;
            pMemo.Visible = false;
            pboxInferAccuracy.Visible = false;
            cAlertPanel.Visible = false;  // 복사 알림 패널도 초기에 숨김

            btnSelectInferImage.Visible = false;

            // 새 이미지 불러오기 버튼 설정
            btnSelectInferImage.Size = new Size(494, 278);  // pInferAccuracy와 동일한 크기
            btnSelectInferImage.Location = new Point(0, 0); // pInferAccuracy 내에서의 위치
            btnSelectInferImage.Enabled = true;
            btnSelectInferImage.Cursor = Cursors.Hand;
            btnSelectInferImage.Click += new EventHandler(btnSelectInferImage_Click);

            pInferAccuracy.MouseEnter += (s, e) =>
            {
                if (pSideInfer.Visible)
                {
                    btnSelectInferImage.Visible = true;
                    btnSelectInferImage.BringToFront();
                    btnSelectInferImage.BackgroundImage = Properties.Resources.btn_selectinferimage_hover;
                }
            };

            pInferAccuracy.MouseLeave += (s, e) =>
            {
                if (!btnSelectInferImage.ClientRectangle.Contains(btnSelectInferImage.PointToClient(Control.MousePosition)))
                {
                    btnSelectInferImage.Visible = false;
                    btnSelectInferImage.BackgroundImage = Properties.Resources.btn_selectinferimage;
                }
            };

            // 버튼에도 MouseEnter/Leave 이벤트 추가
            btnSelectInferImage.MouseEnter += (s, e) =>
            {
                btnSelectInferImage.Visible = true;
                btnSelectInferImage.BackgroundImage = Properties.Resources.btn_selectinferimage_hover;
            };

            btnSelectInferImage.MouseLeave += (s, e) =>
            {
                if (!pInferAccuracy.ClientRectangle.Contains(pInferAccuracy.PointToClient(Control.MousePosition)))
                {
                    btnSelectInferImage.Visible = false;
                    btnSelectInferImage.BackgroundImage = Properties.Resources.btn_selectinferimage;
                }
            };

            MemoUtils.ApplyStyle(tboxMemo);
            SetupThresholdControls();
            ScrollUtils.AdjustPanelScroll(pSideInfer);



            //ToolTipUtils.CustomToolTip(pboxGraphe, "자세히 보려면 클릭하세요.");
            ToolTipUtils.CustomToolTip(btnInfoThreshold,
              "AI의 분류 기준입니다. 예측 결과가 이 값보다 높으면 '맞다(1)'고 판단하고, 낮으면 '아니다(0)'로 처리합니다.");

            ToolTipUtils.CustomToolTip(btnInfoGraph,
              "AI 모델의 성능을 한눈에 확인할 수 있는 그래프입니다. 정확도, 재현율 등의 성능 지표가 포함되어 있습니다.");
            ToolTipUtils.CustomToolTip(btnSelectInferImage, "추론에 사용할 이미지를 가져오려면 클릭하세요.");

            ButtonUtils.SetupButton(btnRunModel, "btnRunModel_clicked", "btn_run_model");
            ButtonUtils.SetupButton(btnNextBlock, "btn_next_block_clicked", "btn_next_block1");
            ButtonUtils.SetupButton(btnPreBlock, "btn_pre_block_clicked", "btn_pre_block1");
            ButtonUtils.SetupButton(btnTrash, "btn_trash_clicked", "btn_trash_block");
            ButtonUtils.SetupButton(btnQuestionMemo, "btn_question_memo_clicked", "btn_question_memo");
            ButtonUtils.SetupButton(btnCloseMemo, "btn_close_25_clicked", "btn_close_25");
            ButtonUtils.SetupButton(btnCopy, "btn_copy_hover", "btn_copy");
            ButtonUtils.SetTransparentStyle(btnSelectInferImage);

			blockCount = 0; // 블럭 개수 초기화
			undoCount = 0;
			btnNextBlock.Visible = false; // 처음에는 보이지 않게 설정
			btnPreBlock.Visible = false; // 처음에는 보이지 않게 설정
            // 정언이가 선언
            //생성자---------------
            blocklyPresenter = new BlocklyPresenter(this);
            this.mainView = view;
            blocklyModel = BlocklyModel.Instance;
            InitializeWebView2();

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
            ucPracticeBlockList.SizeChanged += (s, e) =>
            {
                int contentHeight = ucPracticeBlockList.content.Height;
                int viewportHeight = pSelectBlock.Size.Height;

                int newMax = contentHeight - viewportHeight;
                if (newMax <= 0)
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

            // mAlertPanel 초기에는 숨김
            mAlertPanel.Visible = false;
            // btnQuestionMemo 클릭 이벤트 핸들러 등록
            btnQuestionMemo.Click += btnQuestionMemo_Click;

            // 여기에 UcCode 추가
            try
            {
                if (ucCode２ != null)
                {
                    // BlocklyPresenter에 기존 ucCode２ 설정
                    blocklyPresenter.SetCodeView(ucCode２);
                    Console.WriteLine("[DEBUG] UcPracticeBlockCode: ICodeView 설정 완료");

                    // BlocklyModel 이벤트 구독 확인
                    blocklyModel.BlockAllCodeChanged += (code) =>
                    {
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
            ThresholdUtilsPractice.Setup(
                tbarThreshold,
                tboxThreshold,
                (newValue) => currentThreshold = newValue,
                this
            );
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
							case "blockCount":
								var jsonCount = root.GetProperty("count").ToString();
								blockCount = int.Parse(jsonCount);
								break;
							case "blockCreated":
								var blockType = root.GetProperty("blockType").ToString();
								var newValue = root.GetProperty("allValues");
								var value = JsonSerializer.Deserialize<Dictionary<string, object>>(newValue.GetRawText());
								blocklyPresenter.setFieldValue(blockType, value);
								break;

							case "blockFieldUpdated":
								blockType = root.GetProperty("blockType").ToString();
								var allValues = root.GetProperty("allValues");
								value = JsonSerializer.Deserialize<Dictionary<string, object>>(allValues.GetRawText());
								blocklyPresenter.setFieldValue(blockType, value);
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

			// 단축키 이벤트 등록
			webViewblock.KeyDown += (sender, e) =>
			{
				if (e.Control)
				{
					if (e.KeyCode == Keys.Z && !e.Shift) // Ctrl + Z
					{
						if (btnPreBlock.Visible)
						{
							btnPreBlock_Click(btnPreBlock, EventArgs.Empty);
							e.Handled = true;
						}
					}
					else if (e.KeyCode == Keys.Y || (e.KeyCode == Keys.Z && e.Shift)) // Ctrl + Y 또는 Ctrl + Shift + Z
					{
						if (btnNextBlock.Visible)
						{
							btnNextBlock_Click(btnNextBlock, EventArgs.Empty);
							e.Handled = true;
						}
					}
				}
				// Delete 키는 WebView2로 전파되도록 함
				else if (e.KeyCode == Keys.Delete)
				{
					e.Handled = false;
					e.SuppressKeyPress = false;
				}
			};

			webViewblock.Source = new Uri(uri);
		}

		// JS 함수 호출 = 블럭 넣기
		public void addBlock(string blockType)
		{
			if(btnPreBlock.Visible == false)
			{
				btnPreBlock.Visible = true;
				btnNextBlock.Visible = false;
				undoCount = 0;
			}
			webViewblock.ExecuteScriptAsync($"addBlock('{blockType}')");
			webViewblock.ExecuteScriptAsync($"getblockCount()");
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
		private void btnNextBlock_Click(object sender, EventArgs e)
		{
			--undoCount;
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
		private void btnPreBlock_Click(object sender, EventArgs e)
		{
			++undoCount;
			webViewblock.ExecuteScriptAsync($"undo()");
			webViewblock.ExecuteScriptAsync($"getblockCount()");

			if (undoCount < 10 && undoCount > 0 && blockCount > 1) // <- 이거 왜 1이여야하지?
			{
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
		private void btnTrash_Click(object sender, EventArgs e)
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

		private bool checkBlockPosition(string blockType, int nowPosition)
		{
			float correctPosition;
			switch (blockType)
			{
				case "start":
					correctPosition = 0;
					break;
				case "pipInstall":
					correctPosition = 1;
					break;
				case "loadModel":
					correctPosition = 2;
					break;
				case "loadModelWithLayer":
					correctPosition = 2;
					break;
				case "layer":
					correctPosition = 2.5f;
					break;
				case "loadDataset":
					correctPosition = 3;
					break;
				case "machineLearning":
					correctPosition = 4;
					break;
				case "resultGraph":
					correctPosition = 5;
					break;
				case "imgPath":
					correctPosition = 6;
					break;
				case "modelInference":
					correctPosition = 7;
					break;
				case "visualizeResult":
					correctPosition = 8;
					break;
				default:
					correctPosition = -1;
					break;
			}

			if (correctPosition != nowPosition)
			{
				return false;
			}
			return true;
		}

		private void blockErrorMessage(string blockType)
		{
			switch (blockType)
			{
				case "start":
					errorType = "블록 배치 오류";
					missingType = "\"pipInstall\"";
					errorMessage = "\"패키지 설치\"블록이 필요합니다. 아래 순서에 맞게 배치해주세요.\n";
					errorMessage += "[시작] - [패키지 설치]";
					break;
				case "pipInstall":
					errorType = "블록 배치 오류";
					missingType = "\"loadModel\"";
					errorMessage = "\"모델 불러오기\"블록이 필요합니다. 아래 순서에 맞게 배치해주세요.\n";
					errorMessage += "[패키지 설치] - [모델 불러오기]";
					break;
				case "loadModel":
					errorType = "블록 배치 오류";
					missingType = "\"loadDataset\"";
					errorMessage = "\"데이터 불러오기\"블록이 필요합니다. 아래 순서에 맞게 배치해주세요.\n";
					errorMessage += "[모델 불러오기] - [데이터 불러오기]";
					break;
				case "loadDataset":
					errorType = "블록 배치 오류";
					missingType = "\"machineLearning\"";
					errorMessage = "\"모델 학습하기\"블록이 필요합니다. 아래 순서에 맞게 배치해주세요.\n";
					errorMessage += "[데이터 불러오기] - [모델 학습하기]";
					break;
				case "machineLearning":
					errorType = "블록 배치 오류";
					missingType = "\"resultGraph\"";
					errorMessage = "\"학습 결과 그래프 출력하기\"블록이 필요합니다. 아래 순서에 맞게 배치해주세요.\n";
					errorMessage += "[모델 학습하기] - [학습 결과 그래프 출력하기]";
					break;
				case "resultGraph":
					errorType = "블록 배치 오류";
					missingType = "\"imgPath\"";
					errorMessage = "\"이미지 불러오기\"블록이 필요합니다. 아래 순서에 맞게 배치해주세요.\n";
					errorMessage += "[학습 결과 그래프 출력하기] - [이미지 불러오기]";
					break;
				case "imgPath":
					errorType = "블록 배치 오류";
					missingType = "\"modelInference\"";
					errorMessage = "\"추론 실행하기\"블록이 필요합니다. 아래 순서에 맞게 배치해주세요.\n";
					errorMessage += "[이미지 불러오기] - [추론 실행하기]";
					break;
				case "modelInference":
					errorType = "블록 배치 오류";
					missingType = "\"visualizeResult\"";
					errorMessage = "\"결과 시각화하기\"블록이 필요합니다. 아래 순서에 맞게 배치해주세요.\n";
					errorMessage += "[추론 실행하기] - [결과 시각화하기]";
					break;
				case "loadModelWithLayer":
					errorType = "블록 배치 오류";
					missingType = "\"loadDataset\"";
					errorMessage = "\"데이터 불러오기\"블록이 필요합니다. 아래 순서에 맞게 배치해주세요.\n";
					errorMessage += "[모델 불러오기] - [데이터 불러오기]";
					break;
				case "layer":
					errorType = "블록 배치 오류";
					missingType = "\"layer\"";
					errorMessage = "\"레이어 수정 모델 불러오기\"블록이 필요합니다. 아래 순서에 맞게 배치해주세요.\n";
					errorMessage += "[레이어 수정 모델 불러오기] 안에 [layer] 블럭을 넣어주세요.";
					break;
			}
		}

		// 블록 에러 처리는 참 어려워어
		public bool isBlockError()
		{
			if (blocklyModel == null || blocklyModel.blockTypes == null)
			{
				errorType = "블록 배치 오류";
				missingType = "MISSING \"시작\"";
				errorMessage = "\"시작블록\"이 맨 앞에 있어야 합니다.\n";
				errorMessage += "시작블록에 다른 블록들을 연결해주세요.\n";
				return true;
			}

			if (blocklyModel.blockTypes.Count == 11 || blocklyModel.blockTypes.Count == 9)
			{
				for (int i = 0; i < blocklyModel.blockTypes.Count; i++)
				{
					BlockInfo block = blocklyModel.blockTypes[i];
					if (block == null) continue;
					
					string blockType = block.type;
					if (!checkBlockPosition(blockType, i))
					{
						blockErrorMessage(blockType);
						return true;
					}

					if(blockType == "loadModelWithLayer")
					{
						if(block.children != null)
						{
							if(block.children.Count > 1)
							{
								MessageBox.Show("블럭 9개 block child");
								blockErrorMessage("layer");
								return true;
							}
						}
						else
						{
							MessageBox.Show("블럭 9개 block child null");
							blockErrorMessage("layer");
							return true;
						}
					}
				}
			}
			else if (blocklyModel.blockTypes.Count < 11)
			{
				int lastBlock = blocklyModel.blockTypes.Count - 1;
				if (lastBlock < 0) return true;

				BlockInfo block = blocklyModel.blockTypes[lastBlock];
				if (block == null) return true;

				for (int i = 0; i < blocklyModel.blockTypes.Count; i++)
				{
					BlockInfo blockInfo = blocklyModel.blockTypes[i];
					if (blockInfo == null) continue;

					string blockType = blockInfo.type;
					if (!checkBlockPosition(blockType, i))
					{
						if (i > 0)
						{
							blockInfo = blocklyModel.blockTypes[i - 1];
							if (blockInfo != null)
							{
								blockType = blockInfo.type;
								blockErrorMessage(blockType);
								return true;
							}
						}
					}

					if (blockType == "imgPath")
					{
						if (string.IsNullOrEmpty(blocklyModel.imgPath))
						{
							errorType = "파라미터 오류";
							missingType = "파라미터 \"이미지 파일\"";
							errorMessage = "\"이미지 불러오기\"블록의 필수 파라미터인 \"이미지 파일\"이 없습니다.\n";
							errorMessage += "\"파일 선택\"버튼을 눌러 이미지를 선택해주세요.";
							return true;
						}
					}
					else if (blockType == "loadModelWithLayer")
					{
						if (block.children != null)
						{
							if (block.children.Count > 1)
							{
								MessageBox.Show("블럭 11개 보다 적은 block child");
								blockErrorMessage("layer");
								return true;
							}
						}
						else
						{
							MessageBox.Show("블럭 11개 보다 적은 block child null");
							blockErrorMessage("layer");
							return true;
						}
					}
				}
				blockErrorMessage(block.type);
				return true;
			}

			return false;
		}

		// 실행 버튼 클릭 이벤트
		private void btnRunModel_Click(object sender, EventArgs e)
		{
			if (blocklyModel.blockTypes != null)
			{
				// 블록 순서가 맞는지 판단
				if (!isBlockError()) // 순서가 맞을 떄
				{
					// 파이썬 코드 실행
					//RunButtonClicked?.Invoke(sender, e);
				}
				else // 순서가 틀릴 때
				{
					ShowToastMessage(errorType, missingType, errorMessage);
				}
			}
			else
			{
				errorType = "블록 배치 오류";
				missingType = "MISSING \"시작\"";
				errorMessage = "\"시작블록\"이 맨 앞에 있어야 합니다.\n";
				errorMessage += "시작블록에 다른 블록들을 연결해주세요.\n";
				ShowToastMessage(errorType, missingType, errorMessage);
			}
		}

		private async void ShowToastMessage(string errorType, string missingType, string errorMessage)
		{
			// 이전 토스트 메시지가 있다면 취소
			_toastCancellationSource?.Cancel();
			_toastCancellationSource = new CancellationTokenSource();
			var token = _toastCancellationSource.Token;

			try
			{
				pErrorToast.Visible = true;
				pErrorToast.FillColor = Color.FromArgb(0, pErrorToast.FillColor);
				lbErrorType.Text = errorType;
				lbMissingType.Text = missingType;
				lbErrorMessage.Text = errorMessage;

				// 2초 대기 (취소 가능)
				await Task.Delay(2000, token);
				pErrorToast.Visible = false;
			}
			catch (OperationCanceledException)
			{
				// 토스트가 취소된 경우 아무것도 하지 않음
			}
			finally
			{
				_toastCancellationSource?.Dispose();
				_toastCancellationSource = null;
			}
		}

        private void UcPracticeBlockCode_Load(object sender, EventArgs e)
        {
            // 초기에는 숨기길 패널들
            pSideInfer.Visible = false;
            ibtnCloseInfer.Visible = false;
            pboxInferAccuracy.Visible = false;
            pMemo.Visible = false;
            cAlertPanel.Visible = false;  // 복사 알림 패널도 초기에 숨김

            SetupThresholdControls();
            MemoUtils.ApplyStyle(tboxMemo);
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
            catch (Exception ex) {
                Console.WriteLine($"[ERROR] UcPracticeBlockCode: 확대/축소 레벨 업데이트 중 오류 발생 - {ex.Message}");
            }
        }
                    
                    
        // 추론 이미지 불러오기
        private void btnSelectInferImage_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "이미지 파일 선택";
                openFileDialog.Filter = "이미지 파일|*.jpg;*.jpeg;*.png";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                openFileDialog.Multiselect = false;
                openFileDialog.RestoreDirectory = true;

                Form parentForm = this.FindForm();
                if (parentForm != null)
                {
                    if (openFileDialog.ShowDialog(parentForm) == DialogResult.OK)
                    {
                        string absolutePath = openFileDialog.FileName;
                        selectedImagePath = Path.GetFullPath(absolutePath).Trim();

                        // 프로젝트 루트의 inference_images 디렉토리 경로 설정
                        string projectDir = AppDomain.CurrentDomain.BaseDirectory;
                        string inferenceImagesDir = Path.Combine(projectDir, "..", "..", "inference_images");

                        // inference_images 디렉토리가 없으면 생성
                        if (!Directory.Exists(inferenceImagesDir))
                        {
                            Directory.CreateDirectory(inferenceImagesDir);
                            Console.WriteLine($"[INFO] inference_images 디렉토리 생성됨: {inferenceImagesDir}");
                        }

                        string fileName = Path.GetFileName(absolutePath);
                        string destinationPath = Path.Combine(inferenceImagesDir, fileName);

                        // 파일 복사 (같은 이름의 파일이 있으면 덮어쓰기)
                        File.Copy(absolutePath, destinationPath, true);

                        // inference_images 기준 상대 경로 설정
                        string relativePath = Path.Combine("inference_images", fileName).Replace("\\", "/");

                        // BlocklyModel에 상대 경로 설정
                        BlocklyModel.Instance.imgPath = relativePath;
                        Console.WriteLine($"[INFO] 이미지가 {relativePath}에 저장되었습니다.");

                        using (var stream = new FileStream(absolutePath, FileMode.Open, FileAccess.Read))
                        {
                            var originalImage = Image.FromStream(stream);
                            pboxInferAccuracy.Size = new Size(431, 275);
                            pboxInferAccuracy.SizeMode = PictureBoxSizeMode.Zoom;
                            pboxInferAccuracy.Image = originalImage;
                            pboxInferAccuracy.Visible = true;
                        }

                        btnSelectInferImage.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("부모 폼을 찾을 수 없습니다.", "오류",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"이미지 로드 중 오류가 발생했습니다: {ex.Message}", "오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ShowDialogInferenceLoading()
        {
            using (var dialog = new DialogInferenceLoading())
            {
                dialog.ShowDialog();
            }
        }


        // DialogInferenceLoading 닫고 pboxInferAccuracy에 추론 결과 이미지 띄우는 함수
            //var practiceView = new UcPracticeBlockCode(mainView);
            //practiceView.ShowPracticeInferResultImage(resultImage); 사용하시면 됩니다.
        public void ShowPracticeInferResultImage(System.Drawing.Image resultImage)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ShowPracticeInferResultImage(resultImage)));
                return;
            }

            dialogLoadingInfer?.Close();
            dialogLoadingInfer = null;

            if (resultImage != null)
            {
                pboxInferAccuracy.Size = new Size(494, 278);
                pboxInferAccuracy.SizeMode = PictureBoxSizeMode.Zoom;
                pboxInferAccuracy.Image = resultImage;
                pboxInferAccuracy.Visible = true;
                btnSelectInferImage.Visible = false;
            }
        }

        private void btnQuestionMemo_Click(object sender, EventArgs e)
        {
            // mAlertPanel을 보이게 설정
            mAlertPanel.Visible = true;

            // 2초 후에 mAlertPanel을 숨기는 타이머 설정
            Timer timer = new Timer();
            timer.Interval = 2000; // 2초
            timer.Tick += (s, args) =>
            {
                mAlertPanel.Visible = false;
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }

        private void ibtnCopy_Click(object sender, EventArgs e)
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

            // cAlertPanel을 보이게 설정
            cAlertPanel.Visible = true;

            // 1초 후에 cAlertPanel을 숨기는 타이머 설정
            Timer timer = new Timer();
            timer.Interval = 1000; // 1초
            timer.Tick += (s, args) =>
            {
                cAlertPanel.Visible = false;
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }

    }
}
