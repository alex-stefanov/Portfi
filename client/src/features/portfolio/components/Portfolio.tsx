import { ImageGallery } from './ImageGallery';
import { PortfolioDetails } from './PortfolioDetails';
import { PortfolioHeader } from './PortfolioHeader/PortfolioHeader';
import { ProjectList } from './ProjectList';
import ThemeCustomizer from './ThemeCustomizer';

import type { Portfolio as TPortfolio } from '../types/Portfolio';

export const Portfolio = ({ portfolio }: { portfolio: TPortfolio }) => {
  return (
    <>
      <PortfolioHeader portfolio={portfolio} />
      <ImageGallery images={portfolio.images} />
      <PortfolioDetails portfolio={portfolio} />
      <ProjectList projects={portfolio.projects} />
      <ThemeCustomizer />
    </>
  );
};
