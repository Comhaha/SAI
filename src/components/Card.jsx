import React from 'react';

function Card({ title, description, image, alt, borderBottom = true }) {
  return (
    <div className={`flex gap-4 items-start ${borderBottom ? 'border-b pb-4' : ''} max-[900px]:flex-row max-[900px]:items-center`}>
      <div className="flex-1 flex flex-col justify-between max-[900px]:w-2/3">
        <div className="text-[23px] font-bold font-['Noto_Sans_KR'] mb-1 max-[900px]:text-[16px]">{title}</div>
        <div className="text-[18px] font-medium font-['Noto_Sans_KR'] text-gray-600 max-[900px]:text-[12px]">
          {description}
        </div>
      </div>
      <div className="flex-shrink-0 w-[180px] max-[900px]:w-[120px]">
        <img src={image} alt={alt} className="w-full h-auto object-cover rounded" />
      </div>
    </div>
  );
}

export default Card; 