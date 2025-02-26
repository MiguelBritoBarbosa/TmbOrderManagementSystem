import { Order } from '@/config/domain/orders/orders';
import { API_ROOT_URL } from '@/config/siteConfig';
import { fetchJson } from '@/utils/featchJson';

export async function postOrder(payload: {}): Promise<Order> {
    const url = `${API_ROOT_URL}/orders`;
    const order = await fetchJson<Order>(url, 'POST', payload);
    return order ?? null;
}
