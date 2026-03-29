import { create } from "zustand";
import { persist } from 'zustand/middleware'
import Cookies from "js-cookie";
import { User } from '@/lib/types'

interface AuthState{
    user: User | null 
    token: string | null 
    isAuthenticated: boolean
    login: (token: string, user: User) => void
    logout: () => void
}

export const useAuthStore = create<AuthState>()(
    persist(
        (set) => ({
            user: null,
            token: null,
            isAuthenticated: false,

            login: (token, user) => {
                Cookies.set('token', token, { expires: 1})
                set({ token,user, isAuthenticated: true })
            },
            logout: () => {
                Cookies.remove('token')
                set({ token: null, user: null, isAuthenticated: false })
            },
        }),
        { name: 'iasales-auth' }
    )
)