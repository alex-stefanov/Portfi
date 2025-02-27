import 'server-only';

import { extractAuthCookie } from '@/utils/supabase/extractAuthCookie';

import { API_CONFIG } from '../config/apiConfig';

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

    if (!res.ok) {
      const errorText = await res.text();
      throw new ApiError(res.status, res.statusText, errorText);
    }

    return res.json();
  } catch (err) {
    const apiError = err instanceof ApiError ? err : ApiError.fromUnknownError(err);

    console.error('API Request Failed:', {
      method,
      url,
      status: apiError.status,
      statusText: apiError.statusText,
      responseBody: apiError.responseBody,
    });

    throw apiError;
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
 * Custom error class for API errors.
 */

export class ApiError extends Error {
  status: number;
  statusText: string;
  responseBody: string;

  constructor(status: number, statusText: string, responseBody: string) {
    super(`API Error ${status}: ${statusText} - ${responseBody}`);
    this.status = status;
    this.statusText = statusText;
    this.responseBody = responseBody;
  }

  static fromUnknownError(error: unknown): ApiError {
    return new ApiError(
      500,
      'Internal Server Error',
      error instanceof Error ? error.message : 'An unknown error occurred',
    );
  }
}

/**
 * API request helper functions.
 */

const get = (path: string, options?: RequestInit) =>
  request('GET', path, undefined, options);

const post = (path: string, data: DataProps, options?: RequestInit) => {
  return request('POST', path, data, options);
};

const put = (path: string, data: DataProps, options?: RequestInit) => {
  return request('PUT', path, data, options);
};

const del = (path: string, options?: RequestInit) => {
  return request('DELETE', path, undefined, options);
};

export { get, post, put, del };
