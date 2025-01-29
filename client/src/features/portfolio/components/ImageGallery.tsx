'use client';

import Image from 'next/image';

import { ChevronLeft, ChevronRight, Upload } from 'lucide-react';
import { useState } from 'react';

import { Button } from '@/components/ui/button';

type ImageGalleryProps = {
  images: string[];
};

const MAX_FILE_SIZE = 4 * 1024 * 1024; // 4 MB in bytes

export const ImageGallery = ({ images }: ImageGalleryProps) => {
  const [currentIndex, setCurrentIndex] = useState(0);

  const nextImage = () => {
    setCurrentIndex((prevIndex) => (prevIndex + 1) % images.length);
  };

  const prevImage = () => {
    setCurrentIndex((prevIndex) => (prevIndex - 1 + images.length) % images.length);
  };

  const handleImageUpload = (e: React.ChangeEvent<HTMLInputElement>) => {
    // Handle the image upload

    const files = Array.from(e.target.files || []).filter((file) => {
      if (file.type.startsWith('image/') && file.size < MAX_FILE_SIZE) {
        return file;
      }

      console.log('Image uploaded:', files[0]);
    });

    e.target.value = '';
  };

  return (
    <div className="container mb-8">
      <div className="relative mb-4 h-96 w-full">
        <Image
          src={images[currentIndex] || '/placeholder.svg'}
          alt={`Portfolio image ${currentIndex + 1}`}
          layout="fill"
          objectFit="cover"
          className="rounded-lg"
        />
        <Button
          variant="outline"
          className="absolute left-4 top-1/2 -translate-y-1/2 transform"
          onClick={prevImage}
        >
          <ChevronLeft className="h-6 w-6" />
        </Button>
        <Button
          variant="outline"
          className="absolute right-4 top-1/2 -translate-y-1/2 transform"
          onClick={nextImage}
        >
          <ChevronRight className="h-6 w-6" />
        </Button>
      </div>
      <div className="mb-4 flex items-center justify-between">
        <div className="flex space-x-2 overflow-x-auto">
          {images.map((image: string, index: number) => (
            <Image
              key={index}
              src={image || '/placeholder.svg'}
              alt={`Thumbnail ${index + 1}`}
              width={80}
              height={60}
              className={`cursor-pointer rounded ${index === currentIndex ? 'ring-2 ring-primary' : ''}`}
              onClick={() => setCurrentIndex(index)}
            />
          ))}
        </div>
        <div>
          <input
            type="file"
            id="image-upload"
            className="hidden"
            accept="image/*"
            onChange={handleImageUpload}
          />
          <label htmlFor="image-upload">
            <Button variant="outline">
              <Upload className="mr-2 h-4 w-4" /> Upload Images
            </Button>
          </label>
        </div>
      </div>
    </div>
  );
};
