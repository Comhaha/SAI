using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAI.Application.Service;
using System.Windows.Forms;

namespace SAI.App.Views.Interfaces
{
    public interface IYoloTutorialView
    {   // view 에서 YoloTutorialRun 버튼 이벤트 발생시
        event EventHandler RunButtonClicked;

        void AppendLog(string text);
        void ClearLog();
        void SetLogVisible(bool visible);
        void ShowErrorMessage(string message);

        void ShowTutorialTrainingChart(string csvPath);
    }
}
