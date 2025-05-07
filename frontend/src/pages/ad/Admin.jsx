import background from '@/assets/images/background.svg';
import adminImg from '@/assets/images/admin.svg';
import AdminModal from './AdminModal';
import { useState } from 'react';

function Admin() {
  const [open, setOpen] = useState(false);

  return (
    <div className="w-full h-[710px] relative overflow-hidden">
      <img
        src={background}
        alt="background"
        className="w-full h-full object-cover absolute top-0 left-0 z-0"
      />
      <div className="relative z-10 flex flex-col justify-center items-center w-full h-full">
        <img src={adminImg} alt="admin" className="h-[250px] w-auto mb-8" />
        <button
          className="w-[337px] h-[57px] px-8 py-2 rounded-[4px] text-white font-bold btn-shadow"
          style={{
            backgroundColor: '#69758B',
            fontSize: '20px',
            fontFamily: 'Noto Sans KR, sans-serif',
            fontWeight: 700,
          }}
          onClick={() => setOpen(true)}
        >
          Get Secret Key
        </button>
        <AdminModal open={open} onClose={() => setOpen(false)} />
      </div>
    </div>
  );
}

export default Admin;
