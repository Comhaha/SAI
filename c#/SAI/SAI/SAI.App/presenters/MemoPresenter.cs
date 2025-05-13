using System;
using SAI.SAI.App.Models;

namespace SAI.SAI.App.Presenters
{
    public class MemoPresenter
    {
        private MemoModel memoModel;

        public MemoPresenter()
        {
            memoModel = new MemoModel();
        }

        public void SaveMemoText(string text)
        {
            // 텍스트 내용을 모델에 저장
            memoModel.MemoText = text;
            Console.WriteLine($"[MEMO] 저장된 텍스트: {text}");
        }

        public string GetMemoText()
        {
            // 모델에서 텍스트 내용 가져오기
            return memoModel.MemoText;
        }
    }
}