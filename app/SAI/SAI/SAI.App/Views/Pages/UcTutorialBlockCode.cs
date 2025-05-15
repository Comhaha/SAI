using System;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using SAI.SAI.App.Forms.Dialogs;
using SAI.SAI.App.Models.Events;
using SAI.SAI.App.Views.Common;
using SAI.SAI.App.Presenters;
using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.Application.Interop;
using Guna.UI2.WinForms;
using SAI.SAI.App.Models;
using System.Diagnostics;
using static SAI.SAI.App.Models.BlocklyModel;
using System.Collections.Generic;

namespace SAI.SAI.App.Views.Pages
{
    public partial class UcTutorialBlockCode : UserControl, IUcShowDialogView, IBlocklyView, IYoloTutorialView
	{
		private YoloTutorialPresenter yoloTutorialPresenter;
		private BlocklyPresenter blocklyPresenter;
		private UcShowDialogPresenter ucShowDialogPresenter;
		
		private BlocklyModel blocklyModel;

		private readonly IMainView mainView;

		public event EventHandler HomeButtonClicked;

		public event EventHandler<BlockEventArgs> AddBlockButtonClicked;
        public event EventHandler RunButtonClicked;

        private JsBridge jsBridge;

        private bool isInferPanelVisible = false;
        private double currentThreshold = 0.5;
        private bool isMemoPanelVisible = false;
		private MemoPresenter memoPresenter;


		private int undoCount = 0;
		public UcTutorialBlockCode(IMainView view)
		{
			InitializeComponent();
			blocklyPresenter = new BlocklyPresenter(this);
			yoloTutorialPresenter = new YoloTutorialPresenter(this);
			memoPresenter = new MemoPresenter(); // MemoPresenter 초기화

			blocklyModel = BlocklyModel.Instance;

			tboxMemo.TextChanged += tboxMemo_TextChanged;

			btnRunModel.Click += (s, e) => RunButtonClicked?.Invoke(s, e);

			this.mainView = view;
			ucShowDialogPresenter = new UcShowDialogPresenter(this);

			undoCount = 0;

			btnNextBlock.Visible = false; // 초기화 시 보이지 않게 설정


			ibtnHome.BackColor = Color.Transparent;
			ibtnDone.BackColor = Color.Transparent;
			ibtnInfer.BackColor = Color.Transparent;
			ibtnMemo.BackColor = Color.Transparent;

            // 홈페이지로 이동
            ibtnHome.Click += (s, e) => {
                mainView.LoadPage(new UcSelectType(mainView));
            };

            ToolTipUtils.CustomToolTip(pboxGraphe, "자세히 보려면 클릭하세요.");
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
            ButtonUtils.SetupButton(btnSelectInferImage, "btn_selectinferimage_hover", "btn_selectinferimage");
            ButtonUtils.SetupButton(btnCopy, "btn_copy_hover", "btn_copy");

            InitializeWebView2();

			// 블록 시작만 보이고 나머지는 안 보이게 초기화.
			InitializeBlockButton();
			setBtnBlockStart();

            // 여기에 UcCode 추가
            try
            {
                // 새로운 UcCode 인스턴스를 생성하는 대신 디자이너에서 만든 ucCode1 사용
                // ucCode1은 이미 pCode에 추가되어 있으므로 다시 추가할 필요 없음
                if (ucCode1 != null)
                {
                    // BlocklyPresenter에 기존 ucCode1 설정
                    blocklyPresenter.SetCodeView(ucCode1);
                    Console.WriteLine("[DEBUG] UcTutorialBlockCode: ICodeView 설정 완료");
                }
                else
                {
                    Console.WriteLine("[ERROR] UcTutorialBlockCode: ucCode1이 null입니다");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] UcTutorialBlockCode: ICodeView 설정 중 오류 - {ex.Message}");
            }


            // btnRunModel
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

			// 블록 버튼 클릭 이벤트 처리
			btnBlockStart.Click += (s, e) =>
			{
				Console.WriteLine("[DEBUG] 더블클릭 이벤트 발생: start");
				AddBlockButtonClicked?.Invoke(this, new BlockEventArgs("start"));
				setBtnPip();
			};
			btnPip.Click += (s, e) =>
			{
				Console.WriteLine("[DEBUG] 더블클릭 이벤트 발생: pipInstall");
				AddBlockButtonClicked?.Invoke(this, new BlockEventArgs("pipInstall"));
				setBtnLoadModel();
			};
			btnLoadModel.Click += (s, e) =>
			{
				AddBlockButtonClicked?.Invoke(this, new BlockEventArgs("loadModel"));
				setBtnLoadDataset();
			};
			btnLoadDataset.Click += (s, e) =>
			{
				AddBlockButtonClicked?.Invoke(this, new BlockEventArgs("loadDataset"));
				setBtnMachineLearning();
			};
			btnMachineLearning.Click += (s, e) =>
			{
				AddBlockButtonClicked?.Invoke(this, new BlockEventArgs("machineLearning"));
				setBtnResultGraph();
			};
			btnResultGraph.Click += (s, e) =>
			{
				AddBlockButtonClicked?.Invoke(this, new BlockEventArgs("resultGraph"));
				setBtnImgPath();
			};
			btnImgPath.Click += (s, e) =>
			{
				AddBlockButtonClicked?.Invoke(this, new BlockEventArgs("imgPath"));
				setBtnModelInference();
			};
			btnModelInference.Click += (s, e) =>
			{
				AddBlockButtonClicked?.Invoke(this, new BlockEventArgs("modelInference"));
				setBtnVisualizeResult();
			};
			btnVisualizeResult.Click += (s, e) =>
			{
				AddBlockButtonClicked?.Invoke(this, new BlockEventArgs("visualizeResult"));
				setButtonInVisible(btnVisualizeResult);
				pToDoList.BackgroundImage = Properties.Resources.p_todolist_step2;
				pTxtDescription.BackgroundImage = Properties.Resources.lbl_run;
			};
		}

 

