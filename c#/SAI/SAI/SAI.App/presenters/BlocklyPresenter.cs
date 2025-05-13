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
                if (newCode != "")
                {
                    MessageBox.Show(newCode);
                }
            };

            // 전체 블록 코드가 변경되면 실행되는 이벤트
            // 혜정언니 여기를 작성하면 돼!
            blocklyModel.BlockAllCodeChanged += (newAllCode) =>
            {
                try
                {
                    Console.WriteLine($"[DEBUG] BlocklyPresenter: 전체 코드가 변경됨 ({newAllCode?.Length ?? 0}자)");

                    // CodePresenter가 있는 경우 코드 업데이트
                    if (codeBoxPresenter != null)
                    {
                        // UcTabCodeContainer에 코드 업데이트
                        var tabCodeContainer = ((Control)view).Controls.OfType<UcTabCodeContainer>().FirstOrDefault();
                        if (tabCodeContainer != null)
                        {
                            // "전체 코드" 탭과 "모델생성" 탭 모두 업데이트
                            tabCodeContainer.UpdateMainCode(newAllCode);
                            tabCodeContainer.UpdateModelCode(newAllCode);
                            Console.WriteLine("[DEBUG] BlocklyPresenter: UcTabCodeContainer 코드 업데이트 완료");
                        }
                        else
                        {
                            // UcCode 인스턴스 직접 찾기 시도
                            var ucCode = ((Control)view).Controls.OfType<UcCode>().FirstOrDefault();
                            if (ucCode != null)
                            {
                                // 코드 누적하지 않도록 설정 (매번 새로운 코드로 대체)
                                ucCode.SetAccumulateMode(false);
                                ucCode.UpdateCode(newAllCode);
                                Console.WriteLine("[DEBUG] BlocklyPresenter: UcCode 직접 업데이트 완료");
                            }
                            else
                            {
                                // 기존 방식으로 폴백
                                codeBoxPresenter.UpdateCode(newAllCode);
                                Console.WriteLine("[DEBUG] BlocklyPresenter: CodePresenter 업데이트 완료 (폴백)");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("[WARNING] BlocklyPresenter: codeBoxPresenter가 null입니다!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] BlocklyPresenter: 전체 코드 업데이트 오류 - {ex.Message}");
                    Console.WriteLine($"[ERROR] 스택 트레이스: {ex.StackTrace}");
                }
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

        public void SetCodePresenter(CodePresenter presenter)
        {
            this.codeBoxPresenter = presenter;
            Console.WriteLine("[DEBUG] BlocklyPresenter: CodePresenter가 설정되었습니다.");
        }

        // presenter가 view와 service에게 전달해주기 위한 메소드
        public void HandleJsMessage(string code, string type)
		{
			if(type == "blockAllCode")
			{
				blocklyModel.blockAllCode = code;
				blocklyService.SaveCodeToFileInTutorial();
			}
			else if(type == "blockCode")
			{
				blocklyModel.blockCode = code;
			}
		}
	}
}

