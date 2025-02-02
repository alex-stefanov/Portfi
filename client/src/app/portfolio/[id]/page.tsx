import { notFound } from 'next/navigation';

import { Portfolio } from '@/features/portfolio/components/Portfolio';

import type { Portfolio as PortfolioType } from '@/features/portfolio/types/Portfolio';

// This would typically come from a database
const portfolios: PortfolioType[] = [
  {
    id: '1',
    title: 'Web Developer',
    author: 'Alice Johnson',
    avatar: 'https://www.gravatar.com/avatar/2c7d99fe281ecd3bcd65ab915bac6dd5?s=250',
    description:
      'A showcase of innovative web designs and interactive user experiences.',
    tags: ['UI/UX', 'Web Design', 'Animation'],
    technologies: ['React', 'Three.js', 'GSAP'],
    projectCount: 15,
    likes: 230,
    views: 1500,
    rating: 4.8,
    images: [
      '/placeholder.svg?height=600&width=800',
      '/placeholder.svg?height=600&width=800',
      '/placeholder.svg?height=600&width=800',
      '/placeholder.svg?height=600&width=800',
      '/placeholder.svg?height=600&width=800',
    ],
    cv: '/path-to-cv.pdf',
    socialLinks: {
      github: 'https://github.com/alicejohnson',
      linkedin: 'https://linkedin.com/in/alicejohnson',
      twitter: 'https://x.com/alicejohnson',
      instagram: "https://instagram.com/alicejohnson",
      facebook: "https://facebook.com/alicejohnson",
    },
    projects: [
      {
        id: '1',
        title: 'E-commerce Redesign',
        description:
          'Complete overhaul of an e-commerce platform focusing on UX improvements.',
      },
      {
        id: '2',
        title: 'Interactive Data Visualization',
        description:
          'Created an interactive dashboard for visualizing complex datasets.',
      },
      {
        id: '3',
        title: 'Mobile App UI Kit',
        description:
          'Designed a comprehensive UI kit for rapid mobile app development.',
      },
    ],
  },
  // ... other portfolios
];

export default async function PortfolioPage({
  params,
}: {
  params: Promise<{ id: string }>;
}) {
  const { id } = await params;

  const portfolio = portfolios.find((p) => p.id === id);

  if (!portfolio) {
    notFound();
  }

  return (
    <div className="container">
      <Portfolio portfolio={portfolio} />
    </div>
  );
}
