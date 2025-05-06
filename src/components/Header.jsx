import { Link } from "react-router-dom";
import logo from "@/assets/logo.svg";
import SearchIcon from '@mui/icons-material/Search';


function Header() {
    return (
        <>
        <header className="fixed top-0 left-0 right-0 z-50 bg-white h-[80px] w-full">
          <nav className="flex justify-between align-center max-w-[1280px] px-[72px] md:max-w-full md:px-[30px] box-border">
            {/* 로고 */}
            <div className="text-2xl font-bold text-custom-300">
              <Link to="/">
                <img src={logo} alt="SAI:사이" className="h-8" />
              </Link>
            </div>
            {/* 페이지 */}
            <ul className="flex gap-[15px]">
              <li><Link to="/screenshot" className="text-[18px] hover:text-custom-300">Screenshot</Link></li>
              <li><Link to="/download" className="text-[18px] hover:text-custom-300">Download</Link></li>
              <li><Link to="/docs" className="text-[18px] hover:text-custom-300">Docs</Link></li>
              <li><Link to="/admin" className="text-[18px] hover:text-custom-300">Admin</Link></li>
            </ul>
            {/* 인풋 */}
            <div className="flex">
              <input type="text" placeholder="Search" className="w-[100px] h-[40px] rounded-md border border-black/20" />
              <button className="w-[40px] h-[40px] rounded-md bg-black text-white">
                <SearchIcon/>
              </button>
            </div>
          </nav>

        </header>
      </>
    )
}                           

export default Header;