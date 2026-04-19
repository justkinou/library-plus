import { getUserDataFromAccessToken } from '@/lib/auth/user';
import { cookies } from 'next/headers';
import React from 'react'

async function HeaderUserMenu() {
  const cookieStore = await cookies();
  const accessToken = cookieStore.get("accessToken")?.value!;
  const userData = getUserDataFromAccessToken(accessToken);
  const username = userData.name?.length === 0 ? userData.email : userData.name;

  return (
    <span>{ username }</span>
  )
}

export default HeaderUserMenu