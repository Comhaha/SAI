using SAI.SAI.App.Models;
using SAI.SAI.App.Models.Events;
using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.Application.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SAI.SAI.App.Views.Pages; // 추가: UcCode 클래스 접근을 위해 추가

namespace SAI.SAI.App.Presenters
{
    internal class BlocklyPresenter
    {
        private readonly IBlocklyView view;
        private BlocklyService blocklyService;
        private BlocklyModel blocklyModel;
        // 유지: CodePresenter 변수 선언
        private CodePresenter codeBoxPresenter;

        // 수정: CodePresenter 매개변수 추가 (오버로드)
        public BlocklyPresenter(IBlocklyView view, CodePresenter codeBoxPresenter = null)
        {
            this.view = view;
            this.blocklyService = new BlocklyService();
            this.blocklyModel = BlocklyModel.Instance;
            this.codeBoxPresenter = codeBoxPresenter; // CodePresenter 저장

            // 디버깅 메시지 추가
            Console.WriteLine($"[DEBUG] BlocklyPresenter: 생성자 호출됨, codeBoxPresenter {(codeBoxPresenter == null ? "null" : "not null")}");

            this.view.AddBlockButtonClicked += OnAddBlockButtonClicked;
        }

        // 버튼 클릭시 호출되는 이벤트 메소드 -> view에게 전달
        private void OnAddBlockButtonClicked(object sender, BlockEventArgs e)
        {
            // View에게 JS로 블록 추가 명령
            view.addBlock(e.BlockType);
        }

        // 수정: AppendCode 메서드를 사용하여 코드 누적 및 하이라이트 적용
        public void HandleJsMessage(string code)
        {
            // 메시지를 View로 전달해서 RichTextBox에 출력
            view.ShowGeneratedCode(code);

            // 추가: CodePresenter가 있으면 코드 업데이트 - UcCode에 코드 표시
            if (codeBoxPresenter != null)
            {
                try
                {
                    Console.WriteLine($"[DEBUG] BlocklyPresenter: CodePresenter로 코드 전달 시도 ({code?.Length ?? 0}자)");

                    // 수정: UcCode 인스턴스를 찾아 AppendCode 메서드 호출 (각 라인별 하이라이트 적용)
                    var ucCode = ((Control)view).Controls.OfType<UcCode>().FirstOrDefault();
                    if (ucCode != null)
                    {
                        // 수정: 코드 누적하지 않도록 설정 (매번 새로운 코드로 대체)
                        ucCode.SetAccumulateMode(false);

                        // AppendCode 메서드 호출 (코드 덮어쓰기 및 라인별 하이라이트)
                        ucCode.AppendCode(code);
                        Console.WriteLine("[DEBUG] BlocklyPresenter: AppendCode 호출 완료");
                    }
                    else
                    {
                        // UcCode를 찾을 수 없는 경우 기존 방식으로 폴백
                        codeBoxPresenter.UpdateCode(code);
                        Console.WriteLine("[DEBUG] BlocklyPresenter: UpdateCode 호출 완료 (폴백)");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] BlocklyPresenter: CodePresenter 업데이트 오류 - {ex.Message}");
                    Console.WriteLine($"[ERROR] 스택 트레이스: {ex.StackTrace}");
                }
            }
            else
            {
                Console.WriteLine("[WARNING] BlocklyPresenter: codeBoxPresenter가 null입니다!");
            }

            blocklyModel.code = code;
            blocklyService.SaveCodeToFileInTutorial();
        }
    }
}