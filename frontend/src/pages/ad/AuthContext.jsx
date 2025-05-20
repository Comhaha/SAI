import { createContext, useContext, useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';

const AuthContext = createContext();
export function useAuth() {
  return useContext(AuthContext);
}

export function AuthProvider({ children }) {
  const [isLoggedIn, setIsLoggedIn] = useState(() => {
    return localStorage.getItem('isLoggedIn') === 'true';
  });

  // 로그인 상태 변경 시 localStorage 동기화
  const setLoginState = (val) => {
    setIsLoggedIn(val);
    localStorage.setItem('isLoggedIn', val ? 'true' : 'false');
  };

  // 5분 후 자동 로그아웃 타이머
  useEffect(() => {
    if (isLoggedIn) {
      const timer = setTimeout(() => {
        setLoginState(false);
      }, 5 * 60 * 1000);
      return () => clearTimeout(timer);
    }
  }, [isLoggedIn]);

  return (
    <AuthContext.Provider value={{ isLoggedIn, setIsLoggedIn: setLoginState }}>
      {children}
    </AuthContext.Provider>
  );
}
