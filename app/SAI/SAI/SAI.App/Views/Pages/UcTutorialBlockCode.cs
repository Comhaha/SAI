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
using System.Messaging;
using System.Threading.Tasks;
using System.Linq;
using SAI.SAI.Application.Service;
using System.Threading;
using Timer = System.Windows.Forms.Timer;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using CefSharp.DevTools.IndexedDB;

namespace SAI.SAI.App.Views.Pages
{
    public partial class UcTutorialBlockCode : UserControl, IUcShowDialogView, IBlocklyView, IYoloTutorialView, ITutorialInferenceView
    {
        private YoloTutorialPresenter yoloTutorialPresenter;
        private BlocklyPresenter blocklyPresenter;
        private UcShowDialogPresenter ucShowDialogPresenter;
        private DialogInferenceLoading dialogLoadingInfer;

        private BlocklyModel blocklyModel;

        private readonly IMainView mainView;

        public event EventHandler HomeButtonClicked;

        public event EventHandler<BlockEventArgs> AddBlockButtonClicked;
        public event EventHandler RunButtonClicked;

        private JsBridge jsBridge;

        private bool isInferPanelVisible = false;
        private double currentThreshold = 0.25;
        private bool isMemoPanelVisible = false;
        private MemoPresenter memoPresenter;
        private string selectedImagePath = string.Empty; //추론탭에서 선택한 이미지 저장할 변수

        private int blockCount = 0; // 블럭 개수

        private string errorMessage = "";
        private string missingType = "";
        private string errorType = "";

        private CancellationTokenSource _toastCancellationSource;

        private int currentZoomLevel = 80; // 현재 확대/축소 레벨 (기본값 60%)
        private readonly int[] zoomLevels = { 0, 20, 40, 60, 80, 100, 120, 140, 160, 180, 200 }; // 가능한 확대/축소 레벨
        private PythonService.InferenceResult _result;

        private string currentImagePath = string.Empty; // 현재 표시 중인 이미지 경로

        private UcInferenceTab ucInferenceTab;

        public UcTutorialBlockCode(IMainView view)
        {
            InitializeComponent();
            blocklyPresenter = new BlocklyPresenter(this);
            yoloTutorialPresenter = new YoloTutorialPresenter(this);
            memoPresenter = new MemoPresenter(); // MemoPresenter 초기화

            blocklyModel = BlocklyModel.Instance;
            pSideInfer.Visible = false;
            //pleaseControlThreshold.Visible = false;
            errorMessage = "";
            missingType = "";

            tboxMemo.TextChanged += tboxMemo_TextChanged;

            // 확대/축소 버튼 이벤트 추가
            ibtnPlusCode.Click += (s, e) =>
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
                    Console.WriteLine($"[ERROR] UcTutorialBlockCode: 확대 중 오류 발생 - {ex.Message}");
                }
            };

