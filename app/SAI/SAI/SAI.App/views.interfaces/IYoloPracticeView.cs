using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.SAI.App.Views.Interfaces
{
    public interface IYoloPracticeView
    {
        event EventHandler RunButtonClicked;
        void AppendLog(string text);
        void ClearLog();
        void SetLogVisible(bool visible);
        void ShowErrorMessage(string message);
        void ShowPracticeInferResultImage(System.Drawing.Image resultImage);
        // 필요시 추가 메서드
    }
}
