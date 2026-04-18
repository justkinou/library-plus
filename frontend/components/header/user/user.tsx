import React from 'react'
import { cookies } from 'next/headers';
import HeaderLoginLink from './login-link';

async function HeaderUser() {
  const cookieStore = await cookies();
  const isAuthenticated = cookieStore.has('accessToken') && cookieStore.has('refreshToken');

  return (
    <div className="flex gap-2 items-center cursor-pointer transition-colors hover:text-gray-400">
        { !isAuthenticated ?
          <HeaderLoginLink />
          : <>Username</>
        }
    </div>
  )
}

export default HeaderUser