import { Result } from "@/tools/result"
import HttpClient from "../httpClient"
import Speciality from "./models/speciality"
import { SpecialityBlank } from "./models/specialityBlank"

class SpecialitiesProvider {
    public static async save(blank: SpecialityBlank): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/specialities/save", blank)
        return Result.fromAny(result)
    }

    public static async remove(id: string): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/specialities/remove", id)
        return Result.fromAny(result)
    }

    public static async get(id: string): Promise<Speciality> {
        const result = await HttpClient.getJsonAsync("/specialities/get", { id })
        return Speciality.fromAny(result)
    }

    public static async getPage(page: number, pageSize: number): Promise<Speciality[]> {
        const result = await HttpClient.getJsonAsync("/specialities/get_page", { page, pageSize })
        return (result as any[]).map(Speciality.fromAny)
    }

    public static async getBySearchText(searchText: string): Promise<Speciality[]> {
        const result = await HttpClient.getJsonAsync("/specialities/get_by_search_text", {
            searchText,
        })
        return (result as any[]).map(Speciality.fromAny)
    }
}

export default SpecialitiesProvider
