import { Button } from '@/components/ui/button';

export const Hero = () => {
  return (
    <section className="bg-gradient-to-r from-slate-700 to-blue-950 py-20 lg:py-40">
      <div className="container text-center">
        <h1 className="mb-6 text-4xl font-bold text-white md:text-6xl">
          Build a Professional Portfolio with Ease
        </h1>
        <p className="mx-auto mb-8 max-w-2xl text-xl text-white">
          Effortlessly showcase your skills with Portfio. Our simple tools help you
          create a professional online presence that impresses.
        </p>
        <Button size="lg" variant="secondary">
          Start Your Free Trial
        </Button>
      </div>
    </section>
  );
};
