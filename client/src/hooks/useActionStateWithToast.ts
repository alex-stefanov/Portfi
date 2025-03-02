import { useActionState } from 'react';

import { toast } from 'sonner';

type ActionState<T extends { error: boolean }> = (
  prevState: T,
  fd: FormData,
) => Promise<T>;

export function useActionStateWithToast<T extends { error: boolean }>(
  action: ActionState<T>,
  initialState: Awaited<T>,
  cb?: () => unknown,
) {
  const [state, formAction, isPending] = useActionState(
    async (prevState: T, fd: FormData) => {
      const actionResult = await action(prevState, fd);

      if (actionResult.error) {
        toast.error('Something went wrong. Please try again.');
      } else if (!Object.hasOwn(actionResult, 'formError')) {
        toast.success('Your portfolio has been updated.');

        if (cb) {
          cb();
        }
      }

      return actionResult;
    },
    initialState,
  );

  return { state, formAction, isLoading: isPending };
}
