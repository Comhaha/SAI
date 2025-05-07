import { Link, useNavigate } from 'react-router-dom';
import { useToast } from '@/hooks/useToast';
import Toast from './Toast';
import useScrollToTop from '@/hooks/useScrollToTop';

function Footer() {
  const { isVisible, message, showToast } = useToast();
  const navigate = useNavigate();
  const scrollToTop = useScrollToTop();

  const handleComingSoon = (feature) => {
    showToast(`${feature} feature is coming soon!`);
  };

  const handleDownloadClick = () => {
    scrollToTop();
    navigate('/download');
  };
  const handleAdminClick = () => {
    scrollToTop();
    navigate('/admin');
  };

  return (
    <>
      <footer className="bg-white w-full h-[320px] border-t border-black/10 ">
        <div className="max-w-[1296px] mx-auto px-[30px] min-[1130px]:px-[72px] py-[40px]">
          <div className="grid grid-cols-5 max-[900px]:grid-cols-3 max-[720px]:grid-cols-2 gap-8 max-[900px]:gap-4 max-[720px]:gap-2">
            {/* SAI 섹션 */}
            <div>
              <h3 className="font-bold mb-4">SAI</h3>
              <ul className="space-y-2">
                <li>
                  <button
                    onClick={() => handleComingSoon('FAQ')}
                    className="text-gray-600 hover:text-custom-300"
                  >
                    FAQ
                  </button>
                </li>
                <li>
                  <button
                    onClick={() => handleComingSoon('License')}
                    className="text-gray-600 hover:text-custom-300"
                  >
                    License
                  </button>
                </li>
                <li>
                  <button
                    onClick={() => handleComingSoon('Privacy')}
                    className="text-gray-600 hover:text-custom-300"
                  >
                    Privacy
                  </button>
                </li>
                <li>
                  <button
                    onClick={() => handleComingSoon('Citation')}
                    className="text-gray-600 hover:text-custom-300"
                  >
                    Citation
                  </button>
                </li>
                <li>
                  <button
                    onClick={() => handleComingSoon('Contact')}
                    className="text-gray-600 hover:text-custom-300"
                  >
                    Contact
                  </button>
                </li>
              </ul>
            </div>

            {/* Download 섹션 */}
            <div>
              <h3 className="font-bold mb-4">Download</h3>
              <ul className="space-y-2">
                <li>
                  <button
                    onClick={handleDownloadClick}
                    className="text-gray-600 hover:text-custom-300 w-full text-left"
                  >
                    Windows
                  </button>
                </li>
                <li>
                  <button
                    onClick={handleDownloadClick}
                    className="text-gray-600 hover:text-custom-300 w-full text-left"
                  >
                    Mac OS
                  </button>
                </li>
                <li>
                  <button
                    onClick={handleDownloadClick}
                    className="text-gray-600 hover:text-custom-300 w-full text-left"
                  >
                    Other platforms
                  </button>
                </li>
              </ul>
            </div>

            {/* Community 섹션 */}
            <div>
              <h3 className="font-bold mb-4">Community</h3>
              <ul className="space-y-2">
                <li>
                  <a
                    href="https://twitter.com/sai"
                    target="_blank"
                    rel="noopener noreferrer"
                    className="text-gray-600 hover:text-custom-300"
                  >
                    Twitter
                  </a>
                </li>
                <li>
                  <a
                    href="https://facebook.com/sai"
                    target="_blank"
                    rel="noopener noreferrer"
                    className="text-gray-600 hover:text-custom-300"
                  >
                    Facebook
                  </a>
                </li>
                <li>
                  <a
                    href="https://stackoverflow.com/questions/tagged/sai"
                    target="_blank"
                    rel="noopener noreferrer"
                    className="text-gray-600 hover:text-custom-300"
                  >
                    Stack Exchange
                  </a>
                </li>
                <li>
                  <a
                    href="https://youtube.com/sai"
                    target="_blank"
                    rel="noopener noreferrer"
                    className="text-gray-600 hover:text-custom-300"
                  >
                    YouTube
                  </a>
                </li>
                <li>
                  <a
                    href="https://discord.gg/sai"
                    target="_blank"
                    rel="noopener noreferrer"
                    className="text-gray-600 hover:text-custom-300"
                  >
                    Discord
                  </a>
                </li>
              </ul>
            </div>

            {/* Documentation 섹션 */}
            <div>
              <h3 className="font-bold mb-4">Documentation</h3>
              <ul className="space-y-2">
                <li>
                  <button
                    onClick={() => handleComingSoon('Documentation')}
                    className="text-gray-600 hover:text-custom-300"
                  >
                    Get started
                  </button>
                </li>
                <li>
                  <button
                    onClick={() => handleComingSoon('YouTube tutorials')}
                    className="text-gray-600 hover:text-custom-300"
                  >
                    YouTube tutorials
                  </button>
                </li>
                <li>
                  <button
                    onClick={() => handleComingSoon('Example workflows')}
                    className="text-gray-600 hover:text-custom-300"
                  >
                    Example workflows
                  </button>
                </li>
                <li>
                  <button
                    onClick={() => handleComingSoon('Widgets')}
                    className="text-gray-600 hover:text-custom-300"
                  >
                    Widgets
                  </button>
                </li>
                <li>
                  <button
                    onClick={() => handleComingSoon('Scripting')}
                    className="text-gray-600 hover:text-custom-300"
                  >
                    Scripting
                  </button>
                </li>
              </ul>
            </div>

            {/* Developers 섹션 */}
            <div>
              <h3 className="font-bold mb-4">Developers</h3>
              <ul className="space-y-2">
                <li>
                  <a
                    href=""
                    target="_blank"
                    rel="noopener noreferrer"
                    className="text-gray-600 hover:text-custom-300"
                  >
                    GitHub
                  </a>
                </li>
                <li>
                  <button
                    onClick={() => handleComingSoon('Getting started')}
                    className="text-gray-600 hover:text-custom-300"
                  >
                    Getting started
                  </button>
                </li>
              </ul>
              <button
                onClick={() => handleComingSoon('Donate')}
                className="mt-6 inline-block px-6 py-3 bg-custom-300 text-white rounded-lg
                          hover:bg-opacity-90 transition-all shadow-md max-[900px]:mx-auto max-[720px]:mx-auto"
              >
                Donate to SAi
              </button>
            </div>
          </div>

          <div className="mt-12 pt-8 border-t border-gray-100 text-center text-gray-600 max-[720px]:mt-6 max-[720px]:pt-4">
            <p>Copyright © SAi. All rights reserved.</p>
          </div>
        </div>
      </footer>
      <Toast isVisible={isVisible} message={message} />
    </>
  );
}

export default Footer;
