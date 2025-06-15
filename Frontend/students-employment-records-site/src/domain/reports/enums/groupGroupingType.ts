import { enumToArrayNumber } from "@/tools/enums/enumUtils"
import { NeverUnreachable } from "@/tools/neverUreachable"

export enum GroupGroupingType {
    Groups = 1,
    StructuralUnits = 2,
    EducationLevel = 3,
    EnrollmentYearPeriod = 4,
    Curators = 5,
    Clusters = 6,
    NotGrouping = 7,
}

export namespace GroupGroupingType {
    export function getAll(): GroupGroupingType[] {
        return enumToArrayNumber<GroupGroupingType>(GroupGroupingType)
    }

    export function getDisplayName(type: GroupGroupingType) {
        switch (type) {
            case GroupGroupingType.Groups:
                return "По группам"
            case GroupGroupingType.StructuralUnits:
                return "По структурным подразделениям"
            case GroupGroupingType.EducationLevel:
                return "По уровням образования"
            case GroupGroupingType.EnrollmentYearPeriod:
                return "По году поступления"
            case GroupGroupingType.Curators:
                return "По кураторам"
            case GroupGroupingType.Clusters:
                return "По кластерам"
            case GroupGroupingType.NotGrouping:
                return "Не фильтровать"
            default:
                throw new NeverUnreachable(type)
        }
    }
}
