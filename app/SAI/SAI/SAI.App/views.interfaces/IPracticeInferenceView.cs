using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.SAI.App.Views.Interfaces
{
    internal interface IPracticeInferenceView
    {
        void ShowDialogInferenceLoading();

        void ShowPracticeInferResultImage(System.Drawing.Image resultImage);
    }
}
