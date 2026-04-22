"use client";

import Link from "next/link";
import { usePathname } from "next/navigation";
import path from "path";

const links = [{ href: "/profile/account", label: "Account" }];

export default function Sidebar() {
  const pathname = usePathname();

  return (
    <nav className="flex flex-col gap-2">
      {links.map((link) => {
        const active = pathname === link.href;
        return (
          <Link
            key={link.href}
            href={link.href}
            className={
              active
                ? "font-semibold text-primary"
                : "text-muted-foreground hover:text-foreground"
            }
          >
            {link.label}
          </Link>
        );
      })}
    </nav>
  );
}
