using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SAI.SAI.App.Views.Common
{
    internal class ToolTipUtils
    {
        private const int PaddingX = 10;
        private const int PaddingY = 6;
        private const int MaxWidth = 500;

        private static readonly Color DefaultBack = Color.FromArgb(50, 50, 50);
        private static readonly Color DefaultFore = Color.White;
        private static readonly Font DefaultFont = new Font("Noto Sans KR", 13f);


        public static void CustomToolTip(
            Control target,
            string text,
            Color? backColor = null,
            Color? foreColor = null,
            Font font = null)
        {
            var tip = new ToolTip
            {
                AutoPopDelay = 3000,
                InitialDelay = 300,
                ReshowDelay = 300,
                ShowAlways = true,
                OwnerDraw = true
            };

            Color bg = backColor ?? DefaultBack;
            Color fg = foreColor ?? DefaultFore;
            Font fnt = font ?? DefaultFont;

            /* ----------- 그리기 ----------- */
            tip.Draw += (s, e) =>
            {
                // 배경
                using (SolidBrush fill = new SolidBrush(bg))
                using (Pen pen = new Pen(bg))
                {
                    e.Graphics.FillRectangle(fill, e.Bounds);
                    e.Graphics.DrawRectangle(pen,
                        e.Bounds.X, e.Bounds.Y,
                        e.Bounds.Width - 1,
                        e.Bounds.Height - 1);

                    Rectangle textRect = new Rectangle(
                        e.Bounds.X + PaddingX,
                        e.Bounds.Y + PaddingY,
                        e.Bounds.Width - PaddingX * 2,
                        e.Bounds.Height - PaddingY * 2);

                    TextRenderer.DrawText(e.Graphics, e.ToolTipText, fnt, textRect, fg,
                        TextFormatFlags.Left | TextFormatFlags.VerticalCenter |
                        TextFormatFlags.WordBreak);
                }
            };

            /* ----------- 크기 계산 ----------- */
            tip.Popup += (s, e) =>
            {
                Size textSize = TextRenderer.MeasureText(
                    text, fnt, new Size(MaxWidth, 0),
                    TextFormatFlags.Left | TextFormatFlags.WordBreak);

                e.ToolTipSize = new Size(
                    textSize.Width + PaddingX * 2,
                    textSize.Height + PaddingY * 2);
            };

            tip.SetToolTip(target, text);
        }
    }
}
