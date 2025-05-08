using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;


namespace SAI.SAI.App.Resources.Styles
{
    internal class StyleUtil
    {
        public static void RoundTopCorners(Control control, int radius)
        {
            int w = control.Width;
            int h = control.Height;

            GraphicsPath path = new GraphicsPath();
            path.StartFigure();

            // 왼쪽 위 둥글게
            path.AddArc(0, 0, radius, radius, 180, 90);

            // 위쪽 직선
            path.AddLine(radius, 0, w - radius, 0);

            // 오른쪽 위 둥글게
            path.AddArc(w - radius, 0, radius, radius, 270, 90);

            // 나머지 직선
            path.AddLine(w, radius, w, h);
            path.AddLine(w, h, 0, h);
            path.AddLine(0, h, 0, radius);

            path.CloseFigure();
            control.Region = new Region(path);
        }
    }
}
