import { Order } from "@/config/domain/orders/orders";
import { ColumnDef } from "@tanstack/react-table";

export const columns: ColumnDef<Order>[] = [
    {
        accessorKey: "client",
        header: "Client",
    },
    {
        accessorKey: "product",
        header: "Product",
    },
    {
        accessorKey: "value",
        header: "Value",
    },
    {
        accessorKey: "status",
        header: "Status",
    },
    {
        accessorKey: "createdAt",
        header: "Created At",
    },
]
