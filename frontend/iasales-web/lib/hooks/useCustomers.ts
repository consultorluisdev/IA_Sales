import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import api from '@/lib/api'
import { Customer } from '@/lib/types'

export function useCustomers() {
    return useQuery({
        queryKey: ['customers'],
        queryFn: async () => {
            const { data } = await api.get<Customer[]>('/api/customers')
            return data
        },
    })
}

export function useCreateCustomer() {
    const qc = useQueryClient()
    return useMutation({
        mutationFn: async (dto: Partial<Customer>) => {
            const { data } = await api.post<Customer>('/api/customers', dto)
            return data
        },
        onSuccess: () => qc.invalidateQueries({ queryKey: ['customers'] }),
    })

}