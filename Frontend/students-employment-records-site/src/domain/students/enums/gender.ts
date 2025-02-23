import { enumToArrayNumber } from "@/tools/enums/enumUtils"
import { NeverUnreachable } from "@/tools/neverUreachable"

export enum Gender {
    Male = 1,
    Female = 2,
}

export namespace Gender {
    export function getAll(): Gender[] {
        return enumToArrayNumber<Gender>(Gender)
    }

    export function getDisplayText(gender: Gender): string {
        switch (gender) {
            case Gender.Male:
                return "Мужской"
            case Gender.Female:
                return "Женский"
            default:
                throw new NeverUnreachable(gender)
        }
    }
}
