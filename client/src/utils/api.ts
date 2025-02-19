import 'server-only';

import { API_CONFIG } from '@/config/apiConfig';

import { extractAuthCookie } from '@/utils/supabase/extractAuthCookie';

type RequestMethod = 'GET' | 'POST' | 'PUT' | 'DELETE';
type DataProps = Record<string, unknown>;

const defaultHeaders: HeadersInit = {
  Accept: 'application/json',
};

/**
 * Main request function handling API calls.
 */

const request = async (
  method: RequestMethod,
  path: string,
  data?: DataProps,
  fetchOptions: RequestInit = {},
) => {
  const url = `${API_CONFIG.SERVER_URL}/${path}`;
  const headers = await getHeaders(method);

  const options: RequestInit = {
    method,
    headers,
    ...(data && method !== 'GET' && { body: JSON.stringify(data) }),
    ...fetchOptions,
  };

  try {
    const res = await fetchWithTimeout(url, options);
    if (!res.ok) handleError(res);

    return res.json();
  } catch (error) {
    console.error('API Request Failed:', {
      method,
      url,
      error: error instanceof Error ? error.message : 'Unknown error',
    });

    throw error;
  }
};

/**
 * Constructs headers for the request.
 */

const getHeaders = async (method: RequestMethod): Promise<HeadersInit> => {
  const cookie = await extractAuthCookie();

  return {
    ...defaultHeaders,
    ...(cookie && { cookie }),
    ...(method !== 'GET' && { 'Content-Type': 'application/json' }),
  };
};

/**
 * Handles API request errors and throws a detailed error message.
 */

const handleError = async (res: Response) => {
  const errorText = (await res.text()) || 'No response body';
  throw new Error(`Error ${res.status}: ${res.statusText} - ${errorText}`);
};

/**
 * Fetches with a timeout to prevent hanging requests.
 */

const fetchWithTimeout = async (
  url: string,
  options: RequestInit,
  timeout = 10000,
) => {
  const controller = new AbortController();
  const id = setTimeout(() => controller.abort(), timeout);

  try {
    return await fetch(url, { ...options, signal: controller.signal });
  } finally {
    clearTimeout(id);
  }
};

/**
 * API request helper functions.
 */

export const get = (path: string, options?: RequestInit) =>
  request('GET', path, undefined, options);

export const post = (path: string, data: DataProps, options?: RequestInit) => {
  return request('POST', path, data, options);
};

export const put = (path: string, data: DataProps, options?: RequestInit) => {
  return request('PUT', path, data, options);
};

export const del = (path: string, options?: RequestInit) => {
  return request('DELETE', path, undefined, options);
};
