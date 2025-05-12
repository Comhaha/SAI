

using Guna.UI2.WinForms;
using System.Drawing;

namespace SAI.SAI.App.Views.Common
{
    internal class MemoUtils
    {

        private static readonly Color MemoColor = ColorTranslator.FromHtml("#F7FFB8");

        public static void ApplyStyle(Guna2TextBox textBox)
        {
            textBox.FillColor = MemoColor;
            textBox.BorderColor = MemoColor;
            textBox.FocusedState.BorderColor = MemoColor;
            textBox.HoverState.BorderColor = MemoColor;
            textBox.DisabledState.BorderColor = MemoColor;
        }
    }
}
