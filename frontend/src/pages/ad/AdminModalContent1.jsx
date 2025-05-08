import VisibilityIcon from '@mui/icons-material/Visibility';
import VisibilityOffIcon from '@mui/icons-material/VisibilityOff';

function AdminModalContent1({ adminKey, setAdminKey, showAdminKey, setShowAdminKey, onNext }) {
  return (
    <>
      <h2 className="font-bold mb-6 text-left" style={{ fontSize: '25px' }}>
        Please Enter your Admin key !
      </h2>
      <div className="mb-10 text-left">
        <label className="block mb-[15px] font-medium text-left" style={{ fontSize: '16px' }}>
          Admin key
        </label>
        <div className="relative">
          <input
            type={showAdminKey ? 'text' : 'password'}
            value={adminKey}
            onChange={(e) => setAdminKey(e.target.value)}
            maxLength={20}
            className="w-full border border-gray-400 rounded px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-200 pr-10"
          />
          <button
            type="button"
            className="absolute top-1/2 right-3 -translate-y-1/2 text-gray-500 hover:text-gray-700"
            onClick={() => setShowAdminKey((prev) => !prev)}
            tabIndex={-1}
          >
            {showAdminKey ? (
              <VisibilityOffIcon fontSize="small" />
            ) : (
              <VisibilityIcon fontSize="small" />
            )}
          </button>
        </div>
      </div>
      <div className="flex justify-center">
        <button
          className="w-[116px] h-[46px] rounded bg-[#69758B] text-white font-bold btn-shadow"
          style={{ fontSize: '16px', fontFamily: 'Noto Sans KR, sans-serif', fontWeight: 700 }}
          onClick={onNext}
        >
          Next
        </button>
      </div>
    </>
  );
}

export default AdminModalContent1;
