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


        public BlocklyPresenter(IBlocklyView view)
        {
            this.view = view;
            this.blocklyService = new BlocklyService();
            this.blocklyModel = BlocklyModel.Instance;
            this.view.AddBlockButtonClicked += OnAddBlockButtonClicked;
            this.view.AddBlockButtonDoubleClicked += OnAddBlockDoubleClicked;


            blocklyModel.BlockCodeChanged += (newCode) =>
            {
                Console.WriteLine($"[DEBUG] BlockCodeChanged 이벤트 발생: 코드 길이 = {newCode?.Length ?? 0}자");
                if (string.IsNullOrEmpty(newCode))
                {
                    Console.WriteLine("[DEBUG] 코드가 비어있어 무시됨");
                    return;
                }

                try
                {
                    Console.WriteLine($"[DEBUG] BlockCodeChanged 이벤트 처리 시작");

                    // codeView 확인
                    if (codeView != null)
                    {
                        Console.WriteLine($"[DEBUG] codeView 타입: {codeView.GetType().FullName}");

                        if (codeView is UcCode ucCode)
                        {
                            Console.WriteLine($"[DEBUG] UcCode로 캐스팅 성공, HighlightCodeSegment 호출 시도");

                            // View에 표시된 코드 확인
                            string viewText = ucCode.Text;
                            Console.WriteLine($"[DEBUG] View에 표시된 코드 길이: {viewText?.Length ?? 0}자");

                            // 코드 하이라이트 시도
                            ucCode.HighlightCodeSegment(newCode);
                            Console.WriteLine($"[DEBUG] HighlightCodeSegment 호출 완료");
                        }
                        else
                        {
                            Console.WriteLine($"[WARNING] codeView를 UcCode로 캐스팅 실패");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"[ERROR] codeView가 null임");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] BlockCodeChanged 이벤트 처리 중 오류: {ex.Message}");
                    Console.WriteLine($"[ERROR] 스택 트레이스: {ex.StackTrace}");
                }
            };

            // 전체 블록 코드가 변경되면 실행되는 이벤트
            // 혜정언니 여기를 작성하면 돼!
            blocklyModel.BlockAllCodeChanged += (newAllCode) =>
            {
                try
                {
                    Console.WriteLine($"[DEBUG] BlocklyPresenter: 전체 코드가 변경됨 ({newAllCode?.Length ?? 0}자)");

                    // 여기도 중요! codeView 사용
                    if (codeView != null)
                    {
                        Console.WriteLine("[DEBUG] BlocklyPresenter: codeView 사용, 코드 업데이트 시도");
                        codeView.UpdateCode(newAllCode);
                        Console.WriteLine("[DEBUG] BlocklyPresenter: codeView 업데이트 완료");
                    }
                    else
                    {
                        Console.WriteLine("[WARNING] BlocklyPresenter: codeView가 null입니다");
                        MessageBox.Show("코드 에디터를 찾을 수 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            Console.WriteLine($"[DEBUG] OnAddBlockDoubleClicked 호출됨: BlockType = {e.BlockType}");

            // 개별 코드 가져오기 전에 로깅 추가
            Console.WriteLine($"[DEBUG] getPythonCodeByType 호출 시작: {e.BlockType}");
            view.getPythonCodeByType(e.BlockType);
            Console.WriteLine($"[DEBUG] getPythonCodeByType 호출 완료");

            // blockCode 초기화
            Console.WriteLine("[DEBUG] blockCode 초기화");
            blocklyModel.blockCode = "";
            Console.WriteLine("[DEBUG] OnAddBlockDoubleClicked 종료");
        }
        // 버튼 클릭시 호출되는 이벤트 메소드 -> view에게 전달
        private void OnAddBlockButtonClicked(object sender, BlockEventArgs e)
        {
            // View에게 JS로 블록 추가 명령
            view.addBlock(e.BlockType);
            // blockAllCode 초기화
            blocklyModel.blockAllCode = "";
        }

        // 혜정 추가 ICodeView 설정 메서드
        private ICodeView codeView;
        public void SetCodeView(ICodeView codeView)
        {
            this.codeView = codeView;
            Console.WriteLine("[DEBUG] BlocklyPresenter: ICodeView가 설정되었습니다.");
        }

        // presenter가 view와 service에게 전달해주기 위한 메소드
        public void HandleJsMessage(string code, string type)
        {
            if (type == "blockAllCode")
            {
                blocklyModel.blockAllCode = code;
                blocklyService.SaveCodeToFileInTutorial();

                //--------혜정언니 꺼 develop에 있던 코드 ----------------------------
                // 여기도 codeView 사용
                if (codeView != null)
                {
                    try
                    {
                        Console.WriteLine($"[DEBUG] BlocklyPresenter: codeView로 전체 코드 전달 시도 ({code?.Length ?? 0}자)");
                        if (codeView is UcCode ucCode)
                        {
                            ucCode.SetAccumulateMode(false);
                        }
                        codeView.UpdateCode(code);
                        Console.WriteLine("[DEBUG] BlocklyPresenter: codeView로 코드 업데이트 완료");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[ERROR] BlocklyPresenter: codeView 업데이트 오류 - {ex.Message}");
                        Console.WriteLine($"[ERROR] 스택 트레이스: {ex.StackTrace}");
                    }
                }
                else
                {
                    Console.WriteLine("[WARNING] BlocklyPresenter: codeView가 null입니다");
                    MessageBox.Show("코드 에디터를 찾을 수 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (type == "blockCode")
            {
                Console.WriteLine($"[DEBUG] 개별 블록 코드 변경 감지: {code?.Length ?? 0}자");
                blocklyModel.blockCode = code;
            }
        }
    }
}

