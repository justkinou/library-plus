import { loginEndpoint, signUpEndpoint } from "@/constants/endpoints"
import { LoginFormSchema, SignUpFormSchema } from "@/forms/auth"
import { SignUpResponseDTO } from "@/types/auth/dto";

export const handleLogin = async ({ email, password }: LoginFormSchema): Promise<string | null> => {
  const response = await fetch(loginEndpoint, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      "Accept": "application/json",
    },
    body: JSON.stringify({ email, password }),
  });

  if (!response.ok) {
    return "Bad credentials";
  }
  return null;
}

export const handleSignUp = async ({ email, password }: SignUpFormSchema): Promise<string | null> => {
  const response = await fetch(signUpEndpoint, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      "Accept": "application/json",
    },
    body: JSON.stringify({ email, password }),
  });

  if (!response.ok) {
    const data: SignUpResponseDTO = await response.json().catch(() => {});
    return data.message ?? "something went wrong";
  }
  return null;
}