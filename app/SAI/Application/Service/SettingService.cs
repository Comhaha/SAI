using SAI.SAI.App.Models;

namespace SAI.SAI.Application.Service
{
    public class SettingService
    {
        private readonly SettingModel settingModel = SettingModel.Instance;

        public bool IsLightTheme => settingModel.IsLightTheme;
        public string ModelPath => settingModel.ModelPath;

        public void SetTheme(bool isLight)
        {
            settingModel.IsLightTheme = isLight;

            // TODO: 이곳에 실제 다크/라이트 테마 적용 로직 추가(미구현)
            // 현재 버튼 UI 바뀌는것만 적용되어 있음

        }
        public void SetModelPath(string path)
        {
            settingModel.ModelPath = path;

            // TODO: 모델 경로 저장하는 기능 추가(미구현)
        }
    }
}
