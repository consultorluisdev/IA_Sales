"use client";

import { useAuthStore } from "@/app/store/authStore";

export default function Dashboard() {
    const user = useAuthStore((s) => s.user);

    return (
        <div className="p-6">
            <div className="mb-8">
                <h1 className="text-2xl font-bold text-white">Bem-vindo, {user?.name}!</h1>
                <p className="text-zinc-500 mt-1">Painel da plataforma IA Sales</p>
            </div>

            <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
                <div className="bg-[#0c0c14] border border-white/10 rounded-xl p-6">
                    <div className="text-3xl font-bold text-[#ff6b2b]">0</div>
                    <div className="text-sm text-zinc-500 mt-1">Produtos</div>
                </div>
                <div className="bg-[#0c0c14] border border-white/10 rounded-xl p-6">
                    <div className="text-3xl font-bold text-[#00d4aa]">0</div>
                    <div className="text-sm text-zinc-500 mt-1">Vendas</div>
                </div>
                <div className="bg-[#0c0c14] border border-white/10 rounded-xl p-6">
                    <div className="text-3xl font-bold text-[#8b5cf6]">R$ 0,00</div>
                    <div className="text-sm text-zinc-500 mt-1">Receita</div>
                </div>
            </div>

            <div className="mt-8 bg-[#0c0c14] border border-white/10 rounded-xl p-6">
                <h2 className="text-lg font-bold text-white mb-4">Comece a usar</h2>
                <p className="text-zinc-500">
                    Funcionalidades estão sendo implementadas. Em breve você podrá gerenciar produtos, 
                    visualizar vendas e muito mais.
                </p>
            </div>
        </div>
    );
}
