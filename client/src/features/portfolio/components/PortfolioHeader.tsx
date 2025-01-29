'use client';

import Image from 'next/image';

import {
  Edit,
  Eye,
  Facebook,
  Github,
  Heart,
  Instagram,
  Linkedin,
  Save,
  Twitter,
  X,
} from 'lucide-react';
import { useState } from 'react';

import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';

import type { Portfolio, SocialLinks } from '../types/Portfolio';

const socialIcons: Record<keyof SocialLinks, React.ElementType> = {
  github: Github,
  linkedin: Linkedin,
  twitter: Twitter,
  instagram: Instagram,
  facebook: Facebook,
};

export const PortfolioHeader = ({ portfolio }: { portfolio: Portfolio }) => {
  const [isEditing, setIsEditing] = useState<boolean>(false);
  const [title, setTitle] = useState<string>(portfolio.title);
  const [isFavorite, setIsFavorite] = useState<boolean>(false);
  const [socialLinks, setSocialLinks] = useState<SocialLinks>(portfolio.socialLinks);
  const [newSocialPlatform, setNewSocialPlatform] = useState<keyof SocialLinks | ''>(
    '',
  );
  const [newSocialLink, setNewSocialLink] = useState<string>('');

  const handleSave = () => {
    console.log('Saving updated title:', title);
    console.log('Saving updated social links:', socialLinks);
    setIsEditing(false);
  };

  const toggleFavorite = () => {
    setIsFavorite((prev) => !prev);
    // Placeholder for database interaction
  };

  const addSocialLink = () => {
    if (newSocialPlatform && newSocialLink && !socialLinks[newSocialPlatform]) {
      setSocialLinks({
        ...socialLinks,
        [newSocialPlatform]: newSocialLink,
      });
      setNewSocialPlatform('');
      setNewSocialLink('');
    }
  };

  const removeSocialLink = (platform: keyof SocialLinks) => {
    const updatedLinks = { ...socialLinks };
    delete updatedLinks[platform];
    setSocialLinks(updatedLinks);
  };
  return (
    <header className="bg-slate-800 py-8 text-primary-foreground">
      <div className="container">
        <div className="mb-4 flex items-center justify-between">
          <div className="flex items-center space-x-4">
            <Image
              src={portfolio.avatar || '/placeholder.svg'}
              alt={portfolio.author}
              width={60}
              height={60}
              className="rounded-full"
            />
            <div>
              {isEditing ? (
                <input
                  type="text"
                  value={title}
                  onChange={(e) => setTitle(e.target.value)}
                  className="border-b border-primary-foreground bg-transparent text-3xl font-bold"
                />
              ) : (
                <h1 className="text-3xl font-bold">{title}</h1>
              )}
              <p className="text-xl">by {portfolio.author}</p>
            </div>
          </div>
          <div className="flex items-center space-x-4">
            <Button variant="secondary" onClick={toggleFavorite}>
              <Heart
                className={`mr-2 h-4 w-4 ${isFavorite ? 'fill-current' : ''}`}
              />
              {isFavorite ? 'Favorited' : 'Favorite'}
            </Button>
            <Button
              variant="ghost"
              onClick={() => (isEditing ? handleSave() : setIsEditing(true))}
            >
              {isEditing ? (
                <Save className="mr-2 h-4 w-4" />
              ) : (
                <Edit className="mr-2 h-4 w-4" />
              )}
              {isEditing ? 'Save' : 'Edit'}
            </Button>
          </div>
        </div>
        <div className="flex items-center justify-between">
          <div className="flex space-x-4">
            {Object.entries(socialLinks).map(([platform, link]) => {
              const Icon = socialIcons[platform as keyof SocialLinks];
              return (
                <div key={platform} className="relative">
                  <a href={link} target="_blank" rel="noopener noreferrer">
                    <Icon className="h-6 w-6" />
                  </a>
                  {isEditing && (
                    <button
                      onClick={() => removeSocialLink(platform as keyof SocialLinks)}
                      className="absolute -right-2 -top-2 rounded-full bg-red-500 p-1"
                    >
                      <X className="h-3 w-3 text-white" />
                    </button>
                  )}
                </div>
              );
            })}
          </div>
          <div className="flex items-center">
            <Eye className="mr-2 h-5 w-5" />
            <span>{portfolio.views} views</span>
          </div>
        </div>
        {isEditing && (
          <div className="mt-4 flex space-x-2">
            <select
              value={newSocialPlatform}
              onChange={(e) =>
                setNewSocialPlatform(e.target.value as keyof SocialLinks)
              }
              className="rounded bg-primary-foreground p-2 text-primary"
            >
              <option value="">Select Platform</option>
              {Object.keys(socialIcons).map((platform) => (
                <option key={platform} value={platform}>
                  {platform.charAt(0).toUpperCase() + platform.slice(1)}
                </option>
              ))}
            </select>
            <Input
              type="text"
              value={newSocialLink}
              onChange={(e) => setNewSocialLink(e.target.value)}
              placeholder="Enter social media link"
              className="flex-grow"
            />
            <Button onClick={addSocialLink}>Add Link</Button>
          </div>
        )}
      </div>
    </header>
  );
};
