import HomeGreetingSection from '@/components/home/GreetingSection'
import HomeTrendingSection from '@/components/home/TrendingSection'
import React from 'react'

function page() {
  return (
    <div className="w-full min-h-full flex flex-col gap-12">
      <HomeGreetingSection />

      <HomeTrendingSection />

      <div style={{ height: "1000px" }}></div>
    </div>
  )
}

export default page