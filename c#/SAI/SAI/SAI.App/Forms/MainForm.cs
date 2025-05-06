using System;
using System.Windows.Forms;
using SAI.SAI.App.Forms;
using CefSharp;
using CefSharp.WinForms;
using System.IO;
using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.App.Presenters;
using SAI.SAI.App.Models;


namespace SAI
{
    public partial class MainForm : Form, IMainView
    {
		public event EventHandler<BlockEventArgs> AddBlockButtonClicked;

		private MainPresenter presenter;
		public MainForm()
        {
            InitializeComponent();

			presenter = new MainPresenter(this);

            // blockly html을 웹뷰에 연결
            string localPath = "C:\\S12P31D201\\c#\\SAI\\SAI\\Blockly\\index.html";
			chromiumWebBrowser1.Load(new Uri(localPath).AbsoluteUri);

			// btnPip 클릭시 이벤트 함수 호출(mainForm에서, 전달값 BlockType(string))
			btnPip.Click += (s, e) => AddBlockButtonClicked?.Invoke(this, new BlockEventArgs("pipInstall"));
		}

		// 이건 Presenter가 호출할 메서드(UI에 있는 웹뷰에 명령을 내리는 UI 행위)
		public void InsertBlockToBlockly(string blockType)
		{
			// 웹뷰에 블록 생성하는 함수(addBlock)를 JavaScript로 실행
			chromiumWebBrowser1.ExecuteScriptAsync($"addBlock('{blockType}')");
		}


		private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
