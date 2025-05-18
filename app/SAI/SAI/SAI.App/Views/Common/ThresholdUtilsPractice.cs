using System;
using Guna.UI2.WinForms;
using System.Windows.Forms;
using SAI.SAI.App.Models;
using SAI.SAI.App.Views.Pages;
using SAI.SAI.App.Views.Interfaces;

namespace SAI.SAI.App.Views.Common
{
    internal class ThresholdUtilsPractice
    {
        public static void Setup(Guna2TrackBar trackBar, Guna2TextBox textBox, Action<double> onThresholdChanged, IPracticeInferenceView view)
        {
            double currentThreshold = 0.5;
            var blocklyModel = BlocklyModel.Instance;

            trackBar.Minimum = 1;
            trackBar.Maximum = 100;
            trackBar.Value = 50;

            textBox.Text = "0.50";
            textBox.TextAlign = HorizontalAlignment.Center;
            textBox.ReadOnly = true;
            textBox.TabStop = false;

            textBox.FillColor = System.Drawing.Color.White;
            textBox.DisabledState.FillColor = System.Drawing.Color.White;
            textBox.DisabledState.ForeColor = System.Drawing.Color.Black;
            textBox.BorderColor = System.Drawing.Color.FromArgb(213, 218, 223);
            textBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(213, 218, 223);
            textBox.Enabled = false;

            // 트랙바 값이 변경될 때는 텍스트박스만 업데이트
            trackBar.ValueChanged += (s, e) =>
            {
                currentThreshold = trackBar.Value / 100.0;
                textBox.Text = currentThreshold.ToString("0.00");
                Console.WriteLine($"[TrackBar] 현재 threshold 값: {currentThreshold:0.00}");
            };

            // 마우스를 뗄 때 BlocklyModel에 값을 반영
            trackBar.MouseUp += (s, e) =>
            {
                currentThreshold = trackBar.Value / 100.0;
                blocklyModel.accuracy = currentThreshold;
                Console.WriteLine($"[MouseUp] BlocklyModel accuracy 값 업데이트: {blocklyModel.accuracy:0.00}");
                onThresholdChanged?.Invoke(currentThreshold);

                view.ShowDialogInferenceLoading();

            };

            textBox.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                    return;
                }
                if (e.KeyChar == '.' && textBox.Text.Contains("."))
                {
                    e.Handled = true;
                    return;
                }
                if (e.KeyChar == (char)Keys.Enter)
                {
                    UpdateFromTextBox(textBox, trackBar, ref currentThreshold, onThresholdChanged);
                    e.Handled = true;
                }
            };

            textBox.Leave += (s, e) =>
            {
                UpdateFromTextBox(textBox, trackBar, ref currentThreshold, onThresholdChanged);
            };
        }

        private static void UpdateFromTextBox(Guna2TextBox textBox, Guna2TrackBar trackBar, ref double currentThreshold, Action<double> onThresholdChanged)
        {
            var blocklyModel = BlocklyModel.Instance;
            if (double.TryParse(textBox.Text, out double value))
            {
                value = Math.Max(0.01, Math.Min(1.00, value));
                currentThreshold = value;
                textBox.Text = value.ToString("0.00");

                trackBar.ValueChanged -= null;
                trackBar.Value = (int)(value * 100);
                blocklyModel.accuracy = currentThreshold;  // accuracy 값 업데이트
                Console.WriteLine($"[TextBox] BlocklyModel accuracy 값 업데이트: {blocklyModel.accuracy:0.00}");
                onThresholdChanged?.Invoke(currentThreshold);
            }
            else
            {
                textBox.Text = currentThreshold.ToString("0.00");
                Console.WriteLine($"[TextBox] 잘못된 입력으로 인한 복원 값: {currentThreshold:0.00}");
            }
        }
    }

}
