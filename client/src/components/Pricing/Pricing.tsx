import { Check } from 'lucide-react';

import { Button } from '@/components/ui/button';

const plans = [
  {
    name: 'Basic',
    price: '$19',
    features: ['5 Projects', '10 Team Members', 'Basic Analytics', 'Email Support'],
  },
  {
    name: 'Pro',
    price: '$49',
    features: [
      'Unlimited Projects',
      '50 Team Members',
      'Advanced Analytics',
      'Priority Support',
    ],
  },
  {
    name: 'Enterprise',
    price: 'Custom',
    features: [
      'Unlimited Everything',
      'Dedicated Account Manager',
      'Custom Integrations',
      '24/7 Phone Support',
    ],
  },
];

export const Pricing = () => {
  return (
    <section id="pricing" className="bg-gray-50 py-20">
      <div className="container">
        <h2 className="mb-12 text-center text-3xl font-bold">Choose Your Plan</h2>
        <div className="grid grid-cols-1 gap-8 md:grid-cols-3">
          {plans.map((plan, index) => (
            <div
              key={index}
              className="flex flex-col rounded-lg bg-white p-8 shadow-md"
            >
              <h3 className="mb-4 text-2xl font-bold">{plan.name}</h3>
              <p className="mb-6 text-4xl font-bold">{plan.price}</p>
              <ul className="mb-8 flex-grow">
                {plan.features.map((feature, featureIndex) => (
                  <li key={featureIndex} className="mb-2 flex items-center">
                    <Check className="mr-2 h-5 w-5 text-primary" />
                    <span>{feature}</span>
                  </li>
                ))}
              </ul>
              <Button className="w-full">Get Started</Button>
            </div>
          ))}
        </div>
      </div>
    </section>
  );
};
