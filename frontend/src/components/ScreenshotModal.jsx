import { useState } from 'react';
import ZoomInIcon from '@mui/icons-material/ZoomIn';
import ZoomOutIcon from '@mui/icons-material/ZoomOut';
import DownloadIcon from '@mui/icons-material/Download';
import CloseIcon from '@mui/icons-material/Close';
import ArrowBackIosNewIcon from '@mui/icons-material/ArrowBackIosNew';
import ArrowForwardIosIcon from '@mui/icons-material/ArrowForwardIos';

function ScreenshotModal({ images, currentIndex, onClose }) {
  const [index, setIndex] = useState(currentIndex);
  const [zoom, setZoom] = useState(1);

  if (!images || images.length === 0) return null;

  const handlePrev = () => setIndex((prev) => (prev === 0 ? images.length - 1 : prev - 1));
  const handleNext = () => setIndex((prev) => (prev === images.length - 1 ? 0 : prev + 1));
  const handleZoomIn = () => setZoom((z) => Math.min(z + 0.2, 3));
  const handleZoomOut = () => setZoom((z) => Math.max(z - 0.2, 0.5));
  const handleDownload = () => {
    const link = document.createElement('a');
    link.href = images[index].src;
    link.download = images[index].alt || `screenshot-${index + 1}`;
    link.click();
  };

  // 오버레이 클릭 시 닫기, 내부 클릭 시 버블링 방지
  const handleOverlayClick = (e) => {
    if (e.target === e.currentTarget) {
      onClose();
    }
  };

  return (
    <div
      className="fixed inset-0 z-[200] bg-black/70 flex flex-col justify-between h-screen min-h-screen"
      onClick={handleOverlayClick}
    >
      {/* 상단 바 */}
      <div
        className="w-full h-16 flex items-center justify-end px-8 bg-[#222]/80 shadow-lg z-30 mt-20"
        style={{ minHeight: 64 }}
      >
        <div className="flex gap-2">
          <button
            onClick={handleZoomOut}
            className="text-gray-200 hover:bg-gray-700 rounded-full p-2 transition"
          >
            <ZoomOutIcon fontSize="medium" />
          </button>
          <button
            onClick={handleZoomIn}
            className="text-gray-200 hover:bg-gray-700 rounded-full p-2 transition"
          >
            <ZoomInIcon fontSize="medium" />
          </button>
          <button
            onClick={handleDownload}
            className="text-gray-200 hover:bg-gray-700 rounded-full p-2 transition"
          >
            <DownloadIcon fontSize="medium" />
          </button>
          <button
            onClick={onClose}
            className="text-gray-200 hover:bg-gray-700 rounded-full p-2 transition"
          >
            <CloseIcon fontSize="medium" />
          </button>
        </div>
      </div>
      {/* 본문 */}
      <div
        className="flex-1 flex items-center justify-center w-full relative"
        onClick={(e) => e.stopPropagation()}
      >
        {/* 좌우 이동 */}
        <button
          onClick={handlePrev}
          className="absolute left-4 top-1/2 -translate-y-1/2 bg-white/80 hover:bg-blue-400 hover:text-white text-gray-700 shadow rounded-full w-12 h-12 flex items-center justify-center z-10 transition"
        >
          <ArrowBackIosNewIcon fontSize="medium" />
        </button>
        <div className="flex flex-col items-center w-full">
          <img
            src={images[index].src}
            alt={images[index].alt}
            style={{ transform: `scale(${zoom})`, transition: 'transform 0.2s' }}
            className="max-h-[70vh] max-w-full rounded-[2px] bg-white shadow-lg mx-auto"
          />
        </div>
        <button
          onClick={handleNext}
          className="absolute right-4 top-1/2 -translate-y-1/2 bg-white/80 hover:bg-blue-400 hover:text-white text-gray-700 shadow rounded-full w-12 h-12 flex items-center justify-center z-10 transition"
        >
          <ArrowForwardIosIcon fontSize="medium" />
        </button>
      </div>
      {/* 하단 썸네일 바 */}
      <div
        className="w-full bg-black/60 py-3 px-2 flex gap-2 overflow-x-auto items-center"
        style={{ minHeight: 64 }}
      >
        {images.map((img, i) => (
          <img
            key={i}
            src={img.src}
            alt={img.alt}
            onClick={() => {
              setIndex(i);
              setZoom(1);
            }}
            className={`w-20 h-16 object-cover cursor-pointer border-2 ${
              i === index ? 'border-blue-500' : 'border-transparent'
            } rounded-[2px] bg-white`}
            style={{ minWidth: 80 }}
          />
        ))}
      </div>
    </div>
  );
}

export default ScreenshotModal;
