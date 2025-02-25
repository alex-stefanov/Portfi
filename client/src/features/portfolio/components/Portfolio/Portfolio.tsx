import { PortfolioDetails } from './PortfolioDetails';
import { ProjectList } from './ProjectList';
import { PortfolioHeader } from './PortfolioHeader/PortfolioHeader';
import { RateAndLike } from './RateAndLike';

import type { Portfolio as TPortfolio } from '../../schemas/portfolioSchemas';

export const Portfolio = ({ portfolio }: { portfolio: TPortfolio }) => {
  return (
    <div className="my-4 space-y-5">
      <PortfolioHeader portfolio={portfolio} />
      <RateAndLike rating={portfolio.rating} />
      <PortfolioDetails portfolio={portfolio} />
      <ProjectList projects={portfolio.projects} />
    </div>
  );
};
