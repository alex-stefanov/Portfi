'use client';

import { ExternalLink, Plus } from 'lucide-react';
import { useEffect, useState } from 'react';

import { SiGithub } from '@icons-pack/react-simple-icons';
import clsx from 'clsx';

import { Badge } from '@/components/ui/badge';
import { Button } from '@/components/ui/button';
import { Card, CardContent, CardFooter, CardHeader } from '@/components/ui/card';
import {
  Carousel,
  CarouselApi,
  CarouselContent,
  CarouselItem,
  CarouselNext,
  CarouselPrevious,
} from '@/components/ui/carousel';

import type { Project } from '../../schemas/portfolioSchemas';

export const ProjectList = ({ projects }: { projects: Project[] }) => {
  return (
    <section className="mb-8">
      <h2 className="mb-4 text-2xl font-bold">Projects</h2>
      <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-3">
        {projects.map((project) => (
          <ProjectCard project={project} key={project.id} />
        ))}
      </div>
      <Button className="mt-4">
        <Plus className="mr-2 h-4 w-4" /> Add New Project
      </Button>
    </section>
  );
};

const ProjectCard = ({ project }: { project: Project }) => {
  return (
    <Card className="flex h-full flex-col">
      <CardHeader className="p-0">
        <ImageCarousel images={project.images} />
      </CardHeader>
      <CardContent className="flex-grow pt-6">
        <h3 className="mb-2 text-2xl font-bold">{project.title}</h3>
        <p className="mb-4 text-muted-foreground">{project.description}</p>
        <div className="mb-4 flex flex-wrap gap-2">
          {project.technologies.map((tech) => (
            <Badge key={tech} variant="secondary">
              {tech}
            </Badge>
          ))}
        </div>
      </CardContent>
      <CardFooter className="flex justify-between">
        <Button variant="outline" asChild>
          <a href={project.liveUrl} target="_blank" rel="noopener noreferrer">
            <ExternalLink className="mr-2 h-4 w-4" />
            Live Demo
          </a>
        </Button>
        <Button variant="outline" asChild>
          <a href={project.sourceCode} target="_blank" rel="noopener noreferrer">
            <SiGithub className="mr-2 h-4 w-4" />
            Source Code
          </a>
        </Button>
      </CardFooter>
    </Card>
  );
};

const ImageCarousel = ({ images }: { images: string[] }) => {
  const [api, setApi] = useState<CarouselApi>();
  const [current, setCurrent] = useState(0);

  useEffect(() => {
    if (!api) {
      return;
    }

    setCurrent(api.selectedScrollSnap());

    api.on('select', () => {
      setCurrent(api.selectedScrollSnap());
    });
  }, [api]);

  return (
    <Card className="relative w-full overflow-hidden">
      <CardContent className="p-0">
        <Carousel className="w-full" setApi={setApi}>
          <CarouselContent>
            {images.map((image, index) => (
              <CarouselItem key={index}>
                <div className="relative aspect-video h-full">
                  <img
                    src={image || '/placeholder.svg'}
                    alt={`Project image ${index + 1}`}
                    className="h-full w-full object-cover transition-opacity duration-500"
                  />
                </div>
              </CarouselItem>
            ))}
          </CarouselContent>
          <CarouselPrevious className="left-2" />
          <CarouselNext className="right-2" />
        </Carousel>
        <div className="absolute bottom-4 left-1/2 flex -translate-x-1/2 transform space-x-2">
          {images.map((_, index) => (
            <button
              key={index}
              className={clsx(
                'h-2 w-2 rounded-full transition-all duration-300',
                index === current ? 'scale-125 bg-white' : 'bg-white/50',
              )}
              onClick={() => api?.scrollTo(index)}
            />
          ))}
        </div>
      </CardContent>
    </Card>
  );
};
