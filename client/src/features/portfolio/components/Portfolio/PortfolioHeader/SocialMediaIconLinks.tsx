import Link from 'next/link';

import { AlertCircle, Linkedin } from 'lucide-react';

import {
  SiFacebook,
  SiGithub,
  SiInstagram,
  SiX,
} from '@icons-pack/react-simple-icons';

import { SocialLinks } from '@/features/portfolio/schemas/portfolioSchemas';

const socialIcons = {
  github: SiGithub,
  linkedin: Linkedin,
  x: SiX,
  instagram: SiInstagram,
  facebook: SiFacebook,
};

export const SocialMediaIconLinks = ({ socials }: { socials: SocialLinks }) => {
  const filterOutEmpty = Object.entries(socials).filter(([_, link]) => link);

  return (
    <div className="relative flex space-x-6">
      {filterOutEmpty.length === 0 ? (
        <div className="flex items-center space-x-2">
          <AlertCircle size="24" />
          <small className="">You haven't set up any social media links yet.</small>
        </div>
      ) : (
        filterOutEmpty.map(([platform, link]) => {
          const Icon = socialIcons[platform as keyof SocialLinks];
          return (
            <Link
              key={platform}
              href={link}
              target="_blank"
              rel="noopener noreferrer"
              className="relative"
            >
              <Icon className="h-7 w-7" />{' '}
            </Link>
          );
        })
      )}
    </div>
  );
};
