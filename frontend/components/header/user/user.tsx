"use client";

import React, { useEffect, useState } from 'react'
import HeaderLoginLink from './login-link';
import HeaderUserMenu from './user-menu';
import { MeResponseDTO } from '@/types/user/dto';
import { meEndpoint } from '@/constants/endpoints';
import { Badge } from '@/components/ui/badge';
import { Spinner } from '@/components/ui/spinner';

function HeaderUser() {
  const [isLoading, setIsLoading] = useState(true);
  const [userData, setUserData] = useState<MeResponseDTO | null>(null);

  useEffect(() => {
    (async () => {
      const response = await fetch(meEndpoint, {
        method: "GET",
      });

      if (response.ok) {
        setUserData(await response.json());
      } else {
        console.log({ status: response.status });
      }
      setIsLoading(false);
    })();
  }, []);

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