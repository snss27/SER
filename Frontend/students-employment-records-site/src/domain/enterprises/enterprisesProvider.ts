import { Enterprise } from "@/domain/enterprises/models/enterprise"
import { Page } from "@/tools/page"
import { Result } from "@/tools/result"
import HttpClient from "../httpClient"
import { EnterpriseBlank } from "./models/enterpriseBlank"

export class EnterprisesProvider {
    public static async save(blank: EnterpriseBlank): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/enterprises/save", blank)
        return Result.fromAny(result)
    }

    public static async remove(id: string): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/enterprises/remove", id)
        return Result.fromAny(result)
    }

    public static async get(id: string): Promise<Enterprise | null> {
        const result = await HttpClient.getJsonAsync("/enterprises/get", { id })
        return result ? Enterprise.fromAny(result) : null
    }

    public static async getPage(page: number, pageSize: number): Promise<Page<Enterprise>> {
        const result = await HttpClient.getJsonAsync("/enterprises/get_page", { page, pageSize })
        return Page.fromAny(result, Enterprise.fromAny)
    }

    public static async getBySearchText(searchText: string): Promise<Enterprise[]> {
        const result = await HttpClient.getJsonAsync("/enterprises/get_by_search_text", {
            searchText,
        })
        return (result as any[]).map(Enterprise.fromAny)
    }
}
