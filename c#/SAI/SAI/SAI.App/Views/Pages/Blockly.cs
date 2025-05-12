using CefSharp;
using CefSharp.WinForms;
using SAI.SAI.App.Forms.Dialogs;
using SAI.SAI.App.Models;
using SAI.SAI.App.Models.Events;
using SAI.SAI.App.Presenters;
using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.App.Views.Pages;
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
		private BlocklyPresenter blocklyPresenter;
		public event EventHandler<BlockEventArgs> AddBlockButtonClicked;
		public event EventHandler<BlockEventArgs> AddBlockButtonDoubleClicked;
		private JsBridge jsBridge;

        // UcCode를 UcTabCodeContainer로 교체
        private UcTabCodeContainer codeContainer;
        // 기본 탭의 코드 에디터 참조 유지
        private UcCode mainCodeEditor;
        private CodePresenter codeBoxPresenter;

        // 블록 클릭 추적을 위한 변수
        private string lastClickedBlockId = null;
        private DateTime lastClickTime = DateTime.MinValue;
        private const int doubleClickTimeMs = 500; // 더블 클릭 간격 - 500ms로 설정
        private int modelTabCount = 0; // 모델 탭 카운터 추가
        private bool isDoubleClickProcessing = false; // 더블클릭 처리 중 플래그 추가

        public Blockly()
        {
			blocklyPresenter = new BlocklyPresenter(this);
			InitializeComponent();

            // 코드박스 초기화 - presenter 초기화 전에 수행
            InitializeCodeContainer();

            // 백그라운드 색깔&이미지
            BackColor = Color.Transparent;
            BackgroundImage = Properties.Resources.img_background;
            Size = new Size(1280, 720);

            // ------ 혜정언니꺼. 이 코드는 없어도 될 거 같아. jsbridge는는 건드리지 말 것. --------------------
            // // JSBridge를 Presenter와 연결하여 메시지 전달 - 블록 클릭 이벤트 처리 추가
            // var bridge = new JsBridge(message =>
            // {
            //     // 블록 클릭/더블클릭 이벤트 처리
            //     if (message != null && message.Contains("blocklyEvent") && message.Contains("blockId"))
            //     {
            //         try
            //         {
            //             if (message.Contains("\"eventType\":\"click\"") ||
            //                 message.Contains("\"eventType\": \"click\""))
            //             {
            //                 int idxStart = message.IndexOf("\"blockId\":") + 11;
            //                 if (idxStart > 10) // 값이 있을 경우
            //                 {
            //                     int idxEnd = message.IndexOf("\"", idxStart);
            //                     if (idxEnd > idxStart)
            //                     {
            //                         string blockId = message.Substring(idxStart, idxEnd - idxStart);
            //                         HandleBlockClick(blockId);
            //                     }
            //                 }
            //             }
            //         }
            //         catch (Exception ex)
            //         {
            //             Console.WriteLine($"[ERROR] Blockly: 블록 클릭 처리 중 오류 - {ex.Message}");
            //         }
            //     }

            //     // 더블클릭 중이 아닌 경우에만 원래 메시지 처리
            //     if (!isDoubleClickProcessing)
            //     {
            //         presenter.HandleJsMessage(message);
            //     }
            // });

            // -----------------------------------------------------------------------

            InitializeWebView2();

			// 버튼클릭이벤트(Blockly에서 이벤트 발생, 전달값 BlockType(string))
			btnStart.Click += (s, e) => AddBlockButtonClicked?.Invoke(this, new BlockEventArgs("start"));
			btnStart.DoubleClick += (s, e) => AddBlockButtonDoubleClicked?.Invoke(this, new BlockEventArgs("start"));
			btnPip.Click += (s, e) => AddBlockButtonClicked?.Invoke(this, new BlockEventArgs("pipInstall"));
			btnPip.DoubleClick += (s, e) => AddBlockButtonDoubleClicked?.Invoke(this, new BlockEventArgs("pipInstall"));
			btnLoadModel.Click += (s, e) => AddBlockButtonClicked?.Invoke(this, new BlockEventArgs("loadModel"));
			btnLoadModel.DoubleClick += (s, e) => AddBlockButtonDoubleClicked?.Invoke(this, new BlockEventArgs("loadModel"));
			btnLoadDataset.Click += (s, e) => AddBlockButtonClicked?.Invoke(this, new BlockEventArgs("loadDataset"));
			btnLoadDataset.DoubleClick += (s, e) => AddBlockButtonDoubleClicked?.Invoke(this, new BlockEventArgs("loadDataset"));
			btnMachineLearning.Click += (s, e) => AddBlockButtonClicked?.Invoke(this, new BlockEventArgs("machineLearning"));
			btnMachineLearning.DoubleClick += (s, e) => AddBlockButtonDoubleClicked?.Invoke(this, new BlockEventArgs("machineLearning"));
			btnResultGraph.Click += (s, e) => AddBlockButtonClicked?.Invoke(this, new BlockEventArgs("resultGraph"));
			btnResultGraph.DoubleClick += (s, e) => AddBlockButtonDoubleClicked?.Invoke(this, new BlockEventArgs("resultGraph"));
			btnImgPath.Click += (s, e) => AddBlockButtonClicked?.Invoke(this, new BlockEventArgs("imgPath"));
			btnImgPath.DoubleClick += (s, e) => AddBlockButtonDoubleClicked?.Invoke(this, new BlockEventArgs("imgPath"));
			btnModelInference.Click += (s, e) => AddBlockButtonClicked?.Invoke(this, new BlockEventArgs("modelInference"));
			btnModelInference.DoubleClick += (s, e) => AddBlockButtonDoubleClicked?.Invoke(this, new BlockEventArgs("modelInference"));
			btnVisualizeResult.Click += (s, e) => AddBlockButtonClicked?.Invoke(this, new BlockEventArgs("visualizeResult"));
			btnVisualizeResult.DoubleClick += (s, e) => AddBlockButtonDoubleClicked?.Invoke(this, new BlockEventArgs("visualizeResult"));
		}

		private async void InitializeWebView2()
		{
			jsBridge = new JsBridge((message, type) =>
			{
				blocklyPresenter.HandleJsMessage(message, type);
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

			webView21.ZoomFactor = 0.7; // 줌 비율 설정

			await webView21.EnsureCoreWebView2Async();
			webView21.Source = new Uri(uri);
            webView21.BringToFront();
		}

		// Presenter가 호출할 메서드(UI에 있는 웹뷰에 명령을 내리는 UI 행위) : 블록 생성
		public void addBlock(string blockType)
		{
			webView21.ExecuteScriptAsync($"addBlock('{blockType}')");
		}

		// 개별 블록 코드를 받아오기위한 JS 코드 호출 함수
		public void getPythonCodeByType(string blockType)
		{
			webView21.ExecuteScriptAsync($"getPythonCodeByType('{blockType}')");
		}

        // ---------- 혜정언니꺼 develop에 있던 코드-------------------------------
        // Blockly에 블록 클릭 리스너 추가
        private void AddBlockClickListener()
        {
            try
            {
                string script = @"
                    try {
                        console.log('Adding block click listener...');
                        
                        // 워크스페이스 클릭 리스너
                        if (typeof Blockly !== 'undefined' && Blockly.getMainWorkspace()) {
                            // 클릭 이벤트 함수 정의
                            function handleBlockClick(e) {
                                const workspace = Blockly.getMainWorkspace();
                                const clickedBlock = workspace.getBlockById(e.blockId);
                                
                                if (clickedBlock) {
                                    // 블록 ID 전송
                                    cefCustom.postMessage(JSON.stringify({
                                        blocklyEvent: true,
                                        eventType: 'click',
                                        blockId: e.blockId
                                    }));
                                }
                            }
                            
                            // 워크스페이스 클릭 리스너 추가
                            Blockly.getMainWorkspace().addChangeListener(function(event) {
                                if (event.type === 'click' && event.blockId) {
                                    handleBlockClick(event);
                                }
                            });
                            
                            console.log('Block click listeners added');
                        }
                    } catch(err) {
                        console.error('Error in block click listener: ' + err);
                    }";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Blockly: 블록 클릭 리스너 추가 오류 - {ex.Message}");
            }
        }

        // 블록 클릭 처리 - 더블 클릭 감지 및 처리 (개선)
        private void HandleBlockClick(string blockId)
        {
            if (string.IsNullOrEmpty(blockId))
                return;

            DateTime now = DateTime.Now;

            // 로그 기록
            Console.WriteLine($"[DEBUG] Block clicked: {blockId}, Last: {lastClickedBlockId}, Time diff: {(now - lastClickTime).TotalMilliseconds}ms");

            // 같은 블록이 일정 시간 내에 다시 클릭되면 더블 클릭으로 처리
            if (blockId == lastClickedBlockId &&
                (now - lastClickTime).TotalMilliseconds < doubleClickTimeMs)
            {
                Console.WriteLine($"[DEBUG] Double click detected on block: {blockId}");

                // 더블클릭 처리 중 플래그 설정
                isDoubleClickProcessing = true;

                // UI 스레드에서 실행
                this.BeginInvoke(new Action(() => {
                    try
                    {
                        // 새 탭 생성 (테스트용 이름)
                        CreateTestTab();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[ERROR] 탭 생성 오류: {ex.Message}");
                    }
                    finally
                    {
                        // 처리 완료 후 플래그 해제 (약간의 딜레이 추가)
                        System.Threading.Timer timer = null;
                        timer = new System.Threading.Timer((obj) => {
                            isDoubleClickProcessing = false;
                            timer.Dispose();
                        }, null, 500, System.Threading.Timeout.Infinite);
                    }
                }));

                // 클릭 상태 초기화
                lastClickedBlockId = null;
                lastClickTime = DateTime.MinValue;
            }
            else
            {
                // 첫 번째 클릭 저장
                lastClickedBlockId = blockId;
                lastClickTime = now;
                isDoubleClickProcessing = false;
            }
        }

        // 테스트용 탭 생성 메서드
        private void CreateTestTab()
        {
            try
            {
                Console.WriteLine("[DEBUG] Blockly: 테스트 탭 생성 시작");

                // 카운터 증가
                modelTabCount++;

                // 현재 전체 코드 가져오기
                string mainCode = string.Empty;
                UcCode mainEditor = codeContainer.GetMainCodeEditor();
                if (mainEditor != null)
                {
                    mainCode = mainEditor.Text;
                    Console.WriteLine($"[DEBUG] 현재 코드 길이: {mainCode?.Length ?? 0}");
                }

                // 새 탭 이름 설정 (테스트용)
                string tabName = $"테스트 {modelTabCount}";

                // 새 탭 추가
                UcCode newEditor = AddCodeTab(tabName);
                if (newEditor != null)
                {
                    // 새 탭에 코드 복사
                    if (!string.IsNullOrEmpty(mainCode))
                    {
                        newEditor.Text = mainCode;
                    }

                    Console.WriteLine($"[DEBUG] {tabName} 탭 생성 완료");
                }
                else
                {
                    Console.WriteLine("[ERROR] 새 탭 생성 실패");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] 테스트 탭 생성 오류: {ex.Message}");
            }
        }

        // 코드 컨테이너 초기화 메서드 (UcTabCodeContainer 사용)
        private void InitializeCodeContainer()
        {
            try
            {
                Console.WriteLine("[DEBUG] Blockly: InitializeCodeContainer 시작");

                // 탭 코드 컨테이너 초기화 및 추가
                codeContainer = new UcTabCodeContainer();
                codeContainer.Dock = DockStyle.Right;
                codeContainer.Width = 325; // richTextBox1과 비슷한 너비
                Console.WriteLine("[DEBUG] Blockly: UcTabCodeContainer 인스턴스 생성됨");

                // 기본 탭("전체 코드")의 코드 에디터 참조 저장
                mainCodeEditor = codeContainer.GetMainCodeEditor();

                // richTextBox1 너비 조정 (숨기기)
                richTextBox1.Width = 0;

                // UcTabCodeContainer 컨트롤 추가
                this.Controls.Add(codeContainer);
                codeContainer.BringToFront(); // 컨트롤을 앞으로 가져오기
                Console.WriteLine("[DEBUG] Blockly: UcTabCodeContainer가 Controls에 추가됨");

                // CodePresenter 초기화 - mainCodeEditor 사용
                codeBoxPresenter = new CodePresenter(mainCodeEditor);
                Console.WriteLine("[DEBUG] Blockly: CodePresenter 인스턴스 생성됨");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Blockly: 코드 컨테이너 초기화 오류 - {ex.Message}");
                Console.WriteLine($"[ERROR] 스택 트레이스: {ex.StackTrace}");
            }
        }

        // 새 코드 탭 추가 (사용자가 호출하는 메서드)
        public UcCode AddCodeTab(string title)
        {
            try
            {
                if (InvokeRequired)
                {
                    return (UcCode)Invoke(new Func<UcCode>(() => AddCodeTab(title)));
                }

                if (codeContainer != null)
                {
                    UcCode newEditor = codeContainer.AddCodeTab(title);
                    Console.WriteLine($"[DEBUG] Blockly: 새 코드 탭 '{title}' 추가됨");
                    return newEditor;
                }
                else
                {
                    Console.WriteLine("[ERROR] codeContainer가 null입니다");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Blockly: 새 코드 탭 추가 오류 - {ex.Message}");
            }

            return null;
        }
        // ---------------------------------------------------------

        // 다이얼로그 확인용 함수(delete)
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

		// blockly 웹뷰 확대 조절 함수
		private void webView21_ZoomFactorChanged(object sender, EventArgs e)
		{
			webView21.ZoomFactor = 0.7;
        }
	}
}
