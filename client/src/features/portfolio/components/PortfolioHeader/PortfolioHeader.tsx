'use client';

import { Edit, Eye, Heart, Save } from 'lucide-react';
import { useState } from 'react';

import clsx from 'clsx';

import { Button } from '@/components/ui/button';

import { PortfolioTitle } from './PortfolioTitle';
import { SocialLinks } from './SocialLinks';

import type { Portfolio } from '../../types/Portfolio';

export const PortfolioHeader = ({ portfolio }: { portfolio: Portfolio }) => {
  const [isEditing, setIsEditing] = useState<boolean>(false);
  const [isFavorite, setIsFavorite] = useState<boolean>(false);

  const handleSave = () => {
    setIsEditing(false);
  };

  const toggleFavorite = () => {
    setIsFavorite((prev) => !prev);
    // Placeholder for database interaction
  };

  return (
    <header className="bg-slate-800 py-8 text-primary-foreground">
      <div className="container">
        <div className="mb-4 flex items-center justify-between">
          <div className="flex items-center space-x-4">
            <img
              src={portfolio.avatar || '/placeholder.svg'}
              alt={portfolio.author}
              width={60}
              height={60}
              className="rounded-full"
            />
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
