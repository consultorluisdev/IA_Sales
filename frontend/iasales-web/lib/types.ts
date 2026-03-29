export interface User {
  id: string
  name: string
  email: string
  role: string
}

export interface AuthResponse {
  token: string
  user: User
}

export interface Product {
  id: string
  name: string
  description?: string
  price: number
  comparePrice?: number
  stock: number
  category?: string
  images: string[]
  tags: string[]
  isActive: boolean
  aiContent?: {
    instagramPost?: string
    adCopy?: string
    hashtags: string[]
    imageSuggestion?: string
  }
  createdAt: string
}

export interface Customer {
  id: string
  tenantId: string
  name: string
  email?: string
  phone?: string
  source: string
  interests: string[]
  totalSpent: number
  orderCount: number
  notes?: string
  createdAt: string
}

export interface Order {
  id: string
  customerId?: string
  customer?: Customer
  status: string
  items: OrderItem[]
  subtotal: number
  discount: number
  shipping: number
  total: number
  channel: string
  createdAt: string
}

export interface OrderItem {
  productId: string
  productName: string
  quantity: number
  unitPrice: number
  size?: string
}

export interface Campaign {
  id: string
  name: string
  platform: string
  objective: string
  budgetDaily: number
  status: string
  aiGenerated: boolean
  adCopy?: string
  headline?: string
  spend: number
  roas: number
  createdAt: string
}