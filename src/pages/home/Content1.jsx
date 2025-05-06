import background from "@/assets/images/background.svg";
import character from "@/assets/images/character.svg";

function Content1() {
    return (
        <div className="w-full h-[630px] max-[1130px]:h-[440px] max-[920px]:h-[360px] max-[720px]:h-[720px] relative overflow-hidden">
            <img src={background} alt="background" className="w-full h-full object-cover absolute top-0 left-0 z-0" />
            <div className="relative z-10 flex justify-between items-center w-full h-full max-w-[1200px] mx-auto px-8">
                <div className="flex flex-col justify-center h-full max-w-[420px]">
                    <h1 className="text-[55px] font-bold leading-tight text-gray-800 mb-4">
                        AI Block Coding<br />Easy and Fun
                    </h1>
                    <p className="text-[21px] text-gray-700 mb-6">
                        Open source AI block coding and data<br />visualization.
                    </p>
                    <button
                        className="text-white text-[18px] font-medium px-6 py-3 rounded shadow transition-all w-fit"
                        style={{
                            background: 'linear-gradient(90deg, #2878BD 0%, #4F96D3 50%, #2878BD 100%)'
                        }}
                    >
                        Download SAi 3.38.1
                    </button>
                </div>
                <img src={character} alt="character" className="w-[592px] h-auto object-contain drop-shadow-xl" />
            </div>
        </div>
    )
}       

export default Content1;
