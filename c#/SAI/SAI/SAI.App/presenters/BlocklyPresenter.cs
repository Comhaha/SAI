using SAI.SAI.App.Models;
using SAI.SAI.App.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAI.SAI.App.Presenters
{
	internal class BlocklyPresenter
	{
		private readonly IBlocklyView view;

		public BlocklyPresenter(IBlocklyView view)
		{
			this.view = view;
			this.view.AddBlockButtonClicked += OnAddBlockButtonClicked;			
		}

		// 버튼 클릭시 호출되는 이벤트 메소드
		private void OnAddBlockButtonClicked(object sender, BlockEventArgs e)
		{
			// View에게 JS로 블록 추가 명령
			view.addBlock(e.BlockType);
		}

		public void HandleJsMessage(string code)
		{
			// 메시지를 View로 전달해서 RichTextBox에 출력
			view.ShowGeneratedCode(code);
			SavePythonCodeToFile(code);
		}

		// python 코드를 .py 파일로 생성
		public void SavePythonCodeToFile(string code)
		{
			var path = "C:\\S12P31D201\\c#\\SAI\\SAI\\SAI.Application\\python\\tutorial_script.py"; // 또는 SaveFileDialog를 통해 경로 설정
			// 실습은 train_script.py

			try
			{
				File.WriteAllText(path, code, Encoding.UTF8);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"저장 중 오류 발생: {ex.Message}");
			}
		}

	}
}
