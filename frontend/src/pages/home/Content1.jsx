import background from '@/assets/images/background.svg';
import character from '@/assets/images/character.svg';
import { Link } from 'react-router-dom';
import { motion } from 'motion/react';
import { useFadeInUp } from '@/hooks/useFadeInUp';

function Content1() {
  const fadeInUpTitle = useFadeInUp(0);
  const fadeInUpDesc = useFadeInUp(0.1);
  // Character: smooth up and down (one up, one down)
  const floatMotion = {
    animate: {
      y: [0, -20, 0],
    },
    transition: {
      duration: 3,
      repeat: Infinity,
      repeatType: 'loop',
      ease: 'easeInOut',
    },
  };
  return (
    <div className="w-full h-[540px] max-[1130px]:h-[440px] max-[920px]:h-[360px] max-[720px]:h-[720px] pt-[80px] relative overflow-hidden">
      <img
        src={background}
        alt="background"
        className="w-full h-full object-cover absolute top-0 left-0 z-0"
      />
      <div className="relative z-10 flex justify-between items-center w-full h-full max-w-[1200px] mx-auto px-8 max-[720px]:flex-col max-[720px]:justify-center max-[720px]:items-center">
        <div className="flex flex-col justify-center h-full max-w-[592px] max-[720px]:max-w-full">
          <motion.h1
            {...fadeInUpTitle}
            whileInView="animate"
            viewport={{ once: true, amount: 0.3 }}
            className="text-[40px] font-bold leading-tight text-gray-800 mb-4 max-[900px]:text-[28px] font-['Noto_Sans_KR']"
          >
            AI와 우리의 사이
            <br />
            블록코딩으로 가깝게!
          </motion.h1>
          <motion.p
            {...fadeInUpDesc}
            whileInView="animate"
            viewport={{ once: true, amount: 0.3 }}
            className="text-[18px] text-gray-700 mb-6 max-[900px]:text-[14px] font-['Noto_Sans_KR']"
          >
            SSAFY 교육생을 위한
            <br />
            AI 블록코딩 학습 플랫폼
          </motion.p>
          <Link to="/download" className="w-fit">
            <button
              className="text-white text-[18px] font-medium px-6 py-3 rounded shadow transition-all w-fit max-[900px]:text-[15px] max-[720px]:text-[16px] max-[720px]:mt-2"
              style={{
                background: 'linear-gradient(90deg, #2878BD 0%, #4F96D3 50%, #2878BD 100%)',
              }}
            >
              SAi 3.38.1 다운로드
            </button>
          </Link>
        </div>
        <motion.img
          src={character}
          alt="character"
          className="w-[520px] h-auto object-contain drop-shadow-xl max-[1130px]:w-1/2 max-[720px]:w-3/4 max-[720px]:my-10"
          {...floatMotion}
        />
      </div>
    </div>
  );
}

export default Content1;
