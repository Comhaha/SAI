using System;
using System.IO;

namespace SAI.Application.Service
{
    public class CodeBoxService
    {
        private readonly string codeDirectory;

        public CodeBoxService()
        {
            codeDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CodeFiles");
            if (!Directory.Exists(codeDirectory))
            {
                Directory.CreateDirectory(codeDirectory);
            }
        }

        public void SaveCode(string code)
        {
            string filePath = Path.Combine(codeDirectory, "current_code.cs");
            File.WriteAllText(filePath, code);
        }

        public string LoadCode()
        {
            string filePath = Path.Combine(codeDirectory, "current_code.cs");
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            return string.Empty;
        }
    }
} 