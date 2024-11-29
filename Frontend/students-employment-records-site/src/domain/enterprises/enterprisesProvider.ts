import { Result } from "@/tools/result"
import HttpClient from "../httpClient"
import { EnterpriseBlank } from "./models/enterpriseBlank"
import { Enterprise } from "@/domain/enterprises/models/enterprise"

export class EnterprisesProvider {
    public static async save(blank: EnterpriseBlank): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/enterprises/save", blank)
        return Result.fromAny(result)
    }

    public static async remove(id: string): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/enterprises/remove", id)
        return Result.fromAny(result)
    }

    public static async get(id: string): Promise<Enterprise> {
        const result = await HttpClient.getJsonAsync("/enterprises/get", { id })
        return Enterprise.fromAny(result)
    }

    public static async getPage(page: number, pageSize: number): Promise<Enterprise[]> {
        const result = await HttpClient.getJsonAsync("/enterprises/get_page", { page, pageSize })
        return (result as any[]).map(Enterprise.fromAny)
    }
}
