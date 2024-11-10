import { enumToArrayNumber } from "@/tools/enums/enumUtils"
import { NeverUnreachable } from "@/tools/neverUreachable"

export enum StructuralUnits {
    SP1 = 1,
    SP2 = 2,
    SP3 = 3,
    SP4 = 4,
}

export namespace StructuralUnits {
    export function getAll(): StructuralUnits[] {
        return enumToArrayNumber<StructuralUnits>(StructuralUnits)
    }

    export function getDisplayText(structuralUnit: StructuralUnits): string {
        switch (structuralUnit) {
            case StructuralUnits.SP1:
                return "СП1"
            case StructuralUnits.SP2:
                return "СП2"
            case StructuralUnits.SP3:
                return "СП3"
            case StructuralUnits.SP4:
                return "СП4"
            default:
                throw new NeverUnreachable(structuralUnit)
        }
    }
}
