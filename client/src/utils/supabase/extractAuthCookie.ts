'server-only';

import { cookies } from 'next/headers';

export const extractAuthCookie = async () => {
  try {
    const cookieStore = await cookies();

    const authCookies =
      cookieStore
        .getAll()
        .filter(({ name }) => name.startsWith('sb-'))
        .map(({ name, value }) => `${name}=${value}`)
        .join('; ') || null;

    return authCookies;
  } catch (err: unknown) {
    const errMsg = err instanceof Error ? err.message : String(err);
    console.error(errMsg);

    return null;
  }
};
