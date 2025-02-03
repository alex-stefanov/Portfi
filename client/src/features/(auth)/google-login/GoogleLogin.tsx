'use client';

import { Mail } from 'lucide-react';
import React from 'react';

import { createSupabaseBrowserClient } from '@/utils/supabase/client';

import { Button } from '@/components/ui/button';

export const GoogleLogin = () => {
  const supabase = createSupabaseBrowserClient();

  const loginWithGoogle = async () => {
    try {
      const { error } = await supabase.auth.signInWithOAuth({
        provider: 'google',
        options: {
          redirectTo: `${location.origin}/auth/callback?next=%2F`,
        },
      });

      if (error) {
        throw new Error(error.message);
      }
    } catch (err: unknown) {
      const errMsg =
        err instanceof Error ? err.message : 'An unknown error occurred';

      console.error(errMsg);
    }
  };

  return (
    <Button variant="outline" onClick={loginWithGoogle}>
      <Mail className="mr-2 h-4 w-4" />
      Sign in with Google
    </Button>
  );
};
