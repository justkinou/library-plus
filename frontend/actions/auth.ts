"use server"

import { loginEndpoint } from "@/constants/endpoints"
import { LoginFormSchema } from "@/forms/login"

export const handleLogin = async ({ email, password }: LoginFormSchema) => {
    const response = await fetch(loginEndpoint, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json",
        },
        body: JSON.stringify({ email, password }),
    });

    if (!response.ok) {
        return { error: "Bad credentials" };
    }
    return { error: null };
}