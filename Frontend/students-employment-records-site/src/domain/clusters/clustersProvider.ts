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

    public static async get(id: string): Promise<Cluster> {
        const result = await HttpClient.getJsonAsync("/clusters/get", { id })
        return Cluster.fromAny(result)
    }

    public static async getPage(page: number, pageSize: number): Promise<Cluster[]> {
        const result = await HttpClient.getJsonAsync("/clusters/get_page", {
            page,
            pageSize,
        })
        return (result as any[]).map(Cluster.fromAny)
    }
}
