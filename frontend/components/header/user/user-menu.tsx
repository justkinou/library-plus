"use client"

import React from 'react'
import { MeResponseDTO } from '@/types/user/dto';
import HeaderUserMenuAvatar from './user-menu-avatar';
import { NavigationMenu, NavigationMenuContent, NavigationMenuItem, NavigationMenuList, NavigationMenuTrigger } from '@/components/ui/navigation-menu';
import Link from 'next/link';

interface Props {
  userData: MeResponseDTO;
}

function HeaderUserMenu({ userData } : Props) {
  return (
    <NavigationMenu>
      <NavigationMenuList>
        <NavigationMenuItem>
          <NavigationMenuTrigger>
            <div className="flex gap-2 items-center cursor-pointer transition-colors hover:text-gray-400">
              <HeaderUserMenuAvatar avatarUrl={userData.avatarUrl} />
              <span>{ userData.name ?? userData.email }</span>
            </div>
          </NavigationMenuTrigger>

          <NavigationMenuContent>
            <ul className="w-full">
              <Link href="/profile">Profile</Link>
            </ul>
          </NavigationMenuContent>
        </NavigationMenuItem>
      </NavigationMenuList>
    </NavigationMenu>
    
  )
}

export default HeaderUserMenu