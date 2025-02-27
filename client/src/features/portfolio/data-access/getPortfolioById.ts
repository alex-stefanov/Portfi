import 'server-only';

import { apiRoutes } from '@/constants/apiRoutes';

import * as api from '@/utils/api';

import { Portfolio } from '../schemas/portfolioSchemas';

export const getPortfolioById = async (id: string) => {
  const url = apiRoutes.portfolio.getById(id);

  try {
    const data: Portfolio = await api.get(url);

    return {
      data,
    };
  } catch (error) {
    return {
      error: error as api.ApiError,
    };
  }
};
