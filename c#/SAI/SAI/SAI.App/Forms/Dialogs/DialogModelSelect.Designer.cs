namespace SAI.SAI.App.Forms.Dialogs
{
    partial class DialogModelSelect
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.ibtnGoPractice = new Guna.UI2.WinForms.Guna2ImageButton();
			this.ibtnGoTutorial = new Guna.UI2.WinForms.Guna2ImageButton();
			this.SuspendLayout();
			// 
			// ibtnGoPractice
			// 
			this.ibtnGoPractice.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
			this.ibtnGoPractice.HoverState.Image = global::SAI.Properties.Resources.btn_gopractice_hover;
			this.ibtnGoPractice.HoverState.ImageSize = new System.Drawing.Size(115, 41);
			this.ibtnGoPractice.Image = global::SAI.Properties.Resources.btn_gopractice;
			this.ibtnGoPractice.ImageOffset = new System.Drawing.Point(0, 0);
			this.ibtnGoPractice.ImageRotate = 0F;
			this.ibtnGoPractice.ImageSize = new System.Drawing.Size(115, 41);
			this.ibtnGoPractice.Location = new System.Drawing.Point(592, 288);
			this.ibtnGoPractice.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.ibtnGoPractice.Name = "ibtnGoPractice";
			this.ibtnGoPractice.PressedState.ImageSize = new System.Drawing.Size(64, 64);
			this.ibtnGoPractice.Size = new System.Drawing.Size(71, 26);
			this.ibtnGoPractice.TabIndex = 1;
			this.ibtnGoPractice.Click += new System.EventHandler(this.ibtnGoPractice_Click);
			// 
			// ibtnGoTutorial
			// 
			this.ibtnGoTutorial.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
			this.ibtnGoTutorial.HoverState.Image = global::SAI.Properties.Resources.btn_gotutorial_hover;
			this.ibtnGoTutorial.HoverState.ImageSize = new System.Drawing.Size(115, 41);
			this.ibtnGoTutorial.Image = global::SAI.Properties.Resources.btn_gotutorial;
			this.ibtnGoTutorial.ImageOffset = new System.Drawing.Point(0, 0);
			this.ibtnGoTutorial.ImageRotate = 0F;
			this.ibtnGoTutorial.ImageSize = new System.Drawing.Size(115, 41);
			this.ibtnGoTutorial.Location = new System.Drawing.Point(495, 288);
			this.ibtnGoTutorial.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.ibtnGoTutorial.Name = "ibtnGoTutorial";
			this.ibtnGoTutorial.PressedState.ImageSize = new System.Drawing.Size(64, 64);
			this.ibtnGoTutorial.Size = new System.Drawing.Size(71, 26);
			this.ibtnGoTutorial.TabIndex = 0;
			this.ibtnGoTutorial.Click += new System.EventHandler(this.ibtnGoTutorial_Click);
			// 
			// DialogModelSelect
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(772, 406);
			this.Controls.Add(this.ibtnGoPractice);
			this.Controls.Add(this.ibtnGoTutorial);
			this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.Name = "DialogModelSelect";
			this.Text = "DialogModelSelect";
			this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2ImageButton ibtnGoTutorial;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnGoPractice;
    }
}