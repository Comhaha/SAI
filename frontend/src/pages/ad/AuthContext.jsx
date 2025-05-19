import { createContext, useContext, useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';

const AuthContext = createContext();
export function useAuth() { return useContext(AuthContext); }

export function AuthProvider({ children }) {
  const [isLoggedIn, setIsLoggedIn] = useState(() => {
    return localStorage.getItem('isLoggedIn') === 'true';
  });

  // 로그인 상태 변경 시 localStorage 동기화
  const setLoginState = (val) => {
    setIsLoggedIn(val);
    localStorage.setItem('isLoggedIn', val ? 'true' : 'false');
  };

  return (
    <AuthContext.Provider value={{ isLoggedIn, setIsLoggedIn: setLoginState }}>
      {children}
    </AuthContext.Provider>
  );
} 