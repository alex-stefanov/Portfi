'use client';

import { Heart, Star, ThumbsDown, ThumbsUp } from 'lucide-react';
import React, { useState } from 'react';

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

  const likesHandler = () => {
    setIsLiked((prev) => !prev);
  };

  return (
    <Button
      variant={isLiked ? 'secondary' : 'default'}
      onClick={likesHandler}
      size="sm"
    >
      <ThumbsUp
        className={clsx(
          'cursor-pointer',
          isLiked && 'fill-current text-blue-500',
          // isLiked ? 'fill-current text-blue-500' : 'text-slate-700',
        )}
        size={22}
      />

      <p className="text-small">{isLiked ? 'Liked' : 'Like'}</p>
    </Button>
  );
};

const PortfolioRating = ({ rating }: { rating: number }) => {
  return (
    <div className="flex items-center gap-1">
      {[...Array(5)].map((_, i) => (
        <Star
          key={i}
          className={clsx(
            'h-5 w-5',
            i < Math.floor(rating)
              ? 'fill-current text-yellow-600'
              : 'text-gray-300',
          )}
        />
      ))}
      <span className="ml-2">{rating.toFixed(1)}</span>
    </div>
  );
};
