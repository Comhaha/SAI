namespace SAI.App.Models
{
    public class SettingModel
    {
        private static readonly SettingModel instance = new SettingModel();
        public static SettingModel Instance => instance;

        private SettingModel() { }

        public bool IsLightTheme { get; set; } = true;
        public bool IsDarkTheme { get; set;} = true;
        public string ModelPath { get; set; } = System.Windows.Forms.Application.StartupPath;
    }
}
