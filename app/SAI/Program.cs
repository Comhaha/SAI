using CefSharp;
using System;
using SAI.App.Forms;

namespace SAI
{
	internal static class Program
	{
		/// <summary>
		/// 해당 애플리케이션의 주 진입점입니다.
		/// </summary>
		[STAThread]
		static void Main()
		{
			
			CefSharpSettings.WcfEnabled = true;

            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            // 스플래시 화면 먼저 실행
            using (var splash = new SplashForm())
            {
                splash.ShowDialog();
            }

            System.Windows.Forms.Application.Run(new MainForm());
		}
	}
}
