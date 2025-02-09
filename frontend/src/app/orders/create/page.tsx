import { CreateOrderForm } from "@/components/form/createOrderForm";


export default async function CreateOrders() {

    return (
        <section className="w-full justify-self-center self-center grid items-center justify-items-center p-8 pb-20 gap-16 sm:p-20">
            <div className="w-full flex flex-col gap-8 justify-center items-center sm:items-start">
                <h1>Create a new Order</h1>
                <CreateOrderForm />
            </div>
        </section>
    )
}
