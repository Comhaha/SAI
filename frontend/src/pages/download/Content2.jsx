import DownloadIcon from '@mui/icons-material/Download';
import { useToast } from '../../hooks/useToast';
import Toast from '../../components/Toast';
import installImg from '../../assets/images/install.svg';
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

  const handleToast = (msg) => {
    showToast(msg);
  };

  return (
    <>
      <section className="w-full py-16 px-4 bg-transparent flex justify-center">
        <div className="w-full max-w-[1200px] flex flex-col md:flex-row gap-8">
          {/* 왼쪽: 다운로드 안내 */}
          <div className="flex-1 min-w-0">
            <h2 className="text-3xl font-bold mb-8">Windows</h2>
            <div className="space-y-8">
              <div className="bg-white p-6 rounded-lg shadow-md">
                <h3 className="text-xl font-semibold mb-4">Standalone installer (default)</h3>
                <button
                  className="flex items-center gap-2 text-custom-300 hover:text-opacity-80"
                  onClick={handleWindowsDownload}
                  disabled={loading}
                >
                  <DownloadIcon />
                  {loading ? 'Preparing download...' : 'SAI-3.38.1-Minforge-x86_64.exe'}
                </button>
                <p className="text-gray-600 mt-2">Can be used without administrative privileges.</p>
              </div>
              <div className="bg-white p-6 rounded-lg shadow-md">
                <h3 className="text-xl font-semibold mb-4">Portable SAI</h3>
                <button
                  className="flex items-center gap-2 text-custom-300 hover:text-opacity-80"
                  onClick={() =>
                    handleToast('Windows Portable version download will be available soon!')
                  }
                >
                  <DownloadIcon /> SAI-3.38.1.zip
                </button>
                <p className="text-gray-600 mt-2">
                  No installation needed. Just extract the archive and open the shortcut.
                </p>
              </div>
            </div>

            {/* macOS 섹션 */}
            <h2 className="text-3xl font-bold mb-8 mt-16">macOS</h2>
            <div className="space-y-8">
              <div className="bg-white p-6 rounded-lg shadow-md">
                <h3 className="text-xl font-semibold mb-4">SAI for Apple silicon</h3>
                <button
                  className="flex items-center gap-2 text-custom-300 hover:text-opacity-80"
                  onClick={() =>
                    handleToast('macOS Apple Silicon version download will be available soon!')
                  }
                >
                  <DownloadIcon /> SAI-3.38.1-Python3.11.8-arm64.dmg
                </button>
              </div>
              <div className="bg-white p-6 rounded-lg shadow-md">
                <h3 className="text-xl font-semibold mb-4">SAI for Intel</h3>
                <button
                  className="flex items-center gap-2 text-custom-300 hover:text-opacity-80"
                  onClick={() =>
                    handleToast('macOS Intel version download will be available soon!')
                  }
                >
                  <DownloadIcon /> SAI-3.38.1-Python3.10.11-x86_64.dmg
                </button>
              </div>
            </div>

            {/* Other platforms 섹션 */}
            <h2 className="text-3xl font-bold mb-8 mt-16">Other platforms</h2>
            <div className="space-y-8">
              <div className="bg-white p-6 rounded-lg shadow-md">
                <h3 className="text-xl font-semibold mb-4">Anaconda</h3>
                <p className="text-gray-600 mb-4">
                  Create and activate a conda environment for SAI (optional, but recommended)
                </p>
                <pre className="bg-gray-100 p-4 rounded-lg overflow-x-auto">
                  <code>
                    conda create python=3.10 --yes --name sai{`\n`}
                    conda activate sai
                  </code>
                </pre>
              </div>
              <div className="bg-white p-6 rounded-lg shadow-md">
                <h3 className="text-xl font-semibold mb-4">Pip</h3>
                <p className="text-gray-600 mb-4">SAI can also be installed from PyPI.</p>
                <pre className="bg-gray-100 p-4 rounded-lg overflow-x-auto">
                  <code>pip install sai</code>
                </pre>
              </div>
            </div>
          </div>

          {/* 오른쪽: 네모 div (사진 + install 문구) */}
          <div className="flex-shrink-0 w-full md:w-[350px] lg:w-[400px] bg-white rounded-lg shadow-md p-8 flex flex-col items-center justify-center mt-12 md:mt-0 h-[400px] max-[900px]:hidden">
            <img src={installImg} alt="install" className="w-[150px] h-auto mb-6" />
            <h3 className="text-2xl font-bold mb-2 text-gray-800 text-center">
              Installing add-ons
            </h3>
            <p className="text-gray-600 text-center">
              Additional features can be added to SAI by installing add-ons.
              <br />
              You can find add-on manager in Options menu.
            </p>
          </div>
        </div>
      </section>
      <Toast isVisible={isVisible} message={message} />
    </>
  );
}

export default Content2;
