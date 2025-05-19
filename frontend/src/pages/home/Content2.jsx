import content11_1 from '@/assets/images/content1-1-1.svg';
import content11_2 from '@/assets/images/content1-1-2.svg';
import content12 from '@/assets/images/content1-2.svg';
import content13 from '@/assets/images/content1-3.svg';
import content14 from '@/assets/images/content1-4.svg';
import { motion } from "motion/react";
import { useFadeInUp } from "@/hooks/useFadeInUp";
import React, { useState } from 'react';

function Content2() {
  const fadeInUpDesc = useFadeInUp(0.1);
  const fadeInUpImg = useFadeInUp(0.2);
  const [modalImg, setModalImg] = useState(null);

  // 마우스 올리면 모달, 내리면 닫힘
  const handleMouseEnter = (imgSrc) => setModalImg(imgSrc);
  const handleMouseLeave = () => setModalImg(null);

  return (
    <div id="content2" className="w-full flex justify-between items-start gap-8 py-16 max-w-[1296px] mx-auto max-[900px]:flex-col">
      {/* 왼쪽 영역 */}
      <motion.div {...fadeInUpDesc} className="flex flex-col w-1/2 max-[900px]:w-full h-full mt-8">
        <div className="text-[40px] font-bold font-['Noto_Sans_KR'] mb-2 max-[900px]:text-[28px] transition-colors duration-200">
          AI 학습이 어려우시다구요?
        </div>
        <div className="text-[18px] font-medium font-['Noto_Sans_KR'] text-gray-600 mb-4 max-[900px]:text-[14px]">
          SAI가 여러분의 AI 학습을 도와드립니다. 복잡한 코드 없이 블록 코딩으로 AI를 쉽게 배우고 활용할 수 있어요. 지금 바로 시작해보세요!
        </div>
        <div className="flex flex-col gap-2 flex-1 items-stretch">
          <motion.img
            {...fadeInUpImg}
            src={content11_1}
            alt="AI 곡선 그래프 1"
            className="w-full max-w-[400px] rounded shadow object-contain aspect-[4/3]"
            style={{ minHeight: 0 }}
          />
          <motion.img
            {...fadeInUpImg}
            src={content11_2}
            alt="AI 곡선 그래프 2"
            className="w-full max-w-[400px] rounded shadow object-contain aspect-[4/3]"
            style={{ minHeight: 0 }}
          />
        </div>
      </motion.div>

      {/* 오른쪽 영역: 세 개 카드 모두 남김, 불필요한 배경/겹침 없음 */}
      <motion.div {...fadeInUpImg} className="flex flex-col w-1/2 gap-2 justify-between h-full max-[900px]:w-full max-[900px]:gap-1">
        {/* AI 라벨링 카드 */}
        <div className="p-0 flex flex-row items-center gap-8 max-[600px]:flex-col max-[600px]:items-start">
          <div className="flex-1 min-w-0">
            <div className="text-[22px] font-bold text-black mb-2">AI 라벨링</div>
            <div className="text-[16px] text-gray-600">데이터에 의미를 부여하는 첫 단계입니다. SAI의 직관적인 인터페이스로 누구나 쉽게 데이터를 분류하고 라벨링할 수 있어요.</div>
          </div>
          <img 
            src={content12} 
            alt="AI 라벨링" 
            className="w-56 h-56 object-contain aspect-square" 
          />
        </div>
        {/* AI 학습 카드 */}
        <div className="p-0 flex flex-row items-center gap-8 max-[600px]:flex-col max-[600px]:items-start">
          <div className="flex-1 min-w-0">
            <div className="text-[22px] font-bold text-black mb-2">AI 학습</div>
            <div className="text-[16px] text-gray-600">라벨링된 데이터로 AI 모델을 학습시켜보세요. SAI가 최적의 학습 방법을 제안하고, 실시간으로 학습 과정을 확인할 수 있습니다.</div>
          </div>
          <img 
            src={content13} 
            alt="AI 학습" 
            className="w-56 h-56 object-contain aspect-square" 
          />
        </div>
        {/* 결과 추출 카드 */}
        <div className="p-0 flex flex-row items-center gap-8 max-[600px]:flex-col max-[600px]:items-start">
          <div className="flex-1 min-w-0">
            <div className="text-[22px] font-bold text-black mb-2">결과 추출</div>
            <div className="text-[16px] text-gray-600">학습된 AI 모델의 결과를 쉽게 확인하고 활용할 수 있습니다. SAI가 자동으로 분석 결과를 시각화하여 보여드려요.</div>
          </div>
          <img 
            src={content14} 
            alt="결과 추출" 
            className="w-56 h-56 object-contain aspect-square" 
          />
        </div>
      </motion.div>
    </div>
  );
}

export default Content2;
