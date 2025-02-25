import Link from 'next/link';

import { siteRoutes } from '@/config/site';

import { PortfolioDiscover } from '@/features/portfolio/schemas/portfolioSchemas';

// import { apiRoutes } from '@/constants/apiRoutes';

// import * as api from '@/utils/api';

import { Badge } from '@/components/ui/badge';
import {
  Card,
  CardContent,
  CardFooter,
  CardHeader,
  CardTitle,
} from '@/components/ui/card';

export const TopPortfolios = async () => {
  const portfolios = await getTopPortfolios();

  return (
    <section id="top-portfolios" className="bg-gray-50 py-20">
      <div className="container">
        <h2 className="mb-12 text-center text-3xl font-bold">Top User Portfolios</h2>
        <div className="grid grid-cols-1 gap-8 md:grid-cols-2 lg:grid-cols-3">
          {portfolios.map((portfolio) => (
            <Link
              key={portfolio.id}
              href={`${siteRoutes.private.portfolios}/${portfolio.id}`}
            >
              <Card className="flex flex-col overflow-hidden">
                <CardHeader className="p-0">
                  <img
                    src={portfolio.images[0]}
                    alt={portfolio.title}
                    width={400}
                    height={300}
                    className="h-48 w-full object-cover"
                  />
                </CardHeader>
                <CardContent className="p-4">
                  <CardTitle className="mb-2 text-xl transition-colors duration-300 group-hover:text-primary">
                    {portfolio.title}
                  </CardTitle>
                  <p className="mb-2 text-sm text-gray-600">
                    by {portfolio.user_details.realName}
                  </p>
                  <p className="mb-4 text-sm text-gray-700">
                    {portfolio.description}
                  </p>
                  <div className="mb-4 flex flex-wrap gap-2">
                    {portfolio.tags.map((tag, index) => (
                      <Badge key={index} variant="secondary">
                        {tag}
                      </Badge>
                    ))}
                  </div>
                  <div className="mb-2 text-sm text-gray-600">
                    <strong>Technologies:</strong>{' '}
                    {portfolio.technologies.join(', ')}
                  </div>
                </CardContent>
                <CardFooter className="mt-auto flex items-center justify-between bg-gray-50 p-4">
                  <span className="text-sm text-gray-600">5 Projects</span>
                  <span className="text-sm text-gray-600">
                    {portfolio.likes} Likes
                  </span>
                </CardFooter>
              </Card>
            </Link>
          ))}
        </div>
      </div>
    </section>
  );
};

const getTopPortfolios = async () => {
  // const url = apiRoutes.portfolio.top;

  // try {
  //   const portfolios: PortfolioDiscover[] = await api.get(url);
  // } catch (err) {
  //   console.error('Failed to fetch top portfolios');
  // }

  // simulate fetching portfolios from an API by adding delay
  await new Promise((resolve) => setTimeout(resolve, 300));

  return portfolios;
};

const portfolioImgUrl = 'https://picsum.photos/800/600?random=1';