            ibtnMinusCode.Click += (s, e) =>
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
                    Console.WriteLine($"[ERROR] UcTutorialBlockCode: 축소 중 오류 발생 - {ex.Message}");
                }
            };

            // 초기 확대/축소 레벨 설정
            currentZoomLevel = 60;
            UpdateCodeZoom();

            //btnSelectInferImage.Visible = false;

            //// 새 이미지 불러오기 버튼 설정
            //btnSelectInferImage.Size = new Size(494, 278);  // pInferAccuracy와 동일한 크기
            //pboxInferAccuracy.Controls.Add(btnSelectInferImage);
            //btnSelectInferImage.Location = new Point(0, 0);
            //btnSelectInferImage.Enabled = true;
            //btnSelectInferImage.Cursor = Cursors.Hand;
            //btnSelectInferImage.Click += new EventHandler(btnSelectInferImage_Click);

            //pboxInferAccuracy.MouseEnter += (s, e) =>
            //{
            //    if (pSideInfer.Visible)
            //    {
            //        btnSelectInferImage.Visible = true;
            //        btnSelectInferImage.BringToFront();
            //        btnSelectInferImage.BackgroundImage = Properties.Resources.btn_selectinferimage_hover;
            //    }
            //};

            //pboxInferAccuracy.MouseLeave += (s, e) =>
            //{
            //    if (!btnSelectInferImage.ClientRectangle.Contains(btnSelectInferImage.PointToClient(Control.MousePosition)))
            //    {
            //        btnSelectInferImage.Visible = false;
            //        btnSelectInferImage.BackgroundImage = Properties.Resources.btn_selectinferimage;
            //    }
            //};

            ////// 버튼에도 MouseEnter/Leave 이벤트 추가
            //btnSelectInferImage.MouseEnter += (s, e) =>
            //{
            //    btnSelectInferImage.Visible = true;
            //    btnSelectInferImage.BackgroundImage = Properties.Resources.btn_selectinferimage_hover;
            //};

            //btnSelectInferImage.MouseLeave += (s, e) =>
            //{
            //    if (!pboxInferAccuracy.ClientRectangle.Contains(pboxInferAccuracy.PointToClient(Control.MousePosition)))
            //    {
            //        btnSelectInferImage.Visible = false;
            //        btnSelectInferImage.BackgroundImage = Properties.Resources.btn_selectinferimage;
            //    }
            //};


            ibtnHome.BackColor = Color.Transparent;
            ibtnDone.BackColor = Color.Transparent;
            ibtnInfer.BackColor = Color.Transparent;
            ibtnMemo.BackColor = Color.Transparent;
            ButtonUtils.SetTransparentStyle(btnCopy);

            // PercentUtils로 퍼센트 박스 스타일 일괄 적용
            PercentUtils.SetupPercentTextBox(tboxZoomCode, 0.5f, 0, 0);

            this.mainView = view;
            ucShowDialogPresenter = new UcShowDialogPresenter(this);

            blockCount = 0; // 블럭 개수 초기화

            ibtnHome.BackColor = Color.Transparent;
            ibtnDone.BackColor = Color.Transparent;
            ibtnInfer.BackColor = Color.Transparent;
            ibtnMemo.BackColor = Color.Transparent;
            pZoomCode.BackColor = Color.Transparent;
            cAlertPanel.BackColor = Color.Transparent;

            pSideInfer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;

            // 홈페이지로 이동
            ibtnHome.Click += (s, e) => {
                var dialog = new DialogHomeFromLabeling();
                dialog.ShowDialog(this);
                LogCsvModel.instance.clear();
            };

            //ToolTipUtils.CustomToolTip(btnInfoThreshold,
            //"AI의 분류 기준입니다. 예측 결과가 이 값보다 높으면 '맞다(1)'고 판단하고, 낮으면 '아니다(0)'로 처리합니다.");

            //ToolTipUtils.CustomToolTip(btnInfoGraph,
            //  "AI 모델의 성능을 한눈에 확인할 수 있는 그래프입니다. 정확도, 재현율 등의 성능 지표가 포함되어 있습니다.");
            //ToolTipUtils.CustomToolTip(btnSelectInferImage, "추론에 사용할 이미지를 가져오려면 클릭하세요.");

            ButtonUtils.SetupButton(btnRunModel, "btnRunModel_clicked", "btn_run_model");
            ButtonUtils.SetupButton(btnQuestionMemo, "btn_question_memo_clicked", "btn_question_memo");
            ButtonUtils.SetupButton(btnCloseMemo, "btn_close_25_clicked", "btn_close_25");
            //ButtonUtils.SetupButton(btnSelectInferImage, "btn_selectinferimage_hover", "btn_selectinferimage");
            ButtonUtils.SetupButton(btnCopy, "btn_copy_hover", "btn_copy");
            //ButtonUtils.SetTransparentStyle(btnSelectInferImage);
            //ButtonUtils.SetTransparentStyle(btnInfoGraph);
            //ButtonUtils.SetTransparentStyle(btnInfoThreshold);
            //pboxInferAccuracy.Image = null;


            // 복사 버튼 클릭 이벤트 추가
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
                        Console.WriteLine("[DEBUG] UcTutorialBlockCode: 코드가 클립보드에 복사됨");
                    }
                    else
                    {
                        Console.WriteLine("[WARNING] UcTutorialBlockCode: 복사할 코드가 없음");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] UcTutorialBlockCode: 코드 복사 중 오류 발생 - {ex.Message}");
                }
            };
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

            // mAlertPanel 초기에는 숨김
            mAlertPanel.Visible = false;
            // btnQuestionMemo 클릭 이벤트 핸들러 등록
            btnQuestionMemo.Click += btnQuestionMemo_Click;


			///////////////////////////////////////////////////
			/// 재영 언니 여기야아아
			// 이미지 경로가 바뀌면 블록에서도 적용되게
			blocklyModel.ImgPathChanged += (newPath) => {
				// 웹뷰에 이미지 경로 전달
				string escapedPath = JsonSerializer.Serialize(newPath);
				webViewblock.ExecuteScriptAsync($"imgPathChanged({escapedPath})");

                if (File.Exists(newPath))
                {
                    ucInferenceTab.ShowResultImage(newPath);
                    //// 기존 이미지 정리
                    //pboxInferAccuracy.Image?.Dispose();

                    //// string 경로를 Image 객체로 변환
                    //pboxInferAccuracy.Size = new Size(494, 278);
                    //pboxInferAccuracy.SizeMode = PictureBoxSizeMode.Zoom;
                    //pboxInferAccuracy.Image = System.Drawing.Image.FromFile(newPath);
                    //pboxInferAccuracy.Visible = true;
                    //pleaseControlThreshold.Visible = true;
                }
            };

			// threshold가 바뀌면 블록에서도 적용되게
			blocklyModel.AccuracyChanged += (newAccuracy) => {
				// 웹뷰에 threshold 전달
				webViewblock.ExecuteScriptAsync($"thresholdChanged({newAccuracy})");
                //tboxThreshold.Text = newAccuracy.ToString();
                //tbarThreshold.Value = (int)(newAccuracy * 100);
                //pleaseControlThreshold.Visible = false;
                ucInferenceTab.SetThresholdValue(newAccuracy);
                ucInferenceTab.HidePleaseControlMessage();
            };
            ///////////////////////////////////////////////////

            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var csvPath = Path.Combine(baseDir,
                @"..\..\SAI.Application\Python\runs\detect\train\example.csv");
            csvPath = Path.GetFullPath(csvPath);
            ShowTutorialTrainingChart(csvPath);

            // 🔧 추론탭 스크롤바 설정 -------------------
            ucInferenceTab = new UcInferenceTab(view);
            pSideInfer.Controls.Add(ucInferenceTab);

            pSideInfer.AutoScroll = false;
            ucInferenceTab.AutoScroll = false;

            pInferTabvScrollBar.Scroll += (s, e) =>
            {
                if (!pInferTabvScrollBar.Visible) return;
                ucInferenceTab.Top = -pInferTabvScrollBar.Value;
            };

            ucInferenceTab.SizeChanged += (s, e) =>
            {
                int contentHeight = ucInferenceTab.Height;
                int viewportHeight = pSideInfer.Height;

                int newMax = contentHeight - viewportHeight;
                if (newMax <= 0)
                {
                    pInferTabvScrollBar.Visible = false;
                    pInferTabvScrollBar.Maximum = 0;
                    pInferTabvScrollBar.Value = 0;
                    ucInferenceTab.Top = 0;
                }
                else
                {
                    pInferTabvScrollBar.Visible = true;
                    pInferTabvScrollBar.Maximum = newMax;
                }
            };

            pSideInfer.MouseEnter += (s, e) => pSideInfer.Focus();

            // 마우스 휠 수동 처리
            pSideInfer.MouseWheel += (s, e) =>
            {
                if (!pInferTabvScrollBar.Visible) return;

                int newValue = pInferTabvScrollBar.Value - e.Delta / 5;
                newValue = Math.Max(pInferTabvScrollBar.Minimum, Math.Min(pInferTabvScrollBar.Maximum, newValue));
                pInferTabvScrollBar.Value = newValue;
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
            pMemo.Visible = false;
            cAlertPanel.Visible = false;  // 복사 알림 패널도 초기에 숨김

            SetupThresholdControls();
            MemoUtils.ApplyStyle(tboxMemo);

        }

        // threshold 바에서 threshold 값을 생성
        // 마우스 떼면 추론 스크립트 실행
        private void SetupThresholdControls()
        {
            ThresholdUtilsTutorial.Setup(
                 ucInferenceTab.ThresholdTrackBar,
                 ucInferenceTab.ThresholdTextBox,
                (newValue) =>
                {
                    currentThreshold = newValue;

                    Console.WriteLine($"[LOG] SetupThresholdControls - selectedImagePath: {blocklyModel.imgPath}");
                    Console.WriteLine($"[LOG] SetupThresholdControls - currentThreshold: {currentThreshold}");

                    // 추론은 백그라운드에서 실행
                    // 이미지경로, threshold 값을 던져야 추론스크립트 실행 가능
                    Task.Run(() =>
                    {
                        _result = yoloTutorialPresenter.RunInferenceDirect(
                            blocklyModel.imgPath,
                            currentThreshold
                        );

                        Console.WriteLine($"[LOG] RunInferenceDirect 결과: success={_result.Success}, image={_result.ResultImage}, error={_result.Error}");
                        if (!string.IsNullOrEmpty(_result.ResultImage))
                        {
                            bool fileExists = System.IO.File.Exists(_result.ResultImage);
                            Console.WriteLine($"[LOG] ResultImage 파일 존재 여부: {fileExists}");
                        }
                        else
                        {
                            Console.WriteLine("[LOG] ResultImage가 비어있음");
                        }

                        // 결과는 UI 스레드로 전달
                        this.Invoke(new Action(() =>
                        {
                            ShowInferenceResult(_result);
                        }));
                    });
                },
                this
            );
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

        // 실행 버튼 클릭 이벤트
        private void btnRunModel_Click(object sender, EventArgs e)
        {
            if(blocklyModel.blockTypes != null)
            {
                // 블록 순서가 맞는지 판단
                if (!isBlockError()) // 순서가 맞을 떄
                {
					string baseDir = AppDomain.CurrentDomain.BaseDirectory;
					string modelPath = Path.GetFullPath(Path.Combine(baseDir, @"..\\..\SAI.Application\\Python\\runs\\detect\\train\\weights\\best.pt"));

					var mainModel = MainModel.Instance;

                    btnRunModel.Enabled = false; // 실행 후 버튼 비활성화

                    // 기존 모델 삭제 후 런 돌림
                    if (!File.Exists(modelPath) || mainModel.DontShowDeleteModelDialog)
					{
						runModel(sender, e);
                        btnRunModel.BackgroundImage = Properties.Resources.btnRunModel_clicked;
					}
					else
					{
                        // 기존모델 삭제하냐고 다이얼로그를 띄운다.
						var dialog = new DialogDeleteModel(runModel);
						dialog.ShowDialog(this);
					}
				}
                else // 순서가 틀릴 때
                {
                    ShowToastMessage(errorType, missingType, errorMessage);
                }
            }
            else
            {
				errorType = "블록 배치 오류";
				missingType = "\"시작\"";
                errorMessage = "\"시작\" 블록이 맨 앞에 있어야 합니다.\n";
                errorMessage += "시작 블록에 다른 블록들을 연결해주세요.\n";
                ShowToastMessage(errorType, missingType, errorMessage);
			}
		}

        public void runModel(object sender, EventArgs e)
        {
			// 파이썬 코드 실행
			RunButtonClicked?.Invoke(sender, e);
            pTxtDescription.BackgroundImage = Properties.Resources.lbl_report;
			pToDoList.BackgroundImage = Properties.Resources.p_todolist_step3;
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
                lbMissingType.Text = "MISSING " + missingType;
                lbErrorMessage.Text = errorMessage;

                // 4초 대기 (취소 가능)
                await Task.Delay(4000, token);
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

        //private void ibtnGoNotion_Click(object sender, EventArgs e)
        //{
        //    string memo = memoPresenter.GetMemoText();
        //    double thresholdValue = tbarThreshold.Value/100.0;

        //    Console.WriteLine("[DEBUG] memo : " + memo + " !");
        //    Console.WriteLine("[DEBUG] thresholdValue : " + thresholdValue + " !");
        //    Console.WriteLine("[DEBUG] _result.ResultImage : " + _result.ResultImage + " !");

        //    using (var dialog = new DialogNotion(memo, thresholdValue, _result.ResultImage, true))
        //    {
        //        dialog.ShowDialog(this);
        //    }
        //}

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
                blocklyPresenter.HandleJsMessage(message, type, "tutorial");
            });

            // tutorial blockly html 가져오는 경로 설정
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string localPath = Path.GetFullPath(Path.Combine(baseDir, @"..\\..\\Blockly\\TutorialBlockly.html"));
            string uri = new Uri(localPath).AbsoluteUri;


            // webview에서 오는 메세지 처리 로직
            webViewblock.WebMessageReceived += async (s, e) =>
            {
                try
                {
					// webview 메세지 JSON 파싱
					var doc = JsonDocument.Parse(e.WebMessageAsJson);
                    var root = doc.RootElement;

                    if (root.ValueKind == JsonValueKind.Object &&
                        root.TryGetProperty("type", out var typeElem))
                    {
                        string type = typeElem.GetString();

                        switch (type)
                        {
                            case "openFile": // 이미지 파일 불러와서 경로 js로 전달
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
                                        
                                        await webViewblock.ExecuteScriptAsync(
                                            $"window.dispatchEvent(new MessageEvent('message', {{ data: {json} }}));"
                                        );
                                    }
                                }
                                break;

                            case "blockAllCode":    // 블럭 전체 코드
                                string blockAllCode = root.GetProperty("code").GetString();
                                jsBridge.receiveMessageFromJs(blockAllCode, type);
                                break;

                            case "blockCode": // 하나의 블럭 코드
                                string blockCode = root.GetProperty("code").GetString();
                                jsBridge.receiveMessageFromJs(blockCode, type);
                                break;

							case "blockDoubleClick": // 하나의 블럭 더블 클릭
								string eventCode = root.GetProperty("code").GetString();
								blocklyPresenter.OnAddBlockDoubleClicked(eventCode);
								break;

							case "blockTypes": // start 블럭에 연결된 전체 블럭 타입
								var jsonTypes = root.GetProperty("types");
								var blockTypes = JsonSerializer.Deserialize<List<BlockInfo>>(jsonTypes.GetRawText());
								blocklyPresenter.setBlockTypes(blockTypes);
								break;

							case "blockCount": // 전체 꺼내져 있는 블럭 개수
								var jsonCount = root.GetProperty("count").ToString();
								blockCount = int.Parse(jsonCount);
								break;

							case "blockCreated": // 전체 꺼내져 있는 블럭 타입
								var blockType = root.GetProperty("blockType").ToString();
                                var newValue = root.GetProperty("allValues");
								var value = JsonSerializer.Deserialize <Dictionary<string, object>>(newValue.GetRawText());
                                blocklyPresenter.setFieldValue(blockType, value);
								break;

							case "blockFieldUpdated": // 필드 값 변경된 블럭
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

			webViewblock.Source = new Uri(uri);
        }

		// JS 함수 호출 = 블럭 넣기
		public void addBlock(string blockType)
		{
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

   //     private void ibtnAiFeedback_Click(object sender, EventArgs e)
   //     {
   //         string memo = memoPresenter.GetMemoText();
   //         double thresholdValue = tbarThreshold.Value / 100.0;

   //         using (var dialog = new DialogNotion(memo, thresholdValue, _result.ResultImage, true))
   //         {
   //             dialog.ShowDialog(this);
			//}
   //     }


        private bool checkBlockPosition(string blockType, int nowPosition)
        {
            if(nowPosition == 1)
            {
                if(blockType != "pipInstall")
				{
					blockErrorMessage("start");
					return false;
				}
			}
			else if (nowPosition == 2)
			{
				if (blockType != "loadModel")
				{
					blockErrorMessage("pipInstall");
					return false;
				}
			}
			else if (nowPosition == 3)
			{
				if (blockType != "loadDataset")
				{
					blockErrorMessage("loadModel");
					return false;
				}
			}
			else if (nowPosition == 4)
			{
				if (blockType != "machineLearning")
				{
					blockErrorMessage("loadDataset");
					return false;
				}
			}
			else if (nowPosition == 5)
			{
				if (blockType != "resultGraph")
				{
					blockErrorMessage("machineLearning");
					return false;
				}
			}
			else if (nowPosition == 6)
			{
				if (blockType != "imgPath")
				{
					blockErrorMessage("resultGraph");
					return false;
				}
			}
			else if (nowPosition == 7)
			{
				if (blockType != "modelInference")
				{
					blockErrorMessage("imgPath");
					return false;
				}
			}
			else if (nowPosition == 8)
			{
				if (blockType != "visualizeResult")
				{
					blockErrorMessage("modelInference");
					return false;
				}
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
				    errorMessage = "\"패키지 설치\" 블록이 필요합니다. 아래 순서에 맞게 배치해주세요.\n";
				    errorMessage += "[시작] - [패키지 설치]";
					break;
				case "pipInstall":
					errorType = "블록 배치 오류";
					missingType = "\"loadModel\"";
					errorMessage = "\"모델 불러오기\" 블록이 필요합니다. 아래 순서에 맞게 배치해주세요.\n";
					errorMessage += "[패키지 설치] - [모델 불러오기]";
					break;
				case "loadModel":
					errorType = "블록 배치 오류";
					missingType = "\"loadDataset\"";
					errorMessage = "\"데이터 불러오기\" 블록이 필요합니다. 아래 순서에 맞게 배치해주세요.\n";
					errorMessage += "[모델 불러오기] - [데이터 불러오기]";
					break;
				case "loadDataset":
					errorType = "블록 배치 오류";
					missingType = "\"machineLearning\"";
					errorMessage = "\"모델 학습하기\" 블록이 필요합니다. 아래 순서에 맞게 배치해주세요.\n";
					errorMessage += "[데이터 불러오기] - [모델 학습하기]";
					break;
				case "machineLearning":
					errorType = "블록 배치 오류";
					missingType = "\"resultGraph\"";
					errorMessage = "\"학습 결과 그래프 출력하기\" 블록이 필요합니다. 아래 순서에 맞게 배치해주세요.\n";
					errorMessage += "[모델 학습하기] - [학습 결과 그래프 출력하기]";
					break;
				case "resultGraph":
					errorType = "블록 배치 오류";
					missingType = "\"imgPath\"";
					errorMessage = "\"이미지 불러오기\" 블록이 필요합니다. 아래 순서에 맞게 배치해주세요.\n";
					errorMessage += "[학습 결과 그래프 출력하기] - [이미지 불러오기]";
					break;
				case "imgPath":
					errorType = "블록 배치 오류";
					missingType = "\"modelInference\"";
					errorMessage = "\"추론 실행하기\" 블록이 필요합니다. 아래 순서에 맞게 배치해주세요.\n";
					errorMessage += "[이미지 불러오기] - [추론 실행하기]";
					break;
				case "modelInference":
					errorType = "블록 배치 오류";
					missingType = "\"visualizeResult\"";
					errorMessage = "\"결과 시각화하기\" 블록이 필요합니다. 아래 순서에 맞게 배치해주세요.\n";
					errorMessage += "[추론 실행하기] - [결과 시각화하기]";
					break;
			}
		}

        // 블록 에러 처리이이
        public bool isBlockError()
        {
            // start 블럭 밑에 붙어있는 블럭들의 순서를 판단
            for(int i = 0; i < blocklyModel.blockTypes.Count; i++)
            {
				BlockInfo blockType = blocklyModel.blockTypes[i];
				if (!checkBlockPosition(blockType.type, i))
                {
                    return true;
                }
                
                if(blockType.type == "imgPath")
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
			}

            // 만약 count가 9개가 아니라면 마지막 블럭을 오류라고 처리.
            if(blocklyModel.blockTypes.Count < 9)
            {
                blockErrorMessage(blocklyModel.blockTypes[blocklyModel.blockTypes.Count - 1].type);
				return true;
			}

			return false;
        }
        private void UpdateCodeZoom()
        {
            try
            {
                if (ucCode1 != null)
                {
                    // Scintilla 에디터의 폰트 크기 업데이트
                    ucCode1.UpdateFontSize(currentZoomLevel);
                    // 확대/축소 레벨 표시 업데이트
                    tboxZoomCode.Text = $"{currentZoomLevel}%";
                    Console.WriteLine($"[DEBUG] UcTutorialBlockCode: 코드 확대/축소 레벨 변경 - {currentZoomLevel}%");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] UcTutorialBlockCode: 확대/축소 레벨 업데이트 중 오류 발생 - {ex.Message}");
            }
        }

        public void ShowDialogInferenceLoading()
        {
            if (dialogLoadingInfer == null || dialogLoadingInfer.IsDisposed)
            {
                dialogLoadingInfer = new DialogInferenceLoading();
                dialogLoadingInfer.Show();  // 비동기적으로 띄움
            }
        }

        // 추론 이미지 불러오기 버튼 클릭시
        // 사용자 지정 이미지 경로를 blockly.imagepath에 던져줌
        //private void btnSelectInferImage_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        using (OpenFileDialog openFileDialog = new OpenFileDialog())
        //        {
        //            openFileDialog.Filter = "이미지 파일|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
        //            openFileDialog.Title = "이미지 파일 선택";

        //            if (openFileDialog.ShowDialog() == DialogResult.OK)
        //            {
        //                string absolutePath = openFileDialog.FileName;
        //                selectedImagePath = absolutePath;
        //                currentImagePath = absolutePath; // 현재 이미지 경로 저장
        //                blocklyModel.imgPath = selectedImagePath;

        //                // UI 표시용 이미지
        //                using (var stream = new FileStream(absolutePath, FileMode.Open, FileAccess.Read))
        //                {
        //                    var originalImage = System.Drawing.Image.FromStream(stream);
        //                    pboxInferAccuracy.Size = new Size(494, 278);
        //                    pboxInferAccuracy.SizeMode = PictureBoxSizeMode.Zoom;
        //                    pboxInferAccuracy.Image = originalImage;
        //                    pboxInferAccuracy.Visible = true;
        //                }

        //                btnSelectInferImage.Visible = false;
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"이미지 로드 중 오류가 발생했습니다: {ex.Message}", "오류",
        //            MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        // DialogInferenceLoading 닫고 pboxInferAccuracy에 추론 결과 이미지 띄우는 함수
        // var tutorialView = new UcTutorialBlockCode(mainView);
        //tutorialView.ShowTutorialInferResultImage(resultImage);
        public void ShowInferenceResult(PythonService.InferenceResult result)
        {
            btnRunModel.Enabled = true;

            if (InvokeRequired)
            {
                Invoke(new Action(() => ShowInferenceResult(result)));
                return;
            }

            dialogLoadingInfer?.Close();
            dialogLoadingInfer = null;

            if (result.Success)
            {
                if (File.Exists(result.ResultImage))
                {
                    try
                    {
                        // 결과 이미지 경로 저장
                        currentImagePath = result.ResultImage;
                        _result = result;
                        
                        // 파일 이름에 한글이 포함된 경우 Stream을 통해 로드하여 문제 해결
                        using (var stream = new FileStream(result.ResultImage, FileMode.Open, FileAccess.Read))
                        {
                            var image = System.Drawing.Image.FromStream(stream);

                            // ✅ 직접 PictureBox에 표시
                            ucInferenceTab.ShowResultImage(result.ResultImage);
                            //pboxInferAccuracy.Size = new Size(494, 278);
                            //pboxInferAccuracy.SizeMode = PictureBoxSizeMode.Zoom;
                            //pboxInferAccuracy.Image = image;
                            //pboxInferAccuracy.Visible = true;
                            //btnSelectInferImage.Visible = false;
                            
                            // 추론 패널이 현재 표시되어 있지 않다면 표시
                            if (!isInferPanelVisible)
                            {
                                ShowpSIdeInfer();
                                Console.WriteLine("[DEBUG] 추론 패널 표시됨");
                            }
                            
                            // 이미지 클릭 시 원본 이미지를 열 수 있다는 정보 표시
                            ToolTip toolTip = new ToolTip();
                            //toolTip.SetToolTip(pboxInferAccuracy, "이미지를 클릭하여 원본 크기로 보기");
                            ucInferenceTab.SetAccuracyTooltip(toolTip);

                            
                            // 원본 파일명 정보 표시 (필요한 경우)
                            if (!string.IsNullOrEmpty(result.OriginalName))
                            {
                                Console.WriteLine($"[INFO] 원본 이미지 파일명: {result.OriginalName}");
                                // 여기에 원본 파일명을 표시하는 코드 추가 가능
                                // 예: lblOriginalFilename.Text = result.OriginalName;
                            }
                            
                            Console.WriteLine($"[DEBUG] 추론 결과 이미지 표시 완료: {result.ResultImage}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("[ERROR] 이미지 로드 실패: " + ex.Message);
                        MessageBox.Show($"이미지를 로드하는 도중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show($"결과 이미지 파일을 찾을 수 없습니다: {result.ResultImage}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // 추론 실패 다이얼로그 생성 및 표시
                //btnSelectInferImage.Visible = true;
                //pboxInferAccuracy.Visible = false;

                var dialog = new DialogErrorInference();
                dialog.SetErrorMessage(result.Error); // 에러 메시지 설정
                dialog.ShowDialog(this); // 현재 폼을 부모로 지정
            }

            Console.WriteLine("[DEBUG] ShowInferenceResult() 호출됨");
            Console.WriteLine($"[DEBUG] Result.Success = {result.Success}");
            Console.WriteLine($"[DEBUG] Result.ResultImage = {result.ResultImage}");
            Console.WriteLine($"[DEBUG] 파일 존재 여부: {File.Exists(result.ResultImage)}");

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

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                // BlocklyModel에서 전체 코드 가져오기
                string codeToCopy = blocklyModel.blockAllCode;
                
                if (!string.IsNullOrEmpty(codeToCopy))
                {
                    // 클립보드에 코드 복사
                    Clipboard.SetText(codeToCopy);
                    Console.WriteLine("[DEBUG] UcTutorialBlockCode: 코드가 클립보드에 복사됨");
                }
                else
                {
                    Console.WriteLine("[WARNING] UcTutorialBlockCode: 복사할 코드가 없음");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] UcTutorialBlockCode: 코드 복사 중 오류 발생 - {ex.Message}");
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

        //private async void ibtnDownloadAIModel_Click(object sender, EventArgs e)
        //{
        //    string modelFileName = "best.pt";

        //    var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        //    //모델 경로 다시 물어보기
        //    string _modelPath = Path.GetFullPath(Path.Combine(baseDir, @"..\\..\\SAI.Application\\Python\\runs\\detect\\train\\weights\\best.pt"));

        //    if (!File.Exists(_modelPath))
        //    {
        //        MessageBox.Show(
        //            $"모델 파일을 찾을 수 없습니다.\n{_modelPath}",
        //            "오류",
        //            MessageBoxButtons.OK,
        //            MessageBoxIcon.Error);
        //        return;
        //    }

        //    using (var folderDialog = new FolderBrowserDialog())
        //    {
        //        folderDialog.Description = "모델을 복사할 폴더를 선택하세요.";
        //        folderDialog.ShowNewFolderButton = true;

        //        if (folderDialog.ShowDialog() == DialogResult.OK)
        //        {
        //            string destPath = Path.Combine(folderDialog.SelectedPath, modelFileName);

        //            // 비동기 복사 (UI 멈춤 방지)
        //            await CopyModelAsync(_modelPath, destPath);

        //            MessageBox.Show(
        //                $"모델이 복사되었습니다.\n{destPath}",
        //                "완료",
        //                MessageBoxButtons.OK,
        //                MessageBoxIcon.Information);
        //        }
        //    }
        //}
        //private Task CopyModelAsync(string source, string destination)
        //{
        //    return Task.Run(() =>
        //    {
        //        // 존재할 경우 덮어쓰기(true)
        //        File.Copy(source, destination, overwrite: true);
        //    });
        //}

        ///* 이미 있는 ucCsvChart1을 재활용 */
        public void ShowTutorialTrainingChart(string csvPath)
        {
            try
            {
                if (!File.Exists(csvPath))
                {
                    //ShowErrorMessage($"CSV 파일을 찾을 수 없습니다.\n{csvPath}");
                    return;
                }

                /* ① CSV → LogCsvModel 채우기 */
                var model = LogCsvModel.instance;
                new LogCsvPresenter(null).LoadCsv(csvPath);   // 데이터만 채우는 전용 메서드(아래 4-b) 참고)

                /* ② 차트 갱신 */
                ucInferenceTab.CsvChart.SetData();      // 내부에서 model 값을 읽어 그림
                //ucCsvChart1.Visible = true; // 필요하면 처음엔 Visible=false 로 해두고 여기서 켜기
                ucInferenceTab.CsvChart.Visible = true;
            }
            catch (Exception ex)
            {
                //ShowErrorMessage($"차트 로드 중 오류가 발생했습니다: {ex.Message}");
            }
        }

        private void pErrorToastCloseBtn_Click(object sender, EventArgs e)
        {
            pErrorToast.Visible = false;
        }
    }
}
