using SAI.SAI.App.Models;

namespace SAI.SAI.Application.Service
{
    public static class ServiceFactory
    {
        // EC2 서버 URL - 실제 배포시 변경 필요
        private const string EC2_SERVER_URL = "http://54.180.168.49:8000";
        // 로컬 개발 서버 URL
        private const string LOCAL_SERVER_URL = "http://127.0.0.1:8082";

        /// <summary>
        /// 현재 GPU 설정에 따라 적절한 추론 서비스를 반환합니다.
        /// </summary>
        /// <returns>GpuType.Local이면 PythonService, GpuType.Server이면 ApiService</returns>
        public static IInferenceService CreateInferenceService()
        {
            var gpuType = BlocklyModel.Instance?.gpuType ?? GpuType.Local;
            
            switch (gpuType)
            {
                case GpuType.Local:
                    return new PythonService();
                case GpuType.Server:
                    return new ApiService(LOCAL_SERVER_URL);  // 로컬 서버 URL 사용
                default:
                    return new PythonService(); // 기본값
            }
        }

        /// <summary>
        /// 서버 URL을 변경합니다 (개발/테스트용)
        /// </summary>
        /// <param name="url">서버 URL</param>
        /// <returns>ApiService 인스턴스</returns>
        public static IInferenceService CreateApiService(string url)
        {
            return new ApiService(url);
        }

        /// <summary>
        /// 로컬 Python 서비스를 강제로 생성합니다
        /// </summary>
        /// <returns>PythonService 인스턴스</returns>
        public static IInferenceService CreatePythonService()
        {
            return new PythonService();
        }
    }
} 