'use client';

import { Eye, Heart } from 'lucide-react';
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
    <header className="bg-slate-800 h-[600px] py-16 text-primary-foreground">
      <div className="container">
        <div className="mb-4 flex items-center justify-between">
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
              <p className="text-xl">{portfolio.author}</p>
            </div>
          </div>
          <Heart
            size="32"
            onClick={toggleFavorite}
            className={clsx(
              'cursor-pointer text-red-500',
              isFavorite ? 'fill-current' : '',
            )}
          />
        </div>
        <div className="flex items-center justify-between">
          <SocialLinks socialLinks={portfolio.socialLinks} />
          <div className="flex items-center">
            <Eye className="mr-2 h-4 w-4" />
            <small>{portfolio.views} views</small>
          </div>
        </div>
      </div>
    </header>
  );
};
