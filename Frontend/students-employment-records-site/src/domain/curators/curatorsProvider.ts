import { Result } from "@/tools/result"
import HttpClient from "../httpClient"
import Curator from "./models/curator"

class CuratorsProvider {
    public static async save(blank: CuratorsProvider): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/curators/save", blank)
        return Result.fromAny(result)
    }

    public static async remove(id: string): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/curators/remove", id)
        return Result.fromAny(result)
    }

    public static async get(id: string): Promise<Curator> {
        const result = await HttpClient.getJsonAsync("/curators/get", { id })
        return Curator.fromAny(result)
    }

    public static async getPage(page: number, pageSize: number): Promise<Curator[]> {
        const result = await HttpClient.getJsonAsync("/curators/get_page", { page, pageSize })
        return (result as any[]).map(Curator.fromAny)
    }
}

export default CuratorsProvider
