import { Rubik } from "next/font/google";
import './globals.css';
import Header from "@/components/header/header";
import { CustomToaster } from "@/components/Toaster/CustomToaster";
import { Providers } from "@/components/providers";

const rubik = Rubik({subsets:['latin'], variable:'--font-mono'});

function layout({ children }: { children: React.ReactNode }) {
  return (
    <html
      lang="en"
      className={`${rubik.variable} h-full antialiased`}
      suppressHydrationWarning
    >
        <body className="min-h-full flex flex-col px-6 pb-10">
          <Providers>
            <Header />

            {children}
            
            <CustomToaster />
          </Providers>
        </body>
      </html>
  )
}

export default layout