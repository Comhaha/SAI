using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAI.SAI.App.Views.Pages
{
	public partial class AutoSizeLabel : UserControl
	{
		public AutoSizeLabel()
		{
			InitializeComponent();
			this.Resize += (s, e) => ResizeFont();
			label.TextChanged += (s, e) => ResizeFont();
		}

		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override string Text
		{
			get => label.Text;
			set { label.Text = value; ResizeFont(); }
		}

		[Browsable(true)]
		public override Font Font
		{
			get => base.Font;
			set { base.Font = value; label.Font = value; ResizeFont(); }
		}

		[Browsable(true)]
		public override Color ForeColor
		{
			get => base.ForeColor;
			set { base.ForeColor = value; label.ForeColor = value; }
		}

		[Browsable(true)]
		public override Color BackColor
		{
			get => base.BackColor;
			set { base.BackColor = value; label.BackColor = value; }
		}

		[Browsable(true)]
		public ContentAlignment TextAlign
		{
			get => label.TextAlign;
			set { label.TextAlign = value; }
		}

		private void ResizeFont()
		{
			if (string.IsNullOrEmpty(label.Text) || label.Width <= 0 || label.Height <= 0)
				return;

			using (Graphics g = label.CreateGraphics())
			{
				float bestSize = 5f;
				for (float size = 5f; size <= 100f; size += 0.5f)
				{
					var testFont = new Font(label.Font.FontFamily, size, label.Font.Style);
					SizeF textSize = g.MeasureString(label.Text, testFont);
					if (textSize.Width > label.Width || textSize.Height > label.Height)
						break;
					bestSize = size;
				}

				label.Font = new Font(label.Font.FontFamily, bestSize, label.Font.Style);
			}
		}
	}

}
