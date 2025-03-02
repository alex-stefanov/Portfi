'use client';

import { AlertCircle } from 'lucide-react';
import * as React from 'react';

import clsx from 'clsx';

import { Input } from '@/components/ui/input';
import { Label } from '@/components/ui/label';

export interface FormInputProps extends React.InputHTMLAttributes<HTMLInputElement> {
  label?: string;
  error?: string;
  description?: string;
}

const FormInput = React.forwardRef<HTMLInputElement, FormInputProps>(
  ({ className, label, error, description, id, ...props }, ref) => {
    // Generate a unique ID if one isn't provided
    const inputId = React.useId();
    const descriptionId = `${inputId}-description`;
    const errorId = `${inputId}-error`;
    const hasError = !!error;

    return (
      <div className="space-y-2">
        {label && (
          <Label
            htmlFor={inputId}
            className={clsx(hasError && 'text-destructive', 'capitalize')}
          >
            {label}
          </Label>
        )}
        <Input
          id={inputId}
          className={clsx(
            hasError && 'border-destructive focus-visible:ring-destructive',
            className,
          )}
          aria-invalid={hasError}
          aria-describedby={
            description && !hasError ? descriptionId : hasError ? errorId : undefined
          }
          ref={ref}
          {...props}
        />
        {description && !hasError && (
          <p id={descriptionId} className="text-sm text-muted-foreground">
            {description}
          </p>
        )}
        {hasError && (
          <div
            id={errorId}
            className="flex items-center gap-x-2 text-sm text-destructive"
            aria-live="polite"
          >
            <AlertCircle className="h-4 w-4" />
            <p>{error}</p>
          </div>
        )}
      </div>
    );
  },
);
FormInput.displayName = 'FormInput';

export { FormInput };
