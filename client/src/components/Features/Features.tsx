import { BarChart, Lock, Users, Zap } from 'lucide-react';

const features = [
  {
    icon: <Zap className="h-6 w-6 text-primary" />,
    title: 'Showcase Your Work',
    description:
      'Display your projects with beautifully designed templates that make an impact.',
  },
  {
    icon: <Users className="h-6 w-6 text-primary" />,
    title: 'Customizable Layouts',
    description: 'Personalize every detail to match your unique style and brand.',
  },
  {
    icon: <BarChart className="h-6 w-6 text-primary" />,
    title: 'Mobile-Ready Design',
    description: 'Ensure your portfolio looks amazing on any device or screen size.',
  },
  {
    icon: <Lock className="h-6 w-6 text-primary" />,
    title: 'Easy Sharing',
    description:
      'Share your portfolio seamlessly with clients and collaborators via unique links.',
  },
];

export const Features = () => {
  return (
    <section id="features" className="bg-gray-50 py-20">
      <div className="container">
        <h2 className="mb-12 text-center text-3xl font-bold">Why Choose Portfio?</h2>
        <div className="grid grid-cols-1 gap-8 md:grid-cols-2 lg:grid-cols-4">
          {features.map((feature, index) => (
            <div key={index} className="rounded-lg bg-white p-6 shadow-md">
              <div className="mb-4">{feature.icon}</div>
              <h3 className="mb-2 text-xl font-semibold">{feature.title}</h3>
              <p className="text-gray-600">{feature.description}</p>
            </div>
          ))}
        </div>
      </div>
    </section>
  );
};
