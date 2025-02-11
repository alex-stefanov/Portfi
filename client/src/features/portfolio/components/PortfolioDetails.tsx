'use client';

import { Download } from 'lucide-react';

import { Badge } from '@/components/ui/badge';
import { Button } from '@/components/ui/button';
import { Card, CardContent } from '@/components/ui/card';

import type { Portfolio } from '../types/Portfolio';

export const PortfolioDetails = ({ portfolio }: { portfolio: Portfolio }) => {
  return (
    <section className="!mb-20 flex flex-col gap-20 md:flex-row md:gap-8">
      <Card className="max-w-[40rem] flex-1">
        <CardContent className="p-6">
          <div className="mb-4 flex items-start justify-between">
            <div>
              <h2 className="mb-2 text-2xl font-bold">Bio</h2>
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
      <WorkExperience portfolio={portfolio} />
    </section>
  );
};

const WorkExperience = ({ portfolio }: { portfolio: Portfolio }) => {
  return (
    <div className="flex-1 p-6">
      <h2 className="mb-4 text-2xl font-bold">Work Experience</h2>
      <div className="space-y-6">
        {portfolio.userDetails.workExperience
          ?.sort((a, b) => a.yearFrom - b.yearFrom)
          .map((experience, index) => (
            <div key={index} className="ml-2 border-l-2 border-gray-200 pl-4">
              <div className="mb-1 flex items-center justify-between">
                <h3 className="text-md font-bold">{experience.position}</h3>
                <span className="text-sm text-gray-600">
                  {experience.yearFrom} -{' '}
                  {experience.yearTo === new Date().getFullYear()
                    ? 'Present'
                    : experience.yearTo}
                </span>
              </div>
              <p className="mb-1 text-sm">{experience.companyName}</p>
              <p className="font-light">{experience.positionDescription}</p>
            </div>
          ))}
      </div>
    </div>
  );
};
// const WorkExperience = ({ portfolio }: { portfolio: Portfolio }) => {
//   return (
//     <div className="mb-6 flex-1 rounded-lg bg-white p-6 shadow">
//       <h2 className="mb-4 text-2xl font-semibold">Work Experience</h2>
//       <div className="space-y-6">
//         {portfolio.userDetails.workExperience
//           ?.sort((a, b) => b.yearFrom - a.yearFrom)
//           .map((experience, index) => (
//             <div key={index} className="ml-2 border-l-2 border-gray-200 pl-4">
//               <div className="mb-1 flex items-center justify-between">
//                 <h3 className="text-lg font-semibold">{experience.position}</h3>
//                 <span className="text-sm text-gray-600">
//                   {experience.yearFrom} -{' '}
//                   {experience.yearTo === new Date().getFullYear()
//                     ? 'Present'
//                     : experience.yearTo}
//                 </span>
//               </div>
//               <p className="mb-1 text-gray-700">{experience.companyName}</p>
//               <p className="text-gray-600">{experience.positionDescription}</p>
//             </div>
//           ))}
//       </div>
//     </div>
//   );
// };
