import React from 'react'

function layout({ children }: { children: React.ReactNode }) {
  return (
    <div className="flex flex-1 justify-center items-center">{children}</div>
  )
}

export default layout