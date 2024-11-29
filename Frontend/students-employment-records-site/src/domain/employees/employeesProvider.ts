import { Result } from "@/tools/result"
import HttpClient from "../httpClient"
import { Employee } from "@/domain/employees/models/employee"

export class EmployeesProvider {
    public static async save(blank: EmployeesProvider): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/employees/save", blank)
        return Result.fromAny(result)
    }

    public static async remove(id: string): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/employees/remove", id)
        return Result.fromAny(result)
    }

    public static async get(id: string): Promise<Employee> {
        const result = await HttpClient.getJsonAsync("/employees/get", { id })
        return Employee.fromAny(result)
    }

    public static async getPage(page: number, pageSize: number): Promise<Employee[]> {
        const result = await HttpClient.getJsonAsync("/employees/get_page", { page, pageSize })
        return (result as any[]).map(Employee.fromAny)
    }

    public static async getBySearchText(searchText: string): Promise<Employee[]> {
        const result = await HttpClient.getJsonAsync("/employees/get_by_search_text", {
            searchText,
        })
        return (result as any[]).map(Employee.fromAny)
    }
}
