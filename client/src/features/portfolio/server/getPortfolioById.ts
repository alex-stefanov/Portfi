import 'server-only';

import { apiRoutes } from '@/constants/apiRoutes';

type GetPortfolioByIdProps = {
  id: string;
  cookie: string | null;
};

export const getPortfolioById = async ({ id, cookie }: GetPortfolioByIdProps) => {
  const url = apiRoutes.portfolio.getById(id);

  const headers: HeadersInit = {
    'Content-Type': 'application/json',
  };

  if (cookie) {
    headers['cookie'] = cookie;
  }

  try {
    const result = await fetch(url, {
      headers,
    });

    if (!result.ok) {
      throw new Error(result.statusText);
    }

    return await result.json();
  } catch (_err) {
    return null;
  }
};
