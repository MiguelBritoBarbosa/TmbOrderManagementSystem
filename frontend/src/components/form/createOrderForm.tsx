"use client"

import { zodResolver } from "@hookform/resolvers/zod"
import { useForm } from "react-hook-form"
import { z } from "zod"

import { Button } from "@/components/ui/button"
import {
    Form,
    FormControl,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from "@/components/ui/form"
import { Input } from "@/components/ui/input"
import { postOrder } from "@/config/data/orders/postOrder"
import { useToast } from "@/hooks/use-toast"
import { useRouter } from "next/navigation"

export function CreateOrderForm() {
    const router = useRouter();
    const { toast } = useToast()

    const FormSchema = z.object({
        client: z.string().min(2, {
            message: "Client name must be at least 2 characters.",
        }).nonempty(),
        product: z.string().min(2, {
            message: "Product name must be at least 2 characters.",
        }).nonempty(),
        value: z.preprocess((a: any) => {
            if (!a) {
                return a;
            }
            const rawValue = a.replace(/\D/g, "");
            return Number(rawValue) / 100;
        }, z.number().min(0.01, {
            message: "Value must be at least 0.01.",
        }).nonnegative()),
    });

    const form = useForm<z.infer<typeof FormSchema>>({
        resolver: zodResolver(FormSchema),
        defaultValues: {
            client: "",
            product: "",
            value: 0,
        }
    });

    const onSubmit = async (values: z.infer<typeof FormSchema>) => {
        const newOrder = await postOrder(values);
        if (!newOrder) {
            toast({
                variant: "destructive",
                title: "Error:",
                description: (
                    <p>Unable to create order</p>
                ),
            });
        }
        router.push('/orders');
        toast({
            title: "New order",
            description: (
                <p>Order created successfully</p>
            ),
        });
    }

    const currencyFormat = (value: string) => {
        const rawValue = value.replace(/\D/g, "");
        const numberValue = Number(rawValue) / 100;
        return new Intl.NumberFormat("pt-BR", {
            style: "currency",
            currency: "BRL",
        }).format(numberValue);
    };

    return (
        <Form {...form}>
            <form onSubmit={form.handleSubmit(onSubmit)} className="w-full grid gap-4">
                <div className="grid md:grid-cols-2 gap-4">
                    <FormField
                        control={form.control}
                        name="client"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Client:</FormLabel>
                                <FormControl>
                                    <Input placeholder="Client name" {...field} />
                                </FormControl>
                                <FormMessage />
                            </FormItem>
                        )}
                    />
                    <FormField
                        control={form.control}
                        name="product"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Product:</FormLabel>
                                <FormControl>
                                    <Input placeholder="Product name" {...field} />
                                </FormControl>
                                <FormMessage />
                            </FormItem>
                        )}
                    />
                </div>
                <FormField
                    control={form.control}
                    name="value"
                    render={({ field }) => (
                        <FormItem>
                            <FormLabel>Order value:</FormLabel>
                            <FormControl>
                                <Input placeholder="Order value" {...field}
                                    value={currencyFormat(String(field.value))}
                                    onChange={(e) => {
                                        const formatted = currencyFormat(e.target.value);
                                        field.onChange(formatted.replace(/\D/g, ""));
                                    }}
                                />
                            </FormControl>
                            <FormMessage />
                        </FormItem>
                    )}
                />
                <Button className="justify-self-start" type="submit">Submit</Button>
            </form>
        </Form>
    )
}
