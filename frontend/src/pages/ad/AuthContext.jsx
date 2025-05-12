import { createContext, useContext, useState, useEffect, useRef } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';

const AuthContext = createContext();
export function useAuth() { return useContext(AuthContext); }

export function AuthProvider({ children }) {
  const [isLoggedIn, setIsLoggedIn] = useState(() => {
    return localStorage.getItem('isLoggedIn') === 'true';
  });
  const logoutTimer = useRef();

  // 로그인 상태 변경 시 localStorage 동기화 및 5분 타이머
  const setLoginState = (val) => {
    setIsLoggedIn(val);
    localStorage.setItem('isLoggedIn', val ? 'true' : 'false');
    if (val) {
      // 5분 후 자동 로그아웃
      if (logoutTimer.current) clearTimeout(logoutTimer.current);
      logoutTimer.current = setTimeout(() => {
        setIsLoggedIn(false);
        localStorage.setItem('isLoggedIn', 'false');
      }, 300000); // 5분 = 300,000ms
    } else {
      if (logoutTimer.current) clearTimeout(logoutTimer.current);
    }
  };

  // 컴포넌트 언마운트 시 타이머 정리
  useEffect(() => {
    return () => {
      if (logoutTimer.current) clearTimeout(logoutTimer.current);
    };
  }, []);

  return (
    <AuthContext.Provider value={{ isLoggedIn, setIsLoggedIn: setLoginState }}>
      {children}
    </AuthContext.Provider>
  );
} 