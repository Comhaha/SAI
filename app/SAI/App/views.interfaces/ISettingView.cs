
namespace SAI.App.Views.Interfaces
{
    public interface ISettingView
    {
        void SetTheme(bool isLight);
        void SetPath(string path);
        bool IsLightThemeSelected();
        string GetSelectedPath();
        void CloseDialog();
    }
}
