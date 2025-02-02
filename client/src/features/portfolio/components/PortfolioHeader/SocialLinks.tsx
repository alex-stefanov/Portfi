import { Linkedin } from 'lucide-react';

import {
  SiFacebook,
  SiGithub,
  SiInstagram,
  SiX,
} from '@icons-pack/react-simple-icons';

import { Button } from '@/components/ui/button';
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from '@/components/ui/dialog';
import { Input } from '@/components/ui/input';
import { Label } from '@/components/ui/label';

import { SocialLinksArr } from '../../types/Portfolio';
import { EditButton } from '../EditButton';

import type { SocialLinks as TSocialLinks } from '../../types/Portfolio';

const socialIcons: Record<keyof TSocialLinks, React.ElementType> = {
  github: SiGithub,
  linkedin: Linkedin,
  twitter: SiX,
  instagram: SiInstagram,
  facebook: SiFacebook,
};

export const SocialLinks = ({ socialLinks }: { socialLinks: TSocialLinks }) => {
  const socialLinksArr = Object.values(socialLinks);

  const getUserLink = (platform: string) => {
    return (
      socialLinksArr.find((link: string) =>
        link.toLowerCase().includes(platform.toLowerCase()),
      ) || ''
    );
  };

  return (
    <div className="relative flex items-start justify-start gap-1.5">
      <div className="relative flex space-x-6">
        {Object.entries(socialLinks).map(([platform, link]) => {
          const Icon = socialIcons[platform as keyof TSocialLinks];

          return (
            <div key={platform} className="relative">
              <a href={link} target="_blank" rel="noopener noreferrer">
                <Icon className="h-7 w-7" />
              </a>
            </div>
          );
        })}
      </div>
      <Dialog>
        <DialogTrigger asChild>
          <EditButton />
        </DialogTrigger>
        <DialogContent className="sm:max-w-[425px]">
          <DialogHeader>
            <DialogTitle>Edit</DialogTitle>
            <DialogDescription>
              Make changes to your social links here. Click save when you're done.
            </DialogDescription>
          </DialogHeader>
          <div className="grid gap-4 py-4">
            {SocialLinksArr.map((socialMedia) => (
              <div className="grid grid-cols-4 items-center gap-4" key={socialMedia}>
                <Label htmlFor={socialMedia} className="text-right">
                  {socialMedia}
                </Label>
                <Input
                  id={socialMedia}
                  className="col-span-3 h-8"
                  type="url"
                  defaultValue={getUserLink(socialMedia)}
                />
              </div>
            ))}
          </div>
          <DialogFooter>
            <Button type="submit">Save changes</Button>
          </DialogFooter>
        </DialogContent>
      </Dialog>
    </div>
  );
};
