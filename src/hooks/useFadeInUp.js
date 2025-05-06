import { useMemo } from "react";

export function useFadeInUp(delay = 0) {
  return useMemo(() => ({
    initial: { opacity: 0, y: 30 },
    animate: { opacity: 1, y: 0 },
    transition: { duration: 0.5, delay },
  }), [delay]);
} 