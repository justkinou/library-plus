"use client"

import { SmileySadIcon } from '@phosphor-icons/react'
import Link from 'next/link'
import React from 'react'

function NotFound() {
  return (
    <div className="flex-1 flex flex-col justify-center items-center">
      <div className="flex items-center gap-4">
        <span className="text-xl font-bold">404 - Not found</span>
        <SmileySadIcon className="h-12 w-12" />
      </div>

      <Link href="/" className="underline text-gray-400">Home</Link>
    </div>
  )
}

export default NotFound