import { TopPortfolios } from '@/features/portfolio/components/TopPortfolios/TopPortfolios';

import { CTA } from '@/components/CTA/CTA';
import { Features } from '@/components/Features/Features';
import { Hero } from '@/components/Hero/Hero';
import { Testimonials } from '@/components/Testimonials/Testimonials';

export default function Home() {
  return (
    <>
      <Hero />
      <Features />
      <Testimonials />
      <TopPortfolios />
      <CTA />
    </>
  );
}
