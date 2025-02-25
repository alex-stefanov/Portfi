import { Button } from '@/components/ui/button';

export const EditButton = (props: React.ComponentProps<typeof Button>) => {
  return (
    <Button variant="link" size="sm" className="-mt-1 h-5 w-8" {...props}>
      Edit
    </Button>
  );
};
