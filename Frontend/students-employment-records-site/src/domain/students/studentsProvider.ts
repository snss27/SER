import { Result } from "@/tools/result"
import HttpClient from "../httpClient"
import { Student } from "./models/student"
import { StudentBlank } from "./models/studentBlank"

export class StudentsProvider {
    public static async save(blank: StudentBlank): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/students/save", blank)
        return Result.fromAny(result)
    }

    public static async remove(id: string): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/students/remove", id)
        return Result.fromAny(result)
    }

    public static async get(id: string): Promise<Student> {
        const result = await HttpClient.getJsonAsync("/students/get", { id })
        return Student.fromAny(result)
    }

    public static async getPage(page: number, pageSize: number): Promise<Student[]> {
        const result = await HttpClient.getJsonAsync("/students/get_page", {
            page,
            pageSize,
        })
        return (result as any[]).map(Student.fromAny)
    }
}
