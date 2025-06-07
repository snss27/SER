import { enumToArrayNumber } from "@/tools/enums/enumUtils"
import { NeverUnreachable } from "@/tools/neverUreachable"

export enum TargetAgreementVariant {
    All = 1,
    OnlyWithTargetAgreement = 2,
    OnlyWithoutTargetAgreement = 3,
}

export namespace TargetAgreementVariant {
    export function getAll(): TargetAgreementVariant[] {
        return enumToArrayNumber<TargetAgreementVariant>(TargetAgreementVariant)
    }

    export function isWithTargetAgreement(value: TargetAgreementVariant) {
        switch (value) {
            case TargetAgreementVariant.All:
            case TargetAgreementVariant.OnlyWithTargetAgreement:
                return true

            case TargetAgreementVariant.OnlyWithoutTargetAgreement:
                return false
            default:
                throw new NeverUnreachable(value)
        }
    }

    export function getDisplayText(value: TargetAgreementVariant) {
        switch (value) {
            case TargetAgreementVariant.OnlyWithTargetAgreement:
                return "Только целевое обучение"
            case TargetAgreementVariant.OnlyWithoutTargetAgreement:
                return "Только только НЕ целевое обучение"
            case TargetAgreementVariant.All:
                return "Все"
            default:
                throw new NeverUnreachable(value)
        }
    }
}
