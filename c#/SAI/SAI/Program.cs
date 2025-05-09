using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAI
{
	internal static class Program
	{
		[DllImport("user32.dll")]
		private static extern bool SetProcessDPIAware();

		/// <summary>
		/// 해당 애플리케이션의 주 진입점입니다.
		/// </summary>
		[STAThread]
		static void Main()
		{
			CefSharpSettings.WcfEnabled = true;

			SetProcessDPIAware(); // 👈 DPI 무시 설정
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
}
