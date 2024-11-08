import { enumToArrayNumber } from "@/tools/enums/enumUtils"
import { NeverUnreachable } from "@/tools/neverUreachable"

export enum Genders {
    Male = 1,
    Female = 2,
}

export namespace Genders {
    export function getAll(): Genders[] {
        return enumToArrayNumber<Genders>(Genders)
    }

    export function getDisplayText(gender: Genders): string {
        switch (gender) {
            case Genders.Male:
                return "Мужской"
            case Genders.Female:
                return "Женский"
            default:
                throw new NeverUnreachable(gender)
        }
    }
}
