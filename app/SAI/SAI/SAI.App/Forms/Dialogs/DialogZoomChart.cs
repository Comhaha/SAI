using Guna.Charts.WinForms;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SAI.SAI.App.Forms.Dialogs
{
    public partial class DialogZoomChart : Form
    {
        public DialogZoomChart(GunaChart sourceChart)
        {
            ClientSize = new Size(900, 600);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Text = sourceChart.Title.Text;

            BuildZoomChart(sourceChart);
        }

        private void BuildZoomChart(GunaChart src)
        {
            var chart = new GunaChart
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(10)
            };

            chart.Title.Text = src.Title.Text;
            chart.Title.Font = new ChartFont("Segoe UI", 12, ChartFontStyle.Bold);

            chart.XAxes.GridLines.Display = src.XAxes.GridLines.Display;
            chart.YAxes.GridLines.Display = src.YAxes.GridLines.Display;

            /* ─ 데이터셋 복제 ─ */
            foreach (var baseDs in src.Datasets.OfType<GunaSplineDataset>())
            {
                var clone = new GunaSplineDataset
                {
                    Label = baseDs.Label,
                    BorderColor = baseDs.BorderColor,
                    BorderWidth = baseDs.BorderWidth,
                    PointRadius = baseDs.PointRadius,
                    FillColor = baseDs.FillColor,
                    LegendBoxFillColor = baseDs.LegendBoxFillColor
                };

                // ❶ LPoint 로 캐스팅해서 Label/Y 값 복사
                foreach (LPoint pt in baseDs.DataPoints.Cast<LPoint>())
                    clone.DataPoints.Add(pt.Label, pt.Y);

                chart.Datasets.Add(clone);
            }

            chart.Legend.Position = LegendPosition.Right;
            chart.Update();
            Controls.Add(chart);
        }
    }
}
