import { useState, useEffect } from 'react';
import AdminModalContent1 from './AdminModalContent1';
import AdminModalContent2 from './AdminModalContent2';

function AdminModal({ open, onClose }) {
  const [step, setStep] = useState(1);
  const [adminKey, setAdminKey] = useState('');
  const [secretKey] = useState('dDbA1453S0179jS4E15f4b'); // 예시 키
  const [copied, setCopied] = useState(false);
  const [toast, setToast] = useState('');
  const [showAdminKey, setShowAdminKey] = useState(false);

  useEffect(() => {
    if (!open) {
      setStep(1);
      setAdminKey('');
      setCopied(false);
      setToast('');
      setShowAdminKey(false);
    }
  }, [open]);

  const handleCopy = () => {
    navigator.clipboard.writeText(secretKey);
    setCopied(true);
    setToast('Copied to clipboard!');
    setTimeout(() => {
      setCopied(false);
      setToast('');
    }, 1200);
  };

  if (!open) return null;

  return (
    <div className="fixed inset-0 z-50 flex justify-center items-center bg-black bg-opacity-40 py-[60px] px-[65px]">
      <div className="bg-white rounded-lg shadow-lg min-w-[350px] max-w-[90vw] relative w-[695px] h-[422px] flex flex-col justify-between p-0">
        {/* X 버튼 */}
        <button
          className="absolute top-4 right-4 text-2xl text-gray-500 hover:text-gray-700"
          onClick={onClose}
        >
          ×
        </button>
        <div className="flex flex-col justify-between h-full py-[60px] px-[65px]">
          {step === 1 ? (
            <AdminModalContent1
              adminKey={adminKey}
              setAdminKey={setAdminKey}
              showAdminKey={showAdminKey}
              setShowAdminKey={setShowAdminKey}
              onNext={() => setStep(2)}
            />
          ) : (
            <AdminModalContent2
              secretKey={secretKey}
              handleCopy={handleCopy}
              toast={toast}
              onClose={onClose}
            />
          )}
        </div>
      </div>
    </div>
  );
}

export default AdminModal;
