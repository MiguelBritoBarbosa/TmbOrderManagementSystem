import Image from "next/image";
import Link from "next/link";

export default function Home() {
    return (
        <section className="justify-self-center self-center grid grid-rows-[20px_1fr_20px] items-center justify-items-center p-8 pb-20 gap-16 sm:p-20">
            <div className="flex flex-col gap-8 row-start-2 items-center sm:items-start">
                <Image
                    className="dark:invert"
                    src="/next.svg"
                    alt="Next.js logo"
                    width={180}
                    height={38}
                    priority
                />
                <ol className="list-inside list-decimal text-sm text-center sm:text-left">
                    <li className="mb-2">
                        Create a new order here: <Link href="/orders/create">Create order</Link>
                    </li>
                    <li>See all orders created here: <Link href="/orders">Orders</Link></li>
                </ol>
            </div>
            <footer className="row-start-3 flex gap-6 flex-wrap items-center justify-center">
                <p>This project is part of the selection process for a Full-stack developer position at TMB Education. Your goal is to build an API using the Entity Framework and a front-end with React to consume it</p>
            </footer>
        </section>
    );
}
