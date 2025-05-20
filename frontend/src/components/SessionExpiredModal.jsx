import React from 'react';
import { useNavigate } from 'react-router-dom';

function SessionExpiredModal({ onClose }) {
  const navigate = useNavigate();
  return (
    <div className="fixed inset-0 z-[2000] flex items-center justify-center bg-black/10">
      <div className="relative bg-white w-[610px] h-[350px] rounded-[8px] flex flex-col shadow-lg">
        {/* 취소(X) 버튼 */}
        <button
          className="absolute top-[26px] right-[26px] text-2xl text-gray-700 hover:text-black"
          onClick={() => {
            onClose?.();
            navigate('/');
          }}
          aria-label="닫기"
        >
          ×
        </button>
        {/* 내용 */}
        <div className="flex flex-col flex-1 px-[68px] pt-[68px]">
          <div className="text-[25px] font-bold font-['Noto_Sans_KR'] text-black mb-[47px] text-left">
            세션이 만료되었습니다!
          </div>
          <div className="text-[16px] font-bold font-['Noto_Sans_KR'] text-black text-left leading-relaxed">
            토큰 발급을 원하신다면{' '}
            <span
              className="text-[#00704A] underline cursor-pointer"
              onClick={() => {
                onClose?.();
                navigate('/admin/login');
              }}
            >
              로그인
            </span>{' '}
            버튼을 누르세요
            <br />
            취소 버튼을 누르면 홈 화면으로 돌아갑니다.
          </div>
        </div>
        {/* 버튼 영역 */}
        <div className="flex justify-center gap-[18px] mb-[37px]">
          <button
            className="w-[116px] h-[46px] bg-[#00704A] text-white rounded font-bold text-[16px] font-['Noto_Sans_KR'] flex items-center justify-center"
            onClick={() => {
              onClose?.();
              navigate('/');
            }}
          >
            홈
          </button>
          <button
            className="w-[116px] h-[46px] bg-[#00704A] text-white rounded font-bold text-[16px] font-['Noto_Sans_KR'] flex items-center justify-center"
            onClick={() => {
              onClose?.();
              navigate('/admin/login');
            }}
          >
            로그인
          </button>
        </div>
      </div>
    </div>
  );
}

export default SessionExpiredModal;
