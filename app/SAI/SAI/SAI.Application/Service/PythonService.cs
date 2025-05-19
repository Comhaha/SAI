using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using SAI.SAI.App.Models;

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

        public Process RunPythonScript(
            Mode mode,
            Action<string> onOutput,
            Action<string> onError,
            Action<Exception> onException,
            string imagePath = null,
            int epochs = 0,
            int imgsz = 0,
            BlocklyModel blocklyModel = null)
        {
            Process process = null;
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
                string runnerPath, trainScriptPath;
                string trainScriptArgName = "--train-script"; // 인자명 통일

                if (mode == Mode.Tutorial) // 튜토리얼 모드
                {
                    runnerPath = Path.GetFullPath(Path.Combine(baseDir, @"..\..\SAI.Application\Python\scripts\tutorial_runner.py"));
                    trainScriptPath = Path.GetFullPath(Path.Combine(baseDir, @"..\..\SAI.Application\Python\scripts\tutorial_train_script.py"));
                }
                else if (mode == Mode.Practice) // 실습 모드
                {
                    runnerPath = Path.GetFullPath(Path.Combine(baseDir, @"..\..\SAI.Application\Python\scripts\practice_runner.py"));
                    trainScriptPath = Path.GetFullPath(Path.Combine(baseDir, @"..\..\SAI.Application\Python\scripts\practice_train_script.py"));
                }
                else
                {
                    throw new ArgumentException("지원하지 않는 모드입니다.");
                }

                // 블록 모델 검증
                if (blocklyModel == null)
                {
                    string errorMsg = "블록 모델이 null입니다.";
                    Console.WriteLine(errorMsg);
                    onError?.Invoke(errorMsg);
                    return null;
                }

                if (!blocklyModel.blockTypes.Any())
                {
                    string errorMsg = "블록 모델이 비어있습니다. 블록을 추가해주세요.";
                    Console.WriteLine(errorMsg);
                    onError?.Invoke(errorMsg);
                    return null;
                }

                var blockTypes = blocklyModel.blockTypes.Select(b => b.type).ToList();
                string blocksArg = string.Join(" ", blockTypes);

                // 여기에 파라미터 딕셔너리 생성 및 JSON 직렬화 추가
                // 추가할 파라미터가 있으면 여기에 추가하면됨
                var paramDict = new Dictionary<string, object>
                {
                    { "imgPath", blocklyModel.imgPath },
                    { "accuracy", blocklyModel.accuracy },
                    { "model", blocklyModel.model },
                    //{ "epoch", blocklyModel.epoch },
                    { "epoch", 1 },
                    { "imgsz", blocklyModel.imgsz },
                    { "Conv", blocklyModel.Conv },
                    { "C2f", blocklyModel.C2f },
                    { "Upsample_scale", blocklyModel.Upsample_scale },
                    { "blockTypes", blocklyModel.blockTypes }
                };
                string paramJson = JsonSerializer.Serialize(paramDict);

                Console.WriteLine($"[DEBUG] paramJson: {paramJson}");

                // 인자에 추가
                string extraArgs = $" --params \"{paramJson.Replace("\"", "\\\"")}\"";

                Console.WriteLine($"Script Path: {runnerPath}");
                onOutput?.Invoke($"Script Path: {runnerPath}");

                // 학습 스크립트 존재 확인
                if (!File.Exists(runnerPath))
                {
                    string errorMsg = $"스크립트 파일을 찾을 수 없습니다: {runnerPath}";
                    Console.WriteLine(errorMsg);
                    onError?.Invoke(errorMsg);
                    return null;
                }

                // 가상환경 없을 때 에러
                if (!File.Exists(pythonExe))
                {
                    string errorMsg = $"파이썬 실행 파일을 찾을 수 없습니다: {pythonExe}";
                    Console.WriteLine(errorMsg);
                    onError?.Invoke(errorMsg);
                    return null;
                }

                // 예시: 이미지 경로, epochs, imgsz 등도 넘길 수 있음
                if (!string.IsNullOrEmpty(imagePath))
                    extraArgs += $" --image-path \"{imagePath}\"";
                if (epochs > 0)
                    extraArgs += $" --epochs {epochs}";
                if (imgsz > 0)
                    extraArgs += $" --imgsz {imgsz}";

                // 실행 설정
                var psi = new ProcessStartInfo
                {
                    FileName = pythonExe,
                    Arguments = $"\"{runnerPath}\" {trainScriptArgName} \"{trainScriptPath}\" --blocks {blocksArg}{extraArgs}",
                    WorkingDirectory = Path.GetDirectoryName(runnerPath),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    StandardOutputEncoding = Encoding.UTF8,
                    StandardErrorEncoding = Encoding.UTF8,
                };
                psi.EnvironmentVariables["PYTHONIOENCODING"] = "utf-8";

                // 환경 변수 설정
                psi.EnvironmentVariables["PATH"] =
                    pythonDir + ";" +
                    pythonScriptsDir + ";" +
                    Environment.GetEnvironmentVariable("PATH");
                psi.EnvironmentVariables["PYTHONPATH"] = Path.GetDirectoryName(runnerPath);

                Console.WriteLine($"[LOG] pythonExe: {pythonExe}, Exists: {File.Exists(pythonExe)}");
                Console.WriteLine($"[LOG] scriptPath: {runnerPath}, Exists: {File.Exists(runnerPath)}");
                Console.WriteLine($"[LOG] imagePath: {imagePath}, Exists: {File.Exists(imagePath)}");
                Console.WriteLine($"[LOG] Arguments: {psi.Arguments}");

                process = new Process { StartInfo = psi, EnableRaisingEvents = true };
                process.OutputDataReceived += (s, e) => { if (!string.IsNullOrEmpty(e.Data)) onOutput?.Invoke(e.Data); };
                process.ErrorDataReceived += (s, e) => { if (!string.IsNullOrEmpty(e.Data)) onError?.Invoke(e.Data); };
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                return process;

            }
            catch (Exception ex)
            {
                onException?.Invoke(ex);
            }
            return null;
        }

        public class InferenceResult
        {
            public bool Success { get; set; }
            public string ResultImage { get; set; }
            public double InferenceTime { get; set; }
            public string Error { get; set; }
        }
        // 추론 실행 스크립트
        // 추론 탭에서 추론만 따로 실행할때 사용
        public InferenceResult RunInference(string imagePath, double conf)
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string pythonExe = Path.GetFullPath(Path.Combine(baseDir, @"..\..\SAI.Application\Python\venv\python.exe"));
            string scriptPath = Path.GetFullPath(Path.Combine(baseDir, @"..\..\SAI.Application\Python\scripts\inference.py"));

            Console.WriteLine($"[LOG] pythonExe: {pythonExe}, Exists: {File.Exists(pythonExe)}");
            Console.WriteLine($"[LOG] scriptPath: {scriptPath}, Exists: {File.Exists(scriptPath)}");
            Console.WriteLine($"[LOG] imagePath: {imagePath}, Exists: {File.Exists(imagePath)}");

            var psi = new ProcessStartInfo
            {
                FileName = pythonExe,
                Arguments = $"\"{scriptPath}\" --image \"{imagePath}\" --conf {conf}",
                WorkingDirectory = Path.GetDirectoryName(scriptPath),
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding = Encoding.UTF8
            };

            Console.WriteLine($"[LOG] Arguments: {psi.Arguments}");

            var process = new Process { StartInfo = psi };
            try
            {
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (process.ExitCode == 0)
                {
                    string jsonResult = null;
                    foreach (var line in output.Split('\n'))
                    {
                        if (line.Trim().StartsWith("INFERENCE_RESULT:"))
                        {
                            jsonResult = line.Substring("INFERENCE_RESULT:".Length).Trim();
                            break;
                        }
                    }
                    if (jsonResult == null)
                        return new InferenceResult { Success = false, Error = "INFERENCE_RESULT가 출력에 없습니다." };
                    var result = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonResult);
                    if (result["success"].GetBoolean())
                    {
                        return new InferenceResult
                        {
                            Success = true,
                            ResultImage = result["result_image"].GetString(),
                            InferenceTime = result.ContainsKey("inference_time") ? result["inference_time"].GetDouble() : 0
                        };
                    }
                    else
                    {
                        return new InferenceResult { Success = false, Error = result["error"].GetString() };
                    }
                }
                else
                {
                    return new InferenceResult { Success = false, Error = error };
                }
            }
            catch (Exception ex)
            {
                return new InferenceResult { Success = false, Error = ex.Message };
            }
        }

        public void Run(Mode mode)
        {
            if (mode == Mode.Tutorial)
            {
                RunPythonScript(Mode.Tutorial, null, null, null);
            }
            else if (mode == Mode.Practice)
            {
                RunPythonScript(Mode.Practice, null, null, null);
            }
            else
            {
                throw new ArgumentException("지원하지 않는 모드입니다.");
            }
        }
    }
}