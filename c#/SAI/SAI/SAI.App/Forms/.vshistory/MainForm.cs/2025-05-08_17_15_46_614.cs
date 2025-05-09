using System;
using System.Windows.Forms;
using SAI.SAI.App.Forms;
using CefSharp;
using CefSharp.WinForms;
using System.IO;
using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.App.Presenters;
using SAI.SAI.App.Models;
using SAI.SAI.App.Views.Pages;
using System.Linq;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.RegularExpressions;


namespace SAI
{
    public partial class MainForm : Form, IMainView
    {
		private MainPresenter presenter;

		public MainForm()
        {
            InitializeComponent();
			presenter = new MainPresenter(this);

			Size = new Size(1280, 720);
			MaximizeBox = false;
			MaximumSize = new Size(1280, 720);
			MinimumSize = new Size(1280, 720);
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			// 초기 페이지인 Blockly를 불러온다.
			presenter.Initialize();
		}

		private void guna2HtmlLabel1_Click(object sender, EventArgs e)
		{

		}

		// 이건 Presenter가 호출할 메서드(UI에 있는 패널에 있던 페이지를 지우고, 크기를 채우고, 페이지를 넣는다.)
		public void LoadPage(UserControl page)
		{
			guna2Panel1.Controls.Clear();
			page.Dock = DockStyle.Fill;
			guna2Panel1.BackColor = Color.Transparent;
			guna2Panel1.Controls.Add(page);
			guna2Panel1.BringToFront();
		}

private void guna2Button1_Click(object sender, EventArgs e)
{
    logOutput.Visible = true;
    logOutput.Clear();

    Task.Run(() => RunPythonScript());
}

private void RunPythonScript()
{
    var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            log
    string pythonExe = Path.GetFullPath(Path.Combine(baseDir, @"..\..\...\SAI.Application\venv\Scripts\python.exe"));
    string scriptPath = Path.GetFullPath(Path.Combine(baseDir, @"..\..\SAI.Application\python\test_script.py"));

    var psi = new ProcessStartInfo
    {
        FileName = pythonExe,
        Arguments = $"-u \"{scriptPath}\"", // 실시간 로그를 위한 -u
        WorkingDirectory = Path.GetDirectoryName(scriptPath),
        UseShellExecute = false,
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        CreateNoWindow = true
    };

    var process = new Process { StartInfo = psi, EnableRaisingEvents = true };

    process.OutputDataReceived += OnOutputReceived;
    process.ErrorDataReceived += OnErrorReceived;

    try
    {
        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
        process.WaitForExit();
    }
    catch (Exception ex)
    {
        ShowErrorMessage("❌ 파이썬 실행 중 예외 발생:\n" + ex.Message);
    }
}

private void OnOutputReceived(object sender, DataReceivedEventArgs e)
{
    if (!string.IsNullOrEmpty(e.Data))
    {
        AppendLog(e.Data);
    }
}

private void OnErrorReceived(object sender, DataReceivedEventArgs e)
{
    if (!string.IsNullOrEmpty(e.Data))
    {
        AppendLog("[Error] " + e.Data);
    }
}

private void AppendLog(string text)
{
    if (logOutput.InvokeRequired)
    {
        logOutput.Invoke(new MethodInvoker(() => AppendLog(text)));
    }
    else
    {
        logOutput.AppendText(text + Environment.NewLine);
        logOutput.SelectionStart = logOutput.Text.Length;
        logOutput.ScrollToCaret();
    }
}

private void ShowErrorMessage(string message)
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



        private void guna2ProgressBar1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void logOutput_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
