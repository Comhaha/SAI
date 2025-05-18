namespace SAI.SAI.App.Views.Pages
{
    partial class LayoutTest
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.tpParent = new System.Windows.Forms.TableLayoutPanel();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.tpParent.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpParent
            // 
            this.tpParent.ColumnCount = 3;
            this.tpParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.61975F));
            this.tpParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.27157F));
            this.tpParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.108685F));
            this.tpParent.Controls.Add(this.guna2Panel1, 1, 1);
            this.tpParent.Location = new System.Drawing.Point(0, 0);
            this.tpParent.Name = "tpParent";
            this.tpParent.RowCount = 3;
            this.tpParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.90416F));
            this.tpParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.99432F));
            this.tpParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.101527F));
            this.tpParent.Size = new System.Drawing.Size(1280, 720);
            this.tpParent.TabIndex = 0;
            this.tpParent.Paint += new System.Windows.Forms.PaintEventHandler(this.tpParent_Paint);
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.Location = new System.Drawing.Point(151, 103);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1059, 577);
            this.guna2Panel1.TabIndex = 0;
            // 
            // LayoutTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tpParent);
            this.Name = "LayoutTest";
            this.Size = new System.Drawing.Size(1280, 720);
            this.tpParent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tpParent;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
    }
}
