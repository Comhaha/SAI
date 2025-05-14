using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAI.SAI.App.Models;
using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.Application.Dto;
using SAI.SAI.Application.Service;

namespace SAI.SAI.App.Presenters
{
    public class AiFeedbackPresenter : IDisposable
    {
        private readonly IAiFeedbackView _view;
        private readonly AiFeedbackService _service;

        public AiFeedbackPresenter(IAiFeedbackView view, AiFeedbackService service)
        {
            _view = view;
            _service = service;
            _view.SendRequested += OnSendRequested;
        }

        private async void OnSendRequested(object sender, EventArgs e)
        {
            try
            {
                _view.SetBusy(true);

                var dto = new AiFeedbackRequestDto
                {
                    code = _view.CodeText,
                    log = _view.LogText,
                    image = _view.ImagePath
                };

                var result = await _service.SendAsync(dto);
                NotionModel.Instance.RedirectUrl = result.result.redirectUrl;
                _view.ShowSendResult(true, result.result.feedbackId, result.result.feedback);
            }
            catch (Exception ex)
            {
                _view.ShowSendResult(false, "", "전송 실패: " + ex.Message);
            }
            finally
            {
                _view.SetBusy(false);
            }
        }

        public void Dispose()
        {
            _view.SendRequested -= OnSendRequested;
            _service.Dispose();
        }
    }
}
