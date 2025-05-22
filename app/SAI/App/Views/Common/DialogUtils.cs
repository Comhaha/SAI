using System.Drawing;
using System.Windows.Forms;

namespace SAI.App.Views.Common
{
    internal class DialogUtils
    {
        public static void ApplyDefaultStyle(Form dialogForm, Color transparentColor)
        {
            dialogForm.StartPosition = FormStartPosition.CenterParent;
            dialogForm.FormBorderStyle = FormBorderStyle.None;
            dialogForm.TopMost = true;
            dialogForm.BackColor = transparentColor;
            dialogForm.TransparencyKey = transparentColor;
        }
    }
}
