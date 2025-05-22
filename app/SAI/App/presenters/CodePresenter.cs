//using SAI.SAI.App.Models;
//using SAI.SAI.App.Views.Interfaces;
//using SAI.SAI.App.Views.Pages;
//using System;

//namespace SAI.SAI.App.Presenters
//{
//    public class CodePresenter
//    {
//        private readonly ICodeView view;
//        private readonly BlocklyModel model;

//        public CodePresenter(ICodeView view)
//        {
//            this.view = view ?? throw new ArgumentNullException(nameof(view));
//            this.model = BlocklyModel.Instance;
//            Console.WriteLine("[DEBUG] CodePresenter: 생성자 호출됨");

//            // 추가: BlockCodeChanged 이벤트에 핸들러 추가
//            this.model.BlockCodeChanged += OnBlockCodeChanged;
//        }

//        // 추가: 블록 코드 변경시 호출되는 이벤트 핸들러
//        private void OnBlockCodeChanged(string newCode)
//        {
//            try
//            {
//                Console.WriteLine($"[DEBUG] CodePresenter: OnBlockCodeChanged 호출됨 ({newCode?.Length ?? 0}자)");

//                if (string.IsNullOrEmpty(newCode))
//                {
//                    Console.WriteLine("[DEBUG] CodePresenter: 빈 코드 무시");
//                    return;
//                }

//                // 코드 업데이트
//                Console.WriteLine("[DEBUG] CodePresenter: 코드 업데이트 시도");
//                UpdateCode(newCode);
//                Console.WriteLine("[DEBUG] CodePresenter: 코드 업데이트 완료");

//                // UcCode 타입 확인 및 하이라이트 적용
//                if (view is UcCode ucCode)
//                {
//                    Console.WriteLine("[DEBUG] CodePresenter: UcCode 타입 확인됨, 하이라이트 시도");

//                    // 하이라이트 색상 및 스타일 직접 수정 시도
//                    try
//                    {
//                        // 주석을 찾아 하이라이트 시도
//                        Console.WriteLine("[DEBUG] CodePresenter: 주석 찾기 시작");
//                        FindAndHighlightComment(ucCode, newCode);
//                        Console.WriteLine("[DEBUG] CodePresenter: 주석 찾기 및 하이라이트 완료");
//                    }
//                    catch (Exception ex)
//                    {
//                        Console.WriteLine($"[ERROR] CodePresenter: 주석 찾기 오류 - {ex.Message}");
//                        Console.WriteLine($"[ERROR] 스택 트레이스: {ex.StackTrace}");
//                        // 실패시 첫 번째 라인 하이라이트
//                        ucCode.HighlightLine(0);
//                    }
//                }
//                else
//                {
//                    Console.WriteLine($"[WARNING] CodePresenter: view는 UcCode 타입이 아님 - {view?.GetType().FullName ?? "null"}");
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"[ERROR] CodePresenter: OnBlockCodeChanged 처리 중 오류 - {ex.Message}");
//                Console.WriteLine($"[ERROR] 스택 트레이스: {ex.StackTrace}");
//            }
//        }

//        // 주석을 찾아 하이라이트하는 메서드 - 간소화 버전
//        // CodePresenter.cs 클래스에 있는 메서드입니다.
//        private void FindAndHighlightComment(UcCode ucCode, string code)
//        {
//            try
//            {
//                Console.WriteLine($"[DEBUG] 주석 찾기 시작: {code?.Length ?? 0}자");

//                if (string.IsNullOrEmpty(code))
//                {
//                    Console.WriteLine("[DEBUG] 코드가 비어있어 하이라이트 중단");
//                    return;
//                }

//                // 주요 블록 주석 패턴
//                string[] possibleKeywords = new string[] {
//                    " 패키지 설치", " 모델 불러오기", " 데이터 불러오기",
//                    " 모델 학습", " 학습 결과", " 이미지 경로", " 추론 실행", " 결과 시각화"
//                };

//                // 코드에서 첫 번째 주석 찾기
//                int commentIndex = code.IndexOf("#");
//                if (commentIndex >= 0)
//                {
//                    // 주석 라인 추출
//                    int lineEndIndex = code.IndexOf('\n', commentIndex);
//                    string commentLine = lineEndIndex > 0
//                        ? code.Substring(commentIndex, lineEndIndex - commentIndex)
//                        : code.Substring(commentIndex);

//                    // 주석에서 키워드 찾기
//                    string foundKeyword = null;
//                    foreach (string keyword in possibleKeywords)
//                    {
//                        if (commentLine.Contains(keyword))
//                        {
//                            foundKeyword = keyword;
//                            break;
//                        }
//                    }

//                    if (foundKeyword != null)
//                    {
//                        Console.WriteLine($"[DEBUG] 키워드 찾음: {foundKeyword}");
//                        ucCode.HighlightBlockByComment(commentLine);
//                    }
//                    else
//                    {
//                        Console.WriteLine("[DEBUG] 키워드를 찾지 못함, 전체 코드 하이라이트");
//                        ucCode.HighlightBlockByComment(null);
//                    }
//                }
//                else
//                {
//                    Console.WriteLine("[DEBUG] 주석을 찾지 못함, 전체 코드 하이라이트");
//                    ucCode.HighlightBlockByComment(null);
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"[ERROR] 주석 찾기 오류: {ex.Message}");
//                try
//                {
//                    ucCode.HighlightLine(0);
//                }
//                catch (Exception innerEx)
//                {
//                    Console.WriteLine($"[ERROR] 대체 하이라이트 적용 중 오류: {innerEx.Message}");
//                }
//            }
//        }
//        public void UpdateCode(string code)
//        {
//            try
//            {
//                Console.WriteLine($"[DEBUG] CodePresenter: UpdateCode 메서드 호출됨 ({code?.Length ?? 0}자)");

//                // view가 null인지 확인
//                if (view == null)
//                {
//                    Console.WriteLine("[ERROR] CodePresenter: view가 null입니다!");
//                    return;
//                }

//                // 코드가 null인지 확인
//                if (code == null)
//                {
//                    Console.WriteLine("[WARNING] CodePresenter: 전달된 코드가 null입니다!");
//                    code = string.Empty;
//                }

//                // ICodeView 인터페이스를 통해 UpdateCode 호출
//                view.UpdateCode(code);
//                Console.WriteLine("[DEBUG] CodePresenter: view.UpdateCode 호출 완료");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"[ERROR] CodePresenter: 코드 업데이트 중 오류 발생 - {ex.Message}");
//                Console.WriteLine($"[ERROR] 스택 트레이스: {ex.StackTrace}");
//            }
//        }

//        // 필요시 BlocklyModel에서 코드 가져오기
//        public void LoadCodeFromModel()
//        {
//            try
//            {
//                if (model != null && !string.IsNullOrEmpty(model.blockAllCode))
//                {
//                    view.UpdateCode(model.blockAllCode);
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"모델에서 코드 로드 오류: {ex.Message}");
//            }
//        }
//    }
//}