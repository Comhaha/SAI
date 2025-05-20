using System.Drawing;
using System.Windows.Forms;

namespace SAI.SAI.App.Views.Common
{
    internal class ToolTipUtils
    {
        public static void CustomToolTip(Control targetControl, string tooltipText)
        {
            ToolTip toolTip = new ToolTip
            {
                AutoPopDelay = 3000,
                InitialDelay = 300,
                ReshowDelay = 300,
                ShowAlways = true,
                OwnerDraw = true
            };

            toolTip.Draw += (s, e) =>
            {
                using (Font notoSans = new Font("Noto Sans KR", 13))
                {
                    e.DrawBackground();
                    e.DrawBorder();
                    e.Graphics.DrawString(e.ToolTipText, notoSans, Brushes.Black, new PointF(2, 2));
                }
            };

            toolTip.Popup += (s, e) =>
            {
                using (Font notoSans = new Font("Noto Sans KR", 9))
                {
                    string text = toolTip.GetToolTip(targetControl);
                    Size size = TextRenderer.MeasureText(text, notoSans);
                    e.ToolTipSize = new Size(size.Width + 8, size.Height + 4);
                }
            };

            toolTip.SetToolTip(targetControl, tooltipText);
        }
    }
}
