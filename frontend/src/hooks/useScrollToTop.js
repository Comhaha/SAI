import { useCallback } from 'react';

function useScrollToTop() {
  return useCallback(() => {
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }, []);
}

export default useScrollToTop;
