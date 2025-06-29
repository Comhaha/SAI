using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAI.SAI.App.Models;
using SAI.SAI.App.Views.Interfaces;

namespace SAI.SAI.App.Presenters
{
    public class LogImagePresenter
    {
        private readonly IChartView _view;

        
        private string _imagePath;

        public LogImagePresenter(IChartView view)
        {
            this._view = view;

            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            this._imagePath = Path.GetFullPath(Path.Combine(baseDir, "SAI.Application", "Python", "runs", "detect", "train13", "results.png"));
        }

        public void SetImage()
        {
            var images = LoadImage();
            LogImageModel.Instance.Images = images;
            _view.SetImage();
        }

        private List<Bitmap> LoadImage()
        {
            List<Bitmap> list = new List<Bitmap>();
            if (!File.Exists(_imagePath)) return list;

            Bitmap src = new Bitmap(_imagePath);
            int rows = 2, cols = 5;
            int w = src.Width / cols;
            int h = src.Height / rows;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    int thisW = (c == cols - 1) ? src.Width - c * w : w;
                    int thisH = (r == rows - 1) ? src.Height - r * h : h;

                    var rect = new Rectangle(c * w, r * h, thisW, thisH);
                    Bitmap piece = src.Clone(rect, src.PixelFormat);
                    list.Add(piece);
                }
            }
            return list;
        }
    }
}
