"use client";

import Link from "next/link";
import { usePathname } from "next/navigation";
import { BellIcon, ShoppingBagIcon, UserIcon } from "@phosphor-icons/react";

const links = [
  { href: "/profile/account", label: "Account", icon: UserIcon },
  { href: "/profile/rentals", label: "My rentals", icon: ShoppingBagIcon },
  { href: "/profile/notifications", label: "Notifications", icon: BellIcon },
];

export default function Sidebar() {
  const pathname = usePathname();

  return (
    <aside className="w-full max-w-60 bg-light">
      <nav className="flex flex-col divide-y deivide-light-contrast">
        {links.map(({ href, label, icon: Icon }) => {
          const active = pathname === href;
          return (
            <Link
              key={href}
              href={href}
              aria-current={active ? "page" : undefined}
              className={[
                "flex h-12 items-center gap-3  text-[15px] transition-colors px-6",
                active
                  ? "font-semibold text-primary hover:text-dark underline"
                  : "text-dark hover:text-primary",
              ].join(" ")}
            >
              <Icon
                size={20}
                weight={active ? "fill" : "regular"}
                className={active ? "text-primary" : "text-dark"}
              />
              <span className="underline-offset-2">{label}</span>
            </Link>
          );
        })}
      </nav>
    </aside>
  );
}
