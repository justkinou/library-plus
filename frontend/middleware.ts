import { NextRequest, NextResponse } from "next/server";

export function middleware(request: NextRequest) {
    const refreshToken = request.cookies.get("refreshToken");
    if (refreshToken !== undefined) {
        return NextResponse.redirect("/profile");
    }
    return NextResponse.next();
}

export const config = {
  matcher: ["/login", "/sign-up", "/password-reset"],
}