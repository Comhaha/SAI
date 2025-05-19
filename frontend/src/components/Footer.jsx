import { useToast } from '@/hooks/useToast';
import Toast from './Toast';

function Footer() {
  const { isVisible, message, showToast } = useToast();

  return (
    <>
      <footer className="bg-white w-full border-t border-black/10">
        <div className="max-w-[1296px] mx-auto px-[30px] py-[20px] text-center text-gray-600">
          <p>Copyright © SAi. All rights reserved.</p>
        </div>
      </footer>
      <Toast isVisible={isVisible} message={message} />
    </>
  );
}

export default Footer;
