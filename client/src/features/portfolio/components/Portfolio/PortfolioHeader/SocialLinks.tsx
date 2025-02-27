'use client';

import { Linkedin } from 'lucide-react';
import { useActionState, useEffect, useState } from 'react';

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

import { supportedSocialMediasArr } from '../../../schemas/portfolioSchemas';
import { updateSocialLinksAction } from '../../../server-actions/updateSocialLinksAction';
import { EditButton } from './EditButton';

import type { SocialLinks as TSocialLinks } from '../../../schemas/portfolioSchemas';

const socialIcons: Record<keyof TSocialLinks, React.ElementType> = {
  github: SiGithub,
  linkedin: Linkedin,
  twitter: SiX,
  instagram: SiInstagram,
  facebook: SiFacebook,
};

export const SocialLinks = ({ socialLinks }: { socialLinks: TSocialLinks }) => {
  const [isOpen, setIsOpen] = useState(false);
  const [state, formAction, isPending] = useActionState(updateSocialLinksAction, {
    socials: socialLinks,
    error: false,
  });

  const socialMediaLinksArr = Object.values(state.socials);

  useEffect(() => {
    if (!state.error) {
      toast.success('Social links updated successfully.');
      setIsOpen(false);
    } else {
      toast.error('Failed to update social links. Please try again.');
    }
  }, [state]);

  const getUserLink = (platform: string) => {
    return (
      socialMediaLinksArr.find((link: string) =>
        link.toLowerCase().includes(platform.toLowerCase()),
      ) || ''
    );
  };

  return (
    <div className="relative flex items-start justify-start gap-1.5">
      <IconsWithSocialLinks socials={state.socials} />
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
          <form action={formAction} id="social-links">
            <div className="grid gap-4 py-4">
              {supportedSocialMediasArr.map((socialMedia) => (
                <div
                  className="grid grid-cols-4 items-center gap-4"
                  key={socialMedia}
                >
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
          </form>
          <DialogFooter>
            <LoadingButton loading={isPending} form="social-links">
              Save changes
            </LoadingButton>
          </DialogFooter>
        </DialogContent>
      </Dialog>
    </div>
  );
};

const IconsWithSocialLinks = ({ socials }: { socials: TSocialLinks }) => {
  return (
    <div className="relative flex space-x-6">
      {Object.entries(socials)
        .filter(([_, link]) => link) // filter out empty links
        .map(([platform, link]) => {
          const Icon = socialIcons[platform as keyof TSocialLinks];
          return (
            <a
              key={platform}
              href={link}
              target="_blank"
              rel="noopener noreferrer"
              className="relative"
            >
              <Icon className="h-7 w-7" />
            </a>
          );
        })}
    </div>
  );
};
