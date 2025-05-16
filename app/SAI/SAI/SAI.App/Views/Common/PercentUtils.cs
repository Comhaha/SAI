using Guna.UI2.WinForms;
using System.Drawing;
using System.Windows.Forms;

namespace SAI.SAI.App.Views.Common
{
    public static class PercentUtils
    {
        /// <summary>
        /// 퍼센트 표시용 Guna2TextBox의 폰트, 패딩, 정렬, 읽기전용을 한 번에 세팅합니다.
        /// </summary>
        /// <param name="textBox">대상 Guna2TextBox</param>
        /// <param name="fontScale">폰트 크기 배율 (예: 0.5f)</param>
        /// <param name="paddingTop">위쪽 패딩(px)</param>
        /// <param name="paddingBottom">아래쪽 패딩(px)</param>
        /// <param name="maxLength">최대 글자 수</param>
        public static void SetupPercentTextBox(Guna2TextBox textBox, float fontScale = 0.5f, int paddingTop = 0, int paddingBottom = 0, int maxLength = 8)
        {
            if (textBox == null) return;

            // 폰트 크기 축소
            textBox.Font = new Font(textBox.Font.FontFamily, textBox.Font.Size * fontScale, textBox.Font.Style);

            // 가운데 정렬
            textBox.TextAlign = HorizontalAlignment.Center;

            // 읽기 전용
            textBox.ReadOnly = true;

            // 최대 글자 수 설정 (예: 8글자까지)
            textBox.MaxLength = maxLength;
        }
    }
}