        private void setButtonVisible(Guna2Button button)
		{
			button.Visible = true;
		}
		private void setButtonInVisible(Guna2Button button)
		{
			button.Visible = false;
		}

		private void InitializeBlockButton()
		{
			btnBlockStart.Visible = false;
			btnPip.Visible = false;
			btnLoadModel.Visible = false;
			btnLoadDataset.Visible = false;
			btnMachineLearning.Visible = false;
			btnResultGraph.Visible = false;
			btnImgPath.Visible = false;
			btnModelInference.Visible = false;
			btnVisualizeResult.Visible = false;
		}

		private void setBtnBlockStart()
		{
			setButtonVisible(btnBlockStart);

			pTxtDescription.BackgroundImage = Properties.Resources.lbl_start;
			// 시작 블럭
			btnBlockStart.BackColor = Color.Transparent;
			btnBlockStart.PressedColor = Color.Transparent;
			btnBlockStart.CheckedState.FillColor = Color.Transparent;
			btnBlockStart.DisabledState.FillColor = Color.Transparent;
			btnBlockStart.HoverState.FillColor = Color.Transparent;
			// btnClose 마우스 입력 될 때
			btnBlockStart.MouseEnter += (s, e) =>
			{
				btnBlockStart.BackColor = Color.Transparent;
				btnBlockStart.BackgroundImage = Properties.Resources.btnBlockStartClicked;
			};
			// btnClose 마우스 떠날때
			btnBlockStart.MouseLeave += (s, e) =>
			{
				btnBlockStart.BackgroundImage = Properties.Resources.btnBlockStart;
			};
		}

		private void setBtnPip()
		{
			setButtonVisible(btnPip);
			setButtonInVisible(btnBlockStart);

			pTxtDescription.BackgroundImage = Properties.Resources.lbl_pip_install;
			// 패키지 설치 블럭			
			btnPip.BackColor = Color.Transparent;
			btnPip.PressedColor = Color.Transparent;
			btnPip.CheckedState.FillColor = Color.Transparent;
			btnPip.DisabledState.FillColor = Color.Transparent;
			btnPip.HoverState.FillColor = Color.Transparent;
			// btnClose 마우스 입력 될 때
			btnPip.MouseEnter += (s, e) =>
			{
				btnPip.BackColor = Color.Transparent;
				btnPip.BackgroundImage = Properties.Resources.btnPipInstallClicked;
			};
			// btnClose 마우스 떠날때
			btnPip.MouseLeave += (s, e) =>
			{
				btnPip.BackgroundImage = Properties.Resources.btnPipInstall;
			};
		}

		private void setBtnLoadModel()
		{
			setButtonVisible(btnLoadModel);
			setButtonInVisible(btnPip);

			pTxtDescription.BackgroundImage = Properties.Resources.lbl_load_model;
			// 패키지 설치 블럭			
			btnLoadModel.BackColor = Color.Transparent;
			btnLoadModel.PressedColor = Color.Transparent;
			btnLoadModel.CheckedState.FillColor = Color.Transparent;
			btnLoadModel.DisabledState.FillColor = Color.Transparent;
			btnLoadModel.HoverState.FillColor = Color.Transparent;
			// btnClose 마우스 입력 될 때
			btnLoadModel.MouseEnter += (s, e) =>
			{
				btnLoadModel.BackColor = Color.Transparent;
				btnLoadModel.BackgroundImage = Properties.Resources.btnLoadModelClicked;
			};
			// btnClose 마우스 떠날때
			btnLoadModel.MouseLeave += (s, e) =>
			{
				btnLoadModel.BackgroundImage = Properties.Resources.btnLoadModel;
			};
		}

