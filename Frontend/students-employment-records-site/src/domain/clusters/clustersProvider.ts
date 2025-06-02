import { Page } from "@/tools/page"
import { Result } from "@/tools/result"
import HttpClient from "../httpClient"
import { Cluster } from "./models/cluster"
import { ClusterBlank } from "./models/clusterBlank"

export class ClustersProvider {
    public static async save(blank: ClusterBlank): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/clusters/save", blank)
        return Result.fromAny(result)
    }

    public static async remove(id: string): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/clusters/remove", id)
        return Result.fromAny(result)
    }

    public static async get(id: string): Promise<Cluster | null> {
        const result = await HttpClient.getJsonAsync("/clusters/get", { id })
        return result ? Cluster.fromAny(result) : null
    }

    public static async getPage(page: number, pageSize: number): Promise<Page<Cluster>> {
        const result = await HttpClient.getJsonAsync("/clusters/get_page", {
            page,
            pageSize,
        })
        return Page.fromAny(result, Cluster.fromAny)
    }

    public static async getBySearchText(searchText: string): Promise<Cluster[]> {
        const result = await HttpClient.getJsonAsync("/clusters/get_by_search_text", {
            searchText,
        })
        return (result as any[]).map(Cluster.fromAny)
    }
}
