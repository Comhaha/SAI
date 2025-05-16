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
        private bool isMemoPanelVisible = false;

        private double currentThreshold = 0.5; // threshold 기본값 0.5
        private string selectedImagePath = string.Empty; // 추론 이미지 경로를 저장할 변수

        private int undoCount = 0; // 뒤로가기 카운트

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
			ButtonUtils.SetTransparentStyle(btnCopy);

            // 초기에는 숨기길 패널들
            pSideInfer.Visible = false;
            ibtnCloseInfer.Visible = false;
            pMemo.Visible = false;
            pboxInferAccuracy.Visible = false;
            btnSelectInferImage.Visible = false;

            // 새 이미지 불러오기 버튼 설정
            btnSelectInferImage.Size = new Size(329, 185);  // pInferAccuracy와 동일한 크기
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


            ToolTipUtils.CustomToolTip(pboxGraphe, "자세히 보려면 클릭하세요.");
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
            ButtonUtils.SetupButton(btnCopy, "btn_copy_hover", "btn_copy");
			ButtonUtils.SetTransparentStyle(btnSelectInferImage);

			// 정언이가 선언
            //생성자---------------
            blocklyPresenter = new BlocklyPresenter(this);
            this.mainView = view;
            blocklyModel = BlocklyModel.Instance;
			InitializeWebView2();

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
                        selectedImagePath = absolutePath;

                        // 프로젝트 루트의 inference_images 디렉토리 (사용자 추론용 이미지 폴더 따로 설정)경로 설정
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
                            pboxInferAccuracy.Size = new Size(287, 185);
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
    }
}
