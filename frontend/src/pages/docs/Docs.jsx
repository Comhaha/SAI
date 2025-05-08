function Docs() {
  return (
    <div className="min-h-screen w-full flex flex-col justify-center items-center bg-white py-12">
      <div className="flex-1 flex flex-col items-center justify-center w-full">
        <h1 className="text-5xl font-extrabold text-center mb-16 mt-8">Documentation</h1>
        <div className="flex flex-col w-full max-w-[1280px] min-w-[480px] mx-auto px-[15px] min-[1130px]:px-[72px] min-[900px]:px-[30px] min-[480px]:px-[15px] my-5 items-center justify-center">
          <div className="w-full grid grid-cols-1 md:grid-cols-3 gap-12 mb-24">
            {/* Visual Programming */}
            <div className="flex flex-col items-start justify-center text-left">
              <h2 className="text-2xl font-bold mb-4">Visual Programming</h2>
              <ul className="space-y-2">
                <li>
                  <a href="#" className="text-[#2878BE] hover:underline font-medium">
                    Getting started
                  </a>
                </li>
                <li>
                  <a href="#" className="text-[#2878BE] hover:underline font-medium">
                    YouTube tutorials
                  </a>
                </li>
                <li>
                  <a href="#" className="text-[#2878BE] hover:underline font-medium">
                    Example workflows
                  </a>
                </li>
              </ul>
            </div>
            {/* Development */}
            <div className="flex flex-col items-start justify-center text-left">
              <h2 className="text-2xl font-bold mb-4">Development</h2>
              <ul className="space-y-2">
                <li>
                  <a href="#" className="text-[#2878BE] hover:underline font-medium">
                    Widget development
                  </a>
                </li>
                <li>
                  <a href="#" className="text-[#2878BE] hover:underline font-medium">
                    Example addon
                  </a>
                </li>
              </ul>
            </div>
            {/* Python Library */}
            <div className="flex flex-col items-start justify-center text-left">
              <h2 className="text-2xl font-bold mb-4">Python Library</h2>
              <ul className="space-y-2">
                <li>
                  <a href="#" className="text-[#2878BE] hover:underline font-medium">
                    Tutorial
                  </a>
                </li>
                <li>
                  <a href="#" className="text-[#2878BE] hover:underline font-medium">
                    Reference
                  </a>
                </li>
                <li>
                  <a href="#" className="text-[#2878BE] hover:underline font-medium">
                    Orange 2.7 documentation
                  </a>
                </li>
              </ul>
            </div>
          </div>
        </div>
        <div className="text-xl text-left mt-12 mb-4 w-full max-w-[1280px] mx-auto">
          For a list of frequently asked questions, see{' '}
          <a href="#" className="text-[#2878BE] hover:underline">
            FAQ
          </a>
          . Also, feel free to reach out to us in our{' '}
          <a href="#" className="text-[#2878BE] hover:underline">
            Discord chatroom
          </a>
          .
        </div>
      </div>
    </div>
  );
}

export default Docs;
