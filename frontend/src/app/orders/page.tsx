import { columns } from "@/components/dataTable/columns";
import { DataTable } from "@/components/dataTable/data-table";
import { getAllOrders } from "@/config/data/orders/getAllOrders"
import { formatOrders } from "@/config/domain/orders/formatOrders";


export default async function Orders() {

    const orders = await getAllOrders();
    const formattedOrders = formatOrders(orders);
    console.log(formattedOrders);

    return (
        <section className="w-full justify-self-center self-center grid items-center justify-items-center p-8 pb-20 gap-16 sm:p-20">
            <div className="w-full flex flex-col gap-8 justify-center items-center sm:items-start">
                <DataTable columns={columns} data={formattedOrders} />
            </div>
        </section>
    )
}
