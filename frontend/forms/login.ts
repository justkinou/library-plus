import * as z from 'zod'

export const loginFormSchema = z.object({
  email: z.email("Must be a valid email"),
  password: z
    .string()
    .nonempty("Password is required")
});

export type LoginFormSchema = z.infer<typeof loginFormSchema>;