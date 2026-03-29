"use client";

import { useState, useEffect } from "react";
import { useRouter } from "next/navigation";
import Link from "next/link";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";
import api from "@/lib/api";
import { useAuthStore } from "@/app/store/authStore";

const schema = z.object({
    email: z.string().email('Email inválido'),
    password: z.string().min(6, 'Minimo 6 caracteres'),
})

type FormData = z.infer<typeof schema>

export default function Login() {
    const router = useRouter()
    const [error, setError] = useState('')
    const { login, isAuthenticated } = useAuthStore()
    const [loading, setLoading] = useState(false)

    useEffect(() => {
        if (isAuthenticated) {
            router.push('/');
        }
    }, [isAuthenticated, router]);

    if (isAuthenticated) {
        return null;
    }

    const { register, handleSubmit, formState: { errors } } = useForm<FormData>({
        resolver: zodResolver(schema),
    })

    const onSubmit = async (data: FormData) => {
        setLoading(true)
        setError('')
        
        try {
            const res = await api.post('/api/auth/login', data)
            login(res.data.token, res.data.user)
            router.push('/')
        } catch (error) {
            const err = error as { response?: { data?: { message?: string } } }
            setError(err.response?.data?.message || 'Erro ao realizar login. Tente novamente.')
        } finally {
            setLoading(false)
        }
    }
    return(
     <div className="min-h-screen bg-[#05050a] flex items-center justify-center p-4">
      <div className="w-full max-w-md">
        {/* Logo */}
        <div className="text-center mb-10">
          <div className="inline-flex items-center justify-center w-14 h-14 rounded-2xl bg-gradient-to-br from-[#ff6b2b] to-[#ff4500] mb-4 shadow-[0_8px_24px_#ff6b2b40]">
            <span className="text-white font-black text-xl tracking-widest">IA</span>
          </div>
          <h1 className="text-4xl font-black tracking-[0.2em] text-white">
            IA<span className="text-[#ff6b2b]">SALES</span>
          </h1>
          <p className="text-zinc-500 text-xs font-mono mt-1 tracking-widest uppercase">Fashion SaaS Platform</p>
        </div>

        {/* Card */}
        <div className="bg-[#0c0c14] border border-white/10 rounded-2xl p-8 shadow-[0_24px_64px_#00000080]">
          <h2 className="text-xl font-bold text-white mb-6 text-center">Entrar na plataforma</h2>

          {error && (
            <div className="bg-red-500/10 border border-red-500/30 text-red-400 text-sm rounded-lg p-3 mb-4 font-mono">
              {error}
            </div>
          )}

          <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
            <div>
              <label className="block text-[10px] font-semibold text-zinc-500 uppercase tracking-widest mb-2 font-mono">
                Email
              </label>
              <input
                {...register('email')}
                type="email"
                placeholder="seu@email.com"
                className="w-full bg-[#12121e] border border-white/10 rounded-lg px-4 py-3 text-sm text-white placeholder-zinc-600 outline-none focus:border-[#ff6b2b] focus:ring-2 focus:ring-[#ff6b2b]/20 transition-all"
              />
              {errors.email && <p className="text-red-400 text-xs mt-1 font-mono">{errors.email.message}</p>}
            </div>

            <div>
              <label className="block text-[10px] font-semibold text-zinc-500 uppercase tracking-widest mb-2 font-mono">
                Senha
              </label>
              <input
                {...register('password')}
                type="password"
                placeholder="••••••••"
                className="w-full bg-[#12121e] border border-white/10 rounded-lg px-4 py-3 text-sm text-white placeholder-zinc-600 outline-none focus:border-[#ff6b2b] focus:ring-2 focus:ring-[#ff6b2b]/20 transition-all"
              />
              {errors.password && <p className="text-red-400 text-xs mt-1 font-mono">{errors.password.message}</p>}
            </div>

            <button
              type="submit"
              disabled={loading}
              className="w-full bg-gradient-to-r from-[#ff6b2b] to-[#ff4500] text-white font-bold py-3 rounded-lg mt-2 hover:opacity-90 transition-all shadow-[0_4px_16px_#ff6b2b35] disabled:opacity-50 disabled:cursor-not-allowed tracking-wide"
            >
              {loading ? 'Entrando...' : 'Entrar'}
            </button>
          </form>

          <p className="text-center text-zinc-600 text-xs mt-6 font-mono">
            Não tem conta?{' '}
            <Link href="/register" className="text-[#ff6b2b] hover:underline">
              Criar conta
            </Link>
          </p>
        </div>
      </div>
    </div>   
    )
}
