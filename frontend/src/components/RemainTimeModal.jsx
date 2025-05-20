import React from 'react';

function RemainTimeModal({ text, className }) {
  return (
    <div
      className={`z-[9999] w-[221px] h-[40px] bg-white rounded-[8px] border border-[#222] shadow-[0_2px_8px_rgba(0,0,0,0.08)] flex items-center justify-center ${
        className || ''
      }`}
    >
      <span className="font-['Noto_Sans_KR'] font-bold text-[20px] text-[#222]">{text}</span>
    </div>
  );
}

export default RemainTimeModal;
