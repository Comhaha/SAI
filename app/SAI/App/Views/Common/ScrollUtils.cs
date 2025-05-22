using System.Drawing;
using System.Windows.Forms;

namespace SAI.App.Views.Common
{
    internal class ScrollUtils
    {
        public static void AdjustPanelScroll(ScrollableControl scrollablePanel)
        {
            scrollablePanel.AutoScroll = true;

            int maxBottom = 0;
            foreach (Control ctl in scrollablePanel.Controls)
            {
                if (ctl.Visible && ctl.Bottom > maxBottom)
                    maxBottom = ctl.Bottom;
            }

            scrollablePanel.AutoScrollMinSize = new Size(0, maxBottom + 50);
        }

    }
}
