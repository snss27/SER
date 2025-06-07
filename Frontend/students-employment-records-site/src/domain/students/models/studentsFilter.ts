import { AdditionalQualification } from "@/domain/additionalQualifications/models/additionalQualification"
import { Enterprise } from "@/domain/enterprises/models/enterprise"
import { Group } from "@/domain/groups/models/group"
import { ForeignCitizenVariant } from "../enums/foreignCitizenVariant"
import { Gender } from "../enums/gender"
import { MustServeInArmyVariant } from "../enums/mustServeInArmyVariant"
import { OnPaidStudyVariant } from "../enums/onPaidStudyVariant"
import { SocialStatus } from "../enums/socialStatus"
import { StudentStatus } from "../enums/studentStatus"
import { TargetAgreementVariant } from "../enums/targetAgreementVariant"

export interface StudentsFilter {
    searchText: string
    gender: Gender | null
    statuses: StudentStatus[]
    birthDatePeriod: [Date | null, Date | null]
    socialStatuses: SocialStatus[]
    groups: Group[]
    onPaidStudyVariant: OnPaidStudyVariant
    foreignCitizenVariant: ForeignCitizenVariant
    additionalQualifications: AdditionalQualification[]
    targetAgreementVariant: TargetAgreementVariant
    targetAgreementEnterprises: Enterprise[]
    mustServeInArmyVariant: MustServeInArmyVariant
    armyCallDatePeriod: [Date | null, Date | null]
}

export namespace StudentsFilter {
    export function empty(): StudentsFilter {
        return {
            searchText: "",
            gender: null,
            statuses: [],
            birthDatePeriod: [null, null],
            socialStatuses: [],
            groups: [],
            onPaidStudyVariant: OnPaidStudyVariant.All,
            foreignCitizenVariant: ForeignCitizenVariant.All,
            additionalQualifications: [],
            targetAgreementVariant: TargetAgreementVariant.All,
            targetAgreementEnterprises: [],
            mustServeInArmyVariant: MustServeInArmyVariant.All,
            armyCallDatePeriod: [null, null],
        }
    }

    export function reducer(state: StudentsFilter, action: Action): StudentsFilter {
        switch (action.type) {
            case "CHANGE_SEARCH_TEXT":
                return { ...state, searchText: action.payload.searchText }

            case "RESET_FILTERS":
                return { ...empty(), searchText: state.searchText }

            case "CHANGE_GENDER":
                return { ...state, gender: action.payload.gender }

            case "CHANGE_STATUSES":
                return { ...state, statuses: action.payload.statuses }

            case "SET":
                return { ...action.payload.studentsFilter }

            case "CHANGE_BIRTH_DATE_PERIOD":
                return { ...state, birthDatePeriod: action.payload.birthDatePeriod }

            case "CHANGE_SOCIAL_STATUSES":
                return { ...state, socialStatuses: action.payload.socialStatuses }

            case "CHANGE_GROUPS":
                return { ...state, groups: action.payload.groups }

            case "CHANGE_ON_PAID_STUDY_VARIANT":
                return { ...state, onPaidStudyVariant: action.payload.onPaidStudyVariant }

            case "CHANGE_FOREIGN_CITIZEN_VARIANT":
                return { ...state, foreignCitizenVariant: action.payload.foreignCitizenVariant }

            case "CHANGE_ADDITIONAL_QUALIFICATIONS":
                return {
                    ...state,
                    additionalQualifications: action.payload.additionalQualifications,
                }

            case "CHANGE_TARGET_AGREEMENT_VARIANT": {
                const { targetAgreementVariant } = action.payload
                const isWithTargetAgreement =
                    TargetAgreementVariant.isWithTargetAgreement(targetAgreementVariant)
                return {
                    ...state,
                    targetAgreementEnterprises: isWithTargetAgreement
                        ? state.targetAgreementEnterprises
                        : [],
                    targetAgreementVariant: action.payload.targetAgreementVariant,
                }
            }

            case "CHANGE_TARGET_AGREEMENT_ENTERPRISES": {
                return {
                    ...state,
                    targetAgreementEnterprises: action.payload.targetAgreementEnterprises,
                }
            }

            case "CHANGE_MUST_SERVE_IN_ARMY_VARIANT": {
                const { mustServeInArmyVariant } = action.payload
                const isMustServe = MustServeInArmyVariant.isMustServe(mustServeInArmyVariant)
                return {
                    ...state,
                    armyCallDatePeriod: isMustServe ? state.armyCallDatePeriod : [null, null],
                    mustServeInArmyVariant,
                }
            }
            case "CHANGE_ARMY_CALL_DATE_PERIOD":
                return { ...state, armyCallDatePeriod: action.payload.armyCallDatePeriod }

            default:
                return { ...state }
        }
    }
}

export type Action =
    | {
          type: "CHANGE_SEARCH_TEXT"
          payload: { searchText: string }
      }
    | {
          type: "RESET_FILTERS"
      }
    | {
          type: "CHANGE_GENDER"
          payload: { gender: Gender | null }
      }
    | {
          type: "SET"
          payload: { studentsFilter: StudentsFilter }
      }
    | {
          type: "CHANGE_STATUSES"
          payload: { statuses: StudentStatus[] }
      }
    | {
          type: "CHANGE_BIRTH_DATE_PERIOD"
          payload: { birthDatePeriod: [Date | null, Date | null] }
      }
    | {
          type: "CHANGE_SOCIAL_STATUSES"
          payload: { socialStatuses: SocialStatus[] }
      }
    | {
          type: "CHANGE_GROUPS"
          payload: { groups: Group[] }
      }
    | {
          type: "CHANGE_ON_PAID_STUDY_VARIANT"
          payload: { onPaidStudyVariant: OnPaidStudyVariant }
      }
    | {
          type: "CHANGE_FOREIGN_CITIZEN_VARIANT"

          payload: { foreignCitizenVariant: ForeignCitizenVariant }
      }
    | {
          type: "CHANGE_ADDITIONAL_QUALIFICATIONS"
          payload: { additionalQualifications: AdditionalQualification[] }
      }
    | {
          type: "CHANGE_TARGET_AGREEMENT_VARIANT"
          payload: { targetAgreementVariant: TargetAgreementVariant }
      }
    | {
          type: "CHANGE_TARGET_AGREEMENT_ENTERPRISES"
          payload: { targetAgreementEnterprises: Enterprise[] }
      }
    | {
          type: "CHANGE_MUST_SERVE_IN_ARMY_VARIANT"
          payload: { mustServeInArmyVariant: MustServeInArmyVariant }
      }
    | {
          type: "CHANGE_ARMY_CALL_DATE_PERIOD"
          payload: { armyCallDatePeriod: [Date | null, Date | null] }
      }
