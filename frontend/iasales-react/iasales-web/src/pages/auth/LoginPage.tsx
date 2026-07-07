import { useState } from 'react'
import { useNavigate, Link } from 'react-router-dom'
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { z } from 'zod'
import api from '@/lib/api'
import { useAuthStore } from '@/store/authStore'

const schema = z.object({
  email: z.string().email('Email inválido'),
  password: z.string().min(6, 'Mínimo 6 caracteres'),
})

type FormData = z.infer<typeof schema>

export default function LoginPage() {
  const navigate = useNavigate()
  const login = useAuthStore(s => s.login)
  const [error, setError] = useState('')
  const [loading, setLoading] = useState(false)

  const { register, handleSubmit, formState: { errors } } = useForm<FormData>({
    resolver: zodResolver(schema),
  })
  // login
  const onSubmit = async (data: FormData) => {
    setLoading(true)
    setError('')
    try {
      const res = await api.post('/auth/login', data)
      localStorage.setItem('token', res.data.token)
      localStorage.setItem('user', JSON.stringify(res.data.user))

      login(res.data.token, res.data.user)

      navigate('/dashboard')
    
    } catch {
      setError('Email ou senha incorretos.')
    } finally {
      setLoading(false)
    }
  }

  return (
    <div style={{
      minHeight: '100vh',
      background: '#05050a',
      display: 'flex',
      alignItems: 'center',
      justifyContent: 'center',
      padding: 16,
      fontFamily: "'Outfit', sans-serif",
      position: 'relative',
      overflow: 'hidden',
    }}>

      {/* Background grid */}
      <div style={{
        position: 'absolute', inset: 0, opacity: 0.03,
        backgroundImage: 'linear-gradient(#ffffff 1px, transparent 1px), linear-gradient(90deg, #ffffff 1px, transparent 1px)',
        backgroundSize: '40px 40px',
        pointerEvents: 'none',
      }} />

      {/* Background glow */}
      <div style={{
        position: 'absolute', top: '20%', left: '50%',
        transform: 'translateX(-50%)',
        width: 600, height: 600,
        background: 'radial-gradient(circle, #ff6b2b08 0%, transparent 70%)',
        pointerEvents: 'none',
      }} />

      <div style={{ width: '100%', maxWidth: 420, position: 'relative', zIndex: 1 }}>

        {/* Logo */}
        <div style={{ textAlign: 'center', marginBottom: 40 }}>
          <div style={{
            display: 'inline-flex', alignItems: 'center', justifyContent: 'center',
            width: 56, height: 56, borderRadius: 16,
            background: 'linear-gradient(135deg, #ff6b2b, #ff4500)',
            marginBottom: 16,
            boxShadow: '0 8px 32px #ff6b2b50',
          }}>
            <span style={{ color: '#fff', fontFamily: "'Bebas Neue', sans-serif", fontSize: 22, letterSpacing: 2 }}>IA</span>
          </div>
          <h1 style={{
            fontFamily: "'Bebas Neue', sans-serif",
            fontSize: 42, letterSpacing: '0.2em',
            color: '#f0f0f8', lineHeight: 1, margin: 0,
          }}>
            IA<span style={{ color: '#ff6b2b' }}>SALES</span>
          </h1>
          <p style={{
            fontFamily: "'JetBrains Mono', monospace",
            fontSize: 10, color: '#55556a',
            letterSpacing: '0.16em', textTransform: 'uppercase',
            marginTop: 6,
          }}>Fashion SaaS Platform</p>
        </div>

        {/* Card */}
        <div style={{
          background: '#0c0c14',
          border: '1px solid #ffffff12',
          borderRadius: 20,
          padding: 32,
          boxShadow: '0 24px 64px #00000090',
          position: 'relative',
          overflow: 'hidden',
        }}>
          {/* Top accent */}
          <div style={{
            position: 'absolute', top: 0, left: 0, right: 0, height: 1,
            background: 'linear-gradient(90deg, transparent, #ff6b2b60, transparent)',
          }} />

          <h2 style={{ fontSize: 20, fontWeight: 700, color: '#f0f0f8', marginBottom: 6 }}>
            Entrar na plataforma
          </h2>
          <p style={{ fontSize: 11, color: '#55556a', fontFamily: "'JetBrains Mono', monospace", marginBottom: 24 }}>
            Acesse seu painel IA Sales
          </p>

          {error && (
            <div style={{
              background: '#ef444415', border: '1px solid #ef444430',
              color: '#ef4444', fontSize: 12, borderRadius: 8,
              padding: '10px 14px', marginBottom: 16,
              fontFamily: "'JetBrains Mono', monospace",
            }}>
              {error}
            </div>
          )}

          <form onSubmit={handleSubmit(onSubmit)}>

            {/* Email */}
            <div style={{ marginBottom: 16 }}>
              <label style={{
                display: 'block', fontSize: 10, fontWeight: 600,
                color: '#55556a', textTransform: 'uppercase',
                letterSpacing: '0.12em', marginBottom: 8,
                fontFamily: "'JetBrains Mono', monospace",
              }}>Email</label>
              <input
                {...register('email')}
                type="email"
                placeholder="seu@email.com"
                style={{
                  width: '100%', background: '#12121e',
                  border: '1px solid #ffffff12', borderRadius: 10,
                  padding: '12px 16px', color: '#f0f0f8',
                  fontSize: 13, outline: 'none',
                  fontFamily: "'Outfit', sans-serif",
                  transition: 'border-color .2s',
                  boxSizing: 'border-box',
                }}
                onFocus={e => e.target.style.borderColor = '#ff6b2b'}
                onBlur={e => e.target.style.borderColor = '#ffffff12'}
              />
              {errors.email && (
                <p style={{ color: '#ef4444', fontSize: 11, marginTop: 4, fontFamily: "'JetBrains Mono', monospace" }}>
                  {errors.email.message}
                </p>
              )}
            </div>

            {/* Senha */}
            <div style={{ marginBottom: 16 }}>
              <label style={{
                display: 'block', fontSize: 10, fontWeight: 600,
                color: '#55556a', textTransform: 'uppercase',
                letterSpacing: '0.12em', marginBottom: 8,
                fontFamily: "'JetBrains Mono', monospace",
              }}>Senha</label>
              <input
                {...register('password')}
                type="password"
                placeholder="••••••••"
                style={{
                  width: '100%', background: '#12121e',
                  border: '1px solid #ffffff12', borderRadius: 10,
                  padding: '12px 16px', color: '#f0f0f8',
                  fontSize: 13, outline: 'none',
                  fontFamily: "'Outfit', sans-serif",
                  transition: 'border-color .2s',
                  boxSizing: 'border-box',
                }}
                onFocus={e => e.target.style.borderColor = '#ff6b2b'}
                onBlur={e => e.target.style.borderColor = '#ffffff12'}
              />
              {errors.password && (
                <p style={{ color: '#ef4444', fontSize: 11, marginTop: 4, fontFamily: "'JetBrains Mono', monospace" }}>
                  {errors.password.message}
                </p>
              )}
            </div>

            {/* Lembrar + Esqueci */}
            <div style={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between', marginBottom: 24 }}>
              <label style={{ display: 'flex', alignItems: 'center', gap: 8, cursor: 'pointer' }}>
                <input type="checkbox" style={{ accentColor: '#ff6b2b', width: 14, height: 14 }} />
                <span style={{ fontSize: 12, color: '#55556a', fontFamily: "'JetBrains Mono', monospace" }}>
                  Lembrar de mim
                </span>
              </label>
              <span style={{ fontSize: 12, color: '#ff6b2b', cursor: 'pointer', fontFamily: "'JetBrains Mono', monospace" }}>
                Esqueci a senha
              </span>
            </div>

            {/* Botão */}
            <button
              type="submit"
              disabled={loading}
              style={{
                width: '100%',
                background: loading ? '#ff6b2b80' : 'linear-gradient(135deg, #ff6b2b, #ff4500)',
                border: 'none', borderRadius: 10,
                padding: '13px 0', color: '#fff',
                fontSize: 13, fontWeight: 700,
                cursor: loading ? 'not-allowed' : 'pointer',
                fontFamily: "'Bebas Neue', sans-serif",
                letterSpacing: '0.2em', fontSize: 15,
                boxShadow: '0 4px 20px #ff6b2b40',
                transition: 'opacity .2s',
              }}
            >
              {loading ? 'ENTRANDO...' : 'ENTRAR'}
            </button>
          </form>

          {/* Divider */}
          <div style={{ display: 'flex', alignItems: 'center', gap: 12, margin: '20px 0' }}>
            <div style={{ flex: 1, height: 1, background: '#ffffff08' }} />
            <span style={{ fontSize: 10, color: '#55556a', fontFamily: "'JetBrains Mono', monospace", textTransform: 'uppercase', letterSpacing: '0.1em' }}>ou</span>
            <div style={{ flex: 1, height: 1, background: '#ffffff08' }} />
          </div>

          <p style={{ textAlign: 'center', fontSize: 12, color: '#55556a', fontFamily: "'JetBrains Mono', monospace" }}>
            Não tem conta?{' '}
            <Link to="/register" style={{ color: '#ff6b2b', textDecoration: 'none', fontWeight: 600 }}>
              Criar conta grátis
            </Link>
          </p>
        </div>

        <p style={{
          textAlign: 'center', fontSize: 10, color: '#2a2a3a',
          fontFamily: "'JetBrains Mono', monospace",
          letterSpacing: '0.14em', marginTop: 24,
        }}>
          IASALES · FASHION SAAS PLATFORM · 2025
        </p>
      </div>
    </div>
  )
}