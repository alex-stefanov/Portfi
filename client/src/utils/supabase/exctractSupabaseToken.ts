import { cookies } from 'next/headers';

export const decodeAuthTokenFromCookies = async () => {
  const cookieStore = await cookies();

  const part1 = cookieStore.get('sb-zwuxrlpqnokmjcbmlxla-auth-token.0')?.value || '';
  const part2 = cookieStore.get('sb-zwuxrlpqnokmjcbmlxla-auth-token.1')?.value || '';

  if (!part1 || !part2) {
    return null;
  }

  const base64Token = part1 + part2;

  const token = Buffer.from(base64Token.replace('base64-', ''), 'base64').toString(
    'utf-8',
  );

  return token;
};
