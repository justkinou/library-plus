"use client"
import React, { useEffect } from 'react'
import { Carousel, CarouselApi, CarouselContent, CarouselItem, CarouselNext, CarouselPrevious } from '../ui/carousel'
import { Card, CardContent } from '../ui/card'
import Autoplay from 'embla-carousel-autoplay'

function HomeTrendingSection() {
  const [api, setApi] = React.useState<CarouselApi>()
  const [current, setCurrent] = React.useState(0)
  const [count, setCount] = React.useState(0)

  useEffect(() => {
    if (!api) {
      return
    }

    setCount(api.scrollSnapList().length)
    setCurrent(api.selectedScrollSnap() + 1)

    api.on("select", () => {
      setCurrent(api.selectedScrollSnap() + 1)
    })
  }, [api]);

  console.log({ count, current });

  return (
    <div className="w-full flex flex-col items-center gap-4">
        <h1 className="text-xl font-bold">Trending books</h1>

        <Carousel
          className="w-8/10"
          setApi={setApi}
          opts={{
            align: "start",
            loop: true,
          }}
          plugins={[
            Autoplay({
              delay: 6000,
            }),
          ]}
        >
          <CarouselContent>
            {Array.from({ length: 12 }).map((_, index) => (
              <CarouselItem key={index} className="md:basis-1/3">
                  <Card>
                    <CardContent className="flex items-center justify-center p-2">
                      <span className="text-3xl font-semibold">{index + 1}</span>
                    </CardContent>
                  </Card>
              </CarouselItem>
            ))}
          </CarouselContent>
          <CarouselPrevious />
          <CarouselNext />
        </Carousel>

        <div className="flex justify-center items-center gap-2">
          {Array.from({ length: count }).map((_, index) => (
            <div key={index} className="w-4 h-4 rounded-full bg-card flex justify-center items-center">
              { current === (index + 1) ? <div className="w-3 h-3 rounded-full bg-primary" /> : <></> }
            </div>
          ))}
        </div>
    </div>
  )
}

export default HomeTrendingSection