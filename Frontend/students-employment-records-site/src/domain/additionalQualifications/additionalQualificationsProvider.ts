import { Result } from "@/tools/result"
import HttpClient from "../httpClient"
import AdditionalQualification from "./models/additionalQualification"
import { AdditionalQualificationBlank } from "./models/additionalQualificationBlank"

class AdditionalQualificationsProvider {
    public static async save(blank: AdditionalQualificationBlank): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/additional_qualifications/save", blank)
        return Result.fromAny(result)
    }

    public static async remove(id: string): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/additional_qualifications/remove", id)
        return Result.fromAny(result)
    }

    public static async get(id: string): Promise<AdditionalQualification> {
        const result = await HttpClient.getJsonAsync("/additional_qualifications/get", { id })
        return AdditionalQualification.fromAny(result)
    }

    public static async getPage(
        page: number,
        pageSize: number
    ): Promise<AdditionalQualification[]> {
        const result = await HttpClient.getJsonAsync("/additional_qualifications/get_page", {
            page,
            pageSize,
        })
        return (result as any[]).map(AdditionalQualification.fromAny)
    }
}

export default AdditionalQualificationsProvider
