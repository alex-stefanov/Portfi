import 'server-only';

import { apiRoutes } from '@/constants/apiRoutes';

import * as api from '@/utils/api';

import type { SocialLinks } from '../schemas/portfolioSchemas';

export const updateSocialLinks = async (data: SocialLinks) => {
  const path = apiRoutes.portfolio.updateSocialLinks;
  const result = await api.post(path, data);

  return result;
};
