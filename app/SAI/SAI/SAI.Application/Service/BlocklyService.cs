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
			tutorialPath = Path.Combine(baseDir, "SAI.Application", "Python", "scripts", "tutorial_script.py");
			trainPath = Path.Combine(baseDir, "SAI.Application", "Python", "scripts", "train_script.py");
        }

        // 튜토리얼에서 블록을 통해 생성된 코드를 파일에 저장하는 메소드  
        public void SaveCodeToFileInTutorial()
        {
            var model = BlocklyModel.Instance;
            var path = tutorialPath;

            try
            {
                File.WriteAllText(path, model.blockAllCode, Encoding.UTF8);
            }
            catch (Exception ex)
            {
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