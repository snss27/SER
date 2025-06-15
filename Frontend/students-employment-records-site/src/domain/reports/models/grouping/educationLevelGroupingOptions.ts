import { EducationLevelTypes } from "@/domain/educationLevels/enums/EducationLevelTypes"
import { EducationLevel } from "@/domain/educationLevels/models/educationLevel"
import { EducationLevelGroupingType } from "../../enums/educationLevelGroupingVariant"

export type EducationLevelGroupingOptions =
    | { variant: EducationLevelGroupingType.EducationLevels; educationLevels: EducationLevel[] }
    | {
          variant: EducationLevelGroupingType.EducationLevelTypes
          educationLevelTypes: EducationLevelTypes[]
      }
