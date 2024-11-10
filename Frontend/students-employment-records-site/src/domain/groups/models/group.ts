import { StructuralUnits } from "../enums/structuralUnits"
import { GroupBlank } from "./groupBlank"

class Group {
    constructor(
        public readonly id: string,
        public readonly number: string,
        public readonly structuralUnit: StructuralUnits,
        public readonly specialityId: string,
        public readonly enrollmentYear: number,
        public readonly curatorName: string
    ) {}

    public static fromAny(any: any): Group {
        return new Group(
            any.id,
            any.number,
            any.structuralUnit,
            any.specialityId,
            any.enrollmentYear,
            any.curatorName
        )
    }

    public toBlank(): GroupBlank {
        return {
            id: this.id,
            number: this.number,
            structuralUnit: this.structuralUnit,
            specialityId: this.specialityId,
            enrollmentYear: this.enrollmentYear,
            curatorName: this.curatorName,
        }
    }
}

export default Group
