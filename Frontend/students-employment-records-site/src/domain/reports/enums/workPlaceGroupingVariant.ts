import { enumToArrayNumber } from "@/tools/enums/enumUtils"
import { NeverUnreachable } from "@/tools/neverUreachable"

export enum WorkPlaceGroupingType {
    All = 1,
    OnlyCurrent = 2,
    OnlyPrev = 3,
}

export namespace WorkPlaceGroupingType {
    export function getAll(): WorkPlaceGroupingType[] {
        return enumToArrayNumber<WorkPlaceGroupingType>(WorkPlaceGroupingType)
    }

    export function getDisplayName(type: WorkPlaceGroupingType) {
        switch (type) {
            case WorkPlaceGroupingType.All:
                return "Все"
            case WorkPlaceGroupingType.OnlyCurrent:
                return "Только текущие"
            case WorkPlaceGroupingType.OnlyPrev:
                return "Только предыдущие"
            default:
                throw new NeverUnreachable(type)
        }
    }
}
