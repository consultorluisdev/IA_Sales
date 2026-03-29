"use client";

import Link from "next/link";
import { useAuthStore } from "@/app/store/authStore";
import { useRouter } from "next/navigation";

export default function Header() {
    const { user, logout } = useAuthStore();
    const router = useRouter();

    const handleLogout = () => {
        logout();
        router.push('/auth/login');
    };

    return (
        <header className="h-16 bg-[#0c0c14] border-b border-white/10 px-6 flex items-center justify-between">
            <Link href="/" className="flex items-center gap-3">
                <div className="w-8 h-8 rounded-xl bg-gradient-to-br from-[#ff6b2b] to-[#ff4500] flex items-center justify-center">
                    <span className="text-white font-black text-xs tracking-widest">IA</span>
                </div>
                <span className="font-black tracking-[0.2em] text-white">
                    IA<span className="text-[#ff6b2b]">SALES</span>
                </span>
            </Link>

            <div className="flex items-center gap-4">
                <span className="text-sm text-zinc-400 font-mono">
                    {user?.name || user?.email}
                </span>
                <button
                    onClick={handleLogout}
                    className="px-4 py-2 text-sm font-medium text-zinc-400 hover:text-white hover:bg-white/5 rounded-lg transition-all"
                >
                    Sair
                </button>
            </div>
        </header>
    );
}
