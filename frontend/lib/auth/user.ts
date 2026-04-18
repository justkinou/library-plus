import { UserData } from "@/types/auth/userData";

export const getUserDataFromAccessToken = (accessToken: string): UserData => {
    const payloadBase64 = accessToken.split(".")[1];
    const payload = Buffer.from(payloadBase64, "base64").toString('utf-8');
    return JSON.parse(payload);
}