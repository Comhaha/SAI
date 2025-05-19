import content11 from '@/assets/images/content1-1.svg';
import content21 from '@/assets/images/content2-1.svg';
import content22 from '@/assets/images/content2-2.svg';
import content23 from '@/assets/images/content2-3.svg';
import Card2 from '@/components/Card2';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faChevronRight } from '@fortawesome/free-solid-svg-icons';
import { motion } from 'motion/react';
import { useFadeInUp } from '@/hooks/useFadeInUp';

function Content3() {
  const fadeInUp1 = useFadeInUp(0);
  const fadeInUp2 = useFadeInUp(0.1);
  const fadeInUp3 = useFadeInUp(0.2);
  return (
    <div className="w-full flex flex-col py-16 px-8 max-w-[1296px] mx-auto max-[900px]:py-8">
      <motion.div {...fadeInUp1}>
        <div className="flex flex-row items-start w-full max-[900px]:flex-col">
          <div className="flex-1 flex flex-col justify-center items-start pr-10 max-[900px]:pr-0">
            <div className="text-[32px] font-bold font-['Noto_Sans_KR'] text-black max-[900px]:text-[24px]">
              기초 튜토리얼
            </div>
            <div className="text-[18px] font-medium font-['Noto_Sans_KR'] text-gray-600 max-[900px]:text-[14px]">
              AI 블록코딩의 기초를 배우는 튜토리얼입니다. 데이터 전처리부터 모델 학습까지 단계별로
              학습하며, 실무에서 바로 활용할 수 있는 실력을 키워보세요.
            </div>
            <div className="flex gap-4 text-[15px] font-medium font-['Noto_Sans_KR'] mt-2">
              <a href="/download" className="text-[#2878BE] hover:underline flex items-center">
                튜토리얼 시작하기
                <FontAwesomeIcon
                  icon={faChevronRight}
                  className="ml-1 text-[#2878BE] text-[8px] align-middle"
                />
              </a>
            </div>
          </div>
          <div className="flex-shrink-0 w-1/3 max-[900px]:w-full max-[900px]:mx-auto flex items-center justify-center bg-gray-200 shadow-md select-none">
            <img
              src={content11}
              alt="기초 튜토리얼"
              className="w-full h-auto object-cover rounded-4"
            />
          </div>
        </div>
      </motion.div>
      <hr className="border-t border-gray-200 mt-5 mb-5" />
      <motion.div {...fadeInUp2}>
        <div className="flex flex-row-reverse items-start w-full max-[900px]:flex-col">
          <div className="flex-1 flex flex-col justify-center items-start pl-10 max-[900px]:pl-0">
            <div className="text-[32px] font-bold font-['Noto_Sans_KR'] text-black max-[900px]:text-[24px]">
              실전 연습
            </div>
            <div className="text-[18px] font-medium font-['Noto_Sans_KR'] text-gray-600 max-[900px]:text-[14px]">
              더 복잡한 AI 모델을 구축하는 실전 연습입니다. 다양한 알고리즘과 최적화 기법을 배우고,
              실제 프로젝트에 적용해보세요.
            </div>
            <a
              href="/download"
              className="text-[#2878BE] hover:underline text-[15px] font-medium font-['Noto_Sans_KR'] w-fit flex items-center mt-2"
            >
              심화학습 시작하기
              <FontAwesomeIcon
                icon={faChevronRight}
                className="ml-1 text-[#2878BE] text-[8px] align-middle"
              />
            </a>
          </div>
          <div className="flex-shrink-0 w-1/3 max-[900px]:w-full max-[900px]:mx-auto flex items-center justify-center bg-gray-200 shadow-md select-none">
            <img src={content22} alt="실전 연습" className="w-full h-auto object-cover rounded-4" />
          </div>
        </div>
      </motion.div>
      <hr className="border-t border-gray-200 mt-5 mb-5" />
      <motion.div {...fadeInUp3} className="mb-10">
        <div className="flex flex-row items-start w-full max-[900px]:flex-col">
          <div className="flex-1 flex flex-col justify-center items-start pr-10 max-[900px]:pr-0">
            <div className="text-[32px] font-bold font-['Noto_Sans_KR'] text-black max-[900px]:text-[24px]">
              보고서 작성
            </div>
            <div className="text-[18px] font-medium font-['Noto_Sans_KR'] text-gray-600 max-[900px]:text-[14px]">
              학습한 내용을 바탕으로 보고서를 작성해보세요. 데이터 분석 결과와 모델 성능을
              체계적으로 정리하고, 시각화하여 발표할 수 있습니다.
            </div>
            <a
              href="/download"
              className="text-[#2878BE] hover:underline text-[15px] font-medium font-['Noto_Sans_KR'] w-fit flex items-center mt-2"
            >
              보고서 작성하기
              <FontAwesomeIcon
                icon={faChevronRight}
                className="ml-1 text-[#2878BE] text-[8px] align-middle"
              />
            </a>
          </div>
          <div className="flex-shrink-0 w-1/3 max-[900px]:w-full max-[900px]:mx-auto flex items-center justify-center bg-gray-200 shadow-md select-none">
            <img
              src={content23}
              alt="보고서 작성"
              className="w-full h-auto object-cover rounded-4"
            />
          </div>
        </div>
      </motion.div>
    </div>
  );
}

export default Content3;
