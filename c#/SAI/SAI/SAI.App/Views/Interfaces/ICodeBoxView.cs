using System;

namespace SAI.SAI.App.Views.Interfaces
{
    public interface ICodeBoxView
    {
        void SetCode(string code);
        string GetCode();
        void ClearCode();
    }
} 