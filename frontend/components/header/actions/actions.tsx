"use client";

import { BasketIcon, BellIcon } from '@phosphor-icons/react'
import React from 'react'
import HeaderActionThemeToggle from './theme-toggle';

function HeaderActions() {
  return (
    <div className="flex gap-3">
      <BellIcon className="w-6 h-6 text-foreground cursor-pointer transition-colors hover:text-gray-400" />
    
      <BasketIcon className="w-6 h-6 text-foreground cursor-pointer transition-colors hover:text-gray-400" />
    
      <HeaderActionThemeToggle />
    </div>
  )
}

export default HeaderActions