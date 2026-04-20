"use client";

import { useContext } from 'react';
import HeaderLoginLink from './login-link';
import HeaderUserMenu from './user-menu';
import { Badge } from '@/components/ui/badge';
import { Spinner } from '@/components/ui/spinner';
import { UserContext } from '@/context/userContext';

function HeaderUser() {
  const { userData, isLoading } = useContext(UserContext);

  if (isLoading) {
    return (
      <div className="h-8 flex items-center justify-center">
        <Badge className="bg-card">
          <Spinner data-icon="inline-start" />
          Loading
        </Badge>
      </div>
    );
  }

  return (
    <div className="flex gap-2 items-center cursor-pointer transition-colors hover:text-gray-400">
        { userData === null ?
          <HeaderLoginLink />
          : <HeaderUserMenu userData={userData} />
        }
    </div>
  )
}

export default HeaderUser