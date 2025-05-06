import React from 'react';
import { Link } from 'react-router-dom';

function Card({ title, description, image, alt, borderBottom = true, link = '#' }) {
  return (
    <div className={`flex gap-4 items-start ${borderBottom ? 'border-b pb-4' : ''} max-[720px]:flex-col max-[720px]:items-center`}>
      <div className="w-2/3 flex flex-col justify-between max-[720px]:w-full">
        <Link to={link} className="w-fit group">
          <div
            className="text-[23px] font-bold font-['Noto_Sans_KR'] mb-1 max-[900px]:text-[16px] cursor-pointer transition-colors duration-200 group-hover:text-[#2878BE]"
          >
            {title}
          </div>
        </Link>
        <div className="text-[18px] font-medium font-['Noto_Sans_KR'] text-gray-600 mb-2 max-[720px]:text-[14px]">
          {description}
        </div>
      </div>
      <div className="w-1/3 flex-shrink-0 h-[180px] max-[720px]:w-full max-[720px]:mx-auto flex items-center justify-center bg-gray-200 shadow-md select-none">
        <img src={image} alt={alt} className="w-full h-full object-cover rounded" />
      </div>
    </div>
  );
}

export default Card; 