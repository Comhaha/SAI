using SAI.SAI.App.Models;
using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.Application.Service;
using System;

namespace SAI.SAI.App.Presenters
{
    internal class CodeBoxPresenter
    {
        private readonly ICodeBoxView view;
        private readonly CodeBoxService codeBoxService;
        private readonly CodeBoxModel codeBoxModel;

        public CodeBoxPresenter(ICodeBoxView view)
        {
            this.view = view;
            this.codeBoxService = new CodeBoxService();
            this.codeBoxModel = CodeBoxModel.Instance;
        }

        public void UpdateCode(string code)
        {
            view.SetCode(code);
            codeBoxModel.Code = code;
            codeBoxService.SaveCode(code);
        }

        public void ClearCode()
        {
            view.ClearCode();
            codeBoxModel.Code = string.Empty;
        }
    }
} 