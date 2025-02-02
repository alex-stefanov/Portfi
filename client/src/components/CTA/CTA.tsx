import { Button } from '@/components/ui/button';

export const CTA = () => {
  return (
    <section className="bg-primary py-20">
      <div className="container text-center">
        <h2 className="mb-4 text-3xl font-bold text-white">
          Showcase Your Work Like a Pro
        </h2>
        <p className="mb-8 text-xl text-white">
          Join thousands of creators and professionals who’ve built stunning
          portfolios effortlessly.
        </p>
        <Button size="lg" variant="secondary">
          Start Your Free Trial Today
        </Button>
      </div>
    </section>
  );
};
