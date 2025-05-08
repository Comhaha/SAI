import { motion } from 'motion/react';

function ScreenshotCard({ image, index }) {
  return (
    <motion.div
      key={index}
      className="bg-gray-200 shadow rounded-[2px] flex items-center justify-center overflow-hidden h-[337px] max-[720px]:h-[280px]"
    >
      <img src={image.src} alt={image.alt} className="w-full h-full object-cover" />
    </motion.div>
  );
}

export default ScreenshotCard;
