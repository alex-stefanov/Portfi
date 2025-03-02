'use client';

import { Button } from '@/components/ui/button';
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from '@/components/ui/dialog';

interface EditDialogProps extends React.ComponentPropsWithoutRef<typeof Dialog> {
  description?: string;
  children: React.ReactNode;
}

export const EditDialog = ({ description, children, ...rest }: EditDialogProps) => {
  return (
    <Dialog {...rest}>
      <DialogTrigger asChild>
        <EditButton />
      </DialogTrigger>
      <DialogContent className="sm:max-w-[425px]">
        <DialogHeader>
          <DialogTitle>Edit</DialogTitle>
          <DialogDescription>
            {description ||
              "Make changes to your portfolio. Click save when you're done."}
          </DialogDescription>
        </DialogHeader>
        {children}
      </DialogContent>
    </Dialog>
  );
};

const EditButton = (props: React.ComponentProps<typeof Button>) => {
  return (
    <Button variant="link" size="sm" className="-mt-1 h-5 w-8" {...props}>
      Edit
    </Button>
  );
};
