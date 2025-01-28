'use client';

import Link from 'next/link';
import { useRouter } from 'next/navigation';

import { Activity, LogOut, User } from 'lucide-react';
import { useState } from 'react';

import { Avatar, AvatarFallback, AvatarImage } from '@/components/ui/avatar';
import { Button } from '@/components/ui/button';
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from '@/components/ui/dropdown-menu';

export const Navbar = () => {
  const router = useRouter();
  const [isLoggedIn, setIsLoggedIn] = useState(false); // This is for demonstration. In a real app, you'd use a proper auth state.

  const handleSignIn = () => {
    router.push('/login');
  };

  const handleSignOut = () => {
    setIsLoggedIn(false);
    // In a real app, you'd handle sign out logic here
  };

  // Simulating login for demonstration
  const simulateLogin = () => {
    setIsLoggedIn((prev) => !prev);
  };

  return (
    <header className="border-b bg-white px-6 py-4">
      <div className="container mx-auto flex items-center justify-between">
        <Link href="/" className="flex items-center space-x-2">
          <Activity className="h-6 w-6 text-primary" />
          <span className="text-xl font-bold text-gray-900">Portfio</span>
        </Link>
        <div className="flex items-center gap-6">
          <nav className="hidden space-x-6 md:flex">
            <Link
              href="#features"
              className="text-gray-600 transition-colors hover:text-gray-900"
            >
              Features
            </Link>
            <Link
              href="#testimonials"
              className="text-gray-600 transition-colors hover:text-gray-900"
            >
              Testimonials
            </Link>
            <Link
              href="#pricing"
              className="text-gray-600 transition-colors hover:text-gray-900"
            >
              Pricing
            </Link>
          </nav>
          {isLoggedIn ? (
            <DropdownMenu>
              <DropdownMenuTrigger asChild>
                <Button variant="ghost" className="relative h-9 w-9 rounded-full">
                  <Avatar>
                    <AvatarImage src="https://github.com/shadcn.png" sizes="" />
                    <AvatarFallback>User</AvatarFallback>
                  </Avatar>
                </Button>
              </DropdownMenuTrigger>
              <DropdownMenuContent className="w-56" align="end" forceMount>
                <DropdownMenuItem className="items-cente flex cursor-pointer">
                  <User className="mr-2 h-4 w-4" />
                  <span>Profile</span>
                </DropdownMenuItem>
                <DropdownMenuSeparator />
                <DropdownMenuItem
                  className="flex cursor-pointer items-center"
                  onClick={handleSignOut}
                >
                  <LogOut className="mr-2 h-4 w-4" />
                  <span>Sign out</span>
                </DropdownMenuItem>
              </DropdownMenuContent>
            </DropdownMenu>
          ) : (
            <Button onClick={handleSignIn}>Sign In</Button>
          )}
        </div>
        {/* This button is just for demonstration purposes */}
        <Button onClick={simulateLogin} className="absolute right-0 top-14 mt-4">
          Simulate Login
        </Button>
      </div>
    </header>
  );
};
