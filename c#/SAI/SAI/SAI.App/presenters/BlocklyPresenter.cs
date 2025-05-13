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
                if (string.IsNullOrEmpty(newCode))
                    return;

                try
                {
                    Console.WriteLine($"[DEBUG] BlocklyPresenter: BlockCodeChanged 이벤트 발생 ({newCode?.Length ?? 0}자)");

                    if (codeBoxPresenter != null)
                    {
                        Console.WriteLine("[DEBUG] BlocklyPresenter: CodePresenter를 통해 코드 업데이트 시도");
                        // CodePresenter를 통해 코드 업데이트
                        codeBoxPresenter.UpdateCode(newCode);
                        Console.WriteLine("[DEBUG] BlocklyPresenter: CodePresenter 업데이트 완료");
                    }
                    else
                    {
                        Console.WriteLine("[DEBUG] BlocklyPresenter: CodePresenter가 null, UcCode 직접 업데이트 시도");
                        // CodePresenter가 없는 경우 직접 UcCode 업데이트
                        var ucCode = ((Control)view).Controls.OfType<UcCode>().FirstOrDefault();
                        if (ucCode != null)
                        {
                            Console.WriteLine("[DEBUG] BlocklyPresenter: UcCode 찾음, 코드 업데이트 시도");
                            ucCode.SetAccumulateMode(false);
                            ucCode.UpdateCode(newCode);
                            Console.WriteLine("[DEBUG] BlocklyPresenter: UcCode 업데이트 완료");
                        }
                        else
                        {
                            Console.WriteLine("[WARNING] BlocklyPresenter: UcCode를 찾을 수 없음");
                            MessageBox.Show("코드 에디터를 찾을 수 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] BlocklyPresenter: 코드 업데이트 중 오류 - {ex.Message}");
                    Console.WriteLine($"[ERROR] 스택 트레이스: {ex.StackTrace}");
                    MessageBox.Show($"오류 발생: {ex.Message}", "예외 발생", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };


            // 전체 블록 코드가 변경되면 실행되는 이벤트
            // 혜정언니 여기를 작성하면 돼!
            blocklyModel.BlockAllCodeChanged += (newAllCode) =>
            {
                try
                {
                    Console.WriteLine($"[DEBUG] BlocklyPresenter: 전체 코드가 변경됨 ({newAllCode?.Length ?? 0}자)");

                    // 1. UcTabCodeContainer 먼저 찾기
                    var tabCodeContainer = ((Control)view).Controls.OfType<UcTabCodeContainer>().FirstOrDefault();
                    if (tabCodeContainer != null)
                    {
                        // "전체 코드" 탭과 "모델생성" 탭 모두 업데이트
                        tabCodeContainer.UpdateMainCode(newAllCode);
                        tabCodeContainer.UpdateModelCode(newAllCode);
                        Console.WriteLine("[DEBUG] BlocklyPresenter: UcTabCodeContainer 코드 업데이트 완료");
                        return; // 업데이트 완료했으므로 종료
                    }

                    // 2. UcCode 인스턴스 찾기
                    var ucCode = ((Control)view).Controls.OfType<UcCode>().FirstOrDefault();
                    if (ucCode != null)
                    {
                        // 코드 누적하지 않도록 설정 (매번 새로운 코드로 대체)
                        ucCode.SetAccumulateMode(false);
                        ucCode.UpdateCode(newAllCode);
                        Console.WriteLine("[DEBUG] BlocklyPresenter: UcCode 직접 업데이트 완료");
                        return; // 업데이트 완료했으므로 종료
                    }

                    // 3. CodePresenter 사용 (마지막 대안)
                    if (codeBoxPresenter != null)
                    {
                        codeBoxPresenter.UpdateCode(newAllCode);
                        Console.WriteLine("[DEBUG] BlocklyPresenter: CodePresenter 업데이트 완료");
                    }
                    else
                    {
                        Console.WriteLine("[WARNING] BlocklyPresenter: 코드를 표시할 수 있는 UI 요소가 없습니다!");
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
        // 혜정 추가
        public void SetCodePresenter(CodePresenter presenter)
        {
            this.codeBoxPresenter = presenter;
            Console.WriteLine("[DEBUG] BlocklyPresenter: CodePresenter가 설정되었습니다.");
        }

        // presenter가 view와 service에게 전달해주기 위한 메소드
        public void HandleJsMessage(string code, string type)
        {
            if (type == "blockAllCode")
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
            else if (type == "blockCode")
            {
                Console.WriteLine($"[DEBUG] 개별 블록 코드 변경 감지: {code?.Length ?? 0}자");//디버깅용 주석 한번 추가해봤음
                blocklyModel.blockCode = code;
            }
        }
    }
}

