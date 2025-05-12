using CefSharp;
using CefSharp.WinForms;
using SAI.SAI.App.Forms.Dialogs;
using SAI.SAI.App.Models;
using SAI.SAI.App.Models.Events;
using SAI.SAI.App.Presenters;
using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.Application.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;

namespace SAI.SAI.App.Views.Pages
{
	public partial class Blockly : UserControl, IBlocklyView
	{
		private BlocklyPresenter presenter;
		public event EventHandler<BlockEventArgs> AddBlockButtonClicked;
		private JsBridge jsBridge;

		public Blockly()
		{
			InitializeComponent();

			// 프레젠터 생성
			presenter = new BlocklyPresenter(this);

			// 백그라운드 색깔&이미지
			BackColor = Color.Transparent;
			BackgroundImage = Properties.Resources.img_background;
			Size = new Size(1280, 720);

			InitializeWebView2();

			// btnPip 클릭시 presenter에게 이벤트 발생했다고 호출
			// 버튼클릭이벤트(Blockly에서 이벤트 발생, 전달값 BlockType(string))
			btnStart.Click += (s, e) => AddBlockButtonClicked?.Invoke(this, new BlockEventArgs("start"));
			btnPip.Click += (s, e) => AddBlockButtonClicked?.Invoke(this, new BlockEventArgs("pipInstall"));
			btnLoadModel.Click += (s, e) => AddBlockButtonClicked?.Invoke(this, new BlockEventArgs("loadModel"));
			btnLoadDataset.Click += (s, e) => AddBlockButtonClicked?.Invoke(this, new BlockEventArgs("loadDataset"));
			btnMachineLearning.Click += (s, e) => AddBlockButtonClicked?.Invoke(this, new BlockEventArgs("machineLearning"));
			btnResultGraph.Click += (s, e) => AddBlockButtonClicked?.Invoke(this, new BlockEventArgs("resultGraph"));
			btnImgPath.Click += (s, e) => AddBlockButtonClicked?.Invoke(this, new BlockEventArgs("imgPath"));
			btnModelInference.Click += (s, e) => AddBlockButtonClicked?.Invoke(this, new BlockEventArgs("modelInference"));
			btnVisualizeResult.Click += (s, e) => AddBlockButtonClicked?.Invoke(this, new BlockEventArgs("visualizeResult"));
		}

		private async void InitializeWebView2()
		{
			jsBridge = new JsBridge(message =>
			{
				presenter.HandleJsMessage(message);
			});

			var baseDir = AppDomain.CurrentDomain.BaseDirectory;
			string localPath = Path.GetFullPath(Path.Combine(baseDir, @"..\\..\\Blockly\\TutorialBlockly.html"));
			string uri = new Uri(localPath).AbsoluteUri;

			webView21.WebMessageReceived += async (s, e) =>
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

										await webView21.ExecuteScriptAsync(
											$"window.dispatchEvent(new MessageEvent('message', {{ data: {json} }}));"
										);
									}
								}
								break;

							case "blockCode":
								string code = root.GetProperty("code").GetString();
								jsBridge.receiveFromJs(code);
								break;
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show($"WebView2 메시지 처리 오류: {ex.Message}");
				}
			};


			webView21.ZoomFactor = 0.9; // 줌 비율 설정

			await webView21.EnsureCoreWebView2Async();
			webView21.Source = new Uri(uri);
		}

		// Presenter가 호출할 메서드(UI에 있는 웹뷰에 명령을 내리는 UI 행위) : 블록 생성
		public void addBlock(string blockType)
		{
			webView21.ExecuteScriptAsync($"addBlock('{blockType}')");
		}

		// 코드를 richText에 출력
		public void ShowGeneratedCode(string code)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new Action(() => ShowGeneratedCode(code)));
				return;
			}

			richTextBox1.Text = code;
		}

		private void btnDialog_Click(object sender, EventArgs e)
		{

			//using (var dialog = new DialogQuitTrain())
			//using (var dialog = new DialogHomeFromTrain())
			//using (var dialog = new DialogComfirmTrain())
			//using (var dialog = new DialogHomeFromLabeling())
			//using (var dialog = new DialogCompleteTutorial())
			//using (var dialog = new DialogCompleteLabeling())
			//using (var dialog = new DialogConfirmExit())
			//{
			//	dialog.ShowDialog();
			//}

			var model = MainModel.Instance;
			using (var dialog = new DialogDeleteModel())
			{
				if (!model.DontShowDeleteModelDialog)
				{
					dialog.ShowDialog();
				}
			}
		}

		private void webView21_ZoomFactorChanged(object sender, EventArgs e)
		{
			webView21.ZoomFactor = 0.9; // 줌 비율 설정
		}
	}
}
