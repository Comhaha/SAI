using CefSharp;
using CefSharp.WinForms;
using SAI.SAI.App.Models;
using SAI.SAI.App.Views.Interfaces;
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
		public event EventHandler<BlockEventArgs> AddBlockButtonClicked;
		public Blockly()
		{
			InitializeComponent();

			// blockly html을 웹뷰에 연결
			string localPath = "C:\\S12P31D201\\c#\\SAI\\SAI\\Blockly\\index.html";
			chromiumWebBrowser1.Load(new Uri(localPath).AbsoluteUri);

			// btnPip 클릭시 presenter에게 이벤트 발생했다고 호출
			// 버튼클릭이벤트(Blockly에서 이벤트 발생, 전달값 BlockType(string))
			btnPip.Click += (s, e) => AddBlockButtonClicked?.Invoke(this, new BlockEventArgs("pipInstall"));
		}


		// 이건 Presenter가 호출할 메서드(UI에 있는 웹뷰에 명령을 내리는 UI 행위)
		public void addBlock(string blockType)
		{
			chromiumWebBrowser1.ExecuteScriptAsync($"addBlock('{blockType}')");
		}
	}
}
