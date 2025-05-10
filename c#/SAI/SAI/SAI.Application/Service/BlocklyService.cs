using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SAI.SAI.App.Models;

namespace SAI.SAI.Application.Service
{
    public class BlocklyService
    {
        private readonly string tutorialPath = "C:\\S12P31D201\\c#\\SAI\\SAI\\SAI.Application\\python\\tutorial_script.py";
        private readonly string trainPath = "C:\\S12P31D201\\c#\\SAI\\SAI\\SAI.Application\\python\\train_script.py";

        // 튜토리얼에서 블록을 통해 생성된 코드를 파일에 저장하는 메소드
        public void SaveCodeToFileInTutorial()
        {
            var model = BlocklyModel.Instance;
            var path = tutorialPath;

            try
            {
                File.WriteAllText(path, model.code, Encoding.UTF8);
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
                File.WriteAllText(path, model.code, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"코드 저장 중 오류 발생: {ex.Message}");
            }
        }
    }
}
