import type { Metadata } from "next";
import { Montserrat } from "next/font/google";
import "@/styles/globals.css";
import { SidebarProvider, SidebarTrigger } from "@/components/ui/sidebar";
import { AppSidebar } from "@/components/app-sidebar";
import NprogressComponent from "@/config/nprogress";
import '@/styles/nprogress.css';

const montserrat = Montserrat({
    subsets: ['latin'],
    weight: ['500', '600', '700', '800', '900'],
    variable: '--font-montserrat',
});

export const metadata: Metadata = {
    title: "TMB Order Management System",
    description: "A Concept Test for full stack developer vacancy",
};

export default function RootLayout({
    children,
}: Readonly<{
    children: React.ReactNode;
}>) {
    return (
        <html lang="pt-br">
            <body
                className={`${montserrat.className}`}
            >
                <NprogressComponent />
                <SidebarProvider defaultOpen={true}>
                    <AppSidebar />
                    <main className="w-full">
                        <SidebarTrigger />
                        {children}
                    </main>
                </SidebarProvider>
            </body>
        </html>
    );
}
