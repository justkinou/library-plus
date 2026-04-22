import HomeGreetingSection from '@/components/home/greetingSection'
import React from 'react'

function page() {
  return (
    <div className="w-full min-h-full">
      <HomeGreetingSection />

      <div style={{ height: "1000px" }}></div>
    </div>
  )
}

export default page