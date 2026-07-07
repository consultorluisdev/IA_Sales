import { useAuthStore } from "@/store/authStore";

export default function DashboardPage() {
    const user = useAuthStore((s) => s.user);

    return (
        <div>
            <h1 className="text-2xl font-bold text-white mb-6">
                Bem-vindo, {user?.name || 'Usuário'}!
            </h1>
            <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
                <div className="bg-[#0c0c14] border border-white/10 rounded-xl p-6">
                    <h3 className="text-zinc-500 text-sm font-mono uppercase">Clientes</h3>
                    <p className="text-3xl font-bold text-white mt-2">0</p>
                </div>
                <div className="bg-[#0c0c14] border border-white/10 rounded-xl p-6">
                    <h3 className="text-zinc-500 text-sm font-mono uppercase">Produtos</h3>
                    <p className="text-3xl font-bold text-white mt-2">0</p>
                </div>
                <div className="bg-[#0c0c14] border border-white/10 rounded-xl p-6">
                    <h3 className="text-zinc-500 text-sm font-mono uppercase">Vendas</h3>
                    <p className="text-3xl font-bold text-white mt-2">R$ 0</p>
                </div>
            </div>
        </div>
    );
}
