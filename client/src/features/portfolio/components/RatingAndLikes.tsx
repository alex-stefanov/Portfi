'use client';

import { Star, ThumbsUp } from 'lucide-react';
import { useState } from 'react';

import clsx from 'clsx';

import { Button } from '@/components/ui/button';

export const RateAndLike = ({ rating }: { rating: number }) => {
  return (
    <section className="flex justify-between">
      <PortfolioRating rating={rating} />
      <LikeOrUnlike />
    </section>
  );
};

const LikeOrUnlike = () => {
  const [isLiked, setIsLiked] = useState(false);

  const likesHandler = async () => {
    // Uncomment

    // const url = 'add api url here';

    // try {
    //   const res = await fetch(url, {
    //     credentials: 'include',
    //   });

    //   if (!res.ok) {
    //     const error = await res.text();
    //     throw new Error(error);
    //   }

    //   const data = await res.json();
    // } catch (err) {
    //   const errMsg = err instanceof Error ? err.message : 'An error occurred';
    //   console.error(errMsg);
    // }

    setIsLiked((prev) => !prev);
  };

  return (
    <Button
      variant={isLiked ? 'secondary' : 'default'}
      onClick={likesHandler}
      size="sm"
    >
      <ThumbsUp
        className={clsx('cursor-pointer', isLiked && 'fill-current text-blue-500')}
        size={22}
      />

      <p className="text-small">{isLiked ? 'Liked' : 'Like'}</p>
    </Button>
  );
};

const PortfolioRating = ({ rating }: { rating: number }) => {
  return (
    <Button size="default" variant="outline" className="h-auto cursor-pointer px-10">
      <div className="flex flex-col">
        <div className="mb-1 flex gap-1">
          {[...Array(5)].map((_, i) => (
            <Star
              size={20}
              key={i}
              className={clsx(
                i < Math.floor(rating)
                  ? 'fill-current text-yellow-500'
                  : 'text-gray-300',
              )}
            />
          ))}
        </div>

        <p>
          <span>{rating.toFixed(1)}</span> out of 5
        </p>

        <p className="font-light text-slate-700">340 reviews</p>
      </div>
    </Button>
  );
};
