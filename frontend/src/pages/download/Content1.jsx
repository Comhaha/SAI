import background from '@/assets/images/background.svg';
import character from '@/assets/images/download_ch.svg';

function Content1() {
  return (
    <div className="w-full h-[365px] pt-[80px] relative overflow-hidden">
      <img
        src={background}
        alt="background"
        className="w-full h-full object-cover absolute top-0 left-0 z-0"
      />
      <div className="relative z-10 flex justify-center items-center w-full h-full max-w-[1200px] mx-auto px-8 gap-x-12">
        <div className="flex flex-col justify-center h-full max-w-[420px]">
          <h1 className="text-[40px] font-bold leading-tight text-gray-800 mb-4">
            Suggested Download
          </h1>
          <button className="mt-[50px] text-white text-[18px] font-medium px-6 py-2 rounded shadow transition-all w-fit bg-[#2878BD] hover:bg-[#4F96D3]">
            SAi 3.38.1 for Windows
          </button>
        </div>
        <img
          src={character}
          alt="character"
          className="w-[422px] h-auto object-contain drop-shadow-xl max-[720px]:hidden translate-y-[20%]"
        />
      </div>
    </div>
  );
}

export default Content1;
