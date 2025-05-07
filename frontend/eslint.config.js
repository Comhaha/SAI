import js from '@eslint/js';
import { FlatCompat } from '@eslint/eslintrc';
import path from 'path';
import { fileURLToPath } from 'url';

const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);

const compat = new FlatCompat({
  baseDirectory: __dirname,
  recommendedConfig: js.configs.recommended,
});

export default [
  js.configs.recommended, //자바스크립트 기본 추천
  ...compat.extends(
    'plugin:react/recommended',//React 필수 규칙
    'plugin:react-hooks/recommended', //React Hooks 필수 규칙
    'plugin:jsx-a11y/recommended', //접근성 기본 규칙
    'plugin:prettier/recommended'  //Prettier 연동
  ),
  {
    settings: {
      react: {
        version: 'detect',
      },
    },
    files: ['**/*.{js,jsx}'],
    languageOptions: {
      parserOptions: {
        ecmaFeatures: {
          jsx: true,
        },
      },
    },
    rules: {
      'react/jsx-filename-extension': [1, { extensions: ['.jsx'] }],
      // 'react/react-in-jsx-scope': 'off', // Next.js나 최신 React에서는 필요 없음
      'no-var': 'warn',
      'no-console': ['warn', { allow: ['warn', 'error'] }],
    },
  },
];
