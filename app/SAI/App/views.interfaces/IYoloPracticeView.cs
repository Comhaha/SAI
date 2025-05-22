using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.App.Views.Interfaces
{
    public interface IYoloPracticeView
    {
        event EventHandler RunButtonClicked;
        void AppendLog(string text);
        void ClearLog();
        void SetLogVisible(bool visible);
        void ShowErrorMessage(string message);

        void ShowTrainingChart(string csvPath);
    }
}
