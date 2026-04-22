import Sidebar from "@/components/profile/sidebar";
import React from "react";

export default function ProfileLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <section className="flex flex-1 gap-6 py-6">
      <aside className="w-64 shrink-0">
        <Sidebar />
      </aside>
      <main className="min-w-0 flex-1">{children}</main>
    </section>
  );
}
