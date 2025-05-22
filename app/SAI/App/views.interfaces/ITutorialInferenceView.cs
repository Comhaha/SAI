using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAI.Application.Service;

namespace SAI.App.Views.Interfaces
{
    internal interface ITutorialInferenceView
    {
        void ShowDialogInferenceLoading();
       // void ShowTutorialInferResultImage(System.Drawing.Image resultImage);
        void ShowInferenceResult(PythonService.InferenceResult result);

    }
}
