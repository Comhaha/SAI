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
        // ✅ 추가: 정적 변수로 현재 열려있는 다이얼로그 추적
        private static DialogStartcampInput _currentInstance = null;

        private YoloTutorialPresenter yoloTutorialPresenter;
        private string selectedImagePath = string.Empty;
        private string currentImagePath = string.Empty;
        private double currentThreshold = 0.5; // threshold 기본값 0.5
        private PythonService.InferenceResult _result;
        private DialogInferenceLoading dialogLoadingInfer;

        // ✅ 추가: 추론 결과 표시 중인지 확인하는 플래그
        private bool isShowingInferenceResult = false;

        public DialogStartcampInput()
        {
            InitializeComponent();
            this.TopMost = false;

            _currentInstance = this;

            // 기본 다이얼로그 스타일 적용
            DialogUtils.ApplyDefaultStyle(this, Color.Gray);
            ButtonUtils.SetupButton(btnClose2, "bg_yellow_btn_close_clicked", "bg_yellow_btn_close");
            btnClose2.Click += (s, e) => { this.Close(); };

            // YoloTutorialPresenter 초기화
            yoloTutorialPresenter = new YoloTutorialPresenter(this);

            // ✅ 추가: BlocklyModel의 ImgPathChanged 이벤트 구독
            var blocklyModel = BlocklyModel.Instance;
            if (blocklyModel != null)
            {
                blocklyModel.ImgPathChanged += OnBlocklyImagePathChanged;
            }

            // 새 이미지 불러오기 버튼 설정
            btnStartcampInput.Size = new Size(720, 687);
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

        // ✅ 추가: BlocklyModel에서 이미지 경로가 변경될 때 호출되는 메서드
        private void OnBlocklyImagePathChanged(string newPath)
        {
            // ✅ 추론 결과 표시 중이면 동기화 스킵
            if (isShowingInferenceResult)
            {
                Console.WriteLine("[DEBUG] DialogStartcampInput: 추론 결과 표시 중이므로 이미지 경로 동기화 스킵");
                return;
            }

            if (InvokeRequired)
            {
                Invoke(new Action(() => OnBlocklyImagePathChanged(newPath)));
                return;
            }

            if (!string.IsNullOrEmpty(newPath) && File.Exists(newPath))
            {
                selectedImagePath = newPath;
                currentImagePath = newPath;

                try
                {
                    // 기존 이미지 정리
                    pboxStartcampInput.Image?.Dispose();

                    // 새 이미지 로드
                    using (var stream = new FileStream(newPath, FileMode.Open, FileAccess.Read))
                    {
                        var originalImage = System.Drawing.Image.FromStream(stream);
                        pboxStartcampInput.Size = new Size(720, 687);
                        pboxStartcampInput.SizeMode = PictureBoxSizeMode.Zoom;
                        pboxStartcampInput.Image = originalImage;
                        pboxStartcampInput.Visible = true;
                    }

                    btnStartcampInput.Visible = false;

                    Console.WriteLine($"[DEBUG] DialogStartcampInput: 블록에서 이미지 경로 변경됨 - {newPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] DialogStartcampInput: 블록 이미지 로드 실패 - {ex.Message}");
                }
            }
        }



        // 3. 정적 메서드 추가
        /// <summary>
        /// 현재 열려있는 DialogStartcampInput에 추론 결과를 전달
        /// </summary>
        public static void UpdateCurrentDialogWithResult(PythonService.InferenceResult result)
        {
            if (_currentInstance != null && !_currentInstance.IsDisposed)
            {
                if (_currentInstance.InvokeRequired)
                {
                    _currentInstance.Invoke(new Action(() => {
                        _currentInstance.ShowPracticeInferResultImage(result);
                    }));
                }
                else
                {
                    _currentInstance.ShowPracticeInferResultImage(result);
                }
                Console.WriteLine("[DEBUG] DialogStartcampInput 자동 업데이트 완료");
            }
            else
            {
                Console.WriteLine("[DEBUG] DialogStartcampInput 인스턴스가 없어서 업데이트 스킵");
            }
        }

        // 이미지 경로를 받는 생성자 추가
        public DialogStartcampInput(string imagePath) : this()
        {
            this.TopMost = false;
            Console.WriteLine($"[DEBUG] DialogStartcampInput 생성자 호출: {imagePath}");

            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
            {
                selectedImagePath = imagePath;
                currentImagePath = imagePath;

                try
                {
                    using (var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    {
                        var originalImage = System.Drawing.Image.FromStream(stream);

                        // ✅ PictureBox 설정 및 이미지 표시
                        pboxStartcampInput.Size = new Size(720, 687);
                        pboxStartcampInput.SizeMode = PictureBoxSizeMode.Zoom;
                        pboxStartcampInput.Image = originalImage;
                        pboxStartcampInput.Visible = true;
                    }

                    // ✅ 버튼 숨기기 (이미지가 이미 로드되었으므로)
                    btnStartcampInput.Visible = false;

                    // ✅ 블록에서 사용한 threshold 값을 가져와서 동기화
                    var blocklyModel = BlocklyModel.Instance;
                    double blockThreshold = blocklyModel?.accuracy ?? 0.5;

                    // threshold 컨트롤 설정 (블록 값으로 동기화)
                    if (tbarThreshold != null && tboxThreshold != null)
                    {
                        // 블록의 accuracy 값을 threshold 컨트롤에 설정
                        int trackBarValue = (int)(blockThreshold * 100); // 0.5 -> 50
                        tbarThreshold.Value = Math.Max(tbarThreshold.Minimum, Math.Min(tbarThreshold.Maximum, trackBarValue));
                        tboxThreshold.Text = blockThreshold.ToString("F2"); // 0.50 형식으로 표시
                        currentThreshold = blockThreshold;

                        Console.WriteLine($"[DEBUG] 블록 threshold 값 동기화: {blockThreshold} (TrackBar: {trackBarValue})");
                    }
                    

                    Console.WriteLine($"[DEBUG] DialogStartcampInput: 이미지 로드 완료 - {imagePath}");

                    // ✅ 추론 결과 이미지인지 확인하여 UI 상태 조정
                    if (imagePath.Contains("predict") || imagePath.Contains("result") || imagePath.Contains("inference"))
                    {
                        // 추론 결과 이미지인 경우
                        this.Text = "추론 결과 - AI 블록 코딩";
                        Console.WriteLine("[DEBUG] 추론 결과 이미지로 인식됨");
                    }
                    else
                    {
                        // 일반 이미지인 경우
                        this.Text = "이미지 선택 - AI 블록 코딩";
                        Console.WriteLine("[DEBUG] 일반 이미지로 인식됨");
                    }

                    // ✅ 이미지 로드 후 폼 중앙 정렬
                    this.StartPosition = FormStartPosition.CenterParent;

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] DialogStartcampInput: 이미지 로드 실패 - {ex.Message}");

                    // 실패시 기본 상태로 되돌리기
                    pboxStartcampInput.Image = null;
                    btnStartcampInput.Visible = true;
                    selectedImagePath = string.Empty;
                    currentImagePath = string.Empty;

                    MessageBox.Show($"이미지를 로드하는 중 오류가 발생했습니다: {ex.Message}", "오류",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Console.WriteLine($"[WARNING] DialogStartcampInput: 유효하지 않은 이미지 경로 - {imagePath}");

                // 기본 상태 유지 (이미지 선택 버튼 표시)
                btnStartcampInput.Visible = true;
                this.Text = "이미지 선택 - AI 블록 코딩";

                // ✅ 블록 threshold 값 동기화 (이미지가 없어도)
                var blocklyModel = BlocklyModel.Instance;
                double blockThreshold = blocklyModel?.accuracy ?? 0.5;

                if (tbarThreshold != null && tboxThreshold != null)
                {
                    int trackBarValue = (int)(blockThreshold * 100);
                    tbarThreshold.Value = Math.Max(tbarThreshold.Minimum, Math.Min(tbarThreshold.Maximum, trackBarValue));
                    tboxThreshold.Text = blockThreshold.ToString("F2");
                    currentThreshold = blockThreshold;

                    Console.WriteLine($"[DEBUG] 기본 상태에서 블록 threshold 값 동기화: {blockThreshold}");
                }

                if (!string.IsNullOrEmpty(imagePath))
                {
                    MessageBox.Show($"지정된 이미지 파일을 찾을 수 없습니다:\n{imagePath}", "파일 없음",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void SetupHoverEvents()
        {
            // pboxStartcampInput 마우스 이벤트
            pboxStartcampInput.MouseEnter += (s, e) =>
            {
                btnStartcampInput.Visible = true;
                btnStartcampInput.BackgroundImage = Properties.Resources.btn_startcamp_input;
            };

            pboxStartcampInput.MouseLeave += (s, e) =>
            {
                if (!btnStartcampInput.ClientRectangle.Contains(btnStartcampInput.PointToClient(Control.MousePosition)))
                {
                    btnStartcampInput.Visible = false;
                    btnStartcampInput.BackgroundImage = Properties.Resources.btn_startcamp_input;
                }
            };

            // btnStartcampInput 마우스 이벤트
            btnStartcampInput.MouseEnter += (s, e) =>
            {
                btnStartcampInput.Visible = true;
                btnStartcampInput.BackgroundImage = Properties.Resources.btn_startcamp_input;
            };

            btnStartcampInput.MouseLeave += (s, e) =>
            {
                if (!pboxStartcampInput.ClientRectangle.Contains(pboxStartcampInput.PointToClient(Control.MousePosition)))
                {
                    btnStartcampInput.Visible = false;
                    btnStartcampInput.BackgroundImage = Properties.Resources.btn_startcamp_input;
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

                    // 현재 BlocklyModel의 이미지 경로 사용 (항상 최신 상태 보장)
                    var blocklyModel = BlocklyModel.Instance;
                    string imagePathToUse = !string.IsNullOrEmpty(blocklyModel?.imgPath) ? blocklyModel.imgPath : selectedImagePath;

                    Console.WriteLine($"[LOG] DialogStartcampInput - selectedImagePath: {selectedImagePath}");
                    Console.WriteLine($"[LOG] DialogStartcampInput - currentThreshold: {currentThreshold}");

                    // 추론은 백그라운드에서 실행
                    // 이미지경로, threshold 값을 던져야 추론스크립트 실행 가능
                    Task.Run(async () =>
                    {
                        _result = await yoloTutorialPresenter.RunInferenceDirect(
                            imagePathToUse,
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

                        // ✅ 추가: BlocklyModel의 imgPath도 동기화
                        var blocklyModel = BlocklyModel.Instance;
                        if (blocklyModel != null)
                        {
                            blocklyModel.imgPath = absolutePath;
                            Console.WriteLine($"[DEBUG] DialogStartcampInput: BlocklyModel.imgPath 동기화 완료 - {absolutePath}");
                        }

                        // 이미지 표시
                        using (var stream = new FileStream(absolutePath, FileMode.Open, FileAccess.Read))
                        {
                            var originalImage = System.Drawing.Image.FromStream(stream);
                            pboxStartcampInput.Size = new Size(720, 687);
                            //pboxStartcampInput.SizeMode = PicticeBoxSizeMode.Zoom;
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
        // DialogStartcampInput.cs에서 ShowPracticeInferResultImage 메서드만 수정

        public void ShowPracticeInferResultImage(PythonService.InferenceResult result)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ShowPracticeInferResultImage(result)));
                return;
            }

            // ✅ 핵심 수정: 더 안전하게 다이얼로그 닫기
            try
            {
                if (dialogLoadingInfer != null && !dialogLoadingInfer.IsDisposed)
                {
                    dialogLoadingInfer.Close();
                    dialogLoadingInfer.Dispose();
                    Console.WriteLine("[DEBUG] DialogInferenceLoading 닫기 완료");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[WARNING] DialogInferenceLoading 닫기 중 오류: {ex.Message}");
            }
            finally
            {
                dialogLoadingInfer = null;
            }

            if (result.Success)
            {
                if (File.Exists(result.ResultImage))
                {
                    try
                    {

                        // ✅ 추가: 기존 이미지 정리
                        if (pboxStartcampInput.Image != null)
                        {
                            pboxStartcampInput.Image.Dispose();
                        }
                        // 결과 이미지 경로 저장
                        currentImagePath = result.ResultImage;
                        _result = result;

                        // 파일 이름에 한글이 포함된 경우 Stream을 통해 로드하여 문제 해결
                        using (var stream = new FileStream(result.ResultImage, FileMode.Open, FileAccess.Read))
                        {
                            var image = System.Drawing.Image.FromStream(stream);

                            // ✅ 직접 PictureBox에 표시
                            pboxStartcampInput.Size = new Size(720, 687);
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
            string imageToShow = null;

            // 1. 추론 결과 이미지가 있는지 확인 (우선순위)
            if (_result != null && _result.Success && !string.IsNullOrEmpty(_result.ResultImage) && File.Exists(_result.ResultImage))
            {
                imageToShow = _result.ResultImage;
                Console.WriteLine($"[INFO] 추론 결과 이미지 표시: {imageToShow}");
            }
            // 2. 추론 결과가 없으면 현재 표시된 이미지 사용
            else if (!string.IsNullOrEmpty(currentImagePath) && File.Exists(currentImagePath))
            {
                imageToShow = currentImagePath;
                Console.WriteLine($"[INFO] 현재 이미지 표시: {imageToShow}");
            }
            // 3. 그것도 없으면 원본 선택된 이미지 사용
            else if (!string.IsNullOrEmpty(selectedImagePath) && File.Exists(selectedImagePath))
            {
                imageToShow = selectedImagePath;
                Console.WriteLine($"[INFO] 선택된 이미지 표시: {imageToShow}");
            }

            // 이미지가 있으면 기본 뷰어로 열기
            if (!string.IsNullOrEmpty(imageToShow))
            {
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                    {
                        FileName = imageToShow,
                        UseShellExecute = true
                    });
                    Console.WriteLine($"[INFO] 원본 크기 이미지 열기 성공: {imageToShow}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] 이미지 열기 실패: {ex.Message}");
                    MessageBox.Show($"이미지를 열 수 없습니다.\n{ex.Message}", "오류",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("표시할 이미지가 없습니다.\n먼저 이미지를 선택하고 추론을 실행해주세요.", "알림",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine("[WARNING] 표시할 이미지 없음");
            }
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
            this.TopMost = false;
            // 필요한 초기화 작업
            Console.WriteLine("[DEBUG] DialogStartcampInput 로드 완료");
        }

        // 다이얼로그 종료 시 정리
        // ✅ 추가: 다이얼로그 종료 시 이벤트 구독 해제
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // BlocklyModel 이벤트 구독 해제
            var blocklyModel = BlocklyModel.Instance;
            if (blocklyModel != null)
            {
                blocklyModel.ImgPathChanged -= OnBlocklyImagePathChanged;
            }

            // ✅ 기존: 다이얼로그 닫힐 때 추적 해제
            if (_currentInstance == this)
            {
                _currentInstance = null;
            }

            // 리소스 정리
            pboxStartcampInput.Image?.Dispose();
            dialogLoadingInfer?.Close();
            base.OnFormClosed(e);
        }

        private void DialogStartcampInput_Load_1(object sender, EventArgs e)
        {

        }
    }
}