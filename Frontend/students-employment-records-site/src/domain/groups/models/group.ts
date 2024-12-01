import { StructuralUnits } from "../enums/structuralUnits"
import { GroupBlank } from "./groupBlank"
import { EducationLevel } from "@/domain/educationLevels/models/educationLevel"
import { Employee } from "@/domain/employees/models/employee"

export class Group {
    constructor(
        public readonly id: string,
        public readonly number: string,
        public readonly structuralUnit: StructuralUnits,
        public readonly educationLevel: EducationLevel | null,
        public readonly enrollmentYear: number,
        public readonly curator: Employee | null
    ) {}

    public static fromAny(any: any): Group {
        const educationLevel =
            any.educationLevel === null ? null : EducationLevel.fromAny(any.educationLevel)
        const curator = any.curator === null ? null : Employee.fromAny(any.curator)
        return new Group(
            any.id,
            any.number,
            any.structuralUnit,
            educationLevel,
            any.enrollmentYear,
            curator
        )
    }

    public toBlank(): GroupBlank {
        return {
            id: this.id,
            number: this.number,
            structuralUnit: this.structuralUnit,
            educationLevelId: this.educationLevel?.id ?? null,
            enrollmentYear: this.enrollmentYear,
            curatorId: this.curator?.id ?? null,
        }
    }
}
