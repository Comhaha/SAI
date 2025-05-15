using SAI.SAI.App.Views.Interfaces;
using SAI.SAI.Application.Service;

namespace SAI.SAI.App.Presenters
{
    public class SettingPresenter
    {
        private readonly ISettingView view;
        private readonly SettingService settingService;

        public SettingPresenter(ISettingView view)
        {
            this.view = view;
            this.settingService = new SettingService();
        }

        public void OnViewLoaded()
        {
            view.SetTheme(settingService.IsLightTheme);
            view.SetPath(settingService.ModelPath);
        }

        public void OnThemeChanged(bool isLight)
        {
            settingService.SetTheme(isLight);
        }

        public void OnPathChanged(string path)
        {
            settingService.SetModelPath(path);
        }

        public void OnSave() 
        {
            view.CloseDialog();
        }

        public void OnClose()
        {
            view.CloseDialog();
        }
        public void OnCancel()
        {
            view.CloseDialog();
        }
    }
}
