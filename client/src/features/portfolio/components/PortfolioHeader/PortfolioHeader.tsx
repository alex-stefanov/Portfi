'use client';

import { Eye, Heart, Star } from 'lucide-react';
import { useState } from 'react';

import { AvatarFallback, AvatarImage } from '@radix-ui/react-avatar';
import clsx from 'clsx';

import { Avatar } from '@/components/ui/avatar';

import { PortfolioTitle } from './PortfolioTitle';
import { SocialLinks } from './SocialLinks';

import type { Portfolio } from '../../types/Portfolio';

export const PortfolioHeader = ({ portfolio }: { portfolio: Portfolio }) => {
  const [isFavorite, setIsFavorite] = useState<boolean>(false);

  const toggleFavorite = () => {
    setIsFavorite((prev) => !prev);
    // Placeholder for database interaction
  };

  return (
    <header className="h-[600px] bg-slate-800 py-16 text-primary-foreground">
      <div className="container h-full">
        <div className="flex h-full items-start justify-between gap-2">
          {/* 1 */}
          <section className="mb-4 flex justify-between">
            <div className="flex items-center space-x-4">
              <Avatar className="h-32 w-32">
                <AvatarImage
                  src={portfolio.avatar || 'https://github.com/shadcn.png'}
                  alt={portfolio.author}
                />
                <AvatarFallback>Pic</AvatarFallback>
              </Avatar>
              <div>
                <PortfolioTitle title={portfolio.title} />
                <p className="mb-5 text-xl">{portfolio.author}</p>
                <SocialLinks socialLinks={portfolio.socialLinks} />
              </div>
            </div>
          </section>
          {/* 2 */}
          <section className="flex h-full flex-col pt-3.5 items-end">
            <PortfolioRating rating={portfolio.rating} />

            <div className="mt-auto flex items-center">
              <Eye className="mr-2 h-4 w-4" />
              <small>{portfolio.views} views</small>
            </div>
          </section>
        </div>
        {/* <section className="flex items-center">
          <Heart
            size="32"
            onClick={toggleFavorite}
            className={clsx(
              'cursor-pointer text-red-500',
              isFavorite ? 'fill-current' : '',
            )}
          />
        </section> */}
      </div>
    </header>
  );
};

const PortfolioRating = ({ rating }: { rating: number }) => {
  return (
    <div className="flex items-center gap-1">
      {[...Array(5)].map((_, i) => (
        <Star
          key={i}
          className={clsx(
            'h-4 w-4',
            i < Math.floor(rating)
              ? 'fill-current text-yellow-300'
              : 'text-gray-300',
          )}
        />
      ))}
      <span className="ml-2">{rating.toFixed(1)}</span>
    </div>
  );
};
