import { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import logo from '@/assets/images/logo.svg';
import MenuIcon from '@mui/icons-material/Menu';
import CloseIcon from '@mui/icons-material/Close';
import useScrollToTop from '@/hooks/useScrollToTop';

function Header() {
  const [menuOpen, setMenuOpen] = useState(false);
  const navigate = useNavigate();
  const scrollToTop = useScrollToTop();

  const handleNav = (path) => {
    scrollToTop();
    navigate(path);
    setMenuOpen(false);
  };

  return (
    <>
      <header className="fixed top-0 left-0 right-0 z-50 bg-white h-[80px] w-full box-border border-b border-custom-300/30 shadow">
        <nav className="flex justify-between items-center h-[80px] max-w-[1280px] mx-auto px-[30px] min-[1130px]:px-[72px]">
          {/* 로고 */}
          <div className="flex items-center">
            <button
              onClick={() => handleNav('/')}
              className="bg-transparent border-none p-0 m-0 cursor-pointer"
            >
              <img src={logo} alt="SAI:사이" className="h-10" />
            </button>
          </div>
          {/* 데스크탑 네비게이션 */}
          <ul className="flex items-center gap-6 max-[900px]:hidden">
            <li>
              <button
                onClick={() => handleNav('/download')}
                className="text-[18px] hover:text-custom-300 font-medium bg-transparent border-none cursor-pointer"
              >
                Download
              </button>
            </li>
            <li>
              <button
                onClick={() => {
                  scrollToTop();
                  navigate('/', { state: { scrollTo: 'content2' } });
                  setMenuOpen(false);
                }}
                className="text-[18px] hover:text-custom-300 font-medium bg-transparent border-none cursor-pointer"
              >
                Screenshots
              </button>
            </li>
          </ul>
          {/* 모바일 햄버거 버튼 */}
          <button
            className="hidden max-[900px]:flex items-center justify-center w-12 h-12 rounded bg-[#2878BE] hover:bg-[#4096d7] transition-colors"
            onClick={() => setMenuOpen(true)}
            aria-label="Open menu"
            style={{ zIndex: 60 }}
          >
            <MenuIcon style={{ color: 'white', fontSize: 32 }} />
          </button>
        </nav>
        {/* 모바일 오버레이 메뉴 */}
        {menuOpen && (
          <div className="fixed inset-0 z-[100] bg-black/60 flex flex-col">
            <div className="absolute top-0 right-0 w-3/4 max-w-xs h-full bg-white shadow-lg flex flex-col p-8 animate-slide-in">
              <button
                className="absolute top-4 right-4"
                onClick={() => setMenuOpen(false)}
                aria-label="Close menu"
              >
                <CloseIcon style={{ fontSize: 32 }} />
              </button>
              <nav className="flex flex-col gap-8 mt-12">
                <button
                  onClick={() => handleNav('/download')}
                  className="text-xl font-bold text-gray-800 hover:text-[#FF8000] bg-transparent border-none text-left cursor-pointer"
                >
                  Download
                </button>
                <button
                  onClick={() => {
                    scrollToTop();
                    navigate('/', { state: { scrollTo: 'content2' } });
                    setMenuOpen(false);
                  }}
                  className="text-xl font-bold text-gray-800 hover:text-[#FF8000] bg-transparent border-none text-left cursor-pointer"
                >
                  Screenshots
                </button>
              </nav>
            </div>
            {/* 클릭시 메뉴 닫기용 오버레이 */}
            <div className="flex-1" onClick={() => setMenuOpen(false)} />
          </div>
        )}
      </header>
      {/* 모바일 메뉴 슬라이드 애니메이션 */}
      <style>
        {`
          @keyframes slide-in {
            from { transform: translateX(100%); }
            to { transform: translateX(0); }
          }
          .animate-slide-in {
            animation: slide-in 0.25s cubic-bezier(0.4,0,0.2,1);
          }
        `}
      </style>
    </>
  );
}

export default Header;
