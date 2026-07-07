import { BrowserRouter, Routes, Route, Navigate, Outlet } from 'react-router-dom'
import { useAuthStore } from '@/store/authStore'
import LoginPage from '@/pages/auth/LoginPage'
import RegisterPage from '@/pages/auth/RegisterPage'
import DashboardLayout from '@/layout/DashboardLayout'       
import DashboardPage from '@/pages/Dashboard/DashboardPage'

function PrivateRoute() {
    const isAuthenticated = useAuthStore((s) => s.isAuthenticated)
    if (!isAuthenticated) {
        return <Navigate to="/login" replace />
    }
    return <Outlet />
}

function PublicRoute() {
    const isAuthenticated = useAuthStore((s) => s.isAuthenticated)
    if (isAuthenticated) {
        return <Navigate to="/dashboard" replace />
    }
    return <Outlet />
}

function AppRoutes() {
  return (
    <Routes>
      <Route element={<PublicRoute />}>
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
      </Route>
      <Route element={<PrivateRoute />}>
        <Route path="/dashboard" element={<DashboardLayout />}>
          <Route index element={<DashboardPage />} />
        </Route>
      </Route>
      <Route path="/" element={<Navigate to="/dashboard" replace />} />
      <Route path="*" element={<Navigate to="/dashboard" replace />} />
    </Routes>
  )
}

export default function App() {
  return (
    <BrowserRouter>
      <AppRoutes />
    </BrowserRouter>
  )
}