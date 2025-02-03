'use client';

import { Download } from 'lucide-react';

import { Badge } from '@/components/ui/badge';
import { Button } from '@/components/ui/button';
import { Card, CardContent } from '@/components/ui/card';

import type { Portfolio } from '../types/Portfolio';

export const PortfolioDetails = ({ portfolio }: { portfolio: Portfolio }) => {
  return (
    <Card className="!mb-14">
      <CardContent className="p-6">
        <div className="mb-4 flex items-start justify-between">
          <div>
            <h2 className="mb-2 text-2xl font-bold">About</h2>
            <p>{portfolio.description}</p>
          </div>
        </div>
        <div className="mb-4">
          <h3 className="mb-2 text-lg font-semibold">Technologies</h3>
          <div className="mb-2 flex flex-wrap gap-2">
            {portfolio.technologies.map((tech, index) => (
              <Badge
                key={index}
                variant="secondary"
                className="flex items-center gap-1"
              >
                {tech}
              </Badge>
            ))}
          </div>
        </div>
        <div className="mb-4">
          <h3 className="mb-2 text-lg font-semibold">Tags</h3>
          <div className="mb-2 flex flex-wrap gap-2">
            {portfolio.tags.map((tag, index) => (
              <Badge key={index} className="flex items-center gap-1">
                {tag}
              </Badge>
            ))}
          </div>
        </div>
        <div className="flex gap-4">
          <Button variant="outline">
            <Download className="mr-2 h-4 w-4" /> Download CV
          </Button>
          <Button variant="outline">
            <Download className="mr-2 h-4 w-4" /> Download PDF
          </Button>
        </div>
      </CardContent>
    </Card>
  );
};
