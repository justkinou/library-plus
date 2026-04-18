"use client"

import { GearSixIcon, MoonIcon, SunIcon } from '@phosphor-icons/react';
import { useTheme } from 'next-themes'
import React, { useEffect, useState } from 'react'

const themes = ["light", "dark", "system"]

function HeaderActionThemeToggle() {
  const { theme, setTheme } = useTheme();
  const [mounted, setMounted] = useState(false);
  const nextIndex = themes.findIndex((e) => e === theme) + 1;
  const nextTheme = themes[nextIndex % themes.length];

  useEffect(() => {
    setMounted(true);
  }, [])

  if (!mounted) {
    return <div
        className="w-6 h-6"
      />
  }

  return (
      <div
        title={`Switch to ${nextTheme} theme`}
        className="cursor-pointer transition-colors hover:text-gray-400"
        onClick={() => {
          setTheme(nextTheme);
        }}
      >
        {
          theme === "light" ?
          <MoonIcon  className="w-6 h-6" /> :
          ( theme === "dark" ?
            <GearSixIcon className="w-6 h-6" /> :
            <SunIcon className="w-6 h-6" />
          )
        }
      </div>
  )
}

export default HeaderActionThemeToggle