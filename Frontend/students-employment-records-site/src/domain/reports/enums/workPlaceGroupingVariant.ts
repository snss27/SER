import { enumToArrayNumber } from "@/tools/enums/enumUtils"
import { NeverUnreachable } from "@/tools/neverUreachable"

export enum WorkPlaceGroupingVariant {
    All = 1,
    OnlyCurrent = 2,
    OnlyPrev = 3,
}

export namespace WorkPlaceGroupingVariant {
    export function getAll(): WorkPlaceGroupingVariant[] {
        return enumToArrayNumber<WorkPlaceGroupingVariant>(WorkPlaceGroupingVariant)
    }

    export function getDisplayName(type: WorkPlaceGroupingVariant) {
        switch (type) {
            case WorkPlaceGroupingVariant.All:
                return "Все"
            case WorkPlaceGroupingVariant.OnlyCurrent:
                return "Только текущие"
            case WorkPlaceGroupingVariant.OnlyPrev:
                return "Только предыдущие"
            default:
                throw new NeverUnreachable(type)
        }
    }
}
