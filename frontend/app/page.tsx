import GreetingSection from '@/components/home/GreetingSection'
import HowDoWeOperateSecton from '@/components/home/howDoWeOperate/HowDoWeOperateSecton'
import TrendingSection from '@/components/home/TrendingSection'
import React from 'react'

function page() {
  return (
    <div className="w-full min-h-full flex flex-col gap-36 bg-card">
      <GreetingSection />

      <TrendingSection />

      <HowDoWeOperateSecton />

      <div style={{ height: "1000px" }}></div>
    </div>
  )
}

export default page