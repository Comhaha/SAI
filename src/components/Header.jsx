import { Link } from "react-router-dom";
import logo from "@/assets/images/logo.svg";
import SearchIcon from '@mui/icons-material/Search';


function Header() {
    return (
        <>
        <header className="fixed top-0 left-0 right-0 z-50 bg-white h-[80px] w-full box-border border-b border-custom-300/30">
          <nav className="flex justify-between items-center h-[80px]  max-w-[1280px] px-[30px] md:max-w-full min-[1130px]:px-[72px] box-border">
            {/* 로고 */}
            <div className="text-2xl font-bold text-custom-300">
              <Link to="/">
                <img src={logo} alt="SAI:사이" className="h-10" />
              </Link>
            </div>
            {/* 페이지 */}
            <ul className="flex justify-center items-center gap-[15px]">
              <li><Link to="/screenshot" className="text-[18px] hover:text-custom-300">Screenshot</Link></li>
              <li><Link to="/download" className="text-[18px] hover:text-custom-300">Download</Link></li>
              <li><Link to="/docs" className="text-[18px] hover:text-custom-300">Docs</Link></li>
              <li><Link to="/admin" className="text-[18px] hover:text-custom-300">Admin</Link></li>
               {/* 인풋 */}
            <li className="flex">
              <input type="text" placeholder="Search" className="w-[100px] h-[40px] px-3 rounded-l-md border border-black/20" />
              <button className="w-[40px] h-[40px] rounded-r-md bg-black/70 text-white">
                <SearchIcon/>
              </button>
            </li>
            </ul>
           
          </nav>

        </header>
      </>
    )
}                           

export default Header;