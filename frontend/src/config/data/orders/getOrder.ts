import { Order } from '@/config/domain/orders/orders';
import { API_URL } from '@/config/siteConfig';
import { fetchJson } from '@/utils/featchJson';

export async function getOrder(id: string): Promise<Order> {
    const url = `${API_URL}/orders/${id}`;
    const order = await fetchJson<Order>(url, 'GET');
    return order ?? null;
}
