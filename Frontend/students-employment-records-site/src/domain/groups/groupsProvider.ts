import { Result } from "@/tools/result"
import HttpClient from "../httpClient"

class GroupsProvider {
    public static async save(blank: GroupsProvider): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/groups/save", blank)
        return Result.fromAny(result)
    }
}

export default GroupsProvider
