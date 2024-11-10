import { Result } from "@/tools/result"
import HttpClient from "../httpClient"
import { SpecialityBlank } from "./models/specialityBlank"

export class SpecialitiesProvider {
    public static async save(blank: SpecialityBlank): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/specialities/save", blank)
        return Result.fromAny(result)
    }
}
