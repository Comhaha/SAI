import { useEffect, useRef, useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCopy as faRegCopy } from '@fortawesome/free-regular-svg-icons';
import { faArrowsRotate, faClock } from '@fortawesome/free-solid-svg-icons';
import loginImg from '@/assets/images/login_ch.svg';
import background from '@/assets/images/background.svg';
import apiClient from '@/api/apiClient';
import { useNavigate } from 'react-router-dom';
import RemainTimeModal from '@/components/RemainTimeModal';
import SessionExpiredModal from '@/components/SessionExpiredModal';
import { useAuth } from './AuthContext';
import AdminSt from './AdminSt';

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
  const [showRemainModal, setShowRemainModal] = useState(false);
  const [remainText, setRemainText] = useState('');
  const [guideModal, setGuideModal] = useState({ show: false, text: '' });
  const clockBtnRef = useRef(null);
  const { isLoggedIn } = useAuth();
  const [showSessionModal, setShowSessionModal] = useState(false);

  useEffect(() => {
    timerRef.current = setInterval(() => {
      setTime((t) => (t > 0 ? t - 1 : 0));
    }, 1000);
    return () => clearInterval(timerRef.current);
  }, []);

  useEffect(() => {
    // API Key 요청
    apiClient
      .get('/api/token')
      .then((res) => {
        if (res.data && res.data.result) {
          setApiKey(res.data.result.token);
        } else {
          // 비로그인 상태에서는 상위에서 AdminSt가 렌더링됨
        }
      })
      .catch(() => {
        // 비로그인 상태에서는 상위에서 AdminSt가 렌더링됨
      });
  }, [navigate]);

  useEffect(() => {
    if (!isLoggedIn) setShowSessionModal(true);
  }, [isLoggedIn]);

  const handleCopy = () => {
    navigator.clipboard.writeText(apiKey);
    setCopied(true);
    setTimeout(() => setCopied(false), 1200);
  };

  const handleReset = async () => {
    if (!isLoggedIn) {
      setShowSessionModal(true);
      return;
    }
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

  // 남은 시간 모달 핸들러
  const handleShowRemainTime = async () => {
    try {
      const res = await apiClient.get('/api/token/time');
      if (res.data && res.data.isSuccess && res.data.result) {
        setRemainText(`남은시간: ${res.data.result}`);
      } else {
        setRemainText('남은시간: 정보 없음');
      }
    } catch {
      setRemainText('남은시간: 정보 없음');
    }
    setShowRemainModal(true);
    setTimeout(() => setShowRemainModal(false), 2000);
  };

  // 안내 모달 핸들러
  const showGuide = (text) => {
    setGuideModal({ show: true, text });
    setTimeout(() => setGuideModal({ show: false, text: '' }), 2000);
  };

  // 비로그인 상태에서는 아무것도 렌더링하지 않음 (상위에서 AdminSt가 렌더링)
  if (!isLoggedIn) return null;

  return (
    <div className="w-full flex justify-center items-center relative overflow-hidden h-[630px] pt-20 box-border">
      <img
        src={background}
        alt="background"
        className="w-full h-full object-cover absolute top-0 left-0 z-0"
      />
      <div className="relative z-10 flex flex-col items-center justify-center w-full h-full">
        {/* Main Box */}
        <div className="flex flex-col items-center w-[768px] h-[235px] my-[45px] -mt-[5px] max-[720px]:w-full max-[720px]:px-0">
          {/* Title */}
          <div className="flex items-center justify-center w-full">
            <img
              src={loginImg}
              alt="login"
              className="h-[80px] w-[80px] mr-[5px] self-center mt-2"
            />
            <span className="text-[50px] font-bold font-['Noto_Sans_KR'] text-black">
              Secret Token
            </span>
          </div>
          {/* Spacer */}
          <div className="h-[60px]" />
          {/* API Key Input + Buttons */}
          <div className="relative flex items-center w-full justify-center max-[720px]:flex-col max-[720px]:items-stretch max-[720px]:px-2">
            <div className="relative flex-1 flex items-center justify-center max-[720px]:w-full">
              <div className="w-[618px] h-[60px] bg-[#FEDC75] border border-black rounded-[6px] outline-none shadow-[4px_4px_0px_#000] flex items-center justify-between font-['Noto_Sans_KR'] mx-2 max-[720px]:w-full max-[480px]:w-[90vw]">
                <span className="text-[#677589] font-bold text-[20px] pl-4 truncate select-all">
                  {apiKey}
                </span>
                <button
                  className="flex items-center justify-center w-8 h-8 mr-4 cursor-pointer"
                  onClick={handleCopy}
                  title="Copy"
                  tabIndex={-1}
                  style={{ minWidth: 32 }}
                >
                  <FontAwesomeIcon
                    icon={faRegCopy}
                    className="text-[22px]"
                    style={{ color: '#68758B' }}
                  />
                </button>
              </div>
            </div>
            {/* 버튼 2개와 모달을 같은 relative 컨테이너로 감쌈 */}
            <div className="flex max-[720px]:flex-row max-[720px]:mt-2 w-auto max-[720px]:w-full relative">
              <button
                className="w-[52px] h-[60px] flex items-center justify-center bg-[#68758B] rounded-[6px] shadow-[2px_2px_0px_0px_#000] max-[720px]:flex-1"
                style={{ marginRight: '5px' }}
                title="Timer"
                onClick={handleShowRemainTime}
                ref={clockBtnRef}
                onMouseEnter={() => showGuide('토큰 남은 시간 확인하기')}
                onMouseLeave={() => setGuideModal({ show: false, text: '' })}
              >
                <FontAwesomeIcon
                  icon={faClock}
                  className="text-[22px]"
                  style={{ color: '#FFFCEF' }}
                />
              </button>
              <button
                className="w-[52px] h-[60px] flex items-center justify-center bg-[#68758B] rounded-[6px] shadow-[2px_2px_0px_0px_#000] max-[720px]:flex-1"
                onClick={handleReset}
                title="Reset Token"
                onMouseEnter={() => showGuide('새로운 토큰 발급하기')}
                onMouseLeave={() => setGuideModal({ show: false, text: '' })}
              >
                <FontAwesomeIcon
                  icon={faArrowsRotate}
                  className="text-[22px]"
                  style={{ color: '#FFFCEF' }}
                />
              </button>
              {showRemainModal && (
                <RemainTimeModal text={remainText} className="absolute right-0 bottom-[65px]" />
              )}
              {guideModal.show && (
                <RemainTimeModal
                  text={guideModal.text}
                  className="absolute right-0 bottom-[65px]"
                />
              )}
            </div>
            {/* 복사 경고 메시지: input 아래에 영어로 */}
            {copied && (
              <div className="absolute left-1/2 -translate-x-1/2 top-full mt-2 w-full flex justify-center">
                <span className="text-xs text-red-600 font-bold">
                  복사가 완료되었습니다. 보안에 유의하세요
                </span>
              </div>
            )}
          </div>
        </div>
      </div>
      {showSessionModal && <SessionExpiredModal onClose={() => setShowSessionModal(false)} />}
    </div>
  );
}

export default AdminRe;
