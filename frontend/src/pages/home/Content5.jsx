import background from '@/assets/images/background.svg';
import { Link } from 'react-router-dom';

function Content5() {
    return (
        <div className="w-full h-[450px] relative overflow-hidden flex items-center justify-center">
            {/* Background image */}
            <img
                src={background}
                alt="background"
                className="absolute top-0 left-0 w-full h-full object-cover z-0"
            />
            {/* Foreground content */}
            <div className="relative z-10 w-full flex flex-col items-center justify-center">
                <div className="text-[38px] font-bold text-gray-900 mb-4 text-center">Admin Key &amp; Secret Key</div>
                <div className="text-[18px] font-medium text-gray-700 mb-7 text-center max-w-[600px]">
                    If you have received an admin key, click the button below to issue your secret key.<br />
                    Entering the secret key allows you to generate reports.<br />
                    You can safely distribute keys to multiple students, ensuring secure and easy management for everyone.
                </div>
                <Link to="/admin">
                    <button className="bg-blue-500 text-white px-6 py-2 rounded font-medium shadow hover:bg-blue-300 transition-colors text-[18px]">
                        Get Secret Key
                    </button>
                </Link>
            </div>
        </div>
    );
}

export default Content5;