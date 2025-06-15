import { enumToArrayNumber } from "@/tools/enums/enumUtils"
import { NeverUnreachable } from "@/tools/neverUreachable"

export enum SocialStatus {
    Orphan = 1,
    Invalid = 2,
    OVZ = 3,
}

export namespace SocialStatus {
    export function getAll(): SocialStatus[] {
        return enumToArrayNumber<SocialStatus>(SocialStatus)
    }

    export function getDisplayText(status: SocialStatus): string {
        switch (status) {
            case SocialStatus.Orphan:
                return "Сирота"
            case SocialStatus.Invalid:
                return "Инвалидность"
            case SocialStatus.OVZ:
                return "ОВЗ"
            default:
                throw new NeverUnreachable(status)
        }
    }
}
