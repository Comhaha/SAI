import background from "@/assets/images/background.svg";

function Content1() {
    return (
        <div className="w-full h-[630px] max-[1130px]:h-[440px] max-[920px]:h-[360px] max-[720px]:h-[720px] ">
            <img src={background} alt="background" className="w-full h-full object-cover" />
        </div>
    )
}       

export default Content1;
