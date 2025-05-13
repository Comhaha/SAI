namespace SAI.SAI.App.Models
{
    public class MemoModel
    {
        private string memoText;

        public string MemoText
        {
            get { return memoText; }
            set { memoText = value; }
        }

        public MemoModel()
        {
            memoText = string.Empty;
        }
    }
}