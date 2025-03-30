import { EducationLevel } from "@/domain/educationLevels/models/educationLevel"
import { Result } from "@/tools/result"
import HttpClient from "../httpClient"
import { EducationLevelBlank } from "./models/educationLevelBlank"

export class EducationLevelsProvider {
    public static async save(blank: EducationLevelBlank): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/education_levels/save", blank)
        return Result.fromAny(result)
    }

    public static async remove(id: string): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/education_levels/remove", id)
        return Result.fromAny(result)
    }

    public static async get(id: string): Promise<EducationLevel | null> {
        const result = await HttpClient.getJsonAsync("/education_levels/get", { id })
        return result ? EducationLevel.fromAny(result) : null
    }

    public static async getPage(page: number, pageSize: number): Promise<EducationLevel[]> {
        const result = await HttpClient.getJsonAsync("/education_levels/get_page", {
            page,
            pageSize,
        })
        return (result as any[]).map(EducationLevel.fromAny)
    }

    public static async getBySearchText(searchText: string): Promise<EducationLevel[]> {
        const result = await HttpClient.getJsonAsync("/education_levels/get_by_search_text", {
            searchText,
        })
        return (result as any[]).map(EducationLevel.fromAny)
    }
}
