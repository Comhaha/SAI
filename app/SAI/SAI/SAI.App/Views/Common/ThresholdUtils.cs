using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guna.UI2.WinForms;
using System.Windows.Forms;

namespace SAI.SAI.App.Views.Common
{
    internal class ThresholdUtils
    {
        public static void Setup(Guna2TrackBar trackBar, Guna2TextBox textBox, Action<double> onThresholdChanged)
        {
            double currentThreshold = 0.5;

            trackBar.Minimum = 1;
            trackBar.Maximum = 100;
            trackBar.Value = 50;

            textBox.Text = "0.50";
            textBox.TextAlign = HorizontalAlignment.Center;

            trackBar.ValueChanged += (s, e) =>
            {
                currentThreshold = trackBar.Value / 100.0;
                textBox.Text = currentThreshold.ToString("0.00");
                onThresholdChanged?.Invoke(currentThreshold);
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
            if (double.TryParse(textBox.Text, out double value))
            {
                value = Math.Max(0.01, Math.Min(1.00, value));
                currentThreshold = value;
                textBox.Text = value.ToString("0.00");

                trackBar.ValueChanged -= null;
                trackBar.Value = (int)(value * 100);
                onThresholdChanged?.Invoke(currentThreshold);
            }
            else
            {
                textBox.Text = currentThreshold.ToString("0.00");
            }
        }

    }
}
