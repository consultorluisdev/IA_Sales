import { create } from "zustand";
import { persist } from "zustand/middleware";
import Cookies from "js-cookie";
import type { User } from "@/lib/types";

interface AuthState {
    user: User | null;
    token: string | null;
    isAuthenticated: boolean;
    login: (token: string, user: User) => void;
    logout: () => void;
}

export const useAuthStore = create<AuthState>()(
    persist(
        (set) => ({
            user: null,
            token: null,
            isAuthenticated: false,
            login: (token, user) => {
                Cookies.set("token", token);
                set({ user, token, isAuthenticated: true });
            },
            logout: () => {
                Cookies.remove("token");
                set({ user: null, token: null, isAuthenticated: false });
            },
        }),
        {
            name: "iasales-auth",
        }
    )
);
        