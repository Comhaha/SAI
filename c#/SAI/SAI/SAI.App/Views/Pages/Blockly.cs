using CefSharp;
using CefSharp.WinForms;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAI.SAI.App.Views.Pages
{
	public partial class Blockly : UserControl, IBlocklyView
	{
		private BlocklyPresenter presenter;
		public event EventHandler<BlockEventArgs> AddBlockButtonClicked;
		public Blockly()
		{
			InitializeComponent();

			// 프레젠터 생성
			presenter = new BlocklyPresenter(this);

			// 백그라운드 색깔&이미지
			BackColor = Color.Transparent;
			BackgroundImage = Properties.Resources.img_background;
			Size = new Size(1280, 720);

			// JSBridge를 Presenter와 연결하여 메시지 전달
			var bridge = new JsBridge(message =>
			{
				presenter.HandleJsMessage(message);
			});

			// JS 객체 등록
			chromiumWebBrowser1.JavascriptObjectRepository.Settings.LegacyBindingEnabled = true;
			chromiumWebBrowser1.JavascriptObjectRepository.Register("cefCustom", bridge, isAsync: false, options: BindingOptions.DefaultBinder);

			// 웹뷰랑 연결
			string localPath = "C:\\S12P31D201\\c#\\SAI\\SAI\\Blockly\\index.html";
			chromiumWebBrowser1.Load(new Uri(localPath).AbsoluteUri);

			// btnPip 클릭시 presenter에게 이벤트 발생했다고 호출
			// 버튼클릭이벤트(Blockly에서 이벤트 발생, 전달값 BlockType(string))
			btnPip.Click += (s, e) => AddBlockButtonClicked?.Invoke(this, new BlockEventArgs("pipInstall"));
			btnHello.Click += (s, e) => AddBlockButtonClicked?.Invoke(this, new BlockEventArgs("hello"));
		}


		// Presenter가 호출할 메서드(UI에 있는 웹뷰에 명령을 내리는 UI 행위) : 블록 생성
		public void addBlock(string blockType)
		{
			chromiumWebBrowser1.ExecuteScriptAsync($"addBlock('{blockType}')");
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
	}
}
