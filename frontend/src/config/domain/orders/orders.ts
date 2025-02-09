export type orderId = string;

export interface Order {
    id: orderId;
    client: string;
    product: string;
    value: number | string;
    status: OrderStatus | string;
    createdAt: string;
}

export enum OrderStatus {
    Pending,
    Processing,
    Finished,
}
