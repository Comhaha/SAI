import React from 'react';

function Card2({
  title,
  description,
  image,
  alt,
  imagePosition = 'right',
  children,
  imageClassName,
}) {
  // imagePosition: 'left' | 'right'
  const flexDirection = imagePosition === 'left' ? 'flex-row-reverse' : 'flex-row';
  // Always stack text above image on mobile
  const responsiveDirection = 'max-[720px]:flex-col';
  return (
    <div
      className={`flex ${flexDirection} gap-8 items-start max-[720px]:flex-col max-[720px]:items-center w-full`}
    >
      <div className="flex-1 flex flex-col justify-center self-start max-[720px]:w-full">
        <div className="text-[32px] font-bold font-['Noto_Sans_KR'] text-black mb-2 max-[900px]:text-[24px]">
          {title}
        </div>
        <div className="text-[18px] font-medium font-['Noto_Sans_KR'] text-gray-600 mb-2 max-[900px]:text-[14px]">
          {description}
        </div>
        {children && <div className="mt-2">{children}</div>}
      </div>
      <div className="flex-shrink-0 w-1/2 h-auto max-[720px]:w-full max-[720px]:mx-auto bg-white shadow-md select-none self-start">
        <img
          src={image}
          alt={alt}
          className={`w-full object-cover rounded-4 ${imageClassName ? imageClassName : 'h-auto'}`}
        />
      </div>
    </div>
  );
}

export default Card2;
