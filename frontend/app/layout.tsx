import { Rubik } from "next/font/google";
import './globals.css';
import Header from "@/components/header/header";
import { ThemeProvider } from "next-themes";

const rubik = Rubik({subsets:['latin'], variable:'--font-mono'});

function layout({ children }: { children: React.ReactNode }) {
  return (
    <html
      lang="en"
      className={`${rubik.variable} h-full antialiased`}
      suppressHydrationWarning
    >
        <body className="min-h-full flex flex-col px-6">
          <ThemeProvider attribute="class">
            <Header />

            {children}
          </ThemeProvider>
        </body>
      </html>
  )
}

export default layout