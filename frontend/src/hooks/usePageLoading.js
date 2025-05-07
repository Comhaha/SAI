import { useState, useEffect } from 'react';
import { useLocation } from 'react-router-dom';

export default function usePageLoading(targetPath = '/screenshot', delay = 1000) {
  const [isLoading, setIsLoading] = useState(false);
  const location = useLocation();

  useEffect(() => {
    if (location.pathname === targetPath) {
      setIsLoading(true);
      const timer = setTimeout(() => {
        setIsLoading(false);
      }, delay);
      return () => clearTimeout(timer);
    }
  }, [location.pathname, targetPath, delay]);

  return isLoading;
} 