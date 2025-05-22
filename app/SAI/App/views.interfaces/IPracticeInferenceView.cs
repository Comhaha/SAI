using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAI.SAI.Application.Service;

namespace SAI.SAI.App.Views.Interfaces
{
    internal interface IPracticeInferenceView
    {
        void ShowDialogInferenceLoading();

        void ShowPracticeInferResultImage(PythonService.InferenceResult result);
    }
}
