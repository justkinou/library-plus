import { Field, FieldGroup, FieldLabel } from "@/components/ui/field";
import { Input } from "@/components/ui/input";
import React from "react";

function ChangePassword() {
  return (
    <section>
      <h2 className="text-xl font-semibold mb-4">Change password</h2>

      <form>
        <FieldGroup className="flex flex-col gap-4">
          <Field>
            <FieldLabel htmlFor="currentPassword">Current password</FieldLabel>
            <Input
              id="currentPassword"
              name="currentPassword"
              type="password"
              autoComplete="current-password"
            />
          </Field>

          <Field>
            <FieldLabel htmlFor="newPassword">New password</FieldLabel>
            <Input
              id="newPassword"
              name="newPassword"
              type="password"
              autoComplete="new-password"
            />
          </Field>

          <Field>
            <FieldLabel htmlFor="confirmPassword">
              Confirm new password
            </FieldLabel>
            <Input
              id="confirmPassword"
              name="confirmPassword"
              type="password"
              autoComplete="new-password"
            />
          </Field>

          <button className="bg-primary px-10 py-1 text-white font-semibold">
            Submit
          </button>
        </FieldGroup>
      </form>
    </section>
  );
}

export default ChangePassword;
