import { MeResponseDTO } from '@/types/user/dto';
import React from 'react'
import HeaderUserMenuAvatar from './user-menu-avatar';
import { serverFetch } from '@/lib/server/fetch';

async function HeaderUserMenu() {
  const response = await serverFetch(`/user/me`, {
    method: "GET",
  });

  if (!response.ok) {
    console.log(response.status, response.statusText);
    return <span>error</span>
  }

  const userData: MeResponseDTO = await response.json();

  return (
    <div className="flex gap-2 items-center cursor-pointer transition-colors hover:text-gray-400">
      <HeaderUserMenuAvatar avatarUrl={userData.avatarUrl}  />
      <span>{ userData.name ?? userData.email }</span>
    </div>
  )
}

export default HeaderUserMenu