import { apiRoutes } from '@/constants/apiRoutes';

import { socialLinksSchema } from '../schemas/portfolioSchemas';

import type { SocialLinks } from '../schemas/portfolioSchemas';

const updateSocialLinksAction = async (socialLinks: SocialLinks) => {
  const validatedInput = socialLinksSchema.safeParse(socialLinks);

  if (!validatedInput.success) {
    return {
      error: validatedInput.error,
    };
  }

  const url = apiRoutes.portfolio.updateSocialLinks;

  
};
