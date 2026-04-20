"use client"

import { meEndpoint } from "@/constants/endpoints";
import { UserData } from "@/types/user/UserData"
import React, { createContext, useEffect, useState } from "react";

export interface IUserContext {
    userData: UserData | null;
    isLoading: boolean;
}

export const UserContext = createContext<IUserContext>({} as IUserContext);

export const UserProvider = ({ children } : { children: React.ReactNode }) => {
    const [userData, setUserData] = useState<UserData | null>(null);
    const [isLoading, setIsLoading] = useState(true);
    
    useEffect(() => {
        (async () => {
        const response = await fetch(meEndpoint, {
            method: "GET",
        });

        if (response.ok) {
            setUserData(await response.json());
        } else {
            setUserData(null);
        }
        setIsLoading(false);
        })();
    }, []);

    return <UserContext.Provider value={{ userData, isLoading }}>
        {children}
    </UserContext.Provider>
}