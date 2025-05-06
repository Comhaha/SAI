import { useState, useCallback } from 'react';

export function useToast() {
  const [isVisible, setIsVisible] = useState(false);
  const [message, setMessage] = useState('');

  const showToast = useCallback((msg) => {
    setMessage(msg);
    setIsVisible(true);
    setTimeout(() => {
      setIsVisible(false);
    }, 3000);
  }, []);

  return { isVisible, message, showToast };
} 