using Guna.UI2.WinForms;
using System.Drawing;
using System.Windows.Forms;

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

            // 맨 위부터 메모를 시작하게 함
            textBox.Multiline = true;
            textBox.ScrollBars = ScrollBars.Vertical;

            // 자동 줄바꿈 설정 (가로 길이 초과 시 자동으로 줄 내림)
            textBox.WordWrap = true;

            // 텍스트 정렬을 왼쪽 상단으로 설정
            textBox.TextAlign = HorizontalAlignment.Left;
        }
    }
}