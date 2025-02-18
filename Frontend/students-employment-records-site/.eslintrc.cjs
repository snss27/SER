module.exports = {
    parser: '@typescript-eslint/parser',
    parserOptions: {
      ecmaVersion: 2020,
      sourceType: 'module',
      ecmaFeatures: {
        jsx: true, // поддержка JSX для React
      },
    },
    settings: {
      react: {
        version: 'detect', // автоматически определяет версию React
      },
    },
    env: {
      browser: true,
      es2021: true,
      node: true,
    },
    extends: [
      'eslint:recommended',
      'plugin:react/recommended',
      'plugin:@typescript-eslint/recommended',
      'next/core-web-vitals', // для Next.js
    ],
    plugins: ['react', '@typescript-eslint'],
    rules: {
      // Здесь можно указать свои правила или оставить пустым
    },
  };
  