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
import { Field, FieldError, FieldGroup, FieldLabel, FieldSet } from "@/components/ui/field"
import { Input } from "@/components/ui/input"
import { InputGroup, InputGroupAddon, InputGroupButton, InputGroupInput } from "@/components/ui/input-group"
import { zodResolver } from "@hookform/resolvers/zod"
import { EyeClosedIcon, EyeIcon } from "@phosphor-icons/react"
import Link from "next/link"
import { useState } from "react"
import { Controller, useForm } from "react-hook-form"
import * as z from 'zod'

const formSchema = z.object({
  email: z.email("Must be a valid email"),
  password: z
    .string()
    .nonempty("Password is required")
    .min(8, "Must be at least 8 character long")
    .max(64, "Must be shorter than 64 characters")
    .regex(/[a-z]/, "Must include at least 1 lowercase character")
    .regex(/[A-Z]/, "Must include at least 1 uppercase character")
    .regex(/[0-9]/, "Must include at least 1 digit")
    .regex(/[^a-zA-Z0-9]/, "Must include at least 1 special character"),
  passwordConfirmation: z.string(),
}).superRefine(({ password, passwordConfirmation }, ctx) => {
  if (password !== passwordConfirmation) {
    ctx.addIssue({
      code: "custom",
      message: "Passwords do not match",
      path: ["passwordConfirmation"],
    })
  }
});

type SignUpFormSchema = z.infer<typeof formSchema>;

export default function page() {
  const form = useForm<SignUpFormSchema>({
    resolver: zodResolver(formSchema),
    mode: "onChange",
    defaultValues: {
      email: "",
      password: "",
      passwordConfirmation: "",
    },
  });
  const [showPassword, setShowPassword] = useState(false);
  const [showPasswordConfirmation, setShowPasswordConfirmation] = useState(false);

  const onSubmit = (data: SignUpFormSchema) => {
    console.log({ data })
  }

  return (
    <Card className="w-full max-w-sm">
      <CardHeader>
        <CardTitle>Sign up to Library+</CardTitle>
        <CardDescription>
          Enter your email and password below to sign up
        </CardDescription>
      </CardHeader>
      <CardContent>
        <form onSubmit={form.handleSubmit(onSubmit)}>
          <FieldSet className="flex flex-col gap-6">
            <FieldGroup>
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

              <Controller
                name="password"
                control={form.control}
                render={({ field, fieldState }) => (
                  <Field data-invalid={fieldState.invalid}>
                    <FieldLabel htmlFor={field.name}>Password</FieldLabel>
                    <InputGroup
                      className="bg-background"
                    >
                      <InputGroupInput
                        { ...field }
                        id={field.name}
                        aria-invalid={fieldState.invalid}
                        placeholder="Your password goes here"
                        autoComplete="off"
                        type={showPassword ? "text" : "password"}
                        required
                        autoFocus
                      />
                      <InputGroupAddon align="inline-end">
                        <InputGroupButton
                          size="icon-sm"
                          className="cursor-pointer"
                          onClick={() => setShowPassword(prev => !prev)}
                        >
                          {showPassword ? <EyeClosedIcon /> : <EyeIcon />}
                        </InputGroupButton>
                      </InputGroupAddon>
                    </InputGroup>
                    {fieldState.invalid && <FieldError errors={[fieldState.error]} />}
                  </Field>
                )}
              />

              <Controller
                name="passwordConfirmation"
                control={form.control}
                render={({ field, fieldState }) => (
                  <Field data-invalid={fieldState.invalid}>
                    <FieldLabel htmlFor={field.name}>Password confirmation</FieldLabel>
                    <InputGroup
                      className="bg-background"
                    >
                      <InputGroupInput
                        { ...field }
                        id={field.name}
                        aria-invalid={fieldState.invalid}
                        placeholder="Enter the password again"
                        autoComplete="off"
                        type={showPasswordConfirmation ? "text" : "password"}
                        required
                        autoFocus
                      />
                      <InputGroupAddon align="inline-end">
                        <InputGroupButton
                          size="icon-sm"
                          className="cursor-pointer"
                          onClick={() => setShowPasswordConfirmation(prev => !prev)}
                        >
                          {showPasswordConfirmation ? <EyeClosedIcon /> : <EyeIcon />}
                        </InputGroupButton>
                      </InputGroupAddon>
                    </InputGroup>
                    {fieldState.invalid && <FieldError errors={[fieldState.error]} />}
                  </Field>
                )}
              />
            </FieldGroup>
          </FieldSet>
        </form>
      </CardContent>
      <CardFooter className="flex-col gap-4">
        <Button type="submit" className="w-full cursor-pointer" disabled={!form.formState.isValid}>
          Sign up
        </Button>
        
        <div className="flex flex-col gap-2 items-center">
          <Link href="/login" className="underline">Already have an account?</Link>
        </div>
      </CardFooter>
    </Card>
  )
}
