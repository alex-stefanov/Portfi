'use client';

import { Github, Mail, MessageCircle } from 'lucide-react';
import { useState } from 'react';

import { Button } from '@/components/ui/button';
import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from '@/components/ui/card';

export default function LoginPage() {
  const [isLoading, setIsLoading] = useState(false);

  const handleLogin = async (provider: string) => {
    setIsLoading(true);
    try {
      // await signIn(provider, { callbackUrl: "/" })
    } catch (error) {
      console.error('Login error:', error);
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="flex min-h-screen items-center justify-center bg-gradient-to-br from-primary/20 to-secondary/20">
      <Card className="w-full max-w-md shadow-xl">
        <CardHeader className="space-y-1">
          <CardTitle className="text-center text-2xl font-bold">
            Welcome back
          </CardTitle>
          <CardDescription className="text-center">
            Sign in to your account using one of the methods below
          </CardDescription>
        </CardHeader>
        <CardContent className="grid gap-4">
          <Button
            variant="outline"
            onClick={() => handleLogin('google')}
            disabled={isLoading}
          >
            <Mail className="mr-2 h-4 w-4" />
            Sign in with Google
          </Button>
          <Button
            variant="outline"
            onClick={() => handleLogin('discord')}
            disabled={isLoading}
            className="rounded border border-[#7289da] bg-[#7289da] px-4 py-2 text-white shadow hover:bg-[#677bc4] hover:text-white"
          >
            <MessageCircle className="mr-2 h-5 w-5" />
            Sign in with Discord
          </Button>
          <Button
            variant="outline"
            onClick={() => handleLogin('github')}
            disabled={isLoading}
            className="rounded border border-[#333] bg-[#333] px-4 py-2 text-white shadow hover:bg-[#2b2b2b] hover:text-white"
          >
            <Github className="mr-2 h-5 w-5" />
            Sign in with GitHub
          </Button>
        </CardContent>
        <CardFooter className="mt-6 flex flex-col space-y-4">
          <div className="text-center text-sm text-muted-foreground">
            By signing in, you agree to our{' '}
            <a
              href="/terms"
              className="text-primary underline hover:text-primary/80"
            >
              Terms of Service
            </a>{' '}
            and{' '}
            <a
              href="/privacy"
              className="text-primary underline hover:text-primary/80"
            >
              Privacy Policy
            </a>
            .
          </div>
        </CardFooter>
      </Card>
    </div>
  );
}
