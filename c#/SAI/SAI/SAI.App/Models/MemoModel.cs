using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
