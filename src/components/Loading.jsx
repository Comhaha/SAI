import Lottie from 'lottie-react';
import loadingAnimation from '../assets/lotties/loading.json';

function Loading() {
  return (
    <div className="fixed inset-0 flex items-center justify-center bg-white bg-opacity-80 backdrop-blur-sm z-50">
      <div className="w-32 h-32">
        <Lottie
          animationData={loadingAnimation}
          loop={true}
          autoplay={true}
        />
      </div>
    </div>
  );
}

export default Loading; 