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

		public BlocklyPresenter(IBlocklyView view)
		{
			this.view = view;
			this.blocklyService = new BlocklyService();
			this.blocklyModel = BlocklyModel.Instance;
			this.view.AddBlockButtonClicked += OnAddBlockButtonClicked;
			this.view.AddBlockButtonDoubleClicked += OnAddBlockDoubleClicked;

			// 블록 하나의 코드가 변경되면 실행되는 이벤트
			blocklyModel.BlockCodeChanged += (newCode) =>
			{
				// 혜정언니 여기를 작성하면 돼!

				// 이 코드는 잘 출력되는지 확인용이라서 지워도 돼!
				if(newCode != "")
				{
					MessageBox.Show(newCode);
				}
			};

			// 전체 블록 코드가 변경되면 실행되는 이벤트
			blocklyModel.BlockAllCodeChanged += (newAllCode) =>
			{
				// 혜정언니 여기를 작성하면 돼!
			};
		}

		private void OnAddBlockDoubleClicked(object sender, BlockEventArgs e)
		{
			view.getPythonCodeByType(e.BlockType);
			// blockCode 초기화
			blocklyModel.blockCode = "";
		}
        // 버튼 클릭시 호출되는 이벤트 메소드 -> view에게 전달
        private void OnAddBlockButtonClicked(object sender, BlockEventArgs e)
        {
			// View에게 JS로 블록 추가 명령
			view.addBlock(e.BlockType);
			// blockAllCode 초기화
			blocklyModel.blockAllCode = "";
		}

		// presenter가 view와 service에게 전달해주기 위한 메소드
		public void HandleJsMessage(string code, string type)
		{
			if(type == "blockAllCode")
			{
				blocklyModel.blockAllCode = code;
				blocklyService.SaveCodeToFileInTutorial();

                //--------혜정언니 꺼 develop에 있던 코드 ----------------------------
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
                // -----------------------------------------
			}
			else if(type == "blockCode")
			{
				blocklyModel.blockCode = code;
			}
		}
	}
}

