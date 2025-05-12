import background from '@/assets/images/background.svg';
import adminImg from '@/assets/images/admin_ch.svg';
import { Link } from 'react-router-dom';

function AdminSt() {
  return (
    <div className="w-full h-[710px] relative overflow-hidden">
      <img
        src={background}
        alt="background"
        className="w-full h-full object-cover absolute top-0 left-0 z-0"
      />
      <div className="relative z-10 flex flex-col justify-center items-center w-full h-full">
        <img src={adminImg} alt="admin" className="h-[332px] w-auto mb-8" />
        <Link
          to="/admin/login"
          className="w-[230px] h-[57px] px-8 py-2 rounded-[4px] text-white font-bold btn-shadow flex items-center justify-center text-[20px] font-['Noto_Sans_KR'] bg-[#137B76]"
        >
          Admin Login
        </Link>
      </div>
    </div>
  );
}

export default AdminSt;
