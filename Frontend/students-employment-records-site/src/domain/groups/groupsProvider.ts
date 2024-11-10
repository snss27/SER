import { Result } from "@/tools/result"
import HttpClient from "../httpClient"
import Group from "./models/group"

class GroupsProvider {
    public static async save(blank: GroupsProvider): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/groups/save", blank)
        return Result.fromAny(result)
    }

    public static async remove(id: string): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/groups/remove", id)
        return Result.fromAny(result)
    }

    public static async getAll(): Promise<Group[]> {
        const result = await HttpClient.getJsonAsync("/groups/get/all")
        return (result as any[]).map(Group.fromAny)
    }

    public static async get(id: string): Promise<Group> {
        const result = await HttpClient.getJsonAsync("/groups/get", { id })
        return Group.fromAny(result)
    }
}

export default GroupsProvider
