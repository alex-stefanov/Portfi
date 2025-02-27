'use client';

import { Palette } from 'lucide-react';
import { useState } from 'react';

import { Button } from '@/components/ui/button';
import { Label } from '@/components/ui/label';

type Theme = {
  name: string;
  primary: string;
  secondary: string;
};

const themes = [
  { name: 'Default', primary: 'bg-blue-600', secondary: 'bg-purple-600' },
  { name: 'Nature', primary: 'bg-green-600', secondary: 'bg-yellow-600' },
  { name: 'Ocean', primary: 'bg-cyan-600', secondary: 'bg-blue-600' },
  { name: 'Sunset', primary: 'bg-orange-600', secondary: 'bg-pink-600' },
];

export default function ThemeCustomizer() {
  const [isOpen, setIsOpen] = useState(false);

  const handleThemeChange = (theme: Theme) => {
    // Apply the theme. For now just logging it
    console.log(`Changing theme to: ${theme.name}`);
    // In a real implementation...might update CSS variables or apply classes
  };

  return (
    <div className="fixed bottom-4 right-4">
      <Button onClick={() => setIsOpen(!isOpen)}>
        <Palette className="mr-2 h-4 w-4" /> Customize Theme
      </Button>
      {isOpen && (
        <div className="absolute bottom-full right-0 mb-2 rounded-lg border bg-background p-4 shadow-lg">
          <h3 className="mb-2 text-lg font-semibold">Choose a Theme</h3>
          <div className="grid grid-cols-2 gap-2">
            {themes.map((theme) => (
              <Button
                key={theme.name}
                variant="outline"
                className="flex flex-col items-center"
                onClick={() => handleThemeChange(theme)}
              >
                <div className="mb-2 flex">
                  <div
                    className={`h-4 w-4 rounded-full ${theme.primary} mr-1`}
                  ></div>
                  <div className={`h-4 w-4 rounded-full ${theme.secondary}`}></div>
                </div>
                <Label>{theme.name}</Label>
              </Button>
            ))}
          </div>
        </div>
      )}
    </div>
  );
}
