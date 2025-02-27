'use server';

// import { updateSocialLinks } from '../data-access/updateSocialLinks';
// import { socialLinksSchema } from '../schemas/portfolioSchemas';
import type { SocialLinks } from '../schemas/portfolioSchemas';

export const updateSocialLinksAction = async (
  currentState: { socials: SocialLinks; error: boolean },
  formData: FormData,
) => {
  // const result = socialLinksSchema.safeParse(socialLinks);

  // if (!result.success) {
  //   return {
  //     error: result.error,
  //   };
  // }

  try {
    // add artificial delay for testing
    await new Promise((resolve) => setTimeout(resolve, 2000));
    // await updateSocialLinks(result.data);
    return {
      socials: {
        ...currentState.socials,
        github: 'https://github.com/flnx',
      },
      error: false,
    };
  } catch (_unusedErr) {
    return {
      socials: currentState.socials,
      error: true,
    };
  }
};
