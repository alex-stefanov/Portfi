import { createSupabaseBrowserClient } from './supabase/client';

export const getAuthSession = async () => {
  const supabase = createSupabaseBrowserClient();
  const { data, error } = await supabase.auth.getSession();

  return { data, error };
};
