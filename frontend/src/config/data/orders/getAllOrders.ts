import { Order } from '@/config/domain/orders/orders';
import { API_ROOT_URL } from '@/config/siteConfig';
import { fetchJson } from '@/utils/featchJson';

export async function getAllOrders(): Promise<Order[]> {
    const url = `${API_ROOT_URL}/orders`;
    const orders = await fetchJson<Order[]>(url, 'GET');
    return orders ?? [];
}
