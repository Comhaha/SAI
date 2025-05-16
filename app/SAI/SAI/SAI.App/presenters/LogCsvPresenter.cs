using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAI.SAI.App.Models;
using SAI.SAI.App.Views.Interfaces;

namespace SAI.SAI.App.Presenters
{
    public class LogCsvPresenter
    {
        private readonly ICsvChartView _view;


        private string _csvPath;

        public LogCsvPresenter(ICsvChartView view)
        {
            this._view = view;

            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            this._csvPath = Path.GetFullPath(Path.Combine(baseDir, @"..\\..\\SAI.Application\\Python\\runs\\detect\\train\\results.csv"));
        }

        public void SetData()
        {
            LoadData();
            _view.SetData();
        }

        private void LoadData()
        {
            if (!File.Exists(_csvPath))
                throw new FileNotFoundException("CSV 파일을 찾을 수 없습니다.", _csvPath);

            var lines = File.ReadAllLines(_csvPath);
            if (lines.Length < 2)
                throw new InvalidDataException("CSV에 데이터가 없습니다.");

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
