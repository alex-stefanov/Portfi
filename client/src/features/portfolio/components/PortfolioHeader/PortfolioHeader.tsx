import { AvatarFallback, AvatarImage } from '@radix-ui/react-avatar';

import { Avatar } from '@/components/ui/avatar';
import { Separator } from '@/components/ui/separator';

import { PortfolioTitle } from './PortfolioTitle';
import { SocialLinks } from './SocialLinks';

import type { Portfolio } from '../../types/Portfolio';

export const PortfolioHeader = ({ portfolio }: { portfolio: Portfolio }) => {
  return (
    <header className="space-y-14 rounded-xl bg-slate-800 px-4 pb-8 pt-16 text-primary-foreground sm:px-8 md:px-12">
      <section className="h-full gap-2 sm:flex sm:items-start sm:justify-between">
        <UserProfile
          author={portfolio.author}
          avatar={portfolio.avatar}
          title={portfolio.title}
        />
      </section>

      <section className="flex flex-col items-center justify-center space-y-10 md:flex-row-reverse md:items-end md:justify-between">
        <PortfolioStats views={portfolio.views} />
        <SocialLinks socialLinks={portfolio.socialLinks} />
      </section>
    </header>
  );
};

const UserProfile = ({
  avatar,
  author,
  title,
}: {
  avatar: string;
  author: string;
  title: string;
}) => {
  return (
    <div className="flex flex-col items-center space-x-4 space-y-4 sm:flex-row sm:space-y-0">
      <Avatar className="h-32 w-32">
        <AvatarImage src={avatar || 'https://github.com/shadcn.png'} alt={author} />
        <AvatarFallback>Pic</AvatarFallback>
      </Avatar>
      <div className="flex flex-col items-center text-center sm:items-start sm:text-left">
        <PortfolioTitle title={title} />
        <p className="text-md mb-5">{author}</p>
        <p className="text-sm sm:text-left">
          A highly motivated and results-oriented full-stack web developer.
        </p>
      </div>
    </div>
  );
};

const PortfolioStats = ({ views }: { views: number }) => {
  return (
    <div className="flex md:-mb-2.5">
      <div className="flex flex-col items-center gap-1">
        <p className="font-bold">92</p>
        <small className="text-gray-200">Likes</small>
      </div>

      <Separator
        orientation="vertical"
        className="h-100 mx-6 my-2.5 bg-gray-700"
        decorative={true}
      />

      <div className="flex flex-col items-center gap-1">
        <p className="font-bold">9</p>
        <small className="text-gray-200">Projects</small>
      </div>

      <Separator
        orientation="vertical"
        className="h-100 mx-6 my-2.5 bg-gray-700"
        decorative={true}
      />

      <div className="flex flex-col items-center gap-1">
        <p className="font-bold">{views}</p>
        <small className="text-gray-200">Views</small>
      </div>
    </div>
  );
};
