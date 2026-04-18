"use client";

import React from 'react'
import Link from 'next/link';
import { UserCircleIcon } from '@phosphor-icons/react';

function HeaderLoginLink() {
  return (
    <>
      <UserCircleIcon className="h-8 w-8" />
      <Link href="/login">Login</Link>
    </>
  )
}

export default HeaderLoginLink