'use client';

import { Linkedin } from 'lucide-react';
import { useState, useTransition } from 'react';

import {
  SiFacebook,
  SiGithub,
  SiInstagram,
  SiX,
} from '@icons-pack/react-simple-icons';
import { toast } from 'sonner';

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
import { LoadingButton } from '@/components/ui/loading-button';

import { SocialLinksArr } from '../../schemas/portfolioSchemas';
import { updateSocialLinksAction } from '../../server-actions/updateSocialLinksAction';
import { EditButton } from '../EditButton';

import type { SocialLinks as TSocialLinks } from '../../schemas/portfolioSchemas';

const socialIcons: Record<keyof TSocialLinks, React.ElementType> = {
  github: SiGithub,
  linkedin: Linkedin,
  twitter: SiX,
  instagram: SiInstagram,
  facebook: SiFacebook,
};

export const SocialLinks = ({ socialLinks }: { socialLinks: TSocialLinks }) => {
  const [isOpen, setIsOpen] = useState(false);
  const [isPending, startTransition] = useTransition();

  const socialLinksArr = Object.values(socialLinks);

  const updateSocialLinks = () => {
    startTransition(async () => {
      const isSuccess = await updateSocialLinksAction(socialLinks);

      if (isSuccess) {
        setIsOpen(false);
        toast.success('Social links updated successfully.', {
          position: 'top-center',
        });
      } else {
        toast.error('Failed to update social links. Please try again.', {
          position: 'top-center',
        });
      }
    });
  };

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
      <Dialog open={isOpen} onOpenChange={setIsOpen}>
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
            <LoadingButton loading={isPending} onClick={updateSocialLinks}>
              Save changes
            </LoadingButton>
          </DialogFooter>
        </DialogContent>
      </Dialog>
    </div>
  );
};
