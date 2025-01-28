import { dirname } from 'path';
import { fileURLToPath } from 'url';

import { FlatCompat } from '@eslint/eslintrc';
import js from '@eslint/js';
import pluginQuery from '@tanstack/eslint-plugin-query';

const __filename = fileURLToPath(import.meta.url);
const __dirname = dirname(__filename);

const compat = new FlatCompat({
  baseDirectory: __dirname,
  recommendedConfig: js.configs.recommended,
});

const eslintConfig = [
  ...pluginQuery.configs['flat/recommended'],
  ...compat.config({
    plugins: ['boundaries'],
    settings: {
      'boundaries/include': ['src/**/*'],
      'boundaries/elements': [
        {
          mode: 'full',
          type: 'shared',
          pattern: [
            'src/components/**/*',
            'src/constants/**/*',
            'src/data-access/**/*',
            'src/hooks/**/*',
            'src/lib/**/*',
            'src/server-actions/**/*',
            'src/types/**/*',
            'src/utils/**/*',
          ],
        },
        {
          mode: 'full',
          type: 'feature',
          capture: ['featureName'],
          pattern: ['src/features/!(*\\(*\\))/**/*'],
        },
        {
          mode: 'full',
          type: 'groupFeature',
          capture: ['groupName', 'groupFeatureName'],
          pattern: [
            'src/features/\\((?<groupName>[^/]+)\\)/(?<groupFeatureName>[^/]+)/**/*',
          ],
        },
        {
          mode: 'full',
          type: 'app',
          capture: ['_', 'fileName'],
          pattern: ['src/app/**/*'],
        },
        {
          mode: 'full',
          type: 'neverImport',
          pattern: ['src/*'],
        },
      ],
    },
    rules: {
      // '@tanstack/query/exhaustive-deps': 'warn',
      // '@tanstack/query/no-rest-destructuring': 'warn',
      // '@tanstack/query/stable-query-client': 'error',
      'react/no-unescaped-entities': 0,
      'boundaries/no-unknown': ['error'],
      'boundaries/no-unknown-files': ['error'],
      'boundaries/element-types': [
        'error',
        {
          default: 'disallow',
          rules: [
            {
              from: ['shared'],
              allow: ['shared'],
            },
            {
              from: ['feature'],
              allow: ['shared', ['feature', { featureName: '${from.featureName}' }]],
            },
            {
              from: ['groupFeature'],
              allow: [
                'shared',
                [
                  'groupFeature',
                  {
                    groupName: '${groupName}',
                    groupFeatureName: '${from.groupFeatureName}',
                  },
                ],
                [
                  'groupFeature',
                  {
                    groupName: '${groupName}',
                    groupFeatureName: 'shared',
                  },
                ],
              ],
            },
            {
              from: ['app', 'neverImport'],
              allow: ['shared', 'feature', 'groupFeature'],
            },
            {
              from: ['app'],
              allow: [['app', { fileName: '*.css' }]],
            },
          ],
        },
      ],
      'no-unused-vars': 'off',
      '@typescript-eslint/no-unused-vars': [
        'error',
        { argsIgnorePattern: '^_', varsIgnorePattern: '^_' },
      ],
      'import/order': 'off',
    },
    extends: [
      'eslint:recommended',
      'prettier',
      'next/typescript',
      'next/core-web-vitals',
      'next',
    ],
  }),
];

export default eslintConfig;
