using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.SAI.App.Views.Interfaces
{
    public interface IAiFeedbackView : IAiBuzyAwareView
    {
        string CodeText { get; }
        string LogImagePath { get; }
        string ResultImagePath { get; }
        string memo { get; }

        event EventHandler SendRequested;

        void ShowSendResult(bool isSuccess, string feedbackId, string feedback);
    }
}
