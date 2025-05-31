using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using SAI.SAI.App.Forms.Dialogs;
using SAI.SAI.App.Models;
using SAI.SAI.App.Presenters;
using SAI.SAI.App.Views.Common;
using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.Application.Service;


namespace SAI.SAI.App.Views.Pages
{
    public partial class UcInferenceTab : UserControl
    {
        private readonly IMainView mainView;
        private BlocklyModel blocklyModel;
        private MemoPresenter memoPresenter;
        private PythonService.InferenceResult _result;
        private string selectedImagePath = string.Empty; //추론탭에서 선택한 이미지 저장할 변수

        public UcInferenceTab(IMainView view)
        {
            InitializeComponent();
            this.mainView = view;

            SetupbtnSelectInferImageOverlay();

            pleaseControlThreshold.Visible = false;
            pboxInferAccuracy.Image = null;

            ToolTipUtils.CustomToolTip(btnInfoThreshold,
            "AI의 분류 기준입니다. 예측 결과가 이 값보다 높으면 '맞다(1)'고 판단하고, 낮으면 '아니다(0)'로 처리합니다.");
            ToolTipUtils.CustomToolTip(btnInfoGraph,
              "AI 모델의 성능을 한눈에 확인할 수 있는 그래프입니다. 정확도, 재현율 등의 성능 지표가 포함되어 있습니다.");
            ToolTipUtils.CustomToolTip(btnSelectInferImage, "추론에 사용할 이미지를 가져오려면 클릭하세요.");

            ButtonUtils.SetupButton(btnSelectInferImage, "btn_selectinferimage_hover", "btn_selectinferimage");
            ButtonUtils.SetTransparentStyle(btnSelectInferImage);
            ButtonUtils.SetTransparentStyle(btnInfoGraph);
            ButtonUtils.SetTransparentStyle(btnInfoThreshold);

        }
        public void showDialog(Form dialog)
        {
            dialog.Owner = mainView as Form;
            dialog.ShowDialog();
        }

        private void SetupbtnSelectInferImageOverlay()
        {
            btnSelectInferImage.Visible = false;
            btnSelectInferImage.Size = new Size(494, 278);
            pboxInferAccuracy.Controls.Add(btnSelectInferImage);
            btnSelectInferImage.Location = new Point(0, 0);
            btnSelectInferImage.Enabled = true;
            btnSelectInferImage.Cursor = Cursors.Hand;
            btnSelectInferImage.Click += btnSelectInferImage_Click;

            pboxInferAccuracy.MouseEnter += (s, e) =>
            {
                if (this.Visible)
                {
                    btnSelectInferImage.Visible = true;
                    btnSelectInferImage.BringToFront();
                    btnSelectInferImage.BackgroundImage = Properties.Resources.btn_selectinferimage_hover;
                }
            };

            pboxInferAccuracy.MouseLeave += (s, e) =>
            {
                if (!btnSelectInferImage.ClientRectangle.Contains(
                        btnSelectInferImage.PointToClient(Control.MousePosition)))
                {
                    btnSelectInferImage.Visible = false;
                    btnSelectInferImage.BackgroundImage = Properties.Resources.btn_selectinferimage;
                }
            };

            btnSelectInferImage.MouseEnter += (s, e) =>
            {
                btnSelectInferImage.Visible = true;
                btnSelectInferImage.BackgroundImage = Properties.Resources.btn_selectinferimage_hover;
            };

            btnSelectInferImage.MouseLeave += (s, e) =>
            {
                if (!pboxInferAccuracy.ClientRectangle.Contains(
                        pboxInferAccuracy.PointToClient(Control.MousePosition)))
                {
                    btnSelectInferImage.Visible = false;
                    btnSelectInferImage.BackgroundImage = Properties.Resources.btn_selectinferimage;
                }
            };
        }

        public void ShowResultImage(string imagePath)
        {
            if (File.Exists(imagePath))
            {
                pboxInferAccuracy.Image?.Dispose();

                var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                var image = System.Drawing.Image.FromStream(stream);

                pboxInferAccuracy.Size = new Size(494, 278);
                pboxInferAccuracy.SizeMode = PictureBoxSizeMode.Zoom;
                pboxInferAccuracy.Image = image;
                pboxInferAccuracy.Visible = true;

                pleaseControlThreshold.Visible = false;
                btnSelectInferImage.Visible = false;
            }
        }

        // 추론 이미지 불러오기 버튼 클릭시
        // 사용자 지정 이미지 경로를 blockly.imagepath에 던져줌
        private void btnSelectInferImage_Click(object sender, EventArgs e)
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
                        blocklyModel.imgPath = selectedImagePath;

                        // UI 표시용 이미지
                        using (var stream = new FileStream(absolutePath, FileMode.Open, FileAccess.Read))
                        {
                            var originalImage = System.Drawing.Image.FromStream(stream);
                            pboxInferAccuracy.Size = new Size(494, 278);
                            pboxInferAccuracy.SizeMode = PictureBoxSizeMode.Zoom;
                            pboxInferAccuracy.Image = originalImage;
                            pboxInferAccuracy.Visible = true;
                        }

                        btnSelectInferImage.Visible = false;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"이미지 로드 중 오류가 발생했습니다: {ex.Message}", "오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 동현이 코드
        private void ibtnGoNotion_Click(object sender, EventArgs e)
        {
            string memo = memoPresenter.GetMemoText();
            double thresholdValue = tbarThreshold.Value / 100.0;

            Console.WriteLine("[DEBUG] memo : " + memo + " !");
            Console.WriteLine("[DEBUG] thresholdValue : " + thresholdValue + " !");
            Console.WriteLine("[DEBUG] _result.ResultImage : " + _result.ResultImage + " !");

            using (var dialog = new DialogNotion(memo, thresholdValue, _result.ResultImage, true))
            {
                dialog.ShowDialog(this);
            }
        }

        private async void ibtnDownloadAIModel_Click(object sender, EventArgs e)
        {
            string modelFileName = "best.pt";

            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            //모델 경로 다시 물어보기
            string _modelPath = Path.GetFullPath(Path.Combine(baseDir, @"..\\..\\SAI.Application\\Python\\runs\\detect\\train\\weights\\best.pt"));

            if (!File.Exists(_modelPath))
            {
                MessageBox.Show(
                    $"모델 파일을 찾을 수 없습니다.\n{_modelPath}",
                    "오류",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            using (var folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "모델을 복사할 폴더를 선택하세요.";
                folderDialog.ShowNewFolderButton = true;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string destPath = Path.Combine(folderDialog.SelectedPath, modelFileName);

                    // 비동기 복사 (UI 멈춤 방지)
                    await CopyModelAsync(_modelPath, destPath);

                    MessageBox.Show(
                        $"모델이 복사되었습니다.\n{destPath}",
                        "완료",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }
        private Task CopyModelAsync(string source, string destination)
        {
            return Task.Run(() =>
            {
                // 존재할 경우 덮어쓰기(true)
                File.Copy(source, destination, overwrite: true);
            });
        }
    }
}
