"use client"

import { Button } from "@/components/ui/button"
import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card"
import { Field, FieldError, FieldLabel, FieldSet } from "@/components/ui/field"
import { Input } from "@/components/ui/input"
import { zodResolver } from "@hookform/resolvers/zod"
import Link from "next/link"
import { Controller, useForm } from "react-hook-form"
import * as z from 'zod'

const formSchema = z.object({
  email: z.email("Must be a valid email")
});

type PasswordResetFormSchema = z.infer<typeof formSchema>;

export default function page() {
  const form = useForm<PasswordResetFormSchema>({
    resolver: zodResolver(formSchema),
    mode: "onChange",
    defaultValues: {
      email: "",
    },
  });

  const onSubmit = (data: PasswordResetFormSchema) => {
    console.log({ data })
  }

  return (
    <Card className="w-full max-w-sm">
      <CardHeader>
        <CardTitle>Reset your password</CardTitle>
        <CardDescription>
          We will send you a password reset link via email
        </CardDescription>
      </CardHeader>
      <CardContent>
        <form onSubmit={form.handleSubmit(onSubmit)}>
          <FieldSet className="flex flex-col gap-6">
              <Controller
                name="email"
                control={form.control}
                render={({ field, fieldState }) => (
                  <Field data-invalid={fieldState.invalid}>
                    <FieldLabel htmlFor={field.name}>Email</FieldLabel>
                    <Input
                      { ...field }
                      id={field.name}
                      aria-invalid={fieldState.invalid}
                      placeholder="example@mail.com"
                      className="bg-background"
                      autoComplete="off"
                      type="email"
                      required
                      autoFocus
                    />
                    {fieldState.invalid && <FieldError errors={[fieldState.error]} />}
                  </Field>
                )}
              />
          </FieldSet>
        </form>
      </CardContent>
      <CardFooter className="flex-col gap-4">
        <Button type="submit" className="w-full cursor-pointer" disabled={!form.formState.isValid}>
          Reset password
        </Button>

        <div className="flex flex-col gap-2 items-center">
          <Link href="/login" className="underline">Go back to the login page</Link>
        </div>
      </CardFooter>
    </Card>
  )
}
