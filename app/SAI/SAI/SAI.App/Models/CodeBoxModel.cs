using System;

namespace SAI.SAI.App.Models
{
    public class CodeBoxModel
    {
        private static CodeBoxModel instance;
        public static CodeBoxModel Instance
        {
            get
            {
                if (instance == null)
                    instance = new CodeBoxModel();
                return instance;
            }
        }

        public string Code { get; set; }

        private CodeBoxModel()
        {
            Code = string.Empty;
        }
    }
} 