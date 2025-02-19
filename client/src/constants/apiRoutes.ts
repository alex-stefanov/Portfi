import { API_CONFIG } from '@/config/apiConfig';

export const apiRoutes = {
  v1: API_CONFIG.BASE_PATH,
  portfolio: {
    getById: (id: string) => `/portfolio/getPortfolioById?portfolioID=${id}`,
    updateSocialLinks: `/portfolio/updateSocialLinks`,
  },
  projects: {
    getProjectsByUsername: (username: string) => {
      return `/project/getGitHubProjectsByUsername?username=${username}`;
    },
  },
};
