"use client";

import AuthGuard from "@/components/AuthGuard";
import Header from "@/components/Header";

export default function ProtectedLayout({
    children,
}: {
    children: React.ReactNode;
}) {
    return (
        <AuthGuard>
            <div className="min-h-screen bg-[#05050a] flex flex-col">
                <Header />
                <main className="flex-1">
                    {children}
                </main>
            </div>
        </AuthGuard>
    );
}
