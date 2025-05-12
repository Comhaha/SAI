using SAI.SAI.App.Models;
using SAI.SAI.App.Views.Interfaces;
using System;

namespace SAI.SAI.App.Presenters
{
    public class CodePresenter
    {
        private readonly ICodeView view;
        private readonly BlocklyModel model;

        public CodePresenter(ICodeView view)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));
            this.model = BlocklyModel.Instance;
            Console.WriteLine("[DEBUG] CodePresenter: 생성자 호출됨");
        }

        public void UpdateCode(string code)
        {
            try
            {
                Console.WriteLine($"[DEBUG] CodePresenter: UpdateCode 메서드 호출됨 ({code?.Length ?? 0}자)");

                // view가 null인지 확인
                if (view == null)
                {
                    Console.WriteLine("[ERROR] CodePresenter: view가 null입니다!");
                    return;
                }

                // 코드가 null인지 확인
                if (code == null)
                {
                    Console.WriteLine("[WARNING] CodePresenter: 전달된 코드가 null입니다!");
                    code = string.Empty;
                }

                // ICodeView 인터페이스를 통해 UpdateCode 호출
                view.UpdateCode(code);
                Console.WriteLine("[DEBUG] CodePresenter: view.UpdateCode 호출 완료");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] CodePresenter: 코드 업데이트 중 오류 발생 - {ex.Message}");
                Console.WriteLine($"[ERROR] 스택 트레이스: {ex.StackTrace}");
            }
        }

        // 필요시 BlocklyModel에서 코드 가져오기
        public void LoadCodeFromModel()
        {
            try
            {
                if (model != null && !string.IsNullOrEmpty(model.code))
                {
                    view.UpdateCode(model.code);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"모델에서 코드 로드 오류: {ex.Message}");
            }
        }
    }
}