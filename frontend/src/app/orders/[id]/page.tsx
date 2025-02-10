import { Card, CardContent, CardDescription, CardFooter, CardHeader, CardTitle } from "@/components/ui/card";
import { getOrder } from "@/config/data/orders/getOrder";
import { formatOrders } from "@/config/domain/orders/formatOrders";

export const dynamic = 'force-dynamic'

export default async function Order({ params }: { params: { id: string } }) {
    const order = await getOrder(params.id);
    const formattedOrder = formatOrders([order])[0];

    console.log(formattedOrder);

    return (
        <section className="w-full justify-self-center self-center grid items-center justify-items-center p-8 pb-20 gap-16 sm:p-20">
            <div className="w-full flex flex-col gap-8 justify-center items-center sm:items-start">
                <h1 className="scroll-m-20 text-4xl font-extrabold tracking-tight lg:text-5xl">Details of Order: </h1>
                <Card>
                    <CardHeader>
                        <CardTitle>Product: {formattedOrder.product}</CardTitle>
                        <CardDescription>Order id {formattedOrder.id}</CardDescription>
                    </CardHeader>
                    <CardContent>
                        <p className="leading-7 [&:not(:first-child)]:mt-6"><strong>buyer:</strong> {formattedOrder.client}</p>
                        <p className="leading-7 [&:not(:first-child)]:mt-6"><strong>Value:</strong> {formattedOrder.value}</p>
                    </CardContent>
                    <CardFooter>
                        <CardDescription>Created at {formattedOrder.createdAt}</CardDescription>
                    </CardFooter>
                </Card>
            </div>
        </section>
    )
}
