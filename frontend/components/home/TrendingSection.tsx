"use client"
import React, { useEffect } from 'react'
import { Carousel, CarouselApi, CarouselContent, CarouselItem, CarouselNext, CarouselPrevious } from '../ui/carousel'
import { Card, CardContent } from '../ui/card'
import Autoplay from 'embla-carousel-autoplay'
import { Button } from '../ui/button'
import Link from 'next/link'

const books = [
  {
    title: "The Great Gatsby",
    author: "F. Scott Fitzgerald",
    language: "English",
    published: 1925,
    availability: "Available now",
    imageURL: "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fd28hgpri8am2if.cloudfront.net%2Fbook_images%2Fonix%2Fcvr9780743273565%2Fthe-great-gatsby-9780743273565_hr.jpg&f=1&nofb=1&ipt=c352c53f1483ecce0698d5bf02ad053ae4ae0487b968bcf015fc1eaa9adae7be"
  },
  {
    title: "Thousand cranes",
    author: "Yasunari Kawabata",
    language: "English",
    published: 1958,
    availability: "Limited availability",
    imageURL: "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fimages-na.ssl-images-amazon.com%2Fimages%2FS%2Fcompressed.photo.goodreads.com%2Fbooks%2F1636603802i%2F14027.jpg&f=1&nofb=1&ipt=812617828b62c610ef145564b085520b5f949fb2c80c238e536a3f49a407806b"
  },
  {
    title: "To Kill a Mockingbird",
    author: "Herper Lee",
    language: "English",
    published: 1960,
    availability: "Limited availability",
    imageURL: "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fanyiko.files.wordpress.com%2F2013%2F01%2Fto-kill-a-mockingbird.jpg&f=1&nofb=1&ipt=650ce9a246c87694b743a0e69ef1947bbfc81ddd82bc56387e3f2848f7ac3b67"
  },
  {
    title: "The Little Prince",
    author: "Antoine de Saint-Exupery",
    language: "French",
    published: 1954,
    availability: "Available",
    imageURL: "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fwallpapers.com%2Fimages%2Fhd%2Fhd-book-cover-of-the-little-prince-e9t2ems785swmqq0.jpg&f=1&nofb=1&ipt=80d443b289d1150ca14f7c709750b02fb99589b67541aab3c9de9d0fe9c10830"
  },
  {
    title: "The Picture of Dorian Gray",
    author: "Oscar Wilde",
    language: "English",
    published: 1890,
    availability: "Available",
    imageURL: "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fd28hgpri8am2if.cloudfront.net%2Fbook_images%2Fonix%2Fcvr9781476788128%2Fthe-picture-of-dorian-gray-9781476788128_hr.jpg&f=1&nofb=1&ipt=c74efffc7ffc3b837ab89597bc9f028f69516c8ee49c12b211b7ae7b7400209e"
  },
];

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
            {books.map((book, index) => (
              <CarouselItem key={index} className="md:basis-1/3">
                <div className="w-full bg-card p-6 flex flex-col items-center gap-4">
                  <img className="max-h-[400px]" src={book.imageURL} alt={book.title + ' image'} />

                  <div className="w-full">
                    <p className="text-lg font-bold">{book.title}</p>
                    <p className="opacity-70">{book.author}</p>
                    <p className="opacity-70">{book.language}</p>
                    <p className="opacity-70">{book.published}</p>
                    <p>{book.availability}</p>
                  </div>

                  <Button className="w-full bg-primary text-light cursor-pointer hover:opacity-80 text-xl py-3">
                    <Link href={`/book/someid`}>Learn more</Link>
                  </Button>
                </div>
              </CarouselItem>
            ))}
          </CarouselContent>
          <CarouselPrevious size="lg" />
          <CarouselNext size="lg" />
        </Carousel>

        <div className="flex justify-center items-center gap-2">
          {books.map((_, index) => (
            <div key={index} className="w-4 h-4 rounded-full bg-card flex justify-center items-center">
              { current === (index + 1) ? <div className="w-3 h-3 rounded-full bg-primary" /> : <></> }
            </div>
          ))}
        </div>
    </div>
  )
}

export default HomeTrendingSection