"use client"

import { UserCircleIcon } from '@phosphor-icons/react';
import React from 'react'

interface Props {
    avatarUrl: string | null;
}

function HeaderUserMenuAvatar({ avatarUrl } : Props) {
  return avatarUrl === null ?
    <UserCircleIcon className="h-6 w-6" /> :
    <img className="h-6 w-6 rounded-full" src={avatarUrl} />
}

export default HeaderUserMenuAvatar;