import { useEffect, useRef, useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCopy as faRegCopy } from '@fortawesome/free-regular-svg-icons';
import { faArrowsRotate } from '@fortawesome/free-solid-svg-icons';
import loginImg from '@/assets/images/login_ch.svg';
import background from '@/assets/images/background.svg';
import apiClient from '@/api/apiClient';
import { useNavigate } from 'react-router-dom';

function formatTime(sec) {
  const h = String(Math.floor(sec / 3600)).padStart(1, '0');
  const m = String(Math.floor((sec % 3600) / 60)).padStart(2, '0');
  const s = String(sec % 60).padStart(2, '0');
  return `${h}:${m}:${s}`;
}

const INIT_TIME = 3 * 60 * 60;

function AdminRe() {
  const [time, setTime] = useState(INIT_TIME); // 3 hours in seconds
  const [copied, setCopied] = useState(false);
  const [apiKey, setApiKey] = useState('');
  const timerRef = useRef();
  const navigate = useNavigate();

  useEffect(() => {
    timerRef.current = setInterval(() => {
      setTime(t => (t > 0 ? t - 1 : 0));
    }, 1000);
    return () => clearInterval(timerRef.current);
  }, []);

  useEffect(() => {
    // API Key 요청
    apiClient.get('/api/token')
      .then(res => {
        if (res.data && res.data.result) {
          setApiKey(res.data.result.token);
        } else {
          navigate('/admin/login');
        }
      })
      .catch(() => {
        navigate('/admin/login');
      });
  }, [navigate]);

  const handleCopy = () => {
    navigator.clipboard.writeText(apiKey);
    setCopied(true);
    setTimeout(() => setCopied(false), 1200);
  };

  const handleReset = async () => {
    setTime(INIT_TIME);
    try {
      const res = await apiClient.post('/api/token/reload');
      if (res.data && res.data.isSuccess && res.data.result && res.data.result.token) {
        setApiKey(res.data.result.token);
      } else {
        navigate('/admin/login');
      }
    } catch {
      navigate('/admin/login');
    }
  };

  return (
    <div className="w-full flex justify-center items-center relative overflow-hidden h-[630px] pt-20 box-border">
      <img
        src={background}
        alt="background"
        className="w-full h-full object-cover absolute top-0 left-0 z-0"
      />
      {/* Timer: 상단 고정 */}
      <div
        className="absolute left-1/2 -translate-x-1/2 z-20 flex items-center justify-center w-[153px] h-[60px] bg-white shadow-[4px_4px_0px_#000] border border-black rounded-[8px] top-[85px]"
      >
        <span className="text-[25px] font-medium font-['Noto_Sans_KR'] text-black">
          {formatTime(time)}
        </span>
        <FontAwesomeIcon
          icon={faArrowsRotate}
          onClick={handleReset}
          className="ml-2 cursor-pointer text-gray-600 hover:text-gray-900 text-[22px]"
          title="Reset Timer"
        />
      </div>
      <div className="relative z-10 flex flex-col items-center justify-center w-full h-full">
        {/* Main Box */}
        <div className="flex flex-col items-center w-[768px] h-[235px] my-[45px] -mt-[5px]">
          {/* Title */}
          <div className="flex items-center justify-center w-full">
            <img src={loginImg} alt="login" className="h-[80px] w-[80px] mr-[5px] self-center mt-2" />
            <span className="text-[50px] font-bold font-['Noto_Sans_KR'] text-black">
              Secret API Key
            </span>
          </div>
          {/* Spacer */}
          <div className="h-[117px]" />
          {/* API Key Input */}
          <div className="relative flex items-center w-full">
            <input
              type="text"
              value={apiKey}
              readOnly
              className="w-[753px] h-[52px] bg-[#FEDC75] text-[#677589] font-bold text-[20px] font-['Noto_Sans_KR'] border border-black rounded-[6px] outline-none shadow-[4px_4px_0px_#000] pl-4 pr-12"
            />
            <div
              className="absolute right-4 flex items-center justify-center w-8 h-8 pr-2 cursor-pointer"
              onClick={handleCopy}
              title="Copy"
            >
              <FontAwesomeIcon icon={faRegCopy} className="text-[22px]" style={{ color: '#677589' }} />
            </div>
            {/* 복사 경고 메시지: input 아래에 영어로 */}
            {copied && (
              <div className="absolute left-1/2 -translate-x-1/2 top-full mt-2 w-full flex justify-center">
                <span className="text-xs text-red-600 font-bold">
                  Please keep your Secret API Key safe and do not share it with anyone.
                </span>
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
}

export default AdminRe;
        