const portfolios: PortfolioDiscover[] = [
  {
    id: 'af7c1fe6-d669-414e-b066-e9733f0de7a8',
    title: 'Creative Design Studio',
    user_details: {
      avatar:
        'https://www.gravatar.com/avatar/2c7d99fe281ecd3bcd65ab915bac6dd5?s=250',
      realName: 'Alice Johnson',
    },
    images: [portfolioImgUrl],
    description:
      'A showcase of innovative web designs and interactive user experiences.',
    tags: ['UI/UX', 'Web Design', 'Animation'],
    technologies: ['React', 'Three.js', 'GSAP'],
    likes: 230,
  },
  {
    id: 'af7c1fe6-d669-414e-b066-e9733f0de7a9',
    title: 'Tech Innovator',
    user_details: {
      avatar:
        'https://www.gravatar.com/avatar/2c7d99fe281ecd3bcd65ab915bac6dd5?s=250',
      realName: 'Bob Smith',
    },
    images: [portfolioImgUrl],
    description:
      'Cutting-edge web applications with a focus on performance and scalability.',
    tags: ['Full Stack', 'Cloud', 'API'],
    technologies: ['Node.js', 'React', 'AWS'],
    likes: 189,
  },
  {
    id: '3',
    title: 'Frontend Maestro',
    user_details: {
      avatar:
        'https://www.gravatar.com/avatar/2c7d99fe281ecd3bcd65ab915bac6dd5?s=250',
      realName: 'Carol White',
    },
    images: [portfolioImgUrl],
    description:
      'Crafting beautiful and responsive user interfaces for modern web applications.',
    tags: ['Frontend', 'Responsive', 'Accessibility'],
    technologies: ['Vue.js', 'Tailwind CSS', 'TypeScript'],
    likes: 275,
  },
  {
    id: '4',
    title: 'Backend Architect',
    user_details: {
      avatar:
        'https://www.gravatar.com/avatar/2c7d99fe281ecd3bcd65ab915bac6dd5?s=250',
      realName: 'David Brown',
    },
    images: [portfolioImgUrl],
    description:
      'Building robust and scalable backend systems for high-traffic applications.',
    tags: ['Backend', 'Microservices', 'DevOps'],
    technologies: ['Go', 'Docker', 'Kubernetes'],
    likes: 152,
  },
  {
    id: '5',
    title: 'UX/UI Designer',
    user_details: {
      avatar:
        'https://www.gravatar.com/avatar/2c7d99fe281ecd3bcd65ab915bac6dd5?s=250',
      realName: 'Eva Green',
    },
    images: [portfolioImgUrl],
    description:
      'Creating intuitive and visually appealing user experiences for web and mobile.',
    tags: ['UX Research', 'UI Design', 'Prototyping'],
    technologies: ['Figma', 'Adobe XD', 'Sketch'],
    likes: 310,
  },
  {
    id: '6',
    title: 'Full Stack Developer',
    user_details: {
      avatar:
        'https://www.gravatar.com/avatar/2c7d99fe281ecd3bcd65ab915bac6dd5?s=250',
      realName: 'Frank Lee',
    },
    images: [portfolioImgUrl],
    description:
      'Versatile developer comfortable with both frontend and backend technologies.',
    tags: ['Full Stack', 'Web Apps', 'Mobile'],
    technologies: ['JavaScript', 'Python', 'React Native'],
    likes: 205,
  },
  {
    id: '7',
    title: 'E-commerce Specialist',
    user_details: {
      avatar:
        'https://www.gravatar.com/avatar/2c7d99fe281ecd3bcd65ab915bac6dd5?s=250',
      realName: 'Grace Taylor',
    },
    images: [portfolioImgUrl],
    description:
      'Developing high-converting e-commerce platforms with seamless user experiences.',
    tags: ['E-commerce', 'Payment Integration', 'SEO'],
    technologies: ['Shopify', 'Next.js', 'Stripe'],
    likes: 178,
  },
  {
    id: '8',
    title: 'Web Performance Guru',
    user_details: {
      avatar:
        'https://www.gravatar.com/avatar/2c7d99fe281ecd3bcd65ab915bac6dd5?s=250',
      realName: 'Henry Wilson',
    },
    images: [portfolioImgUrl],
    description:
      'Optimizing web applications for lightning-fast performance and efficiency.',
    tags: ['Performance', 'Optimization', 'Analytics'],
    technologies: ['Webpack', 'Lighthouse', 'Google Analytics'],
    likes: 220,
  },
  {
    id: '9',
    title: 'AI Integration Expert',
    user_details: {
      avatar:
        'https://www.gravatar.com/avatar/2c7d99fe281ecd3bcd65ab915bac6dd5?s=250',
      realName: 'Ivy Chen',
    },
    images: [portfolioImgUrl],
    description: 'Incorporating cutting-edge AI technologies into web applications.',
    tags: ['AI', 'Machine Learning', 'NLP'],
    technologies: ['TensorFlow.js', 'Python', 'OpenAI API'],
    likes: 195,
  },
];
