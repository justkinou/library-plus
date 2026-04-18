import React from 'react'
import { cookies } from 'next/headers';
import HeaderLoginLink from './login-link';
import HeaderUserMenu from './user-menu';

async function HeaderUser() {
  const cookieStore = await cookies();
  const isAuthenticated = cookieStore.has('accessToken') && cookieStore.has('refreshToken');

  console.dir({ isAuthenticated })

  return (
    <div className="flex gap-2 items-center cursor-pointer transition-colors hover:text-gray-400">
        { !isAuthenticated ?
          <HeaderLoginLink />
          : <HeaderUserMenu />
        }
    </div>
  )
}

export default HeaderUser