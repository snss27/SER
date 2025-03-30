import { Group } from "@/domain/groups/models/group"
import { Result } from "@/tools/result"
import HttpClient from "../httpClient"
import { GroupBlank } from "./models/groupBlank"

export class GroupsProvider {
    public static async save(blank: GroupBlank): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/groups/save", blank)
        return Result.fromAny(result)
    }

    public static async remove(id: string): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/groups/remove", id)
        return Result.fromAny(result)
    }

    public static async get(id: string): Promise<Group | null> {
        const result = await HttpClient.getJsonAsync("/groups/get", { id })
        return result ? Group.fromAny(result) : null
    }

    public static async getPage(page: number, pageSize: number): Promise<Group[]> {
        const result = await HttpClient.getJsonAsync("/groups/get_page", { page, pageSize })
        return (result as any[]).map(Group.fromAny)
    }

    public static async getBySearchText(searchText: string): Promise<Group[]> {
        const result = await HttpClient.getJsonAsync("/groups/get_by_search_text", {
            searchText,
        })
        return (result as any[]).map(Group.fromAny)
    }
}
