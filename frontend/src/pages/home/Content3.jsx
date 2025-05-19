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
    <div className="w-full flex flex-col gap-24 py-16 max-w-[1296px] mx-auto max-[900px]:gap-16 max-[900px]:py-8">
      <motion.div {...fadeInUp1}>
        <Card2
          title="기초 튜토리얼"
          description="AI 블록코딩의 기초를 배우는 튜토리얼입니다. 데이터 전처리부터 모델 학습까지 단계별로 학습하며, 실무에서 바로 활용할 수 있는 실력을 키워보세요."
          image={content11}
          alt="기초 튜토리얼"
          imagePosition="right"
        >
          <div className="flex gap-4 text-[15px] font-medium font-['Noto_Sans_KR']">
            <a href="/download" className="text-[#2878BE] hover:underline flex items-center">
              튜토리얼 시작하기
              <FontAwesomeIcon
                icon={faChevronRight}
                className="ml-1 text-[#2878BE] text-[8px] align-middle"
              />
            </a>
          </div>
        </Card2>
      </motion.div>
      <hr className="border-gray-200" />
      <motion.div {...fadeInUp2}>
        <Card2
          title="실전 연습"
          description="더 복잡한 AI 모델을 구축하는 실전 연습입니다. 다양한 알고리즘과 최적화 기법을 배우고, 실제 프로젝트에 적용해보세요."
          image={content22}
          alt="실전 연습"
          imagePosition="left"
        >
          <a
            href="/download"
            className="text-[#2878BE] hover:underline text-[15px] font-medium font-['Noto_Sans_KR'] w-fit flex items-center"
          >
            심화학습 시작하기
            <FontAwesomeIcon
              icon={faChevronRight}
              className="ml-1 text-[#2878BE] text-[8px] align-middle"
            />
          </a>
        </Card2>
      </motion.div>
      <hr className="border-gray-200" />
      <motion.div {...fadeInUp3}>
        <Card2
          title="보고서 작성"
          description="학습한 내용을 바탕으로 보고서를 작성해보세요. 데이터 분석 결과와 모델 성능을 체계적으로 정리하고, 시각화하여 발표할 수 있습니다."
          image={content23}
          alt="보고서 작성"
          imagePosition="right"
        >
          <a
            href="/download"
            className="text-[#2878BE] hover:underline text-[15px] font-medium font-['Noto_Sans_KR'] w-fit flex items-center"
          >
            보고서 작성하기
            <FontAwesomeIcon
              icon={faChevronRight}
              className="ml-1 text-[#2878BE] text-[8px] align-middle"
            />
          </a>
        </Card2>
      </motion.div>
    </div>
  );
}

export default Content3;
