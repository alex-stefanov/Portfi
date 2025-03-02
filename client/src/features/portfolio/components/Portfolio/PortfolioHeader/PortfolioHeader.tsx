import { Briefcase, GraduationCap, Mail, MapPin } from 'lucide-react';

import { AvatarFallback, AvatarImage } from '@radix-ui/react-avatar';

import { SocialMediaIconLinks } from '@/features/portfolio/components/Portfolio/PortfolioHeader/SocialMediaIconLinks';
import { Avatar } from '@/components/ui/avatar';
import { Separator } from '@/components/ui/separator';

import { EditableSocialLinks } from './EditableSocialLinks';
import { PortfolioTitle } from './PortfolioTitle';

import type { Portfolio } from '../../../schemas/portfolioSchemas';

export const PortfolioHeader = ({ portfolio }: { portfolio: Portfolio }) => {
  const { social_media_links } = portfolio;
  const isEditable = true;

  return (
    <header className="rounded-xl bg-slate-800 px-4 pb-8 pt-16 text-primary-foreground sm:px-8 md:px-12">
      <section>
        <UserProfile portfolio={portfolio} />
      </section>

      <Separator className="mb-5 mt-8 bg-slate-700" />

      <section className="flex flex-col items-center justify-center space-y-10 md:flex-row-reverse md:items-end md:justify-between md:space-y-0">
        <PortfolioStats views={portfolio.views} />

        {isEditable ? (
          <EditableSocialLinks socialLinks={social_media_links} />
        ) : (
          <SocialMediaIconLinks socials={social_media_links} />
        )}
      </section>
    </header>
  );
};

const UserProfile = ({ portfolio }: { portfolio: Portfolio }) => {
  const { user_details, title } = portfolio;
  const { avatar, email, realName, education, location } = user_details;

  const currentWork = user_details.currentPosition;

  return (
    <>
      <div className="mb-9 flex flex-col items-center space-x-4 space-y-4 md:flex-row md:space-y-0">
        <Avatar className="h-32 w-32 ring-4 ring-slate-700">
          <AvatarImage
            src={avatar || 'https://github.com/shadcn.png'}
            alt={realName}
          />
          <AvatarFallback>Pic</AvatarFallback>
        </Avatar>
        <div className="flex flex-col items-center text-center md:items-start md:text-left">
          <PortfolioTitle title={title} />
          <p className="mb-5 text-[1.15rem]">{realName}</p>
          <p className="text-[0.9rem] text-slate-300 sm:text-left">
            A highly motivated and results-oriented full-stack web developer.
          </p>
        </div>
      </div>

      <div className="flex flex-col items-center space-y-2 md:items-start">
        {location && (
          <InfoBadge
            icon={<MapPin size={16} />}
            text={`${location.city}, ${location.country}`}
          />
        )}
        {currentWork && (
          <InfoBadge
            icon={<Briefcase size={16} />}
            text={`${currentWork.position} at ${currentWork.companyName}`}
          />
        )}
        {education && (
          <InfoBadge
            icon={<GraduationCap size={16} />}
            text={`${education.place} (${education.yearFrom}-${education.yearTo})`}
          />
        )}

        <InfoBadge icon={<Mail size={16} />} text={email} />
      </div>
    </>
  );
};

const InfoBadge = ({ icon, text }: { icon: React.ReactNode; text: string }) => (
  <div className="flex items-center gap-1 text-[0.813rem] text-slate-200">
    {icon}
    <span>{text}</span>
  </div>
);

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
