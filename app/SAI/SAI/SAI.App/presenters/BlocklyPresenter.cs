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


            blocklyModel.BlockCodeChanged += (newCode) =>
            {
                if (string.IsNullOrEmpty(newCode))
                {
                    Console.WriteLine("[DEBUG] 코드가 비어있어 무시됨");
                    return;
                }

                try
                {
                    Console.WriteLine($"[DEBUG] BlockCodeChanged 이벤트 처리 시작");
                    Console.WriteLine($"[MESSAGE] newCode 값: {newCode}");

                    // BlocklyModel에서 전체 코드 가져오기
                    string allCode = BlocklyModel.Instance.blockAllCode;
                    Console.WriteLine($"[DEBUG] blockAllCode 값: {(string.IsNullOrEmpty(allCode) ? "비어있음" : allCode.Length + "자")}");

                    // 전체 코드에 newCode가 없는 경우를 처리하기 위해 업데이트된 코드 가져오기
                    if (!string.IsNullOrEmpty(allCode) && !allCode.Contains(newCode))
                    {
                        // newCode가 전체 코드에 포함되지 않으면 다시 동기화 시도
                        Console.WriteLine("[DEBUG] newCode가 blockAllCode에 없음, 동기화 시도");

                        // JavaScript에서 blockAllCode를 다시 가져오도록 시도
                        // (여기에 전체 코드를 업데이트하는 로직 추가)

                        // 업데이트된 전체 코드 다시 가져오기
                        allCode = BlocklyModel.Instance.blockAllCode;
                        Console.WriteLine($"[DEBUG] 업데이트된 blockAllCode 값: {(string.IsNullOrEmpty(allCode) ? "비어있음" : allCode.Length + "자")}");
                    }

                    // codeView 확인
                    if (codeView != null)
                    {
                        Console.WriteLine($"[DEBUG] codeView 타입: {codeView.GetType().FullName}");

                        if (codeView is UcCode ucCode)
                        {
                            Console.WriteLine($"[DEBUG] UcCode로 캐스팅 성공");

                            // View에 표시된 코드 확인
                            string viewText = ucCode.Text;
                            Console.WriteLine($"[DEBUG] View에 표시된 코드 길이: {viewText?.Length ?? 0}자");

                            if (!string.IsNullOrEmpty(viewText))
                            {
                                // 1. 직접 코드 세그먼트 하이라이트 시도
                                Console.WriteLine("[DEBUG] 코드 세그먼트 하이라이트 시도");
                                ucCode.HighlightCodeSegment(newCode);

                                // 2. 만약 코드 세그먼트가 정확히 매치되지 않으면 텍스트 검색 사용
                                if (!viewText.Contains(newCode))
                                {
                                    Console.WriteLine("[DEBUG] 정확한 매치 실패, 텍스트 검색 시도");
                                    ucCode.FindAndHighlightText(newCode);
                                }

                                // 3. 라인 번호 추출 및 하이라이트
                                int lineIndex = FindLineIndex(viewText, newCode);
                                if (lineIndex >= 0)
                                {
                                    Console.WriteLine($"[DEBUG] 코드가 라인 {lineIndex}에서 발견됨, 라인 하이라이트 시도");
                                    ucCode.HighlightLine(lineIndex);
                                }
                            }
                            else
                            {
                                Console.WriteLine("[WARNING] View 텍스트가 비어 있음");
                            }
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
                        Console.WriteLine("[WARNING] BlocklyPresenter: codeView가 null입니다 - 코드는 유지됨");
                        // MessageBox.Show 제거 - 에러 메시지가 필요 없음
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] BlocklyPresenter: 전체 코드 업데이트 오류 - {ex.Message}");
                    Console.WriteLine($"[ERROR] 스택 트레이스: {ex.StackTrace}");
                }
            };
        }

        public void OnAddBlockDoubleClicked(string code)
        {
            // blockCode 초기화
            blocklyModel.blockCode = "";//테스트~잠깐지울게요(혜정)
            blocklyModel.blockCode = code;
        }

        // 버튼 클릭시 호출되는 이벤트 메소드 -> view에게 전달
        private void OnAddBlockButtonClicked(object sender, BlockEventArgs e)
        {
            // View에게 JS로 블록 추가 명령
            view.addBlock(e.BlockType);
            // blockAllCode 초기화
            blocklyModel.blockAllCode = "";
        }

        // 코드 라인 번호 찾기 도우미 메서드
        private int FindLineIndex(string fullCode, string segment)
        {
            try
            {
                if (string.IsNullOrEmpty(fullCode) || string.IsNullOrEmpty(segment))
                    return -1;

                string[] lines = fullCode.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                // 1. 정확한 매치 시도
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains(segment))
                        return i;
                }

                // 2. 부분 매치 시도 (첫 몇 단어)
                string[] words = segment.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length > 0)
                {
                    string firstFewWords = string.Join(" ", words.Take(Math.Min(3, words.Length)));
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].Contains(firstFewWords))
                            return i;
                    }
                }

                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] 라인 인덱스 찾기 중 오류: {ex.Message}");
                return -1;
            }
        }


        // 혜정 추가 ICodeView 설정 메서드
        private ICodeView codeView;
        public void SetCodeView(ICodeView codeView)
        {
            if (codeView == null)
            {
                Console.WriteLine("[ERROR] BlocklyPresenter: SetCodeView에 null이 전달됨");
                return;
            }

            this.codeView = codeView;
            Console.WriteLine($"[DEBUG] BlocklyPresenter: ICodeView가 설정되었습니다. 타입: {codeView.GetType().FullName}");

            // 기존 코드가 있으면 바로 업데이트
            if (!string.IsNullOrEmpty(blocklyModel.blockAllCode))
            {
                Console.WriteLine("[DEBUG] BlocklyPresenter: 기존 코드로 즉시 업데이트 시도");
                try
                {
                    codeView.UpdateCode(blocklyModel.blockAllCode);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] BlocklyPresenter: 초기 코드 업데이트 실패 - {ex.Message}");
                }
            }
        }

        // presenter가 view와 service에게 전달해주기 위한 메소드
        public void HandleJsMessage(string code, string type)
        {
            if (type == "blockAllCode")
            {
                blocklyModel.blockAllCode = code;
                blocklyService.SaveCodeToFileInTutorial();

                //--------혜정언니 꺼 develop에 있던 코드 ----------------------------
                // codeView 사용
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
                    Console.WriteLine("[WARNING] BlocklyPresenter: codeView가 null입니다 - 코드는 유지됨");
                    // MessageBox.Show 제거
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


