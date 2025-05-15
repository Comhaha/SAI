using SAI.SAI.App.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAI.SAI.Application.Service
{
	public class BlocklyService
	{
		private readonly string baseDir;
		private readonly string tutorialPath;
		private readonly string trainPath;

		public BlocklyService()
		{
			baseDir = AppDomain.CurrentDomain.BaseDirectory;
			tutorialPath = Path.GetFullPath(Path.Combine(baseDir, @"..\\..\SAI.Application\\Python\\scripts\\tutorial_script.py"));
			trainPath = Path.GetFullPath(Path.Combine(baseDir, @"..\\..\SAI.Application\\Python\\scripts\\train_script.py"));
		}

		// 튜토리얼에서 블록을 통해 생성된 코드를 파일에 저장하는 메소드  
		public void SaveCodeToFileInTutorial()
		{
			var model = BlocklyModel.Instance;
			var path = tutorialPath;

			try
			{
				Console.WriteLine($"[DEBUG] BlocklyService: 파일 저장 시작");
				Console.WriteLine($"[DEBUG] 저장 경로: {path}");
				Console.WriteLine($"[DEBUG] 저장할 코드 길이: {model.blockAllCode?.Length ?? 0}자");
				Console.WriteLine($"[DEBUG] 저장할 코드 내용:\n{model.blockAllCode}");
				
				File.WriteAllText(path, model.blockAllCode, Encoding.UTF8);
				Console.WriteLine("[DEBUG] BlocklyService: 파일 저장 완료");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"[ERROR] BlocklyService: 파일 저장 중 오류 발생 - {ex.Message}");
				MessageBox.Show($"코드 저장 중 오류 발생: {ex.Message}");
			}
		}

		// 실습에서 블록을 통해 생성된 코드를 파일에 저장하는 메소드  
		public void SaveCodeToFileInTrain()
		{
			var model = BlocklyModel.Instance;
			var path = trainPath;

			try
			{
				File.WriteAllText(path, model.blockAllCode, Encoding.UTF8);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"코드 저장 중 오류 발생: {ex.Message}");
			}
		}
	}
}
