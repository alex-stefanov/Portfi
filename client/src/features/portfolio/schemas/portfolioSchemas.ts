import { z } from 'zod';

export const socialLinksSchema = z.object({
  github: z.string().trim().max(255).url().or(z.literal('')),
  linkedin: z.string().trim().max(255).url().or(z.literal('')),
  x: z.string().trim().max(255).url().or(z.literal('')),
  instagram: z.string().trim().max(255).url().or(z.literal('')),
  facebook: z.string().trim().max(255).url().or(z.literal('')),
});

export type SocialLinks = z.infer<typeof socialLinksSchema>;

type SocialMediaKey = keyof SocialLinks;

export const socialMediaNames = Object.keys(
  socialLinksSchema.shape,
) as SocialMediaKey[];

export type Project = {
  id: string;
  title: string;
  description: string;
  technologies: string[];
  images: string[];
  liveUrl: string;
  sourceCode: string;
};

type UserDetails = {
  id: string;
  avatar: string;
  realName: string;
  email: string;

  location?: {
    country: string;
    state?: string;
    city: string;
  };

  education?: {
    yearFrom: number;
    yearTo: number;
    place: string;
  };

  currentPosition?: {
    companyName: string;
    position: string;
  };

  workExperience?: {
    yearFrom: number;
    yearTo: number;
    companyName: string;
    position: string;
    positionDescription: string;
  }[];
};

export type Portfolio = {
  id: string;
  person_id: string;
  title: string;
  description: string;
  tags: string[];
  technologies: string[];
  biography: string;
  likes: number;
  is_public: boolean;
  views: number;
  rating: number;
  images: string[];
  cv: string | null;
  social_media_links: SocialLinks;
  projects: Project[];
  user_details: UserDetails;
};

// Required data for each portfolio component (on Discover portfolios page or top portfolios on landing page)
// e.g. /api/portfolio/top or /api/portfolio?cursor=init

export type PortfolioDiscover = {
  id: string;
  title: string;
  user_details: {
    avatar: string;
    realName: string;
  };
  images: string[];
  description: string;
  tags: string[];
  technologies: string[];
  likes: number;
};
