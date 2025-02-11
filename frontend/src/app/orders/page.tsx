"use client";
import { columns } from "@/components/dataTable/columns";
import { DataTable } from "@/components/dataTable/data-table";
import { getAllOrders } from "@/config/data/orders/getAllOrders";
import { formatOrders } from "@/config/domain/orders/formatOrders";
import { Order } from "@/config/domain/orders/orders";
import { API_ROOT_URL } from "@/config/siteConfig";
import { useEffect, useState } from "react";
import * as signalR from "@microsoft/signalr";
import { FakeTable } from "@/components/dataTable/fake-table";

export default function Orders() {
    const [formattedOrders, setFormattedOrders] = useState<Order[]>([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        getAllOrders()
            .then(data => {
                setFormattedOrders(formatOrders(data));
                setLoading(false);
            });

        const connection = new signalR.HubConnectionBuilder()
            .withUrl(`${API_ROOT_URL}/orderHub`, {
                withCredentials: true
            })
            .withAutomaticReconnect()
            .build();

        connection.start().catch((err) => console.error(err));

        connection.on("ReceiveOrderUpdate", (updatedOrder: Order) => {
            setLoading(true);
            setFormattedOrders((prevOrders) =>
                prevOrders.map((order) =>
                    order.id === updatedOrder.id ? formatOrders([updatedOrder])[0] : order
                )
            );
            setTimeout(() => {
                setLoading(false);
            }, 500);
        });

        return () => {
            connection.stop();
        };
    }, []);

    return (
        <section className="w-full justify-self-center self-center grid items-center justify-items-center p-8 pb-20 gap-16 sm:p-20">
            <div className="w-full flex flex-col gap-8 justify-center items-center sm:items-start">
                <h1 className="scroll-m-20 text-4xl font-extrabold tracking-tight lg:text-5xl">Listing all Orders</h1>
                {loading ? (
                    <FakeTable columns={columns} data={Array.from({ length: 10 })} />
                ) : (
                    <DataTable columns={columns} data={formattedOrders} />
                )}
            </div>
        </section>
    );
}
