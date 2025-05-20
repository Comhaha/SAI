using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.Charts.WinForms;
using Guna.UI2.WinForms;
using SAI.SAI.App.Forms.Dialogs;
using SAI.SAI.App.Models;
using SAI.SAI.App.Presenters;
using SAI.SAI.App.Views.Common;
using SAI.SAI.App.Views.Interfaces;

namespace SAI.SAI.App.Views.Pages
{
    public partial class UcCsvChart : UserControl, ICsvChartView
    {
        private LogCsvPresenter _presenter;
        private TableLayoutPanel _grid;

        private readonly string[] _metrics =
        {
            "train/box_loss", "train/cls_loss", "train/dfl_loss",
            "metrics/precision(B)", "metrics/recall(B)",
            "val/box_loss", "val/cls_loss", "val/dfl_loss",
            "metrics/mAP50(B)", "metrics/mAP50-95(B)"
        };

        public UcCsvChart()
        {
            InitializeComponent();
        }

        private void UcCsvChart_Load(object sender, EventArgs e)
        {
            if (DesignMode || System.ComponentModel.LicenseManager.UsageMode
                     == System.ComponentModel.LicenseUsageMode.Designtime)
                return;                     // 디자이너에서 열 때는 중단


            BuildGrid();
            _presenter = new LogCsvPresenter(this);
            //_presenter.SetData();

            Console.WriteLine("UcCsvChart 로드 완료");
        }

        public void SetData()
        {
            if (_grid == null) BuildGrid();

            var m = LogCsvModel.instance;
            if (m.titles == null || m.values == null || m.smoothes == null) return;

            int epochCnt = m.values[0].Length;
            string[] xLabels = Enumerable.Range(1, epochCnt)
                                         .Select(i => i.ToString())
                                         .ToArray();

            for (int i = 0; i < _metrics.Length; i++)
            {
                string metric = _metrics[i];
                int col = Array.IndexOf(m.titles, metric);
                if (col == -1) continue;

                var chart = MakeChart(metric, xLabels, m.values[col], m.smoothes[col]);
                _grid.Controls.Add(chart, i / 5, i % 5);   // 2×5 전치
            }
        }

        /* ----------- UI 빌드 ----------- */
        private void BuildGrid()
        {
            _grid = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 5,
                AutoScroll = true
            };
            for (int c = 0; c < 2; c++)
                _grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            for (int r = 0; r < 5; r++)
                _grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 280));
            Controls.Add(_grid);
        }

        /* ----------- 차트 하나 생성 ----------- */
        private Control MakeChart(string title, string[] labels, double[] raw, double[] smooth)
        {
            var chart = new GunaChart
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(5)
            };
            chart.Title.Text = title;
            chart.Legend.Position = LegendPosition.Top;
            chart.XAxes.GridLines.Display = false;
            chart.YAxes.GridLines.Display = true;

            /* ── 실측 (파랑) ───────────────────────── */
            var dsRaw = new GunaSplineDataset
            {
                Label = "results",
                BorderColor = Color.FromArgb(52, 119, 245),
                BorderWidth = 2,
                PointRadius = 0,
                FillColor = Color.Transparent,
                LegendBoxFillColor = Color.FromArgb(52, 119, 245)
            };
            for (int i = 0; i < labels.Length; i++)
                dsRaw.DataPoints.Add(labels[i], raw[i]);

            /* ── 스무스 (주황, 얇은 실선) ───────────── */
            var dsSm = new GunaSplineDataset
            {
                Label = "smooth",
                BorderColor = Color.FromArgb(230, 126, 34),
                BorderWidth = 1,          // 굵기로 차별
                PointRadius = 0,
                FillColor = Color.Transparent,
                LegendBoxFillColor = Color.FromArgb(230, 126, 34)
            };
            for (int i = 0; i < labels.Length; i++)
                dsSm.DataPoints.Add(labels[i], smooth[i]);

            chart.Datasets.Add(dsRaw);
            chart.Datasets.Add(dsSm);
            chart.Update();

            // ── ① 투명 오버레이 생성 ───────────────────
            var overlay = new TransparentOverlay
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand            // 항상 손모양
            };
            overlay.Tag = chart;
            overlay.Click += Chart_Click;      // 기존 핸들러 재사용

            var container = new Panel { Dock = DockStyle.Fill };
            container.Controls.Add(chart);      // ① 차트 먼저
            container.Controls.Add(overlay);    // ② 오버레이 위
            overlay.BringToFront();             // 항상 최상위

            return container;                   // grid 에 이 컨테이너를 배치
        }

        /* 공용 클릭 핸들러 */
        private void Chart_Click(object sender, EventArgs e)
        {
            if (sender is Panel pnl && pnl.Tag is GunaChart gc)
            {
                var dlg = new DialogZoomChart(gc);
                dlg.ShowDialog(this);
            }
        }
    }
}
