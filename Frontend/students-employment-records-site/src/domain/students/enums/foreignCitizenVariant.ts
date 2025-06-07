import { enumToArrayNumber } from "@/tools/enums/enumUtils"
import { NeverUnreachable } from "@/tools/neverUreachable"

export enum ForeignCitizenVariant {
    All = 1,
    OnlyForeignCitizen = 2,
    OnlyNotForeignCitizen = 3,
}

export namespace ForeignCitizenVariant {
    export function getAll(): ForeignCitizenVariant[] {
        return enumToArrayNumber<ForeignCitizenVariant>(ForeignCitizenVariant)
    }

    export function getDisplayText(value: ForeignCitizenVariant) {
        switch (value) {
            case ForeignCitizenVariant.OnlyForeignCitizen:
                return "Только иностранные граждане"
            case ForeignCitizenVariant.OnlyNotForeignCitizen:
                return "Только только НЕ иностранные граждане"
            case ForeignCitizenVariant.All:
                return "Все"
            default:
                throw new NeverUnreachable(value)
        }
    }
}
