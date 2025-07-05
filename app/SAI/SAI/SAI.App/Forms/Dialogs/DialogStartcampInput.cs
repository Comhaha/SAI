using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SAI.SAI.App.Views.Common;
using SAI.SAI.App.Presenters;
using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.App.Models;
using SAI.SAI.Application.Service;
using SAI.SAI.App.Forms.Dialogs;

namespace SAI.SAI.App.Forms.Dialogs
{
    public partial class DialogStartcampInput : Form, IPracticeInferenceView
    {
        private YoloTutorialPresenter yoloTutorialPresenter;
        private string selectedImagePath = string.Empty;
        private string currentImagePath = string.Empty;
        private double currentThreshold = 0.5; // threshold 기본값 0.5
        private PythonService.InferenceResult _result;
        private DialogInferenceLoading dialogLoadingInfer;

        public DialogStartcampInput()
        {
            InitializeComponent();

            // 기본 다이얼로그 스타일 적용
            DialogUtils.ApplyDefaultStyle(this, Color.Gray);
            ButtonUtils.SetupButton(btnClose2, "bg_yellow_btn_close_clicked", "bg_yellow_btn_close");
            btnClose2.Click += (s, e) => { this.Close(); };

            // YoloTutorialPresenter 초기화
            yoloTutorialPresenter = new YoloTutorialPresenter(this);

            // 새 이미지 불러오기 버튼 설정
            btnStartcampInput.Size = new Size(494, 278);  // pInferAccuracy와 동일한 크기
            pboxStartcampInput.Controls.Add(btnStartcampInput);
            btnStartcampInput.Location = new Point(0, 0);
            btnStartcampInput.Enabled = true;
            btnStartcampInput.Cursor = Cursors.Hand;

            // 초기에는 btnStartcampInput을 숨김
            btnStartcampInput.Visible = false;

            // 마우스 호버 이벤트 설정
            SetupHoverEvents();

            // Threshold 컨트롤 설정
            SetupThresholdControls();

            // 기타 스타일 설정
            ButtonUtils.SetTransparentStyle(btnStartcampInput);

            // 초기 이미지 설정
            pboxStartcampInput.Image = null;
            pboxStartcampInput.SizeMode = PictureBoxSizeMode.Zoom;

            // 툴팁 설정
            ToolTipUtils.CustomToolTip(btnStartcampInput, "추론에 사용할 이미지를 가져오려면 클릭하세요.");
            //ToolTipUtils.CustomToolTip(btnInfoThreshold, "AI의 분류 기준입니다. 예측 결과가 이 값보다 높으면 '맞다(1)'고 판단하고, 낮으면 '아니다(0)'로 처리합니다.");
        }

        private void SetupHoverEvents()
        {
            // pboxStartcampInput 마우스 이벤트
            pboxStartcampInput.MouseEnter += (s, e) =>
            {
                btnStartcampInput.Visible = true;
                btnStartcampInput.BackgroundImage = Properties.Resources.btn_selectinferimage_hover;
            };

            pboxStartcampInput.MouseLeave += (s, e) =>
            {
                if (!btnStartcampInput.ClientRectangle.Contains(btnStartcampInput.PointToClient(Control.MousePosition)))
                {
                    btnStartcampInput.Visible = false;
                    btnStartcampInput.BackgroundImage = Properties.Resources.btn_selectinferimage;
                }
            };

            // btnStartcampInput 마우스 이벤트
            btnStartcampInput.MouseEnter += (s, e) =>
            {
                btnStartcampInput.Visible = true;
                btnStartcampInput.BackgroundImage = Properties.Resources.btn_selectinferimage_hover;
            };

            btnStartcampInput.MouseLeave += (s, e) =>
            {
                if (!pboxStartcampInput.ClientRectangle.Contains(pboxStartcampInput.PointToClient(Control.MousePosition)))
                {
                    btnStartcampInput.Visible = false;
                    btnStartcampInput.BackgroundImage = Properties.Resources.btn_selectinferimage;
                }
            };
        }

