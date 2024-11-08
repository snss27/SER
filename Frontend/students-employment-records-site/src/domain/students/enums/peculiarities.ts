import { enumToArrayNumber } from "@/tools/enums/enumUtils"
import { NeverUnreachable } from "@/tools/neverUreachable"

export enum Peculiarities {
    Orphan = 1,
    Invalid = 2,
}

export namespace Peculiarities {
    export function getAll(): Peculiarities[] {
        return enumToArrayNumber<Peculiarities>(Peculiarities)
    }

    export function getDisplayText(peculiarity: Peculiarities): string {
        switch (peculiarity) {
            case Peculiarities.Orphan:
                return "Инвалидность"
            case Peculiarities.Invalid:
                return "Сирота"
            default:
                throw new NeverUnreachable(peculiarity)
        }
    }
}
