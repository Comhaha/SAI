import { useState } from 'react';
import one from '@/assets/screenshots/one.svg';
import two from '@/assets/screenshots/two.svg';
import three from '@/assets/screenshots/three.svg';
import four from '@/assets/screenshots/four.svg';
import five from '@/assets/screenshots/five.svg';
import six from '@/assets/screenshots/six.svg';
import background2 from '@/assets/images/background2.svg';
import ScreenshotCard from '@/components/ScreenshotCard';
import ScreenshotModal from '@/components/ScreenshotModal';

// 스크린샷 이미지 데이터
const screenshotImages = [
  { src: one, alt: 'Screenshot 1' },
  { src: two, alt: 'Screenshot 2' },
  { src: three, alt: 'Screenshot 3' },
  { src: four, alt: 'Screenshot 4' },
  { src: five, alt: 'Screenshot 5' },
  { src: six, alt: 'Screenshot 6' },
  { src: one, alt: 'Screenshot 7' },
  { src: two, alt: 'Screenshot 8' },
  { src: three, alt: 'Screenshot 9' },
  { src: four, alt: 'Screenshot 10' },
  { src: five, alt: 'Screenshot 11' },
  { src: six, alt: 'Screenshot 12' },
  { src: one, alt: 'Screenshot 13' },
  { src: two, alt: 'Screenshot 14' },
  { src: three, alt: 'Screenshot 15' },
  { src: four, alt: 'Screenshot 16' },
  { src: five, alt: 'Screenshot 17' },
  { src: six, alt: 'Screenshot 18' },
];

function Screenshots() {
  const [modalOpen, setModalOpen] = useState(false);
  const [modalIndex, setModalIndex] = useState(0);

  const handleImageClick = (idx) => {
    setModalIndex(idx);
    setModalOpen(true);
  };

  return (
    <div className="w-full min-h-[1023px] pt-24 relative overflow-hidden">
      <img
        src={background2}
        alt="background"
        className="w-full h-full object-cover absolute top-0 left-0 z-0"
      />
      <div className="relative z-10 flex flex-col items-center justify-start w-full min-h-full">
        <div className="flex flex-col w-full max-w-[1280px] min-w-[480px] mx-auto px-[15px] min-[1130px]:px-[72px] min-[900px]:px-[30px] min-[480px]:px-[15px]">
          <h1 className="text-[40px] font-bold text-center mt-[50px] mb-[50px] font-['Noto_Sans_KR']">
            Screenshots
          </h1>
          <div className="grid grid-cols-1 max-[900px]:grid-cols-2 md:grid-cols-3 gap-8 w-full max-[720px]:grid-cols-1 max-[720px]:gap-4">
            {screenshotImages.map((image, idx) => (
              <div key={idx} onClick={() => handleImageClick(idx)} className="cursor-pointer">
                <ScreenshotCard image={image} index={idx} />
              </div>
            ))}
          </div>
        </div>
        {modalOpen && (
          <ScreenshotModal
            images={screenshotImages}
            currentIndex={modalIndex}
            onClose={() => setModalOpen(false)}
          />
        )}
      </div>
    </div>
  );
}

export default Screenshots;
