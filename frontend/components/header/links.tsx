"use client";

import Link from 'next/link';
import { usePathname } from 'next/navigation';
import React from 'react'

const navLinks = [
  { name: "Home", href: "/" },
  { name: "Catalog", href: "/catalog" },
  { name: "My orders", href: "/user/orders" },
];

function HeaderLinks() {
  const pathname = usePathname();

  return (
    <div className="flex gap-4">
      { navLinks.map((link, idx) => {
        const isActive = link.href == pathname;

        return (
          <Link
            className={`text-lg transition-colors ${isActive ? 'underline text-primary' : 'cursor-pointer hover:text-gray-400'}`}
            href={link.href}
            key={idx}
          >{link.name}</Link>
        )
      }) }
    </div>
  )
}

export default HeaderLinks