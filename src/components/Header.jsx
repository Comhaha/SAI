import { ReactComponent as Logo } from '../assets/images/logo.svg';

function Header() {
  return (
    <header className="w-full h-[80px] bg-white flex items-center">
      <div className="w-full max-w-[1280px] mx-auto flex items-center justify-between h-[80px] px-[72px] box-border md:px-[30px]">
        <div className="flex items-center">
          <Logo width={32} height={32} />
        </div>
        <nav className="flex items-center gap-[15px] ml-auto">
          <a href="#" className="text-black text-[18px] font-medium font-['Noto_Sans_KR'] no-underline cursor-pointer">Screenshots</a>
          <a href="#" className="text-black text-[18px] font-medium font-['Noto_Sans_KR'] no-underline cursor-pointer">Download</a>
          <a href="#" className="text-black text-[18px] font-medium font-['Noto_Sans_KR'] no-underline cursor-pointer">Docs</a>
          <a href="#" className="text-black text-[18px] font-medium font-['Noto_Sans_KR'] no-underline cursor-pointer">Admin</a>
          <div className="flex items-center bg-[#444] rounded px-2 h-8 ml-[15px]">
            <input
              className="border-none bg-transparent text-white text-[13px] outline-none w-[60px] placeholder:text-[#ccc] placeholder:text-[11px]"
              placeholder="Search"
              type="text"
            />
            <span className="text-white text-[18px] flex items-center">
              <svg width="18" height="18" fill="none" xmlns="http://www.w3.org/2000/svg"><circle cx="8" cy="8" r="7" stroke="#fff" strokeWidth="2"/><path d="M17 17l-4-4" stroke="#fff" strokeWidth="2" strokeLinecap="round"/></svg>
            </span>
          </div>
        </nav>
      </div>
    </header>
  );
}

export default Header;