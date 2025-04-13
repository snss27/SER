import { Cluster } from "@/domain/clusters/models/cluster"
import { EducationLevel } from "@/domain/educationLevels/models/educationLevel"
import { Employee } from "@/domain/employees/models/employee"
import { StructuralUnits } from "../enums/structuralUnits"
import { GroupBlank } from "./groupBlank"

export class Group {
    constructor(
        public readonly id: string,
        public readonly number: string,
        public readonly structuralUnit: StructuralUnits,
        public readonly educationLevel: EducationLevel,
        public readonly enrollmentYear: number,
        public readonly curator: Employee | null,
        public readonly hasCluster: boolean,
        public readonly cluster: Cluster | null
    ) {}

    public get displayName() {
        return `${this.number} "${this.educationLevel.displayName}"`
    }

    public static fromAny(any: any): Group {
        const educationLevel = EducationLevel.fromAny(any.educationLevel)

        const curator = any.curator === null ? null : Employee.fromAny(any.curator)

        const cluster = any.cluster === null ? null : Cluster.fromAny(any.cluster)

        return new Group(
            any.id,
            any.number,
            any.structuralUnit,
            educationLevel,
            any.enrollmentYear,
            curator,
            any.hasCluster,
            cluster
        )
    }

    public toBlank(): GroupBlank {
        return {
            id: this.id,
            number: this.number,
            structuralUnit: this.structuralUnit,
            educationLevel: this.educationLevel,
            enrollmentYear: this.enrollmentYear,
            curator: this.curator ?? null,
            hasCluster: this.hasCluster,
            cluster: this.cluster ?? null,
        }
    }
}
