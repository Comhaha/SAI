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
                var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                Console.WriteLine($"Base Directory: {baseDir}");
                onOutput?.Invoke($"Base Directory: {baseDir}");

                string pythonExe = Path.GetFullPath(Path.Combine(baseDir, @"..\..\SAI.Application\Python\venv\Scripts\python.exe"));
                Console.WriteLine($"Python Executable Path: {pythonExe}");
                onOutput?.Invoke($"Python Executable Path: {pythonExe}");

                string scriptPath = mode == Mode.Tutorial
                    ? Path.GetFullPath(Path.Combine(baseDir, @"..\..\SAI.Application\Python\scripts\test_script.py"))
                    : Path.GetFullPath(Path.Combine(baseDir, @"..\..\SAI.Application\Python\scripts\yolo_practice.py"));
                Console.WriteLine($"Script Path: {scriptPath}");
                onOutput?.Invoke($"Script Path: {scriptPath}");

                if (!File.Exists(scriptPath))
                {
                    string errorMsg = $"스크립트 파일을 찾을 수 없습니다: {scriptPath}";
                    Console.WriteLine(errorMsg);
                    onError?.Invoke(errorMsg);
                    return;
                }

                if (!File.Exists(pythonExe))
                {
                    string errorMsg = $"파이썬 실행 파일을 찾을 수 없습니다: {pythonExe}";
                    Console.WriteLine(errorMsg);
                    onError?.Invoke(errorMsg);
                    return;
                }

                var psi = new ProcessStartInfo
                {
                    FileName = pythonExe,
                    Arguments = $"-u \"{scriptPath}\"",
                    WorkingDirectory = Path.GetDirectoryName(scriptPath),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = false
                };

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
