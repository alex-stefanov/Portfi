import { API_CONFIG } from '@/config/apiConfig';

const v1 = API_CONFIG.BASE_PATH;

export const apiRoutes = {
  portfolio: {
    getById: (id: string) => `${v1}/portfolio/getPortfolioById?portfolioID=${id}`,
    updateSocialLinks: `${v1}/portfolio/updateSocialLinks`,
  },
  projects: {
    getProjectsByUsername: (username: string) => {
      return `${v1}/project/getGitHubProjectsByUsername?username=${username}`;
    },
  },
};
