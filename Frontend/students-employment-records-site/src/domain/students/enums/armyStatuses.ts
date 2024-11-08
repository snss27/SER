import { enumToArrayNumber } from "@/tools/enums/enumUtils"
import { NeverUnreachable } from "@/tools/neverUreachable"

//TODO Проставлять Female если пол женский?
export enum ArmyStatuses {
    Female = 1,
    Unfit = 2,
    Fit = 3,
}

export namespace ArmyStatuses {
    export function getAll(): ArmyStatuses[] {
        return enumToArrayNumber<ArmyStatuses>(ArmyStatuses)
    }

    export function getDisplayText(armyStatus: ArmyStatuses): string {
        switch (armyStatus) {
            case ArmyStatuses.Female:
                return "Девушка"
            case ArmyStatuses.Unfit:
                return "Не годен"
            case ArmyStatuses.Fit:
                return "Годен"
            default:
                throw new NeverUnreachable(armyStatus)
        }
    }
}
