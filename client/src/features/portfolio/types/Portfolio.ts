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

export type Portfolio = {
  id: string;
  title: string;
  author: string;
  avatar: string;
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
};

export enum SocialLinksEnum {
  Github = 'Github',
  LinkedIn = 'LinkedIn',
  X = 'X',
  Instagram = 'Instagram',
  Facebook = 'Facebook',
}

export const SocialLinksArr = Object.values(SocialLinksEnum);
