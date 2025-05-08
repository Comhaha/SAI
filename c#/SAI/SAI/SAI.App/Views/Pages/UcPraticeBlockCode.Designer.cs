
namespace SAI.SAI.App.Views.Pages
{
    partial class UcPraticeBlockCode
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcPraticeBlockCode));
            this.lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.ibtnHome = new Guna.UI2.WinForms.Guna2ImageButton();
            this.guna2AnimateWindow1 = new Guna.UI2.WinForms.Guna2AnimateWindow(this.components);
            this.ibtnInfer = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ibtnDone = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ibtnMemo = new Guna.UI2.WinForms.Guna2ImageButton();
            this.pCenter = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.pTopCenter = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.btnRunModel = new Guna.UI2.WinForms.Guna2ImageButton();
            this.btnTrashBlock = new Guna.UI2.WinForms.Guna2ImageButton();
            this.btnNextBlock = new Guna.UI2.WinForms.Guna2ImageButton();
            this.btnPreBlock = new Guna.UI2.WinForms.Guna2ImageButton();
            this.pZoomCenter = new Guna.UI2.WinForms.Guna2Panel();
            this.tboxZoomCenter = new Guna.UI2.WinForms.Guna2TextBox();
            this.ibtnMinusBlock = new Guna.UI2.WinForms.Guna2ImageButton();
            this.ibtnPlusBlock = new Guna.UI2.WinForms.Guna2ImageButton();
            this.pRight = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2TextBox1 = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2ImageButton1 = new Guna.UI2.WinForms.Guna2ImageButton();
            this.guna2ImageButton2 = new Guna.UI2.WinForms.Guna2ImageButton();
            this.pLeft = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.pboxBaseFrame = new Guna.UI2.WinForms.Guna2PictureBox();
            this.pCenter.SuspendLayout();
            this.pTopCenter.SuspendLayout();
            this.pZoomCenter.SuspendLayout();
            this.pRight.SuspendLayout();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxBaseFrame)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // ibtnHome
            // 
            this.ibtnHome.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnHome.HoverState.Image = global::SAI.Properties.Resources.btn_home_hover;
            this.ibtnHome.HoverState.ImageSize = new System.Drawing.Size(39, 39);
            this.ibtnHome.Image = global::SAI.Properties.Resources.btn_home;
            this.ibtnHome.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnHome.ImageRotate = 0F;
            this.ibtnHome.ImageSize = new System.Drawing.Size(39, 39);
            resources.ApplyResources(this.ibtnHome, "ibtnHome");
            this.ibtnHome.Name = "ibtnHome";
            this.ibtnHome.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnHome.Click += new System.EventHandler(this.guna2ImageButton1_Click_1);
            // 
            // ibtnInfer
            // 
            this.ibtnInfer.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnInfer.HoverState.Image = global::SAI.Properties.Resources.btn_infer_hover;
            this.ibtnInfer.HoverState.ImageSize = new System.Drawing.Size(38, 158);
            this.ibtnInfer.Image = global::SAI.Properties.Resources.btn_infer;
            this.ibtnInfer.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnInfer.ImageRotate = 0F;
            this.ibtnInfer.ImageSize = new System.Drawing.Size(38, 158);
            resources.ApplyResources(this.ibtnInfer, "ibtnInfer");
            this.ibtnInfer.Name = "ibtnInfer";
            this.ibtnInfer.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnInfer.Click += new System.EventHandler(this.ibtnInfer_Click);
            // 
            // ibtnDone
            // 
            this.ibtnDone.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnDone.HoverState.Image = global::SAI.Properties.Resources.btn_done_hover;
            this.ibtnDone.HoverState.ImageSize = new System.Drawing.Size(154, 39);
            this.ibtnDone.Image = global::SAI.Properties.Resources.btn_done;
            this.ibtnDone.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnDone.ImageRotate = 0F;
            this.ibtnDone.ImageSize = new System.Drawing.Size(154, 39);
            resources.ApplyResources(this.ibtnDone, "ibtnDone");
            this.ibtnDone.Name = "ibtnDone";
            this.ibtnDone.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            // 
            // ibtnMemo
            // 
            resources.ApplyResources(this.ibtnMemo, "ibtnMemo");
            this.ibtnMemo.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnMemo.HoverState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnMemo.Image = global::SAI.Properties.Resources.btn_memo;
            this.ibtnMemo.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnMemo.ImageRotate = 0F;
            this.ibtnMemo.ImageSize = new System.Drawing.Size(56, 56);
            this.ibtnMemo.Name = "ibtnMemo";
            this.ibtnMemo.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            // 
            // pCenter
            // 
            resources.ApplyResources(this.pCenter, "pCenter");
            this.pCenter.BackColor = System.Drawing.Color.Transparent;
            this.pCenter.Controls.Add(this.pTopCenter);
            this.pCenter.Controls.Add(this.pZoomCenter);
            this.pCenter.FillColor = System.Drawing.Color.White;
            this.pCenter.Name = "pCenter";
            this.pCenter.Radius = 12;
            this.pCenter.ShadowColor = System.Drawing.Color.Black;
            this.pCenter.ShadowDepth = 15;
            this.pCenter.ShadowStyle = Guna.UI2.WinForms.Guna2ShadowPanel.ShadowMode.ForwardDiagonal;
            this.pCenter.Paint += new System.Windows.Forms.PaintEventHandler(this.pCenter_Paint);
            // 
            // pTopCenter
            // 
            resources.ApplyResources(this.pTopCenter, "pTopCenter");
            this.pTopCenter.BackColor = System.Drawing.Color.Transparent;
            this.pTopCenter.Controls.Add(this.btnRunModel);
            this.pTopCenter.Controls.Add(this.btnTrashBlock);
            this.pTopCenter.Controls.Add(this.btnNextBlock);
            this.pTopCenter.Controls.Add(this.btnPreBlock);
            this.pTopCenter.FillColor = System.Drawing.Color.White;
            this.pTopCenter.Name = "pTopCenter";
            this.pTopCenter.Radius = 12;
            this.pTopCenter.ShadowColor = System.Drawing.Color.Black;
            this.pTopCenter.ShadowDepth = 15;
            this.pTopCenter.ShadowStyle = Guna.UI2.WinForms.Guna2ShadowPanel.ShadowMode.ForwardDiagonal;
            this.pTopCenter.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2ShadowPanel2_Paint);
            // 
            // btnRunModel
            // 
            this.btnRunModel.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnRunModel.HoverState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnRunModel.Image = global::SAI.Properties.Resources.btn_run_model;
            this.btnRunModel.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnRunModel.ImageRotate = 0F;
            this.btnRunModel.ImageSize = new System.Drawing.Size(10, 12);
            resources.ApplyResources(this.btnRunModel, "btnRunModel");
            this.btnRunModel.Name = "btnRunModel";
            this.btnRunModel.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            // 
            // btnTrashBlock
            // 
            this.btnTrashBlock.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnTrashBlock.HoverState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnTrashBlock.Image = global::SAI.Properties.Resources.btn_trash_block;
            this.btnTrashBlock.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnTrashBlock.ImageRotate = 0F;
            this.btnTrashBlock.ImageSize = new System.Drawing.Size(13, 14);
            resources.ApplyResources(this.btnTrashBlock, "btnTrashBlock");
            this.btnTrashBlock.Name = "btnTrashBlock";
            this.btnTrashBlock.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            // 
            // btnNextBlock
            // 
            this.btnNextBlock.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnNextBlock.HoverState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnNextBlock.Image = global::SAI.Properties.Resources.btn_next_block;
            this.btnNextBlock.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnNextBlock.ImageRotate = 0F;
            this.btnNextBlock.ImageSize = new System.Drawing.Size(15, 10);
            resources.ApplyResources(this.btnNextBlock, "btnNextBlock");
            this.btnNextBlock.Name = "btnNextBlock";
            this.btnNextBlock.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            // 
            // btnPreBlock
            // 
            this.btnPreBlock.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnPreBlock.HoverState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnPreBlock.Image = global::SAI.Properties.Resources.btn_pre_block;
            this.btnPreBlock.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnPreBlock.ImageRotate = 0F;
            this.btnPreBlock.ImageSize = new System.Drawing.Size(15, 10);
            resources.ApplyResources(this.btnPreBlock, "btnPreBlock");
            this.btnPreBlock.Name = "btnPreBlock";
            this.btnPreBlock.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            // 
            // pZoomCenter
            // 
            this.pZoomCenter.BackgroundImage = global::SAI.Properties.Resources.btn_zoom;
            this.pZoomCenter.Controls.Add(this.tboxZoomCenter);
            this.pZoomCenter.Controls.Add(this.ibtnMinusBlock);
            this.pZoomCenter.Controls.Add(this.ibtnPlusBlock);
            resources.ApplyResources(this.pZoomCenter, "pZoomCenter");
            this.pZoomCenter.Name = "pZoomCenter";
            this.pZoomCenter.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_Paint);
            // 
            // tboxZoomCenter
            // 
            this.tboxZoomCenter.BorderColor = System.Drawing.Color.Black;
            this.tboxZoomCenter.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tboxZoomCenter.DefaultText = "";
            this.tboxZoomCenter.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tboxZoomCenter.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tboxZoomCenter.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tboxZoomCenter.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tboxZoomCenter.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            resources.ApplyResources(this.tboxZoomCenter, "tboxZoomCenter");
            this.tboxZoomCenter.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tboxZoomCenter.Name = "tboxZoomCenter";
            this.tboxZoomCenter.PlaceholderText = "";
            this.tboxZoomCenter.SelectedText = "";
            // 
            // ibtnMinusBlock
            // 
            this.ibtnMinusBlock.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnMinusBlock.HoverState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnMinusBlock.Image = global::SAI.Properties.Resources.btn_minus;
            this.ibtnMinusBlock.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnMinusBlock.ImageRotate = 0F;
            this.ibtnMinusBlock.ImageSize = new System.Drawing.Size(9, 9);
            resources.ApplyResources(this.ibtnMinusBlock, "ibtnMinusBlock");
            this.ibtnMinusBlock.Name = "ibtnMinusBlock";
            this.ibtnMinusBlock.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            // 
            // ibtnPlusBlock
            // 
            this.ibtnPlusBlock.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnPlusBlock.HoverState.ImageSize = new System.Drawing.Size(64, 64);
            this.ibtnPlusBlock.Image = global::SAI.Properties.Resources.btn_plus;
            this.ibtnPlusBlock.ImageOffset = new System.Drawing.Point(0, 0);
            this.ibtnPlusBlock.ImageRotate = 0F;
            this.ibtnPlusBlock.ImageSize = new System.Drawing.Size(9, 9);
            resources.ApplyResources(this.ibtnPlusBlock, "ibtnPlusBlock");
            this.ibtnPlusBlock.Name = "ibtnPlusBlock";
            this.ibtnPlusBlock.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            // 
            // pRight
            // 
            resources.ApplyResources(this.pRight, "pRight");
            this.pRight.BackColor = System.Drawing.Color.Transparent;
            this.pRight.Controls.Add(this.guna2Panel1);
            this.pRight.FillColor = System.Drawing.Color.White;
            this.pRight.Name = "pRight";
            this.pRight.Radius = 12;
            this.pRight.ShadowColor = System.Drawing.Color.Black;
            this.pRight.ShadowDepth = 15;
            this.pRight.ShadowStyle = Guna.UI2.WinForms.Guna2ShadowPanel.ShadowMode.ForwardDiagonal;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackgroundImage = global::SAI.Properties.Resources.btn_zoom;
            this.guna2Panel1.Controls.Add(this.guna2TextBox1);
            this.guna2Panel1.Controls.Add(this.guna2ImageButton1);
            this.guna2Panel1.Controls.Add(this.guna2ImageButton2);
            resources.ApplyResources(this.guna2Panel1, "guna2Panel1");
            this.guna2Panel1.Name = "guna2Panel1";
            // 
            // guna2TextBox1
            // 
            this.guna2TextBox1.BorderColor = System.Drawing.Color.Black;
            this.guna2TextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.guna2TextBox1.DefaultText = "";
            this.guna2TextBox1.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.guna2TextBox1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.guna2TextBox1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox1.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            resources.ApplyResources(this.guna2TextBox1, "guna2TextBox1");
            this.guna2TextBox1.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox1.Name = "guna2TextBox1";
            this.guna2TextBox1.PlaceholderText = "";
            this.guna2TextBox1.SelectedText = "";
            // 
            // guna2ImageButton1
            // 
            this.guna2ImageButton1.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.guna2ImageButton1.HoverState.ImageSize = new System.Drawing.Size(64, 64);
            this.guna2ImageButton1.Image = global::SAI.Properties.Resources.btn_minus;
            this.guna2ImageButton1.ImageOffset = new System.Drawing.Point(0, 0);
            this.guna2ImageButton1.ImageRotate = 0F;
            this.guna2ImageButton1.ImageSize = new System.Drawing.Size(9, 9);
            resources.ApplyResources(this.guna2ImageButton1, "guna2ImageButton1");
            this.guna2ImageButton1.Name = "guna2ImageButton1";
            this.guna2ImageButton1.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            // 
            // guna2ImageButton2
            // 
            this.guna2ImageButton2.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.guna2ImageButton2.HoverState.ImageSize = new System.Drawing.Size(64, 64);
            this.guna2ImageButton2.Image = global::SAI.Properties.Resources.btn_plus;
            this.guna2ImageButton2.ImageOffset = new System.Drawing.Point(0, 0);
            this.guna2ImageButton2.ImageRotate = 0F;
            this.guna2ImageButton2.ImageSize = new System.Drawing.Size(9, 9);
            resources.ApplyResources(this.guna2ImageButton2, "guna2ImageButton2");
            this.guna2ImageButton2.Name = "guna2ImageButton2";
            this.guna2ImageButton2.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            // 
            // pLeft
            // 
            resources.ApplyResources(this.pLeft, "pLeft");
            this.pLeft.BackColor = System.Drawing.Color.Transparent;
            this.pLeft.FillColor = System.Drawing.Color.White;
            this.pLeft.Name = "pLeft";
            this.pLeft.Radius = 12;
            this.pLeft.ShadowColor = System.Drawing.Color.Black;
            this.pLeft.ShadowDepth = 15;
            this.pLeft.ShadowStyle = Guna.UI2.WinForms.Guna2ShadowPanel.ShadowMode.ForwardDiagonal;
            // 
            // pboxBaseFrame
            // 
            resources.ApplyResources(this.pboxBaseFrame, "pboxBaseFrame");
            this.pboxBaseFrame.Image = global::SAI.Properties.Resources.img_frame_shadow;
            this.pboxBaseFrame.ImageRotate = 0F;
            this.pboxBaseFrame.Name = "pboxBaseFrame";
            this.pboxBaseFrame.TabStop = false;
            // 
            // UcPraticeBlockCode
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = global::SAI.Properties.Resources.img_background;
            this.Controls.Add(this.pLeft);
            this.Controls.Add(this.ibtnMemo);
            this.Controls.Add(this.pRight);
            this.Controls.Add(this.ibtnDone);
            this.Controls.Add(this.pCenter);
            this.Controls.Add(this.ibtnInfer);
            this.Controls.Add(this.ibtnHome);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pboxBaseFrame);
            resources.ApplyResources(this, "$this");
            this.Name = "UcPraticeBlockCode";
            this.Load += new System.EventHandler(this.UcPraticeBlockCode_Load);
            this.pCenter.ResumeLayout(false);
            this.pTopCenter.ResumeLayout(false);
            this.pZoomCenter.ResumeLayout(false);
            this.pRight.ResumeLayout(false);
            this.guna2Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pboxBaseFrame)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnHome;
        private Guna.UI2.WinForms.Guna2AnimateWindow guna2AnimateWindow1;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnInfer;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnDone;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnMemo;
        private Guna.UI2.WinForms.Guna2ShadowPanel pCenter;
        private Guna.UI2.WinForms.Guna2Panel pZoomCenter;
        private Guna.UI2.WinForms.Guna2ShadowPanel pRight;
        private Guna.UI2.WinForms.Guna2ShadowPanel pLeft;
        private Guna.UI2.WinForms.Guna2PictureBox pboxBaseFrame;
        private Guna.UI2.WinForms.Guna2ShadowPanel pTopCenter;
        private Guna.UI2.WinForms.Guna2ImageButton btnNextBlock;
        private Guna.UI2.WinForms.Guna2ImageButton btnPreBlock;
        private Guna.UI2.WinForms.Guna2ImageButton btnRunModel;
        private Guna.UI2.WinForms.Guna2ImageButton btnTrashBlock;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnPlusBlock;
        private Guna.UI2.WinForms.Guna2ImageButton ibtnMinusBlock;
        private Guna.UI2.WinForms.Guna2TextBox tboxZoomCenter;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox1;
        private Guna.UI2.WinForms.Guna2ImageButton guna2ImageButton1;
        private Guna.UI2.WinForms.Guna2ImageButton guna2ImageButton2;
    }
}
