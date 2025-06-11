import { AdditionalQualification } from "@/domain/additionalQualifications/models/additionalQualification"
import { Enterprise } from "@/domain/enterprises/models/enterprise"
import { ForeignCitizenVariant } from "@/domain/students/enums/foreignCitizenVariant"
import { Gender } from "@/domain/students/enums/gender"
import { OnPaidStudyVariant } from "@/domain/students/enums/onPaidStudyVariant"
import { SocialStatus } from "@/domain/students/enums/socialStatus"
import { StudentStatus } from "@/domain/students/enums/studentStatus"
import { GroupGroupingType } from "../enums/groupGroupingType"
import { WorkPlaceGroupingVariant } from "../enums/workPlaceGroupingVariant"
import { ArmyGroupingVariant } from "./armyGroupingVariant"
import { GroupGroupingOptions } from "./groupGroupingOptions"

export interface ReportGroupingOptions {
    gender: Gender | null
    birthDatePeriod: [Date | null, Date | null]
    onPaidStudyVariant: OnPaidStudyVariant
    groupGroupingOptions: GroupGroupingOptions
    workPlaceEnterprises: Enterprise[]
    workPlaceGroupingVariant: WorkPlaceGroupingVariant
    isUseStrictMatchForAdditionalQualifications: boolean
    additionalQualifications: AdditionalQualification[]
    targetAgreementEnterprises: Enterprise[]
    armyGroupingVariant: ArmyGroupingVariant | null
    isUseStrictMatchForSocialStatuses: boolean
    socialStatuses: SocialStatus[]
    statuses: StudentStatus[]
    foreignCitizenVariant: ForeignCitizenVariant
}

export namespace ReportGroupingOptions {
    export function empty(): ReportGroupingOptions {
        return {
            gender: null,
            birthDatePeriod: [null, null],
            onPaidStudyVariant: OnPaidStudyVariant.All,
            groupGroupingOptions: { type: GroupGroupingType.NotGrouping },
            workPlaceEnterprises: [],
            workPlaceGroupingVariant: WorkPlaceGroupingVariant.All,
            isUseStrictMatchForAdditionalQualifications: false,
            additionalQualifications: [],
            targetAgreementEnterprises: [],
            armyGroupingVariant: null,
            isUseStrictMatchForSocialStatuses: false,
            socialStatuses: [],
            statuses: [],
            foreignCitizenVariant: ForeignCitizenVariant.All,
        }
    }

    export function reducer(state: ReportGroupingOptions, action: Action): ReportGroupingOptions {
        switch (action.type) {
            case "CHANGE_GENDER":
                return { ...state, gender: action.payload.gender }

            case "CHANGE_BIRTH_DATE_PERIOD":
                return { ...state, birthDatePeriod: action.payload.birthDatePeriod }

            case "CHANGE_ON_PAID_STUDY_VARIANT":
                return { ...state, onPaidStudyVariant: action.payload.onPaidStudyVariant }

            case "CHANGE_ADDITIONAL_QUALIFICATIONS":
                return {
                    ...state,
                    additionalQualifications: action.payload.additionalQualifIcations,
                }

            case "CHANGE_IS_USE_STRICT_MATH_FOR_ADDITIONAL_QUALIFICATIONS":
                return {
                    ...state,
                    isUseStrictMatchForAdditionalQualifications:
                        action.payload.isUseStrictMatchForAdditionalQualifications,
                }

            case "CHANGE_TARGET_AGREEMENT_ENTERPRISES":
                return {
                    ...state,
                    targetAgreementEnterprises: action.payload.targetAgreementEnterprises,
                }

            case "CHANGE_IS_USE_STRICT_METH_FOR_SOCIAL_STATUSES":
                return {
                    ...state,
                    isUseStrictMatchForSocialStatuses:
                        action.payload.isUseStrictMatchForSocialStatuses,
                }

            case "CHANGE_SOCIAL_STATUSES":
                return { ...state, socialStatuses: action.payload.socialStatuses }

            case "CHANGE_STATUSES":
                return { ...state, statuses: action.payload.statuses }

            case "CHANGE_WORK_PLACE_ENTERPRISES":
                return { ...state, workPlaceEnterprises: action.payload.workPlaceEnterprises }

            case "CHANGE_WORK_PLACE_GROUPING_VARIANT":
                return {
                    ...state,
                    workPlaceGroupingVariant: action.payload.workPlaceGroupingVariant,
                }

            case "CHANGE_FOREIGN_CITIZEN_VARIAN":
                return { ...state, foreignCitizenVariant: action.payload.foreignCitizenVariant }

            case "CHANGE_GROUP_GROUPING_OPTIONS":
                return { ...state, groupGroupingOptions: action.payload.groupGroupingOptions }

            case "CHANGE_ARMY_GROUPING_VARIANT":
                return { ...state, armyGroupingVariant: action.payload.armyGroupingVariant }

            default:
                return state
        }
    }
}

export type Action =
    | { type: "CHANGE_GENDER"; payload: { gender: Gender | null } }
    | { type: "CHANGE_BIRTH_DATE_PERIOD"; payload: { birthDatePeriod: [Date | null, Date | null] } }
    | { type: "CHANGE_ON_PAID_STUDY_VARIANT"; payload: { onPaidStudyVariant: OnPaidStudyVariant } }
    | {
          type: "CHANGE_ADDITIONAL_QUALIFICATIONS"
          payload: { additionalQualifIcations: AdditionalQualification[] }
      }
    | {
          type: "CHANGE_IS_USE_STRICT_MATH_FOR_ADDITIONAL_QUALIFICATIONS"
          payload: { isUseStrictMatchForAdditionalQualifications: boolean }
      }
    | {
          type: "CHANGE_TARGET_AGREEMENT_ENTERPRISES"
          payload: { targetAgreementEnterprises: Enterprise[] }
      }
    | {
          type: "CHANGE_SOCIAL_STATUSES"
          payload: { socialStatuses: SocialStatus[] }
      }
    | {
          type: "CHANGE_IS_USE_STRICT_METH_FOR_SOCIAL_STATUSES"
          payload: { isUseStrictMatchForSocialStatuses: boolean }
      }
    | {
          type: "CHANGE_STATUSES"
          payload: { statuses: StudentStatus[] }
      }
    | {
          type: "CHANGE_WORK_PLACE_ENTERPRISES"
          payload: { workPlaceEnterprises: Enterprise[] }
      }
    | {
          type: "CHANGE_WORK_PLACE_GROUPING_VARIANT"
          payload: { workPlaceGroupingVariant: WorkPlaceGroupingVariant }
      }
    | {
          type: "CHANGE_FOREIGN_CITIZEN_VARIAN"
          payload: { foreignCitizenVariant: ForeignCitizenVariant }
      }
    | {
          type: "CHANGE_GROUP_GROUPING_OPTIONS"
          payload: { groupGroupingOptions: GroupGroupingOptions }
      }
    | {
          type: "CHANGE_ARMY_GROUPING_VARIANT"
          payload: { armyGroupingVariant: ArmyGroupingVariant | null }
      }
