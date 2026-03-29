import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query'
import api from '@/lib/api'
import { Product } from '@/lib/types'

export function useProducts(page = 1, category?: string, search?: string){
    return useQuery({
        queryKey: ['products', page, category, search],
        queryFn: async () => {
            const { data } = await api.get<Product[]>('/api/products', {
                params: { page, pageSize: 20, category, search},
            })
            return data
        },
    })
}

export function useProduct(id: string){
    return useQuery({
        queryKey: ['product', id],
        queryFn: async () => {
            const { data } = await api.get<Product>(`/api/products/${id}`)
            return data
        },
        enabled: !!id,

    })
}

export function useCreateProduct() {
    const qc = useQueryClient()
    return useMutation({
        mutationFn: async (dto: Partial<Product>) => {
            const { data } = await api.post<Product>('/api/products', dto)
            return data
        },
        onSuccess: () => qc.invalidateQueries({ queryKey: ['products'] }),
    })
}

export function useDeleteProduct(){
    return useMutation({
        mutationFn: async (id: string) => {
            await api.delete(`/api/products/${id}`)},
    })
}