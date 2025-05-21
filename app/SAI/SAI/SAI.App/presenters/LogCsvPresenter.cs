using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp.DevTools.Performance;
using SAI.SAI.App.Models;
using SAI.SAI.App.Views.Interfaces;

namespace SAI.SAI.App.Presenters
{
    public class LogCsvPresenter
    {
        private readonly ICsvChartView _view;

        private string errorMessage = "";

        private string _csvPath;

        public LogCsvPresenter(ICsvChartView view)
        {
            this._view = view;

            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
			this._csvPath = Path.GetFullPath(Path.Combine(baseDir, "SAI.Application", "Python", "runs", "detect", "train", "results.csv"));
        }

        public void LoadCsv(string csvPath)
        {
            var lines = File.ReadAllLines(csvPath);
            if (lines.Length < 2) return;

            var titles = lines[0].Split(',');
            string[] metrics = titles
                    .Skip(1) 
                    .Take(10)
                    .Select(h => h.Trim())
                    .ToArray();
            
            int colCount = metrics.Length;
            int rowCount = lines.Length - 1;

            double[][] values = new double[colCount][];
            double[][] smoothes = new double[colCount][];

            for (int c = 0; c < colCount; c++)
                values[c] = new double[rowCount];

            var culture = CultureInfo.InvariantCulture;

            /* 2) CSV → values[metric][epoch] ----------------------------------- */
            for (int r = 0; r < rowCount; r++)
            {
                string[] parts = lines[r + 1].Split(',').Select(p => p.Trim()).ToArray();   // 데이터 행
                                                            // parts[0] = epoch, parts[1] = time  ==>  실제 지표는 parts[2]부터

                for (int c = 0; c < colCount; c++)
                {
                    //       ┌───────── offset 2 (epoch, time 제외)
                    string cell = parts[c + 1];
                    if (double.TryParse(cell, NumberStyles.Any, culture, out double v))
                        values[c][r] = v;
                    else
                        values[c][r] = double.NaN;           // 파싱 실패 시 NaN
                }
            }

            /* 3) 간단 EMA(α=0.3) 스무딩 --------------------------------------- */
            const double alpha = 0.3;

            for (int c = 0; c < colCount; c++)
            {
                int len = values[c].Length;
                double[] dst = new double[len];
                if (len == 0) continue;

                dst[0] = values[c][0];
                for (int i = 1; i < len; i++)
                    dst[i] = alpha * values[c][i] + (1 - alpha) * dst[i - 1];

                smoothes[c] = dst;
            }

            /* ── 4) 모델에 저장 ─────────────────────────── */
            var model = LogCsvModel.instance;
            model.titles = metrics;     // 길이 10
            model.values = values;     // [metric][epoch]
            model.smoothes = smoothes;     // [metric][epoch]  (EMA)
        }

        private double[] Smooth(double[] src, double alpha)
        {
            double[] dst = new double[src.Length];
            double acc = src[0];
            for (int i = 0; i < src.Length; i++)
            {
                acc = alpha * src[i] + (1 - alpha) * acc;
                dst[i] = acc;
            }
            return dst;
        }

        public void SetData()
        {
            LoadData();
            _view.SetData();
        }

        private void LoadData()
        {
            if (!File.Exists(_csvPath))
            {
                Console.WriteLine("[DEBUG] CSV 파일을 찾을 수 없습니다.\n" + _csvPath);
                return;
            }
               

            var lines = File.ReadAllLines(_csvPath);
            if (lines.Length < 2)
            {
                Console.WriteLine("[DEBUG] CSV에 데이터가 없습니다..\n" + _csvPath);
                return;
            }

                // 1) 헤더 파싱 (epoch 제외)
                var headers = lines[0].Split(',');
                int colCount = headers.Length - 1;          // epoch 제외
                string[] titles = new string[colCount];
                Array.Copy(headers, 1, titles, 0, colCount);

                // 2) 데이터 파싱
                int rowCount = lines.Length - 1;
                double[][] values = new double[colCount][];
                
                for (int c = 0; c < colCount; c++)
                    values[c] = new double[rowCount];

                var culture = CultureInfo.InvariantCulture;

                for (int r = 0; r < rowCount; r++)
                {
                    // lineIndex = r+1 (데이터 시작)
                    var parts = lines[r + 1].Split(',');

                // parts[0] = epoch, 필요하면 int.Parse(parts[0]) 가능
                for (int c = 0; c < colCount; c++)
                {
                    if (double.TryParse(parts[c + 1], NumberStyles.Any, culture, out double v))
                        values[c][r] = v;
                    else
                        values[c][r] = double.NaN;  // 파싱 실패 시 NaN
                }

            }

                double[][] smoothes = new double[colCount][];
                for (int c = 0; c < colCount; c++)
                {
                    int len = values[c].Length;

                    double alpha = 0.3;
                    double[] dst = new double[len];

                    if (len == 0) continue;

                    dst[0] = values[c][0];
                    for(int i = 1; i < len; i++)
                        dst[i] = alpha * values[c][i] + (1 - alpha) * dst[i - 1];
                    
                    smoothes[c] = dst;
                }

            // 3) 모델에 저장
            LogCsvModel.instance.titles = titles;
            LogCsvModel.instance.values = values;
            LogCsvModel.instance.smoothes = smoothes;

        }
    }
}
