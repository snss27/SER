import { Employee } from "@/domain/employees/models/employee"
import { Page } from "@/tools/page"
import { Result } from "@/tools/result"
import HttpClient from "../httpClient"
import { EmployeeBlank } from "./models/employeeBlank"

export class EmployeesProvider {
    public static async save(blank: EmployeeBlank): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/employees/save", blank)
        return Result.fromAny(result)
    }

    public static async remove(id: string): Promise<Result> {
        const result = await HttpClient.postJsonAsync("/employees/remove", id)
        return Result.fromAny(result)
    }

    public static async get(id: string): Promise<Employee | null> {
        const result = await HttpClient.getJsonAsync("/employees/get", { id })
        return result ? Employee.fromAny(result) : null
    }

    public static async getPage(page: number, pageSize: number): Promise<Page<Employee>> {
        const result = await HttpClient.getJsonAsync("/employees/get_page", { page, pageSize })
        return Page.fromAny(result, Employee.fromAny)
    }

    public static async getBySearchText(searchText: string): Promise<Employee[]> {
        const result = await HttpClient.getJsonAsync("/employees/get_by_search_text", {
            searchText,
        })
        return (result as any[]).map(Employee.fromAny)
    }
}
