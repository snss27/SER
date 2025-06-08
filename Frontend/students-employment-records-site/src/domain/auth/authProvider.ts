import { Result } from "@/tools/result"
import HttpClient from "../httpClient"
import { AuthRequest } from "./models/authRequest"

export class AuthProvider {
    public static async Auth(request: AuthRequest): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/auth/auth", request)
        return Result.fromAny(result)
    }
}
