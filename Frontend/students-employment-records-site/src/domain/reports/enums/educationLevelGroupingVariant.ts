import { enumToArrayNumber } from "@/tools/enums/enumUtils"

export enum EducationLevelGroupingVariant {
    EducationLevelTypes = 1,
    EducationLevels = 2,
}

export namespace EducationLevelGroupingVariant {
    export function getAll() {
        return enumToArrayNumber<EducationLevelGroupingVariant>(EducationLevelGroupingVariant)
    }
}
