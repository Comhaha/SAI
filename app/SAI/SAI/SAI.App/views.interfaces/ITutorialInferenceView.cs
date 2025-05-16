using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.SAI.App.Views.Interfaces
{
    internal interface ITutorialInferenceView
    {
        void ShowDialogInferenceLoading();
        void ShowTutorialInferResultImage(System.Drawing.Image resultImage);

    }
}
