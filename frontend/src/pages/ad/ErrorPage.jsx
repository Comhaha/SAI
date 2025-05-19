import background from '@/assets/images/background.svg';
import installImg from '@/assets/images/install.svg';

function ErrorPage() {
  return (
    <div className="w-full h-[710px] relative overflow-hidden">
      <img
        src={background}
        alt="background"
        className="w-full h-full object-cover absolute top-0 left-0 z-0"
      />
      <div className="relative z-10 flex flex-col justify-center items-center w-full h-full">
        <img src={installImg} alt="install image" className="h-[200px] w-auto mb-8" />
        <div className="text-center text-2xl font-bold text-gray-800 font-['Noto_Sans_KR']">
          존재하지 않는 페이지입니다!
        </div>
      </div>
    </div>
  );
}

export default ErrorPage; 