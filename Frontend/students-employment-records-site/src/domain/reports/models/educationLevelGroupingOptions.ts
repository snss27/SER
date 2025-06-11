import { EducationLevelTypes } from "@/domain/educationLevels/enums/EducationLevelTypes"
import { EducationLevel } from "@/domain/educationLevels/models/educationLevel"
import { EducationLevelGroupingVariant } from "../enums/educationLevelGroupingVariant"

export type EducationLevelGroupingOptions =
    | { variant: EducationLevelGroupingVariant.EducationLevels; educationLevels: EducationLevel[] }
    | {
          variant: EducationLevelGroupingVariant.EducationLevelTypes
          educationLevelTypes: EducationLevelTypes[]
      }
