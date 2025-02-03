export type SocialLinks = {
  github?: string;
  linkedin?: string;
  twitter?: string;
  instagram?: string;
  facebook?: string;
};

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
  authorId: string;
  title: string;
  description: string;
  tags: string[];
  technologies: string[];
  projectCount: number;
  likes: number;
  views: number;
  rating: number;
  images: string[];
  cv: string;
  socialLinks: SocialLinks;
  projects: Project[];
  userDetails: UserDetails;
};

export enum SocialLinksEnum {
  Github = 'Github',
  LinkedIn = 'LinkedIn',
  X = 'X',
  Instagram = 'Instagram',
  Facebook = 'Facebook',
}

export const SocialLinksArr = Object.values(SocialLinksEnum);