		private void setBtnLoadDataset()
		{
			setButtonVisible(btnLoadDataset);
			setButtonInVisible(btnLoadModel);

			pTxtDescription.BackgroundImage = Properties.Resources.lbl_load_dataset;
			// 패키지 설치 블럭			
			btnLoadDataset.BackColor = Color.Transparent;
			btnLoadDataset.PressedColor = Color.Transparent;
			btnLoadDataset.CheckedState.FillColor = Color.Transparent;
			btnLoadDataset.DisabledState.FillColor = Color.Transparent;
			btnLoadDataset.HoverState.FillColor = Color.Transparent;
			// btnClose 마우스 입력 될 때
			btnLoadDataset.MouseEnter += (s, e) =>
			{
				btnLoadDataset.BackColor = Color.Transparent;
				btnLoadDataset.BackgroundImage = Properties.Resources.btnLoadDatasetClicked;
			};
			// btnClose 마우스 떠날때
			btnLoadDataset.MouseLeave += (s, e) =>
			{
				btnLoadDataset.BackgroundImage = Properties.Resources.btnLoadDataset;
			};
		}

		private void setBtnMachineLearning()
		{
			setButtonVisible(btnMachineLearning);
			setButtonInVisible(btnLoadDataset);

			pTxtDescription.BackgroundImage = Properties.Resources.lbl_machine_learning;
			// 패키지 설치 블럭			
			btnMachineLearning.BackColor = Color.Transparent;
			btnMachineLearning.PressedColor = Color.Transparent;
			btnMachineLearning.CheckedState.FillColor = Color.Transparent;
			btnMachineLearning.DisabledState.FillColor = Color.Transparent;
			btnMachineLearning.HoverState.FillColor = Color.Transparent;
			// btnClose 마우스 입력 될 때
			btnMachineLearning.MouseEnter += (s, e) =>
			{
				btnMachineLearning.BackColor = Color.Transparent;
				btnMachineLearning.BackgroundImage = Properties.Resources.btnMachineLearningClicked;
			};
			// btnClose 마우스 떠날때
			btnMachineLearning.MouseLeave += (s, e) =>
			{
				btnMachineLearning.BackgroundImage = Properties.Resources.btnMachineLearning;
			};
		}

		private void setBtnResultGraph()
		{
			setButtonVisible(btnResultGraph);
			setButtonInVisible(btnMachineLearning);

			pTxtDescription.BackgroundImage = Properties.Resources.lbl_result_graph;
			// 패키지 설치 블럭			
			btnResultGraph.BackColor = Color.Transparent;
			btnResultGraph.PressedColor = Color.Transparent;
			btnResultGraph.CheckedState.FillColor = Color.Transparent;
			btnResultGraph.DisabledState.FillColor = Color.Transparent;
			btnResultGraph.HoverState.FillColor = Color.Transparent;
			// btnClose 마우스 입력 될 때
			btnResultGraph.MouseEnter += (s, e) =>
			{
				btnResultGraph.BackColor = Color.Transparent;
				btnResultGraph.BackgroundImage = Properties.Resources.btnResultGraphClicked;
			};
			// btnClose 마우스 떠날때
			btnResultGraph.MouseLeave += (s, e) =>
			{
				btnResultGraph.BackgroundImage = Properties.Resources.btnResultGraph;
			};
		}

		private void setBtnImgPath()
		{
			setButtonVisible(btnImgPath);
			setButtonInVisible(btnResultGraph);

			pTxtDescription.BackgroundImage = Properties.Resources.lbl_img_path;
			// 패키지 설치 블럭			
			btnImgPath.BackColor = Color.Transparent;
			btnImgPath.PressedColor = Color.Transparent;
			btnImgPath.CheckedState.FillColor = Color.Transparent;
			btnImgPath.DisabledState.FillColor = Color.Transparent;
			btnImgPath.HoverState.FillColor = Color.Transparent;
			// btnClose 마우스 입력 될 때
			btnImgPath.MouseEnter += (s, e) =>
			{
				btnImgPath.BackColor = Color.Transparent;
				btnImgPath.BackgroundImage = Properties.Resources.btnImgPathClicked;
			};
			// btnClose 마우스 떠날때
			btnImgPath.MouseLeave += (s, e) =>
			{
				btnImgPath.BackgroundImage = Properties.Resources.btnImgPath;
			};
		}

