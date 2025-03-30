import { EmployeeBlank } from "./employeeBlank"

export class Employee {
    constructor(
        public readonly id: string,
        public readonly name: string,
        public readonly secondName: string,
        public readonly lastName: string | null
    ) {}

    public get displayName() {
        return `${this.secondName ?? ""} ${this.name} ${this.lastName ?? ""}`.trim()
    }

    public static fromAny(any: any): Employee {
        return new Employee(any.id, any.name, any.secondName, any.lastName)
    }

    public toBlank(): EmployeeBlank {
        return {
            id: this.id,
            name: this.name,
            secondName: this.secondName,
            lastName: this.lastName,
        }
    }
}
