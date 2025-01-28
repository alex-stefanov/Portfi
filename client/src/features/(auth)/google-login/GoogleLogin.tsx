import { Mail } from 'lucide-react';
import React from 'react';

import { createClient } from '@/utils/supabase/client';

import { Button } from '@/components/ui/button';

export const GoogleLogin = () => {
  const supabase = createClient();

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
    } catch (e) {
      console.error('Something went wrongg');
      // toast.error('Something went wrong. Please try again.');
    }
  };

  return (
    <Button variant="outline" onClick={loginWithGoogle}>
      <Mail className="mr-2 h-4 w-4" />
      Sign in with Google
    </Button>
  );
};
