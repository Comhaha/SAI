using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SAI.SAI.App.Views.Pages;

namespace SAI.SAI.App.Views.Common
{
    public class TransparentOverlay : Panel
    {
        public TransparentOverlay()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.UserPaint, true);
            BackColor = Color.Transparent;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_TRANSPARENT = 0x20;
                var cp = base.CreateParams;
                cp.ExStyle |= WS_EX_TRANSPARENT;   // 진짜 투명
                ToolTipUtils.CustomToolTip(this, "자세히 보려면 클릭하세요.");
                return cp;
            }
        }

        // ▶ 배경을 그리지 않아야 밑에 있는 차트가 그대로 보입니다
        protected override void OnPaintBackground(PaintEventArgs e) { /* no bg */ }

        protected override void OnPaint(PaintEventArgs e) { /* nothing */ }
    }
}
