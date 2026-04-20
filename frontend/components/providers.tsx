"use client";

import { ThemeProvider } from "next-themes";
import { UserProvider } from "@/context/userContext";

export function Providers({ children }: { children: React.ReactNode }) {
  return (
    <ThemeProvider attribute="class">
      <UserProvider>
        {children}
      </UserProvider>
    </ThemeProvider>
  );
}