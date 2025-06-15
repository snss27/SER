import { enumToArrayNumber } from "@/tools/enums/enumUtils"

export enum EducationLevelGroupingType {
    EducationLevelTypes = 1,
    EducationLevels = 2,
}

export namespace EducationLevelGroupingType {
    export function getAll() {
        return enumToArrayNumber<EducationLevelGroupingType>(EducationLevelGroupingType)
    }
}
