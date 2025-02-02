import { ImageGallery } from './ImageGallery';
import { PortfolioDetails } from './PortfolioDetails';
import { PortfolioHeader } from './PortfolioHeader/PortfolioHeader';
import { ProjectList } from './ProjectList';
import { RateAndLike } from './RatingAndLikes';
import ThemeCustomizer from './ThemeCustomizer';

import type { Portfolio as TPortfolio } from '../types/Portfolio';

export const Portfolio = ({ portfolio }: { portfolio: TPortfolio }) => {
  return (
    <div className="mt-4 space-y-5">
      <PortfolioHeader portfolio={portfolio} />
      <RateAndLike rating={portfolio.rating} />
      <PortfolioDetails portfolio={portfolio} />
      <ImageGallery images={portfolio.images} />
      <ProjectList projects={portfolio.projects} />
      <ThemeCustomizer />
    </div>
  );
};
