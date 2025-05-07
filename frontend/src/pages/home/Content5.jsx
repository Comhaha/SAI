import background from '@/assets/images/background.svg';
import { Link, useNavigate } from 'react-router-dom';
import useScrollToTop from '@/hooks/useScrollToTop';

function Content5() {
  const navigate = useNavigate();
  const scrollToTop = useScrollToTop();

  const handleClick = () => {
    scrollToTop();
    navigate('/admin');
  };

  return (
    <div className="w-full h-[450px] relative overflow-hidden flex items-center justify-center">
      {/* Background image */}
      <img
        src={background}
        alt="background"
        className="absolute top-0 left-0 w-full h-full object-cover z-0"
      />
      {/* Foreground content */}
      <div className="relative z-10 w-full flex flex-col items-center justify-center">
        <div className="text-[40px] font-bold font-['Noto_Sans_KR'] text-gray-900 mb-4 text-center max-[720px]:text-[28px]">
          Contribute to SAi
        </div>
        <div className="text-[18px] font-medium text-gray-700 mb-7 text-center max-w-[600px]">
          If you love using SAI and want to support us, a donation would be greatly appreciated.
          <br />
          Your support helps us fix bugs, develop new features, provide free educational content,
          and maintain our infrastructure.
          <br />
          Thank you for helping us make SAI better for everyone!
        </div>
        <button
          onClick={handleClick}
          className="bg-blue-500 text-white px-6 py-2 rounded font-medium shadow hover:bg-blue-300 transition-colors text-[18px]"
        >
          Donate
        </button>
      </div>
    </div>
  );
}

export default Content5;