        private void SetupThresholdControls()
        {
            ThresholdUtilsPractice.Setup(
                tbarThreshold,
                tboxThreshold,
                (newValue) =>
                {
                    currentThreshold = newValue;

                    Console.WriteLine($"[LOG] DialogStartcampInput - selectedImagePath: {selectedImagePath}");
                    Console.WriteLine($"[LOG] DialogStartcampInput - currentThreshold: {currentThreshold}");

                    // 추론은 백그라운드에서 실행
                    // 이미지경로, threshold 값을 던져야 추론스크립트 실행 가능
                    Task.Run(async () =>
                    {
                        _result = await yoloTutorialPresenter.RunInferenceDirect(
                            selectedImagePath,
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
                            ShowPracticeInferResultImage(_result);
                        }));
                    });
                },
                this
            );
        }

        // 추론 이미지 불러오기
        private void btnStartcampInput_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "이미지 파일|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                    openFileDialog.Title = "이미지 파일 선택";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string absolutePath = openFileDialog.FileName;
                        selectedImagePath = absolutePath;
                        currentImagePath = absolutePath;

                        // 이미지 표시
                        using (var stream = new FileStream(absolutePath, FileMode.Open, FileAccess.Read))
                        {
                            var originalImage = System.Drawing.Image.FromStream(stream);
                            pboxStartcampInput.Size = new Size(494, 278);
                            pboxStartcampInput.SizeMode = PictureBoxSizeMode.Zoom;
                            pboxStartcampInput.Image = originalImage;
                            pboxStartcampInput.Visible = true;
                        }

                        btnStartcampInput.Visible = false;

                        Console.WriteLine($"[DEBUG] DialogStartcampInput: 이미지 로드 완료 - {absolutePath}");
                    }
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
            if (dialogLoadingInfer == null || dialogLoadingInfer.IsDisposed)
            {
                dialogLoadingInfer = new DialogInferenceLoading();
                dialogLoadingInfer.Show();  // 비동기적으로 띄움
            }
        }

        // IPracticeInferenceView 인터페이스 구현
        public void ShowPracticeInferResultImage(PythonService.InferenceResult result)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ShowPracticeInferResultImage(result)));
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
                            pboxStartcampInput.Size = new Size(494, 278);
                            pboxStartcampInput.SizeMode = PictureBoxSizeMode.Zoom;
                            pboxStartcampInput.Image = image;
                            pboxStartcampInput.Visible = true;
                            btnStartcampInput.Visible = false;

                            // 이미지 클릭 시 원본 이미지를 열 수 있다는 정보 표시
                            ToolTip toolTip = new ToolTip();
                            toolTip.SetToolTip(pboxStartcampInput, "이미지를 클릭하여 원본 크기로 보기");

                            // 원본 파일명 정보 표시 (필요한 경우)
                            if (!string.IsNullOrEmpty(result.OriginalName))
                            {
                                Console.WriteLine($"[INFO] 원본 이미지 파일명: {result.OriginalName}");
                            }
                        }

                        Console.WriteLine("[DEBUG] DialogStartcampInput: 추론 결과 이미지 표시 완료");
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
                var dialog = new DialogErrorInference();
                dialog.SetErrorMessage(result.Error);
                dialog.ShowDialog(this);
            }
        }

        // 기존 이벤트 핸들러들
        private void panelTitleBar_Paint(object sender, PaintEventArgs e)
        {
        }

        private void tbarThreshold_Scroll(object sender, ScrollEventArgs e)
        {
        }

        private void lblThreshold_Load(object sender, EventArgs e)
        {
        }

        private void pboxInferAccuracy_Click(object sender, EventArgs e)
        {
        }

        private void ibtnSizeup_Click(object sender, EventArgs e)
        {
        }

        private void pboxStartcampInput_Click(object sender, EventArgs e)
        {
            // 이미지 클릭 시 원본 크기로 보기 (선택사항)
            if (!string.IsNullOrEmpty(currentImagePath) && File.Exists(currentImagePath))
            {
                try
                {
                    System.Diagnostics.Process.Start(currentImagePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] 이미지 열기 실패: {ex.Message}");
                }
            }
        }

        // 다이얼로그 로드 시 초기화
        private void DialogStartcampInput_Load(object sender, EventArgs e)
        {
            // 필요한 초기화 작업
            Console.WriteLine("[DEBUG] DialogStartcampInput 로드 완료");
        }

        // 다이얼로그 종료 시 정리
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // 리소스 정리
            pboxStartcampInput.Image?.Dispose();
            dialogLoadingInfer?.Close();
            base.OnFormClosed(e);
        }
    }
}