import { Result } from "@/tools/result"
import HttpClient from "../httpClient"
import WorkPost from "./models/workPost"
import { WorkPostBlank } from "./models/workPostBlank"

class WorkPostsProvider {
    public static async save(blank: WorkPostBlank): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/work_posts/save", blank)
        return Result.fromAny(result)
    }

    public static async remove(id: string): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/work_posts/remove", id)
        return Result.fromAny(result)
    }

    public static async get(id: string): Promise<WorkPost> {
        const result = await HttpClient.getJsonAsync("/work_posts/get", { id })
        return WorkPost.fromAny(result)
    }

    public static async getPage(page: number, pageSize: number): Promise<WorkPost[]> {
        const result = await HttpClient.getJsonAsync("/work_posts/get_page", { page, pageSize })
        return (result as any[]).map(WorkPost.fromAny)
    }
}

export default WorkPostsProvider
