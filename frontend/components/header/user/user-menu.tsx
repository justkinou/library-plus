import { getUserDataFromAccessToken } from '@/lib/auth/user';
import { cookies } from 'next/headers';
import React from 'react'

async function HeaderUserMenu() {
  const cookieStore = await cookies();
  const accessToken = cookieStore.get("accessToken")?.value!;
  const userData = getUserDataFromAccessToken(accessToken);

  return (
    <span>{ userData.email }</span>
  )
}

export default HeaderUserMenu