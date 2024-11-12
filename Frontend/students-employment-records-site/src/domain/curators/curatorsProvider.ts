import { Result } from "@/tools/result"
import HttpClient from "../httpClient"

class CuratorsProvider {
    public static async save(blank: CuratorsProvider): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/curators/save", blank)
        return Result.fromAny(result)
    }

    public static async remove(id: string): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/curators/remove", id)
        return Result.fromAny(result)
    }
}

export default CuratorsProvider