		private void setBtnModelInference()
		{
			setButtonVisible(btnModelInference);
			setButtonInVisible(btnImgPath);

			pTxtDescription.BackgroundImage = Properties.Resources.lbl_model_inference;
			// 패키지 설치 블럭			
			btnModelInference.BackColor = Color.Transparent;
			btnModelInference.PressedColor = Color.Transparent;
			btnModelInference.CheckedState.FillColor = Color.Transparent;
			btnModelInference.DisabledState.FillColor = Color.Transparent;
			btnModelInference.HoverState.FillColor = Color.Transparent;
			// btnClose 마우스 입력 될 때
			btnModelInference.MouseEnter += (s, e) =>
			{
				btnModelInference.BackColor = Color.Transparent;
				btnModelInference.BackgroundImage = Properties.Resources.btnModelInferenceClicked;
			};
			// btnClose 마우스 떠날때
			btnModelInference.MouseLeave += (s, e) =>
			{
				btnModelInference.BackgroundImage = Properties.Resources.btnModelInference;
			};
		}

		private void setBtnVisualizeResult()
		{
			setButtonVisible(btnVisualizeResult);
			setButtonInVisible(btnModelInference);

			pTxtDescription.BackgroundImage = Properties.Resources.lbl_visualize_result;
			// 패키지 설치 블럭			
			btnVisualizeResult.BackColor = Color.Transparent;
			btnVisualizeResult.PressedColor = Color.Transparent;
			btnVisualizeResult.CheckedState.FillColor = Color.Transparent;
			btnVisualizeResult.DisabledState.FillColor = Color.Transparent;
			btnVisualizeResult.HoverState.FillColor = Color.Transparent;
			// btnClose 마우스 입력 될 때
			btnVisualizeResult.MouseEnter += (s, e) =>
			{
				btnVisualizeResult.BackColor = Color.Transparent;
				btnVisualizeResult.BackgroundImage = Properties.Resources.btnVisualizeResultClicked;
			};
			// btnClose 마우스 떠날때
			btnVisualizeResult.MouseLeave += (s, e) =>
			{
				btnVisualizeResult.BackgroundImage = Properties.Resources.btnVisualizeResult;
			};
		}

		private void UcTutorialBlockCode_Load(object sender, EventArgs e)
		{ 
            // 초기에는 숨기길 패널들
            pSideInfer.Visible = false;
            ibtnCloseInfer.Visible = false;
            pboxInferAccuracy.Visible = false;
            pMemo.Visible = false;

            SetupThresholdControls();
            MemoUtils.ApplyStyle(tboxMemo);
        }

        private void SetupThresholdControls()
        {
            ThresholdUtils.Setup(tbarThreshold, tboxThreshold, (newValue) =>
            {
                currentThreshold = newValue;
            });
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
        private void guna2ImageButton1_Click_1(object sender, EventArgs e)
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

		// JS 함수 호출 = 블럭 모두 삭제
        private void btnTrashBlock_Click(object sender, EventArgs e)
        {
			webViewblock.ExecuteScriptAsync($"clear()");
		}

        private void ibtnDone_Click(object sender, EventArgs e)
        {
			ucShowDialogPresenter.clickGoTrain();
		}

		private void btnRunModel_Click(object sender, EventArgs e)
		{
			pTxtDescription.BackgroundImage = Properties.Resources.lbl_report;
			pToDoList.BackgroundImage = Properties.Resources.p_todolist_step3;
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
			string localPath = Path.GetFullPath(Path.Combine(baseDir, @"..\\..\\Blockly\\TutorialBlockly.html"));
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
		private void btnNextBlock_Click(object sender, EventArgs e)
		{
			undoCount--;
			webViewblock.ExecuteScriptAsync($"redo()");
			if(undoCount == 0)
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
			if(undoCount <= 10)
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

        public void AppendLog(string text)
        {
            Debug.WriteLine($"[YOLO Tutorial] {text}");
        }

        public void ClearLog()
        {
            // Debug 출력에서는 Clear() 대신 구분선을 출력하여 로그를 구분
            Debug.WriteLine("\n" + new string('-', 50) + "\n");
        }

        public void SetLogVisible(bool visible)
        {
            // Debug 출력에서는 가시성 설정이 필요 없으므로 빈 메서드로 둡니다
        }

        public void ShowErrorMessage(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => ShowErrorMessage(message)));
            }
            else
            {
                MessageBox.Show(message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tboxMemo_TextChanged(object sender, EventArgs e)
        {
            // MemoPresenter를 통해 텍스트 변경 사항을 모델에 저장
            if (memoPresenter != null)
            {
                memoPresenter.SaveMemoText(tboxMemo.Text);
            }
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

        private void pSideInfer_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void ucCode1_Load(object sender, EventArgs e)
        {

        }

        private void pMain_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
