using SAI.SAI.App.Presenters;
using SAI.SAI.App.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using SAI.SAI.App.Views.Common;

namespace SAI.SAI.App.Forms.Dialogs
{
    public partial class DialogModelSelect : Form
    {
        private DialogLoadPagePresenter presenter;
		public DialogModelSelect()
        {
            InitializeComponent();

			DialogUtils.ApplyDefaultStyle(this, Color.Gray);

			string clicked = "btnYolo";
			clicked = "btnYolo";
			btnYolo.BackgroundImage = Properties.Resources.btn_yolo_clicked;
			btnEfficient.BackgroundImage = Properties.Resources.btn_efficientdet;
			btnFaster.BackgroundImage = Properties.Resources.btn_faster;

			this.BackgroundImage = Properties.Resources.selectYolo;
			btnTutorial.Visible = true;
			btnTrain.Visible = true;


			// btnYolo
			btnYolo.BackColor = Color.Transparent;
			btnYolo.PressedColor = Color.Transparent;
			btnYolo.CheckedState.FillColor = Color.Transparent;
			btnYolo.DisabledState.FillColor = Color.Transparent;
			btnYolo.HoverState.FillColor = Color.Transparent;
			btnYolo.Click += (s, e) =>
			{
				clicked = "btnYolo";
				btnYolo.BackgroundImage = Properties.Resources.btn_yolo_clicked;
				btnEfficient.BackgroundImage = Properties.Resources.btn_efficientdet;
				btnFaster.BackgroundImage = Properties.Resources.btn_faster;

				this.BackgroundImage = Properties.Resources.selectYolo;
				btnTutorial.Visible = true;
				btnTrain.Visible = true;
			};
			// btnClose 마우스 입력 될 때
			btnYolo.MouseEnter += (s, e) =>
			{
				btnYolo.BackColor = Color.Transparent;
				if(clicked != "btnYolo")
					btnYolo.BackgroundImage = Properties.Resources.btn_yolo_hover;
				else
					btnYolo.BackgroundImage = Properties.Resources.btn_yolo_clicked;
			};
			// btnYolo 마우스 떠날때
			btnYolo.MouseLeave += (s, e) =>
			{
				if(clicked != "btnYolo")
					btnYolo.BackgroundImage = Properties.Resources.btn_yolo;
				else
					btnYolo.BackgroundImage = Properties.Resources.btn_yolo_clicked;
			};

			// btnEfficient
			btnEfficient.BackColor = Color.Transparent;
			btnEfficient.PressedColor = Color.Transparent;
			btnEfficient.CheckedState.FillColor = Color.Transparent;
			btnEfficient.DisabledState.FillColor = Color.Transparent;
			btnEfficient.HoverState.FillColor = Color.Transparent;
			btnEfficient.Click += (s, e) =>
			{
				clicked = "btnEfficient";
				btnYolo.BackgroundImage = Properties.Resources.btn_yolo;
				btnEfficient.BackgroundImage = Properties.Resources.btn_efficientdet_clicked;
				btnFaster.BackgroundImage = Properties.Resources.btn_faster;
				this.BackgroundImage = Properties.Resources.selectEfficient;
				btnTutorial.Visible = true;
				btnTrain.Visible = true;
			};
			// btnEfficient 마우스 입력 될 때
			btnEfficient.MouseEnter += (s, e) =>
			{
				btnEfficient.BackColor = Color.Transparent;
				if (clicked != "btnEfficient")
					btnEfficient.BackgroundImage = Properties.Resources.btn_efficientdet_hover;
				else
					btnEfficient.BackgroundImage = Properties.Resources.btn_efficientdet_clicked;
			};
			// btnEfficient 마우스 떠날때
			btnEfficient.MouseLeave += (s, e) =>
			{
				if (clicked != "btnEfficient")
					btnEfficient.BackgroundImage = Properties.Resources.btn_efficientdet;
				else
					btnEfficient.BackgroundImage = Properties.Resources.btn_efficientdet_clicked;
			};

			// btnFaster
			btnFaster.BackColor = Color.Transparent;
			btnFaster.PressedColor = Color.Transparent;
			btnFaster.CheckedState.FillColor = Color.Transparent;
			btnFaster.DisabledState.FillColor = Color.Transparent;
			btnFaster.HoverState.FillColor = Color.Transparent;
			btnFaster.Click += (s, e) =>
			{
				clicked = "btnFaster";
				btnYolo.BackgroundImage = Properties.Resources.btn_yolo;
				btnEfficient.BackgroundImage = Properties.Resources.btn_efficientdet;
				btnFaster.BackgroundImage = Properties.Resources.btn_faster_clicked;
				this.BackgroundImage = Properties.Resources.selectFaster;
				btnTutorial.Visible = true;
				btnTrain.Visible = true;
			};
			// btnFaster 마우스 입력 될 때
			btnFaster.MouseEnter += (s, e) =>
			{
				btnFaster.BackColor = Color.Transparent;
				if (clicked != "btnFaster")
					btnFaster.BackgroundImage = Properties.Resources.btn_faster_hover;
				else
					btnFaster.BackgroundImage = Properties.Resources.btn_faster_clicked;
			};
			// btnFaster 마우스 떠날때
			btnFaster.MouseLeave += (s, e) =>
			{
				if (clicked != "btnFaster")
					btnFaster.BackgroundImage = Properties.Resources.btn_faster;
				else
					btnFaster.BackgroundImage = Properties.Resources.btn_faster_clicked;
			};

			// btnClose
			btnClose.BackColor = Color.Transparent;
			btnClose.PressedColor = Color.Transparent;
			btnClose.CheckedState.FillColor = Color.Transparent;
			btnClose.DisabledState.FillColor = Color.Transparent;
			btnClose.HoverState.FillColor = Color.Transparent;
			btnClose.Click += (s, e) => { this.Close(); };
			// btnClose 마우스 입력 될 때
			btnClose.MouseEnter += (s, e) =>
			{
				btnClose.BackColor = Color.Transparent;
				btnClose.BackgroundImage = Properties.Resources.btn_close_select_model_clicked;
			};
			// btnClose 마우스 떠날때
			btnClose.MouseLeave += (s, e) =>
			{
				btnClose.BackgroundImage = Properties.Resources.btn_close_select_model;
			};

			// btnTutorial
			btnTutorial.BackColor = Color.Transparent;
			btnTutorial.PressedColor = Color.Transparent;
			btnTutorial.CheckedState.FillColor = Color.Transparent;
			btnTutorial.DisabledState.FillColor = Color.Transparent;
			btnTutorial.HoverState.FillColor = Color.Transparent;
			// btnTutorial 마우스 입력 될 때
			btnTutorial.MouseEnter += (s, e) =>
			{
				btnTutorial.BackColor = Color.Transparent;
				btnTutorial.BackgroundImage = Properties.Resources.btn_tutorial_clicked;
			};
			// btnTutorial 마우스 떠날때
			btnTutorial.MouseLeave += (s, e) =>
			{
				btnTutorial.BackgroundImage = Properties.Resources.btn_tutorial;
			};

            // btnTutorial 클릭 이벤트
            btnTutorial.Click += (s, e) =>
            {
                if (clicked == "btnYolo")
                {
                    presenter.clickTutorial();
                    this.Close();
                }
                else
                {
                    var prepareDialog = new DialogPrepare();
                    prepareDialog.ShowDialog(this);
                }
            };

            // btnTrain
            btnTrain.BackColor = Color.Transparent;
			btnTrain.PressedColor = Color.Transparent;
			btnTrain.CheckedState.FillColor = Color.Transparent;
			btnTrain.DisabledState.FillColor = Color.Transparent;
			btnTrain.HoverState.FillColor = Color.Transparent;
			// btnTrain 마우스 입력 될 때
			btnTrain.MouseEnter += (s, e) =>
			{
				btnTrain.BackColor = Color.Transparent;
				btnTrain.BackgroundImage = Properties.Resources.btn_train_clicked;
			};
			// btnTrain 마우스 떠날때
			btnTrain.MouseLeave += (s, e) =>
			{
				btnTrain.BackgroundImage = Properties.Resources.btn_train;
			};

            // btnTrain 클릭 이벤트
            btnTrain.Click += (s, e) =>
            {
                if (clicked == "btnYolo")
                {
                    presenter.clickTrainAtModelSelect();
                    this.Close();
                }
                else
                {
                    var prepareDialog = new DialogPrepare();
                    prepareDialog.ShowDialog(this);
                }
            };
        }

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			var view = this.Owner as IMainView;
			presenter = new DialogLoadPagePresenter(view);
		}
	}

}
