import { cookies } from 'next/headers';

export const serverFetch = async (path: string, init?: RequestInit) => {
  const cookieStore = await cookies();
  const cookieHeader = cookieStore.getAll()
    .map(c => `${c.name}=${c.value}`)
    .join('; ');

  return fetch(`${process.env.API_URL}${path}`, {
    ...init,
    headers: {
      ...init?.headers,
      Cookie: cookieHeader,
    },
  });
};