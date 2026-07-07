import { Outlet } from 'react-router-dom'

export default function DashboardLayout() {
    return (
        <div className="min-h-screen bg-[#05050a]">
            <div className="flex">
                <aside className="w-64 bg-[#0c0c14] border-r border-white/10 min-h-screen">
                    <div className="p-6">
                        <h1 className="text-xl font-black tracking-widest text-white">
                            IA<span className="text-[#ff6b2b]">SALES</span>
                        </h1>
                    </div>
                    <nav className="px-4 space-y-2">
                        <a href="/dashboard" className="block px-4 py-2 text-sm text-zinc-400 hover:text-white hover:bg-white/5 rounded-lg">
                            Dashboard
                        </a>
                        <a href="/dashboard/customers" className="block px-4 py-2 text-sm text-zinc-400 hover:text-white hover:bg-white/5 rounded-lg">
                            Clientes
                        </a>
                        <a href="/dashboard/products" className="block px-4 py-2 text-sm text-zinc-400 hover:text-white hover:bg-white/5 rounded-lg">
                            Produtos
                        </a>
                        <a href="/dashboard/orders" className="block px-4 py-2 text-sm text-zinc-400 hover:text-white hover:bg-white/5 rounded-lg">
                            Vendas
                        </a>
                    </nav>
                </aside>
                <main className="flex-1 p-8">
                    <Outlet />
                </main>
            </div>
        </div>
    );
}
