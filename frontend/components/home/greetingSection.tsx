import React from 'react'
import styles from './styles.module.css'
import { Button } from '../ui/button'

function HomeGreetingSection() {
  return (
    <div className={`w-full p-12 relative overflow-hidden ${styles.sectionContainer}`}>
        <div className="relative z-10 w-full sm:w-1/2 text-center sm:text-left">
            <div className="text-light mb-6">
                <p className="font-bold text-3xl mb-2">Welcome to Library+</p>

                <p>Here you can browse our vast collection of books and rent the ones that are the best for you. Our catalog contains 10,000 written pieces, so everyone can find something for himself</p>
            </div>

            <Button className="bg-primary text-light cursor-pointer hover:opacity-80 text-xl px-8 py-6">Browse catalog</Button>
        </div>
    </div>
  )
}

export default HomeGreetingSection