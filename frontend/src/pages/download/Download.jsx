import Content1 from './Content1';
import Content2 from './Content2';
function Download() {
  return (
    <>
      <div className="flex flex-col w-full">
        <div className="flex w-full">
          <Content1 />
        </div>
        <div className="flex flex-col w-full max-w-[1280px] min-w-[480px] mx-auto px-[15px] min-[1130px]:px-[72px] min-[900px]:px-[30px] min-[480px]:px-[15px] my-5">
          <Content2 />
        </div>
      </div>
    </>
  );
}

export default Download;
