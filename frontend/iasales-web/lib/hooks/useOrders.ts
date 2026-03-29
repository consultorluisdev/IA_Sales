import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import api from '@/lib/api'
import { Order } from '@/lib/types'

export function useOrders() {
    return useQuery({
        queryKey: ['orders'],
        queryFn: async () => {
            const { data } = await api.get<Order[]>('/api/orders')
            return data
        },
    })
}

export function useCreateOrder() {
    const qc = useQueryClient()
    return useMutation({
        mutationFn: async (dot: Partial<Order>) => {
            const { data } = await api.post<Order>('/api/orders', dot)
            return data
        },
        onSuccess: () => qc.invalidateQueries({ queryKey: ['orders'] }),
    })
}