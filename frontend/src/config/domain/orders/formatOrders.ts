import { Order, OrderStatus } from './orders';

export function formatOrders(orders: Order[]): Order[] {
    const formatOrders: Order[] = orders.map((order) => ({
        ...order,
        value: new Intl.NumberFormat('pt-BR', {
            style: 'currency',
            currency: 'BRL',
        }).format(Number(order.value)),
        status: OrderStatus[order.status as number],
        createdAt: new Date(order.createdAt).toLocaleString('pt-BR'),
    }));

    return formatOrders;
}
