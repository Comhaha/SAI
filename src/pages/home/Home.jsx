import Content1 from "./Content1";
import Content2 from "./Content2";
import Content3 from "./Content3";
import Content4 from "./Content4";
import Content5 from "./Content5";

function Home() {

    return (
        <div className="flex flex-col w-full">
            <div className="flex w-full"><Content1 /></div>
            <div className="flex flex-col w-full max-w-[1280px] min-w-[480px] mx-auto px-[15px] min-[1130px]:px-[72px] min-[900px]:px-[30px] min-[480px]:px-[15px] my-5">    
                <Content2 />
                <hr className="my-5"/>
                <Content3 />
                <Content4 />
                <Content5 />
            </div>
         
        
        </div>
        
    )
}                           

export default Home;
