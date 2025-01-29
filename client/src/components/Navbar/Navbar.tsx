'use client';

import Link from 'next/link';
import { useRouter } from 'next/navigation';

import { Activity, LogOut, User } from 'lucide-react';

import { useAuthSession } from '@/hooks/useAuthSession';

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
  const { user, isLoading } = useAuthSession();

  const handleSignIn = () => {
    router.push('/login');
  };

  const isLoggedIn = !!user;

  return (
    <header className="border-b bg-white px-6 py-4">
      <div className="container flex items-center justify-between">
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

          {!isLoading && isLoggedIn ? (
            <UserDropdownMenu />
          ) : (
            <Button onClick={handleSignIn}>Sign In</Button>
          )}
        </div>
      </div>
    </header>
  );
};

const UserDropdownMenu = () => {
  const { user } = useAuthSession();

  return (
    user && (
      <DropdownMenu>
        <DropdownMenuTrigger asChild>
          <Button variant="ghost" className="relative h-9 w-9 rounded-full">
            <Avatar>
              <AvatarImage src={user.avatar_url} />
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
            // onClick={handleSignOut}
          >
            <LogOut className="mr-2 h-4 w-4" />
            <span>Sign out</span>
          </DropdownMenuItem>
        </DropdownMenuContent>
      </DropdownMenu>
    )
  );
};
