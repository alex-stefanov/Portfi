import { API_URL } from '@/config/apiConfig';

export const apiRoutes = {
  portfolio: {
    getById: (id: string) =>
      `${API_URL}/portfolio/getPortfolioById?portfolioID=${id}`,
  },
  projects: {
    getProjectsByUsername: (username: string) =>
      `${API_URL}/project/getGitHubProjectsByUsername?username=${username}`,
  },
};
