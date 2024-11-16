import Curator from "@/domain/curators/models/curator"
import Speciality from "@/domain/specialities/models/speciality"
import { StructuralUnits } from "../enums/structuralUnits"
import { GroupBlank } from "./groupBlank"

class Group {
    constructor(
        public readonly id: string,
        public readonly number: string,
        public readonly structuralUnit: StructuralUnits,
        public readonly speciality: Speciality | null,
        public readonly enrollmentYear: number,
        public readonly curator: Curator | null
    ) {}

    public static fromAny(any: any): Group {
        console.log(any)
        const speciality = any.speciality === null ? null : Speciality.fromAny(any.speciality)
        const curator = any.curator === null ? null : Curator.fromAny(any.curator)
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
            speciality: this.speciality,
            enrollmentYear: this.enrollmentYear,
            curator: this.curator,
        }
    }
}

export default Group
