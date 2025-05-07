import path from 'path';
import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  build: {
    rollupOptions: {
      output: {
        // 에셋 파일명에 해시 포함하여 캐싱 최적화
        assetFileNames: 'assets/[name]-[hash][extname]',
      },
    },
  },
  resolve: {
    alias: {
      '@': path.resolve(__dirname, './src'),
    },
  },
  server: {
    port: 3000,
  },
  // server: {
  //   proxy: {
  //     '/api': {
  //       target: 'https://...',
  //       changeOrigin: true,
  //       rewrite: (path) => path.replace(/^\/api/, '/api'),
  //     },
  //   },
  //   // HMR 관련 설정 추가
  //   hmr: {
  //     overlay: false, // 오류 오버레이 비활성화
  //   },
  //   watch: {
  //     usePolling: true, // 폴링 방식으로 파일 변경 감지
  //     interval: 1000, // 폴링 간격(ms)
  //   },
  // },
  // // 캐시 관련 설정 추가
  // optimizeDeps: {
  //   force: true, // 의존성 강제 재최적화
  // },
  // // 소스맵 설정 - 개발 중 디버깅 용이하게
  // css: {
  //   devSourcemap: true,
  // },
});
