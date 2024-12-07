import { StructuralUnits } from "../enums/structuralUnits"
import { GroupBlank } from "./groupBlank"
import { EducationLevel } from "@/domain/educationLevels/models/educationLevel"
import { Employee } from "@/domain/employees/models/employee"

export class Group {
    constructor(
        public readonly id: string,
        public readonly number: string,
        public readonly structuralUnit: StructuralUnits,
        public readonly speciality: EducationLevel | null,
        public readonly enrollmentYear: number,
        public readonly curator: Employee | null
    ) {}

    public static fromAny(any: any): Group {
        const speciality = any.speciality === null ? null : EducationLevel.fromAny(any.speciality)
        const curator = any.curator === null ? null : Employee.fromAny(any.curator)
        return new Group(
            any.id,
            any.number,
            any.structuralUnit,
            speciality,
            any.enrollmentYear,
            curator
        )
    }

    public toBlank(): GroupBlank {
        return {
            id: this.id,
            number: this.number,
            structuralUnit: this.structuralUnit,
            specialityId: this.speciality?.id ?? null,
            enrollmentYear: this.enrollmentYear,
            curatorId: this.curator?.id ?? null,
        }
    }
}
