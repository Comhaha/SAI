import ContentCopyIcon from '@mui/icons-material/ContentCopy';

function AdminModalContent2({ secretKey, handleCopy, toast, onClose }) {
  return (
    <>
      <h2 className="font-bold mb-6 text-left text-red-600" style={{ fontSize: '25px' }}>
        Please keep API keys secure!
      </h2>
      <div className="mb-10 text-left">
        <label className="block mb-[15px] font-medium text-left" style={{ fontSize: '16px' }}>
          Your Secret API Key
        </label>
        <div className="flex items-center border border-gray-400 rounded px-3 py-2 bg-gray-50">
          <span className="flex-1 select-all break-all">{secretKey}</span>
          <button
            className="ml-2 text-gray-500 hover:text-gray-700"
            onClick={handleCopy}
            title="Copy"
          >
            <ContentCopyIcon fontSize="small" />
          </button>
        </div>
      </div>
      {toast && (
        <div
          className="fixed top-8 left-1/2 -translate-x-1/2 bg-gray-800 text-white px-4 py-2 rounded shadow-lg z-50 text-sm font-bold"
          style={{ fontFamily: 'Noto Sans KR, sans-serif' }}
        >
          {' '}
          {toast}{' '}
        </div>
      )}
      <div className="flex justify-center gap-4">
        <button
          className="w-[116px] h-[46px] rounded bg-[#69758B] text-white font-bold btn-shadow"
          style={{ fontSize: '16px', fontFamily: 'Noto Sans KR, sans-serif', fontWeight: 700 }}
          onClick={onClose}
        >
          Close
        </button>
      </div>
    </>
  );
}

export default AdminModalContent2;
