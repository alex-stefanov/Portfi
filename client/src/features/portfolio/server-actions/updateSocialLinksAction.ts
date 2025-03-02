'use server';

// import { updateSocialLinks } from '../data-access/updateSocialLinks';
import { socialLinksSchema } from '../schemas/portfolioSchemas';

import type { SocialLinks } from '../schemas/portfolioSchemas';

export type PrevState = {
  socials: SocialLinks;
  error: boolean;
};

export const updateSocialLinksAction = async (
  prevState: PrevState,
  formData: FormData,
) => {
  try {
    const userInput = Object.fromEntries(formData.entries());
    const result = socialLinksSchema.safeParse(userInput);

    if (result.error) {
      return {
        socials: {
          ...prevState.socials,
        },
        formError: result.error.flatten().fieldErrors,
        error: false,
      };
    }

    await new Promise((resolve) => setTimeout(resolve, 2000));

    // await updateSocialLinks(result.data);

    return {
      socials: {
        ...result.data,
      },
      error: false,
    };
  } catch (_unusedErr) {
    return {
      socials: prevState.socials,
      error: true,
    };
  }
};
