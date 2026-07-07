import { usePathname, useRouter } from "next/navigation";
import { useAuthStore } from "@/app/store/authStore";
import { Label } from "radix-ui";

const nav = [
    { section: 'Principal', items: [
        { label: 'Dashboard', icon: '', href: '/dashboard' },
        { label: 'CRM Clientes', icon: '', href: '/dashboard/crm', badge: '12' },
        { label: 'Vendas', icon: '', href: '/dashboard/vendas' },
    ]},
    { section: 'Produtos', items: [
        { label: 'Produtos', icon: '', href: '/dashboard/produtos' },
        { label: 'Categorias', icon: '', href: '/dashboard/categorias' },
    ]},
    { section: 'Configurações', items: [
        { label: 'Configurações', icon: '', href: '/dashboard/configuracoes' },
    ]},

]