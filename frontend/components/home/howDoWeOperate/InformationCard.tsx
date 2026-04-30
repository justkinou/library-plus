import React from 'react'

interface Props {
  sequentialNumber: number;
  title: string;
  text: string;
  icon: React.ReactNode;
}

function InformationCard({ sequentialNumber, title, text, icon }: Props) {
  return (
    <div className="w-72 h-72 p-6 bg-background flex flex-col gap-10">
      <div className="flex w-full justify-between items-end">
      <span className="text-7xl font-bold text-primary">{sequentialNumber}</span>

      {icon}
      </div>

      <div>
        <p className="text-xl font-bold">{title}</p>
        <p>{text}</p>
      </div>
    </div>
  )
}

export default InformationCard