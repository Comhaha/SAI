import background from '@/assets/images/background.svg';
import loginImg from '@/assets/images/login_ch.svg';
import { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';
import { useNavigate } from 'react-router-dom';
import { useAuth } from './AuthContext.jsx';
import apiClient from '@/api/apiClient';

function Login() {
  const [show, setShow] = useState(false);
  const [value, setValue] = useState('');
  const [error, setError] = useState('');
  const navigate = useNavigate();
  const { setIsLoggedIn } = useAuth();

  const handleLogin = async () => {
    setError('');
    try {
      // 1. POST 로그인
      const res = await apiClient.post('/api/admin/login', { password: value });
      if (res.data.isSuccess && res.data.result) {
        // 2. 세션 확인
        const sessionRes = await apiClient.get('/api/admin/ping');
        if (sessionRes.data.isSuccess && sessionRes.data.result) {
          setIsLoggedIn(true);
          navigate('/admin');
        } else {
          setError('Server is unstable. Please try again.');
          setTimeout(() => setError(''), 2000);
        }
      } else {
        setError('Login failed. Please check your password.');
        setTimeout(() => setError(''), 2000);
      }
    } catch (err) {
      setError('Server is unstable. Please try again.');
      setTimeout(() => setError(''), 2000);
    }
  };

  return (
    <div className="w-full h-[710px] flex justify-center items-center relative overflow-hidden">
      <img
        src={background}
        alt="background"
        className="w-full h-full object-cover absolute top-0 left-0 z-0"
      />
      <div className="relative z-10 flex flex-col justify-center items-center w-full h-full">
        <div className="flex flex-col justify-between items-center w-[626px] h-[391px]">
          {/* Title */}
          <div className="flex items-start mb-8">
            <img src={loginImg} alt="login" className="h-[80px] w-[80px] mr-4 mt-2" />
            <span
              className="block text-[50px] font-bold font-['Noto_Sans_KR'] text-black"
            >
              Admin Login
            </span>
          </div>
          {/* Input */}
          <div className="relative w-full flex items-center justify-center mb-8 w-[614px]">
            <input
              type={show ? 'text' : 'password'}
              value={value}
              onChange={e => setValue(e.target.value)}
              placeholder="Admin key"
              maxLength={100}
              className="px-4 w-[614px] h-[52px] rounded-[4px] shadow-[4px_4px_0px_#000] text-[20px] font-['Noto_Sans_KR'] font-bold border-2 border-black outline-none placeholder:font-bold placeholder:text-black/50 placeholder:text-[24px] placeholder:font-['Noto_Sans_KR']"
            />
            <button
              type="button"
              onClick={() => setShow(v => !v)}
              className="absolute right-4 top-1/2 -translate-y-1/2 text-2xl text-black focus:outline-none"
              tabIndex={-1}
              aria-label={show ? "Hide password" : "Show password"}
            >
              <FontAwesomeIcon icon={show ? faEye : faEyeSlash} />
            </button>
          </div>
          {/* Error (input 칸 위치 고정) */}
          <div className="h-5 flex items-center justify-center mb-2">
            {error && <span className="text-red-600 text-sm">{error}</span>}
          </div>
          {/* Button */}
          <button
            className="rounded-[4px] font-bold w-[141px] h-[57px] bg-[#677489] text-[#FFFCEF] text-[20px] font-['Noto_Sans_KR'] shadow-[4px_4px_0px_#000]"
            onClick={handleLogin}
          >
            Login
          </button>
        </div>
      </div>
    </div>
  );
}

export default Login;

