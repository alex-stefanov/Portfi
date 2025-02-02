import { useQuery } from '@tanstack/react-query';

import { reactQueryKeys } from '@/constants/reactQueryKeys';

import { getAuthSession } from '@/utils/getAuthSession';

export const useAuthSession = () => {
  const { data, isLoading } = useQuery({
    queryKey: reactQueryKeys.user.session(),
    queryFn: async () => {
      const { data } = await getAuthSession();

      if (!data.session?.user) return null;

      const { id, user_metadata, email } = data.session.user;



      return {
        id,
        avatar_url: user_metadata.avatar_url as string,
        email: email as string,
      };
    },
    staleTime: 60 * 1000 * 10,
  });

  return { user: data, isLoading };
};
