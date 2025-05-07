import React from 'react';

function Card2({ title, description, image, alt, imagePosition = 'right', children }) {
  // imagePosition: 'left' | 'right'
  const flexDirection = imagePosition === 'left' ? 'flex-row-reverse' : 'flex-row';
  // Always stack text above image on mobile
  const responsiveDirection = 'max-[720px]:flex-col';
  return (
    <div className={`flex ${flexDirection} gap-8 items-start ${responsiveDirection} max-[720px]:items-center w-full`}>
      <div className="flex-1 flex flex-col justify-center max-[720px]:w-full">
        <div className="text-[40px] font-bold font-['Noto_Sans_KR'] text-black mb-2 max-[720px]:text-[28px]">{title}</div>
        <div className="text-[18px] font-medium font-['Noto_Sans_KR'] text-gray-600 mb-2 max-[720px]:text-[14px]">{description}</div>
        {children && <div className="mt-2">{children}</div>}
      </div>
      <div className="flex-shrink-0 w-[340px] h-[180px] max-[720px]:w-full max-[720px]:mx-auto flex items-center justify-center bg-gray-200 shadow-md select-none">
        <img src={image} alt={alt} className="w-full h-full object-cover rounded" />
      </div>
    </div>
  );
}

export default Card2; 