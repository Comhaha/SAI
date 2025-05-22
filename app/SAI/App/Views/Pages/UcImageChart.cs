using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using SAI.SAI.App.Models;
using SAI.SAI.App.Presenters;
using SAI.SAI.App.Views.Interfaces;

namespace SAI.SAI.App.Views.Pages
{
    public partial class UcImageChart : UserControl, IChartView
    {
        private readonly Guna2PictureBox[] _boxes = new Guna2PictureBox[10];
        private LogImagePresenter _presenter;

        public UcImageChart()
        {
            InitializeComponent();
            BuildLayout();
        }

        public void SetImage()
        {
            var images = LogImageModel.Instance.Images;
            for (int i = 0; i < _boxes.Length; i++)
            {
                if (i < images.Count)
                {
                    _boxes[i].Image?.Dispose(); // 이전 이미지 해제
                    _boxes[i].Image = images[i];
                }
            }
        }

        private void UcChart_Load(object sender, EventArgs e)
        {
            if (DesignMode || System.ComponentModel.LicenseManager.UsageMode
                     == System.ComponentModel.LicenseUsageMode.Designtime)
                return;

            _presenter = new LogImagePresenter(this);
            _presenter.SetImage();
        }

        // ---------------- UI 생성(2×5) ---------------
        private void BuildLayout()
        {
            // 기존 디자이너의 pictureBox 제거
            Controls.Clear();

            var table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 5,
                ColumnCount = 2
            };
            for (int c = 0; c < 2; c++)
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 300));
            for (int r = 0; r < 5; r++)
                table.RowStyles.Add(new RowStyle(SizeType.Percent, 300));

            for (int i = 0; i < _boxes.Length; i++)
            {
                var pb = new Guna2PictureBox
                {
                    Dock = DockStyle.Fill,
                    SizeMode = PictureBoxSizeMode.Zoom,
                };
                _boxes[i] = pb;
                table.Controls.Add(pb, i % 5, i / 5);
            }
            Controls.Add(table);
        }
    }
}
