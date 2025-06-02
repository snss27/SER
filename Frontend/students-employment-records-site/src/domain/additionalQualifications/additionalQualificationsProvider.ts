import { AdditionalQualification } from "@/domain/additionalQualifications/models/additionalQualification"
import { Page } from "@/tools/page"
import { Result } from "@/tools/result"
import HttpClient from "../httpClient"
import { AdditionalQualificationBlank } from "./models/additionalQualificationBlank"

export class AdditionalQualificationsProvider {
    public static async save(blank: AdditionalQualificationBlank): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/additional_qualifications/save", blank)
        return Result.fromAny(result)
    }

    public static async remove(id: string): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/additional_qualifications/remove", id)
        return Result.fromAny(result)
    }

    public static async get(id: string): Promise<AdditionalQualification | null> {
        const result = await HttpClient.getJsonAsync("/additional_qualifications/get", { id })
        return result ? AdditionalQualification.fromAny(result) : null
    }

    public static async getPage(
        page: number,
        pageSize: number
    ): Promise<Page<AdditionalQualification>> {
        const result = await HttpClient.getJsonAsync("/additional_qualifications/get_page", {
            page,
            pageSize,
        })
        return Page.fromAny(result, AdditionalQualification.fromAny)
    }

    public static async getBySearchText(searchText: string): Promise<AdditionalQualification[]> {
        const result = await HttpClient.getJsonAsync(
            "/additional_qualifications/get_by_search_text",
            {
                searchText,
            }
        )
        return (result as any[]).map(AdditionalQualification.fromAny)
    }
}
