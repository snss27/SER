import { enumToArrayNumber } from "@/tools/enums/enumUtils"
import { NeverUnreachable } from "@/tools/neverUreachable"

export enum MustServeInArmyVariant {
    All = 1,
    OnlyMustServe = 2,
    OnlyNotMustServe = 3,
}

export namespace MustServeInArmyVariant {
    export function getAll(): MustServeInArmyVariant[] {
        return enumToArrayNumber<MustServeInArmyVariant>(MustServeInArmyVariant)
    }

    export function isMustServe(value: MustServeInArmyVariant) {
        switch (value) {
            case MustServeInArmyVariant.All:
            case MustServeInArmyVariant.OnlyMustServe:
                return true
            case MustServeInArmyVariant.OnlyNotMustServe:
                return false
            default:
                throw new NeverUnreachable(value)
        }
    }

    export function getDisplayText(value: MustServeInArmyVariant) {
        switch (value) {
            case MustServeInArmyVariant.OnlyMustServe:
                return "Только подлежащие призыву"
            case MustServeInArmyVariant.OnlyNotMustServe:
                return "Только НЕ подлежащие призыву"
            case MustServeInArmyVariant.All:
                return "Все"
            default:
                throw new NeverUnreachable(value)
        }
    }
}
