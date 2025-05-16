using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.SAI.App.Views.Interfaces
{
    internal interface IInferenceView
    {
        void ShowDialogInferenceLoading();
        void ShowInferResultImage(System.Drawing.Image resultImage);
    }
}
