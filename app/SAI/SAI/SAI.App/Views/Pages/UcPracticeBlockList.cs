using SAI.SAI.App.Models.Events;
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
	public partial class UcPracticeBlockList : UserControl
	{
		public UserControl content => this;
		public UcPracticeBlockList(DynamicUcPracticeBlockCode parent, EventHandler<BlockEventArgs> AddBlockButtonClicked)
		{
			InitializeComponent();

			startEvent(parent, AddBlockButtonClicked);
			pipEvent(parent, AddBlockButtonClicked);
			loadModelEvent(parent, AddBlockButtonClicked);
			loadDatasetEvent(parent, AddBlockButtonClicked);
			modelLearningEvent(parent, AddBlockButtonClicked);
			resultCheckEvent(parent, AddBlockButtonClicked);
			imgPathEvent(parent, AddBlockButtonClicked);
			modelInferenceEvent(parent, AddBlockButtonClicked);
			resultVisualizeEvent(parent, AddBlockButtonClicked);
		}

		private void startEvent(DynamicUcPracticeBlockCode parent, EventHandler<BlockEventArgs> AddBlockButtonClicked)
		{
			// 제목 클릭시 아코디언 열기/닫기
			pTitleStart.Click += (s, e) => { titleStartEvent(); };
			pAccodionStart.Click += (s, e) => { titleStartEvent(); };
			pTxtStart.Click += (s, e) => { titleStartEvent(); };
			// btnStart
			btnStart.BackColor = Color.Transparent;
			btnStart.PressedColor = Color.Transparent;
			btnStart.CheckedState.FillColor = Color.Transparent;
			btnStart.DisabledState.FillColor = Color.Transparent;
			btnStart.HoverState.FillColor = Color.Transparent;
			// btnStart 클릭 될 때
			btnStart.Click += (s, e) =>
			{
				btnStart.BackgroundImage = Properties.Resources.btnBlockStartClicked;
				AddBlockButtonClicked?.Invoke(parent, new BlockEventArgs("start"));
			};
			// btnStart 마우스 입력 될 때
			btnStart.MouseEnter += (s, e) => { btnStart.BackgroundImage = Properties.Resources.btnBlockStartClicked; };
			// btnStart 마우스 떠날때
			btnStart.MouseLeave += (s, e) => { btnStart.BackgroundImage = Properties.Resources.btnBlockStart; };
		}
		private void titleStartEvent()
		{
			if (fpStart.Visible)
			{
				fpStart.Visible = false;
				pAccodionStart.BackgroundImage = global::SAI.Properties.Resources.btnAccordionClosed;
			}
			else
			{
				fpStart.Visible = true;
				pAccodionStart.BackgroundImage = global::SAI.Properties.Resources.btnAccordionShow;
			}
		}

		private void pipEvent(DynamicUcPracticeBlockCode parent, EventHandler<BlockEventArgs> AddBlockButtonClicked)
		{
			// 제목 클릭시 아코디언 열기/닫기
			pTitlePip.Click += (s, e) => { titlePipEvent(); };
			pAccodionPip.Click += (s, e) => { titlePipEvent(); };
			pTxtPip.Click += (s, e) => { titlePipEvent(); };
			// btnPip
			btnPip.BackColor = Color.Transparent;
			btnPip.PressedColor = Color.Transparent;
			btnPip.CheckedState.FillColor = Color.Transparent;
			btnPip.DisabledState.FillColor = Color.Transparent;
			btnPip.HoverState.FillColor = Color.Transparent;
			// btnPip 클릭 될 때
			btnPip.Click += (s, e) =>
			{
				btnPip.BackgroundImage = Properties.Resources.btnPipInstallClicked;
				AddBlockButtonClicked?.Invoke(parent, new BlockEventArgs("pipInstall"));
			};
			// btnPip 마우스 입력 될 때
			btnPip.MouseEnter += (s, e) => { btnPip.BackgroundImage = Properties.Resources.btnPipInstallClicked; };
			// btnStart 마우스 떠날때
			btnPip.MouseLeave += (s, e) => { btnPip.BackgroundImage = Properties.Resources.btnPipInstall; };
		}
		private void titlePipEvent()
		{
			if (fpPip.Visible)
			{
				fpPip.Visible = false;
				pAccodionPip.BackgroundImage = global::SAI.Properties.Resources.btnAccordionClosed;
			}
			else
			{
				fpPip.Visible = true;
				pAccodionPip.BackgroundImage = global::SAI.Properties.Resources.btnAccordionShow;
			}
		}

		private void loadModelEvent(DynamicUcPracticeBlockCode parent, EventHandler<BlockEventArgs> AddBlockButtonClicked)
		{
			// 제목 클릭시 아코디언 열기/닫기
			pTitleLoadModel.Click += (s, e) => { titleLoadModelEvent(); };
			pAccodionLoadModel.Click += (s, e) => { titleLoadModelEvent(); };
			pTxtLoadModel.Click += (s, e) => { titleLoadModelEvent(); };

			// btnLoadModel
			btnLoadModel.BackColor = Color.Transparent;
			btnLoadModel.PressedColor = Color.Transparent;
			btnLoadModel.CheckedState.FillColor = Color.Transparent;
			btnLoadModel.DisabledState.FillColor = Color.Transparent;
			btnLoadModel.HoverState.FillColor = Color.Transparent;
			// btnLoadModel 클릭 될 때
			btnLoadModel.Click += (s, e) =>
			{
				btnLoadModel.BackgroundImage = Properties.Resources.btnLoadModelClicked;
				AddBlockButtonClicked?.Invoke(parent, new BlockEventArgs("loadModel"));
				//  🔥🔥🔥"btnloadwithLayer가 삭제 되면 다시 보이게" 수정해주세요🔥🔥🔥
				btnLayer.Visible = false;
				btnLoadModelWithLayer.Visible = false;
			};
			// btnLoadModel 마우스 입력 될 때
			btnLoadModel.MouseEnter += (s, e) => { btnLoadModel.BackgroundImage = Properties.Resources.btnLoadModelClicked; };
			// btnLoadModel 마우스 떠날때
			btnLoadModel.MouseLeave += (s, e) => { btnLoadModel.BackgroundImage = Properties.Resources.btnLoadModel; };

			// btnLoadModelWithLayer
			btnLoadModelWithLayer.BackColor = Color.Transparent;
			btnLoadModelWithLayer.PressedColor = Color.Transparent;
			btnLoadModelWithLayer.CheckedState.FillColor = Color.Transparent;
			btnLoadModelWithLayer.DisabledState.FillColor = Color.Transparent;
			btnLoadModelWithLayer.HoverState.FillColor = Color.Transparent;
			// btnLoadModelWithLayer 클릭 될 때
			btnLoadModelWithLayer.Click += (s, e) =>
			{
				btnLoadModelWithLayer.BackgroundImage = Properties.Resources.btnLoadModelWithLayerClicked;
				AddBlockButtonClicked?.Invoke(parent, new BlockEventArgs("loadModelWithLayer"));
				//  🔥🔥🔥"btnload가 삭제 되면 다시 보이게" 수정해주세요🔥🔥🔥
				btnLoadModel.Visible = false;
				btnLayer.Visible = true;
			};
			// btnLoadModelWithLayer 마우스 입력 될 때
			btnLoadModelWithLayer.MouseEnter += (s, e) => { btnLoadModelWithLayer.BackgroundImage = Properties.Resources.btnLoadModelWithLayerClicked; };
			// btnLoadModelWithLayer 마우스 떠날때
			btnLoadModelWithLayer.MouseLeave += (s, e) => { btnLoadModelWithLayer.BackgroundImage = Properties.Resources.btnLoadModelWithLayer; };

			// btnLayer
			btnLayer.BackColor = Color.Transparent;
			btnLayer.PressedColor = Color.Transparent;
			btnLayer.CheckedState.FillColor = Color.Transparent;
			btnLayer.DisabledState.FillColor = Color.Transparent;
			btnLayer.HoverState.FillColor = Color.Transparent;
			// btnLayer 클릭 될 때
			btnLayer.Click += (s, e) =>
			{
				btnLayer.BackgroundImage = Properties.Resources.btnLayerClicked;
				AddBlockButtonClicked?.Invoke(parent, new BlockEventArgs("layer"));
				//  🔥🔥🔥"btnload가 삭제 되면 다시 보이게" 수정해주세요🔥🔥🔥
				btnLoadModel.Visible = false;
				btnLoadModelWithLayer.Visible = true;
			};
			// btnLayer 마우스 입력 될 때
			btnLayer.MouseEnter += (s, e) => { btnLayer.BackgroundImage = Properties.Resources.btnLayerClicked; };
			// btnLayer 마우스 떠날때
			btnLayer.MouseLeave += (s, e) => { btnLayer.BackgroundImage = Properties.Resources.btnLayer; };
		}
		private void titleLoadModelEvent()
		{
			if (fpLoadModel.Visible)
			{
				fpLoadModel.Visible = false;
				pAccodionLoadModel.BackgroundImage = global::SAI.Properties.Resources.btnAccordionClosed;
			}
			else
			{
				fpLoadModel.Visible = true;
				pAccodionLoadModel.BackgroundImage = global::SAI.Properties.Resources.btnAccordionShow;
			}
		}

		private void loadDatasetEvent(DynamicUcPracticeBlockCode parent, EventHandler<BlockEventArgs> AddBlockButtonClicked)
		{
			// 제목 클릭시 아코디언 열기/닫기
			pTitleLoadDataset.Click += (s, e) => { titleLoadDatasetEvent(); };
			pAccodionLoadDataset.Click += (s, e) => { titleLoadDatasetEvent(); };
			pTxtLoadDataset.Click += (s, e) => { titleLoadDatasetEvent(); };
			// btnLoadDataset
			btnLoadDataset.BackColor = Color.Transparent;
			btnLoadDataset.PressedColor = Color.Transparent;
			btnLoadDataset.CheckedState.FillColor = Color.Transparent;
			btnLoadDataset.DisabledState.FillColor = Color.Transparent;
			btnLoadDataset.HoverState.FillColor = Color.Transparent;
			// btnLoadDataset 클릭 될 때
			btnLoadDataset.Click += (s, e) =>
			{
				btnLoadDataset.BackgroundImage = Properties.Resources.btnLoadDatasetClicked;
				AddBlockButtonClicked?.Invoke(parent, new BlockEventArgs("loadDataset"));
			};
			// btnLoadDataset 마우스 입력 될 때
			btnLoadDataset.MouseEnter += (s, e) => { btnLoadDataset.BackgroundImage = Properties.Resources.btnLoadDatasetClicked; };
			// btnLoadDataset 마우스 떠날때
			btnLoadDataset.MouseLeave += (s, e) => { btnLoadDataset.BackgroundImage = Properties.Resources.btnLoadDataset; };
		}
		private void titleLoadDatasetEvent()
		{
			if (fpLoadDataset.Visible)
			{
				fpLoadDataset.Visible = false;
				pAccodionLoadDataset.BackgroundImage = global::SAI.Properties.Resources.btnAccordionClosed;
			}
			else
			{
				fpLoadDataset.Visible = true;
				pAccodionLoadDataset.BackgroundImage = global::SAI.Properties.Resources.btnAccordionShow;
			}
		}

		private void modelLearningEvent(DynamicUcPracticeBlockCode parent, EventHandler<BlockEventArgs> AddBlockButtonClicked)
		{
			// 제목 클릭시 아코디언 열기/닫기
			pTitleMachineLearning.Click += (s, e) => { titleModelLearningEvent(); };
			pAccodionLearningMachine.Click += (s, e) => { titleModelLearningEvent(); };
			pTxtMachineLearning.Click += (s, e) => { titleModelLearningEvent(); };
			// btnModelLearning
			btnModelLearning.BackColor = Color.Transparent;
			btnModelLearning.PressedColor = Color.Transparent;
			btnModelLearning.CheckedState.FillColor = Color.Transparent;
			btnModelLearning.DisabledState.FillColor = Color.Transparent;
			btnModelLearning.HoverState.FillColor = Color.Transparent;
			// btnModelLearning 클릭 될 때
			btnModelLearning.Click += (s, e) =>
			{
				btnModelLearning.BackgroundImage = Properties.Resources.btnMachineLearningClicked;
				AddBlockButtonClicked?.Invoke(parent, new BlockEventArgs("machineLearning"));
			};
			// btnModelLearning 마우스 입력 될 때
			btnModelLearning.MouseEnter += (s, e) => { btnModelLearning.BackgroundImage = Properties.Resources.btnMachineLearningClicked; };
			// btnModelLearning 마우스 떠날때
			btnModelLearning.MouseLeave += (s, e) => { btnModelLearning.BackgroundImage = Properties.Resources.btnMachineLearning; };
		}
		private void titleModelLearningEvent()
		{
			if (fpMachineLearning.Visible)
			{
				fpMachineLearning.Visible = false;
				pAccodionLearningMachine.BackgroundImage = global::SAI.Properties.Resources.btnAccordionClosed;
			}
			else
			{
				fpMachineLearning.Visible = true;
				pAccodionLearningMachine.BackgroundImage = global::SAI.Properties.Resources.btnAccordionShow;
			}
		}

		private void resultCheckEvent(DynamicUcPracticeBlockCode parent, EventHandler<BlockEventArgs> AddBlockButtonClicked)
		{
			// 제목 클릭시 아코디언 열기/닫기
			pTitleResultCheck.Click += (s, e) => { titleResultCheckEvent(); };
			pAccodionResultCheck.Click += (s, e) => { titleResultCheckEvent(); };
			pTxtResultCheck.Click += (s, e) => { titleResultCheckEvent(); };
			// btnResultCheck
			btnResultCheck.BackColor = Color.Transparent;
			btnResultCheck.PressedColor = Color.Transparent;
			btnResultCheck.CheckedState.FillColor = Color.Transparent;
			btnResultCheck.DisabledState.FillColor = Color.Transparent;
			btnResultCheck.HoverState.FillColor = Color.Transparent;
			// btnResultCheck 클릭 될 때
			btnResultCheck.Click += (s, e) =>
			{
				btnResultCheck.BackgroundImage = Properties.Resources.btnResultGraphClicked;
				AddBlockButtonClicked?.Invoke(parent, new BlockEventArgs("resultGraph"));
			};
			// btnResultCheck 마우스 입력 될 때
			btnResultCheck.MouseEnter += (s, e) => { btnResultCheck.BackgroundImage = Properties.Resources.btnResultGraphClicked; };
			// btnResultCheck 마우스 떠날때
			btnResultCheck.MouseLeave += (s, e) => { btnResultCheck.BackgroundImage = Properties.Resources.btnResultGraph; };
		}
		private void titleResultCheckEvent()
		{
			if (fpResultCheck.Visible)
			{
				fpResultCheck.Visible = false;
				pAccodionResultCheck.BackgroundImage = global::SAI.Properties.Resources.btnAccordionClosed;
			}
			else
			{
				fpResultCheck.Visible = true;
				pAccodionResultCheck.BackgroundImage = global::SAI.Properties.Resources.btnAccordionShow;
			}
		}

		private void imgPathEvent(DynamicUcPracticeBlockCode parent, EventHandler<BlockEventArgs> AddBlockButtonClicked)
		{
			// 제목 클릭시 아코디언 열기/닫기
			pTitleImgPath.Click += (s, e) => { titleImgPathEvent(); };
			pAccodionImgPath.Click += (s, e) => { titleImgPathEvent(); };
			pTxtImgPath.Click += (s, e) => { titleImgPathEvent(); };
			// btnImgPath
			btnImgPath.BackColor = Color.Transparent;
			btnImgPath.PressedColor = Color.Transparent;
			btnImgPath.CheckedState.FillColor = Color.Transparent;
			btnImgPath.DisabledState.FillColor = Color.Transparent;
			btnImgPath.HoverState.FillColor = Color.Transparent;
			// btnImgPath 클릭 될 때
			btnImgPath.Click += (s, e) =>
			{
				btnImgPath.BackgroundImage = Properties.Resources.btnImgPathClicked;
				AddBlockButtonClicked?.Invoke(parent, new BlockEventArgs("imgPath"));
			};
			// btnImgPath 마우스 입력 될 때
			btnImgPath.MouseEnter += (s, e) => { btnImgPath.BackgroundImage = Properties.Resources.btnImgPathClicked; };
			// btnImgPath 마우스 떠날때
			btnImgPath.MouseLeave += (s, e) => { btnImgPath.BackgroundImage = Properties.Resources.btnImgPath; };
		}
		private void titleImgPathEvent()
		{
			if (fpImgPath.Visible)
			{
				fpImgPath.Visible = false;
				pAccodionImgPath.BackgroundImage = global::SAI.Properties.Resources.btnAccordionClosed;
			}
			else
			{
				fpImgPath.Visible = true;
				pAccodionImgPath.BackgroundImage = global::SAI.Properties.Resources.btnAccordionShow;
			}
		}

		private void modelInferenceEvent(DynamicUcPracticeBlockCode parent, EventHandler<BlockEventArgs> AddBlockButtonClicked)
		{
			// 제목 클릭시 아코디언 열기/닫기
			pTitleModelInference.Click += (s, e) => { titleModelInferenceEvent(); };
			pAccodionModelInference.Click += (s, e) => { titleModelInferenceEvent(); };
			pTxtModelInference.Click += (s, e) => { titleModelInferenceEvent(); };
			// btnModelInference
			btnModelInference.BackColor = Color.Transparent;
			btnModelInference.PressedColor = Color.Transparent;
			btnModelInference.CheckedState.FillColor = Color.Transparent;
			btnModelInference.DisabledState.FillColor = Color.Transparent;
			btnModelInference.HoverState.FillColor = Color.Transparent;
			// btnModelInference 클릭 될 때
			btnModelInference.Click += (s, e) =>
			{
				btnModelInference.BackgroundImage = Properties.Resources.btnModelInferenceClicked;
				AddBlockButtonClicked?.Invoke(parent, new BlockEventArgs("modelInference"));
			};
			// btnModelInference 마우스 입력 될 때
			btnModelInference.MouseEnter += (s, e) => { btnModelInference.BackgroundImage = Properties.Resources.btnModelInferenceClicked; };
			// btnModelInference 마우스 떠날때
			btnModelInference.MouseLeave += (s, e) => { btnModelInference.BackgroundImage = Properties.Resources.btnModelInference; };
		}
		private void titleModelInferenceEvent()
		{
			if (fpModelInference.Visible)
			{
				fpModelInference.Visible = false;
				pAccodionModelInference.BackgroundImage = global::SAI.Properties.Resources.btnAccordionClosed;
			}
			else
			{
				fpModelInference.Visible = true;
				pAccodionModelInference.BackgroundImage = global::SAI.Properties.Resources.btnAccordionShow;
			}
		}

		private void resultVisualizeEvent(DynamicUcPracticeBlockCode parent, EventHandler<BlockEventArgs> AddBlockButtonClicked)
		{
			// 제목 클릭시 아코디언 열기/닫기
			pTitleResultVisualize.Click += (s, e) => { titleResultVisualizeEvent(); };
			pAccodionResultVisualize.Click += (s, e) => { titleResultVisualizeEvent(); };
			pTxtResultVisualize.Click += (s, e) => { titleResultVisualizeEvent(); };
			// btnResultVisualize
			btnResultVisualize.BackColor = Color.Transparent;
			btnResultVisualize.PressedColor = Color.Transparent;
			btnResultVisualize.CheckedState.FillColor = Color.Transparent;
			btnResultVisualize.DisabledState.FillColor = Color.Transparent;
			btnResultVisualize.HoverState.FillColor = Color.Transparent;
			// btnResultVisualize 클릭 될 때
			btnResultVisualize.Click += (s, e) =>
			{
				btnResultVisualize.BackgroundImage = Properties.Resources.btnVisualizeResultClicked;
				AddBlockButtonClicked?.Invoke(parent, new BlockEventArgs("visualizeResult"));
			};
			// btnResultVisualize 마우스 입력 될 때
			btnResultVisualize.MouseEnter += (s, e) => { btnResultVisualize.BackgroundImage = Properties.Resources.btnVisualizeResultClicked; };
			// btnResultVisualize 마우스 떠날때
			btnResultVisualize.MouseLeave += (s, e) => { btnResultVisualize.BackgroundImage = Properties.Resources.btnVisualizeResult; };
		}
		private void titleResultVisualizeEvent()
		{
			if (fpResultVisualize.Visible)
			{
				fpResultVisualize.Visible = false;
				pAccodionResultVisualize.BackgroundImage = global::SAI.Properties.Resources.btnAccordionClosed;
			}
			else
			{
				fpResultVisualize.Visible = true;
				pAccodionResultVisualize.BackgroundImage = global::SAI.Properties.Resources.btnAccordionShow;
			}
		}
	}
}
