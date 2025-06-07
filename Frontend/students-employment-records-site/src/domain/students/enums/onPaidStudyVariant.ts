import { enumToArrayNumber } from "@/tools/enums/enumUtils"
import { NeverUnreachable } from "@/tools/neverUreachable"

export enum OnPaidStudyVariant {
    All = 1,
    OnlyOnPaidStudy = 2,
    OnlyOnFreeStudy = 3,
}

export namespace OnPaidStudyVariant {
    export function getAll(): OnPaidStudyVariant[] {
        return enumToArrayNumber<OnPaidStudyVariant>(OnPaidStudyVariant)
    }

    export function getDisplayText(value: OnPaidStudyVariant) {
        switch (value) {
            case OnPaidStudyVariant.OnlyOnPaidStudy:
                return "Только на платной основе"
            case OnPaidStudyVariant.OnlyOnFreeStudy:
                return "Только на бесплатной основе"
            case OnPaidStudyVariant.All:
                return "Все"
            default:
                throw new NeverUnreachable(value)
        }
    }
}
