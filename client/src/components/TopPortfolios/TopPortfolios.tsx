import Image from 'next/image';
import Link from 'next/link';

import { Badge } from '@/components/ui/badge';
import {
  Card,
  CardContent,
  CardFooter,
  CardHeader,
  CardTitle,
} from '@/components/ui/card';

const portfolios = [
  {
    id: 1,
    title: 'Creative Design Studio',
    author: 'Alice Johnson',
    image: '/placeholder.svg?height=300&width=400',
    description:
      'A showcase of innovative web designs and interactive user experiences.',
    tags: ['UI/UX', 'Web Design', 'Animation'],
    technologies: ['React', 'Three.js', 'GSAP'],
    projectCount: 15,
    likes: 230,
  },
  {
    id: 2,
    title: 'Tech Innovator',
    author: 'Bob Smith',
    image: '/placeholder.svg?height=300&width=400',
    description:
      'Cutting-edge web applications with a focus on performance and scalability.',
    tags: ['Full Stack', 'Cloud', 'API'],
    technologies: ['Node.js', 'React', 'AWS'],
    projectCount: 12,
    likes: 189,
  },
  {
    id: 3,
    title: 'Frontend Maestro',
    author: 'Carol White',
    image: '/placeholder.svg?height=300&width=400',
    description:
      'Crafting beautiful and responsive user interfaces for modern web applications.',
    tags: ['Frontend', 'Responsive', 'Accessibility'],
    technologies: ['Vue.js', 'Tailwind CSS', 'TypeScript'],
    projectCount: 20,
    likes: 275,
  },
  {
    id: 4,
    title: 'Backend Architect',
    author: 'David Brown',
    image: '/placeholder.svg?height=300&width=400',
    description:
      'Building robust and scalable backend systems for high-traffic applications.',
    tags: ['Backend', 'Microservices', 'DevOps'],
    technologies: ['Go', 'Docker', 'Kubernetes'],
    projectCount: 8,
    likes: 152,
  },
  {
    id: 5,
    title: 'UX/UI Designer',
    author: 'Eva Green',
    image: '/placeholder.svg?height=300&width=400',
    description:
      'Creating intuitive and visually appealing user experiences for web and mobile.',
    tags: ['UX Research', 'UI Design', 'Prototyping'],
    technologies: ['Figma', 'Adobe XD', 'Sketch'],
    projectCount: 25,
    likes: 310,
  },
  {
    id: 6,
    title: 'Full Stack Developer',
    author: 'Frank Lee',
    image: '/placeholder.svg?height=300&width=400',
    description:
      'Versatile developer comfortable with both frontend and backend technologies.',
    tags: ['Full Stack', 'Web Apps', 'Mobile'],
    technologies: ['JavaScript', 'Python', 'React Native'],
    projectCount: 18,
    likes: 205,
  },
  {
    id: 7,
    title: 'E-commerce Specialist',
    author: 'Grace Taylor',
    image: '/placeholder.svg?height=300&width=400',
    description:
      'Developing high-converting e-commerce platforms with seamless user experiences.',
    tags: ['E-commerce', 'Payment Integration', 'SEO'],
    technologies: ['Shopify', 'Next.js', 'Stripe'],
    projectCount: 10,
    likes: 178,
  },
  {
    id: 8,
    title: 'Web Performance Guru',
    author: 'Henry Wilson',
    image: '/placeholder.svg?height=300&width=400',
    description:
      'Optimizing web applications for lightning-fast performance and efficiency.',
    tags: ['Performance', 'Optimization', 'Analytics'],
    technologies: ['Webpack', 'Lighthouse', 'Google Analytics'],
    projectCount: 14,
    likes: 220,
  },
  {
    id: 9,
    title: 'AI Integration Expert',
    author: 'Ivy Chen',
    image: '/placeholder.svg?height=300&width=400',
    description: 'Incorporating cutting-edge AI technologies into web applications.',
    tags: ['AI', 'Machine Learning', 'NLP'],
    technologies: ['TensorFlow.js', 'Python', 'OpenAI API'],
    projectCount: 7,
    likes: 195,
  },
];

export const TopPortfolios = () => {
  return (
    <section id="top-portfolios" className="bg-gray-50 py-20">
      <div className="container">
        <h2 className="mb-12 text-center text-3xl font-bold">Top User Portfolios</h2>
        <div className="grid grid-cols-1 gap-8 md:grid-cols-2 lg:grid-cols-3">
          {portfolios.map((portfolio) => (
            <Link key={portfolio.id} href="/portfolio/1">
              <Card className="flex flex-col overflow-hidden">
                <CardHeader className="p-0">
                  <Image
                    src={portfolio.image || '/placeholder.svg'}
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
                  <p className="mb-2 text-sm text-gray-600">by {portfolio.author}</p>
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
                  <span className="text-sm text-gray-600">
                    {portfolio.projectCount} Projects
                  </span>
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
