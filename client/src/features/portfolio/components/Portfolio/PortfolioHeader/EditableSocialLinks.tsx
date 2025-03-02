'use client';

import React, { useState } from 'react';

import { useActionStateWithToast } from '@/hooks/useActionStateWithToast';

import { EditDialog } from '@/components/EditDialog/EditDialog';
import { FormInput } from '@/components/FormInput/FormInput';
import { SocialMediaIconLinks } from '@/features/portfolio/components/Portfolio/PortfolioHeader/SocialMediaIconLinks';
import { LoadingButton } from '@/components/ui/loading-button';

import { socialMediaNames } from '../../../schemas/portfolioSchemas';
import { updateSocialLinksAction as updateSocialsAC } from '../../../server-actions/updateSocialLinksAction';

import type { SocialLinks } from '../../../schemas/portfolioSchemas';

type EditableSocialLinksProps = {
  socialLinks: SocialLinks;
};

export const EditableSocialLinks = ({ socialLinks }: EditableSocialLinksProps) => {
  const [isOpen, setIsOpen] = useState(false);
  const { formAction, isLoading, state } = useActionStateWithToast(
    updateSocialsAC,
    {
      socials: socialLinks,
      error: false,
    },
    closeDialogHandler,
  );

  function closeDialogHandler() {
    setIsOpen(false);
  }

  const fErrors = state.formError;

  return (
    <>
      <SocialMediaIconLinks socials={state.socials} />
      <EditDialog
        onOpenChange={setIsOpen}
        open={isOpen}
        description="Update your social media links"
      >
        <form action={formAction} className="space-y-4">
          <section>
            {socialMediaNames.map((socialMedia) => (
              <FormInput
                type="url"
                name={socialMedia}
                label={socialMedia}
                maxLength={255}
                placeholder={`Enter a valid ${socialMedia} URL`}
                defaultValue={state.socials[socialMedia]}
                key={socialMedia}
                error={fErrors?.[socialMedia]?.[0]}
              />
            ))}
          </section>

          <section className="flex flex-col-reverse sm:flex-row sm:justify-end sm:space-x-2">
            <LoadingButton loading={isLoading}>Save Changes</LoadingButton>
          </section>
        </form>
      </EditDialog>
    </>
  );
};
