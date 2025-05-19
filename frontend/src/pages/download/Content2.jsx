import DownloadIcon from '@mui/icons-material/Download';
import { useToast } from '../../hooks/useToast';
import Toast from '../../components/Toast';
import apiClient from '@/api/apiClient';
import { useState } from 'react';

function Content2() {
  const { isVisible, message, showToast } = useToast();
  const [loading, setLoading] = useState(false);

  const handleWindowsDownload = async () => {
    setLoading(true);
    try {
      const res = await apiClient.get('/api/download');
      if (res.data.isSuccess && res.data.result) {
        window.location.href = res.data.result;
      } else {
        showToast(res.data.message || '다운로드 링크를 가져오지 못했습니다.');
      }
    } catch (err) {
      showToast('The server response is delayed. Please try again in a moment.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <section className="w-full py-16 px-4 bg-transparent flex justify-center">
      <div className="flex w-full max-w-[1200px] mx-auto gap-8">
        <div className="flex-1 min-w-0 flex flex-col">
          <h2 className="text-3xl font-bold mb-8 text-left">Windows</h2>
          <div className="bg-white p-6 rounded-lg shadow-md w-full">
            <h3 className="text-xl font-semibold mb-4">Standalone installer (default)</h3>
            <button
              className="flex items-center gap-2 text-custom-300 hover:text-opacity-80"
              onClick={handleWindowsDownload}
              disabled={loading}
            >
              <DownloadIcon />
              {loading ? '다운로드 준비 중...' : 'SAI-3.38.1-Minforge-x86_64.exe'}
            </button>
            <p className="text-gray-600 mt-2">관리자 권한 없이 사용할 수 있습니다.</p>
          </div>
        </div>
      </div>
      <Toast isVisible={isVisible} message={message} />
    </section>
  );
}

export default Content2;
