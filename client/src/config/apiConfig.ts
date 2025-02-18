const isLocalEnv = process.env.NODE_ENV === 'development';

const API_CONFIG = {
  SERVER_URL: isLocalEnv
    ? process.env.NEXT_PUBLIC_DEV_SERVER_URL
    : process.env.NEXT_PUBLIC_PROD_SERVER_URL,
  BASE_PATH: process.env.NEXT_PUBLIC_API_BASE_PATH,
};

if (!API_CONFIG.SERVER_URL || !API_CONFIG.BASE_PATH) {
  throw new Error(
    'SERVER_URL or BASE_PATH is not defined. Please check your environment variables.',
  );
}

export const API_URL = `${API_CONFIG.SERVER_URL}/${API_CONFIG.BASE_PATH}`;
