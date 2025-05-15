using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.SAI.Application.Service
{
    public class PythonService
    {
        // 모드에 따라 구분 (튜토리얼, 실습)
        public enum Mode
        {
            Tutorial,
            Practice
        }

        public void RunPythonScript(
            Mode mode,
            Action<string> onOutput,
            Action<string> onError,
            Action<Exception> onException)
        {
            try
            {
                // baseDir : "C:\Users\SSAFY\Desktop\3rd PJT\S12P31D201\c#\SAI\SAI\bin\Debug"
                var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                Console.WriteLine($"Base Directory: {baseDir}");
                onOutput?.Invoke($"Base Directory: {baseDir}");

                // python 3.9 임베디드 경로
                string pythonExe = Path.GetFullPath(Path.Combine(baseDir, @"..\..\SAI.Application\Python\venv\python.exe"));
                Console.WriteLine($"Python Executable Path: {pythonExe}");
                onOutput?.Invoke($"Python Executable Path: {pythonExe}");

                // 가상환경 경로
                var pythonDir = Path.GetDirectoryName(pythonExe);
                var pythonScriptsDir = Path.Combine(pythonDir, "Scripts");

                // 학습 스크립트 경로 
                string scriptPath = mode == Mode.Tutorial
                    ? Path.GetFullPath(Path.Combine(baseDir, @"..\..\SAI.Application\Python\scripts\tutorial_train_script.py")) // 튜토리얼 모드
                    : Path.GetFullPath(Path.Combine(baseDir, @"..\..\SAI.Application\Python\scripts\practice_train_script.py")); // 실습 모드
                Console.WriteLine($"Script Path: {scriptPath}");
                onOutput?.Invoke($"Script Path: {scriptPath}");

                // 학습 스크립트 없을 때 에러 
                if (!File.Exists(scriptPath))
                {
                    string errorMsg = $"스크립트 파일을 찾을 수 없습니다: {scriptPath}";
                    Console.WriteLine(errorMsg);
                    onError?.Invoke(errorMsg);
                    return;
                }

                // 가상환경 없을 때 에러
                if (!File.Exists(pythonExe))
                {
                    string errorMsg = $"파이썬 실행 파일을 찾을 수 없습니다: {pythonExe}";
                    Console.WriteLine(errorMsg);
                    onError?.Invoke(errorMsg);
                    return;
                }

                // 실행 설정
                var psi = new ProcessStartInfo
                {
                    FileName = pythonExe,
                    Arguments = $"-u \"{scriptPath}\"",
                    WorkingDirectory = Path.GetDirectoryName(scriptPath),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = false,
                    StandardOutputEncoding = Encoding.UTF8,
                    StandardErrorEncoding = Encoding.UTF8
                };

                // 환경 변수 설정
                psi.EnvironmentVariables["PATH"] =
                    pythonDir + ";" +
                    pythonScriptsDir + ";" +
                    Environment.GetEnvironmentVariable("PATH");
                psi.EnvironmentVariables["PYTHONPATH"] = Path.GetDirectoryName(scriptPath);

                var process = new Process { StartInfo = psi, EnableRaisingEvents = true };
                process.OutputDataReceived += (s, e) => { if (!string.IsNullOrEmpty(e.Data)) onOutput?.Invoke(e.Data); };
                process.ErrorDataReceived += (s, e) => { if (!string.IsNullOrEmpty(e.Data)) onError?.Invoke(e.Data); };
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();
            }
            catch (Exception ex)
            {
                onException?.Invoke(ex);
            }
        }
    }
}