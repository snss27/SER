import { AdditionalQualification } from "@/domain/additionalQualifications/models/additionalQualification"
import { Enterprise } from "@/domain/enterprises/models/enterprise"
import { Group } from "@/domain/groups/models/group"
import { WorkplaceBlank } from "@/domain/workplaces/models/workplaceBlank"
import { Gender } from "../enums/gender"
import { SocialStatus } from "../enums/socialStatus"
import { StudentStatus } from "../enums/studentStatus"
export interface StudentBlank {
    id: string | null
    name: string | null
    secondName: string | null
    lastName: string | null
    status: StudentStatus
    gender: Gender
    phoneNumber: string | null
    representativePhoneNumber: string | null
    representativeAlias: string | null
    birthDate: Date | null
    snils: string | null
    socialStatuses: SocialStatus[]
    address: string | null
    mail: string | null
    inn: string | null
    group: Group | null
    isForeignCitizen: boolean
    isOnPaidStudy: boolean

    passportSeries: string | null
    passportNumber: string | null
    passportIssuedBy: string | null
    passportIssuedDate: Date | null
    passportFiles: string[]

    currentWorkplace: WorkplaceBlank | null
    prevWorkplaces: WorkplaceBlank[]

    additionalQualifications: AdditionalQualification[]

    isTargetAgreement: boolean
    targetAgreementNumber: string | null
    targetAgreementDate: Date | null
    targetAgreementEnterprise: Enterprise | null
    targetAgreementFiles: string[]

    mustServeInArmy: boolean
    armySubpoenaFiles: string[]
    armyCallDate: Date | null

    otherFiles: string[]
}

export namespace StudentBlank {
    export function empty(): StudentBlank {
        return {
            id: null,
            name: null,
            secondName: null,
            lastName: null,
            status: StudentStatus.Active,
            gender: Gender.Male,
            phoneNumber: null,
            representativePhoneNumber: null,
            representativeAlias: null,
            birthDate: null,
            snils: null,
            socialStatuses: [],
            address: null,
            mail: null,
            inn: null,
            group: null,
            isForeignCitizen: false,
            isOnPaidStudy: false,

            passportSeries: null,
            passportNumber: null,
            passportIssuedBy: null,
            passportIssuedDate: null,
            passportFiles: [],

            currentWorkplace: null,
            prevWorkplaces: [],

            additionalQualifications: [],

            isTargetAgreement: false,
            targetAgreementNumber: null,
            targetAgreementDate: null,
            targetAgreementEnterprise: null,
            targetAgreementFiles: [],

            mustServeInArmy: false,
            armySubpoenaFiles: [],
            armyCallDate: null,

            otherFiles: [],
        }
    }

    export function reducer(state: StudentBlank, action: StudentAction): StudentBlank {
        switch (action.type) {
            case "CHANGE_NAME":
                return { ...state, name: action.payload.name }

            case "CHANGE_SECOND_NAME":
                return { ...state, secondName: action.payload.secondName }

            case "CHANGE_LAST_NAME":
                return { ...state, lastName: action.payload.lastName }

            case "CHANGE_STATUS":
                return { ...state, status: action.payload.status }

            case "CHANGE_GENDER":
                return { ...state, gender: action.payload.gender }

            case "CHANGE_PHONE_NUMBER":
                return { ...state, phoneNumber: action.payload.phoneNumber }

            case "CHANGE_REPRESENTATIVE_PHONE_NUMBER":
                return {
                    ...state,
                    representativePhoneNumber: action.payload.representativePhoneNumber,
                }

            case "CHANGE_REPRESENTATIVE_ALIAS":
                return { ...state, representativeAlias: action.payload.representativeAlias }

            case "CHANGE_BIRTH_DATE":
                return { ...state, birthDate: action.payload.birthDate }

            case "CHANGE_SNILS":
                return { ...state, snils: action.payload.snils }

            case "CHANGE_SOCIAL_STATUSES":
                return { ...state, socialStatuses: action.payload.socialStatuses }

            case "CHANGE_ADDRESS":
                return {
                    ...state,
                    address: action.payload.address,
                }
            case "CHANGE_MAIL":
                return {
                    ...state,
                    mail: action.payload.mail,
                }
            case "CHANGE_INN":
                return {
                    ...state,
                    inn: action.payload.inn,
                }

            case "CHANGE_GROUP":
                return { ...state, group: action.payload.group }

            case "CHANGE_IS_FOREIGN_CITIZEN":
                return {
                    ...state,
                    isForeignCitizen: action.payload.isForeignCitizen,
                }

            case "CHANGE_IS_ON_PAID_STUDY":
                return { ...state, isOnPaidStudy: action.payload.isOnPaidStudy }

            case "CHANGE_PASSPORT_SERIES":
                return { ...state, passportSeries: action.payload.passportSeries }

            case "CHANGE_PASSPORT_NUMBER":
                return {
                    ...state,
                    passportNumber: action.payload.passportNumber,
                }

            case "CHANGE_PASSPORT_ISSUED_BY":
                return { ...state, passportIssuedBy: action.payload.passportIssuedBy }

            case "CHANGE_PASSPORT_ISSUED_DATE":
                return { ...state, passportIssuedDate: action.payload.passportIssuedDate }

            case "CHANGE_PASSPORT_FILES":
                return { ...state, passportFiles: action.payload.passportFiles }

            case "CHANGE_CURRENT_WORKPLACE":
                return { ...state, currentWorkplace: action.payload.currentWorkplace }

            case "CHANGE_PREV_WORKPLACES":
                return { ...state, prevWorkplaces: action.payload.prevWorkplaces }

            case "CHANGE_ADDITIONAL_QUALIFICATIONS":
                return {
                    ...state,
                    additionalQualifications: action.payload.additionalQualifications,
                }

            case "CHANGE_IS_TARGET_AGREEMENT":
                return { ...state, isTargetAgreement: action.payload.isTargetAgreement }

            case "CHANGE_TARGET_AGREEMENT_NUMBER":
                return { ...state, targetAgreementNumber: action.payload.targetAgreementNumber }

            case "CHANGE_TARGET_AGREEMENT_DATE":
                return { ...state, targetAgreementDate: action.payload.targetAgreementDate }

            case "CHANGE_TARGET_AGREEMENT_ENTERPRISE":
                return {
                    ...state,
                    targetAgreementEnterprise: action.payload.targetAgreementEnterprise,
                }

            case "CHANGE_TARGET_AGREEMENT_FILE":
                return { ...state, targetAgreementFiles: action.payload.targetAgreementFiles }

            case "CHANGE_MUST_SERVE_IN_ARMY":
                return { ...state, mustServeInArmy: action.payload.mustServeInArmy }

            case "CHANGE_ARMY_SUBPOENA_FILE":
                return { ...state, armySubpoenaFiles: action.payload.armySubpoenaFiles }

            case "CHANGE_ARMY_CALL_DATE":
                return { ...state, armyCallDate: action.payload.armyCallDate }

            case "CHANGE_OTHER_FILES":
                return { ...state, otherFiles: action.payload.otherFiles }

            default:
                return { ...state }
        }
    }
}

export type StudentAction =
    | {
          type: "CHANGE_NAME"
          payload: { name: string }
      }
    | {
          type: "CHANGE_SECOND_NAME"
          payload: { secondName: string }
      }
    | {
          type: "CHANGE_LAST_NAME"
          payload: { lastName: string }
      }
    | {
          type: "CHANGE_STATUS"
          payload: { status: StudentStatus }
      }
    | {
          type: "CHANGE_GENDER"
          payload: { gender: Gender }
      }
    | {
          type: "CHANGE_PHONE_NUMBER"
          payload: { phoneNumber: string | null }
      }
    | {
          type: "CHANGE_REPRESENTATIVE_PHONE_NUMBER"
          payload: { representativePhoneNumber: string | null }
      }
    | {
          type: "CHANGE_REPRESENTATIVE_ALIAS"
          payload: { representativeAlias: string | null }
      }
    | {
          type: "CHANGE_BIRTH_DATE"
          payload: { birthDate: Date | null }
      }
    | {
          type: "CHANGE_SNILS"
          payload: { snils: string | null }
      }
    | {
          type: "CHANGE_SOCIAL_STATUSES"
          payload: { socialStatuses: SocialStatus[] }
      }
    | {
          type: "CHANGE_ADDRESS"
          payload: { address: string | null }
      }
    | {
          type: "CHANGE_MAIL"
          payload: { mail: string | null }
      }
    | {
          type: "CHANGE_INN"
          payload: { inn: string | null }
      }
    | {
          type: "CHANGE_GROUP"
          payload: { group: Group | null }
      }
    | {
          type: "CHANGE_IS_FOREIGN_CITIZEN"
          payload: { isForeignCitizen: boolean }
      }
    | {
          type: "CHANGE_IS_ON_PAID_STUDY"
          payload: { isOnPaidStudy: boolean }
      }
    | {
          type: "CHANGE_PASSPORT_SERIES"
          payload: { passportSeries: string | null }
      }
    | {
          type: "CHANGE_PASSPORT_NUMBER"
          payload: { passportNumber: string | null }
      }
    | {
          type: "CHANGE_PASSPORT_ISSUED_BY"
          payload: { passportIssuedBy: string | null }
      }
    | {
          type: "CHANGE_PASSPORT_ISSUED_DATE"
          payload: { passportIssuedDate: Date | null }
      }
    | {
          type: "CHANGE_PASSPORT_FILES"
          payload: { passportFiles: string[] }
      }
    | {
          type: "CHANGE_CURRENT_WORKPLACE"
          payload: { currentWorkplace: WorkplaceBlank | null }
      }
    | {
          type: "CHANGE_PREV_WORKPLACES"
          payload: { prevWorkplaces: WorkplaceBlank[] }
      }
    | {
          type: "CHANGE_ADDITIONAL_QUALIFICATIONS"
          payload: { additionalQualifications: AdditionalQualification[] }
      }
    | {
          type: "CHANGE_IS_TARGET_AGREEMENT"
          payload: { isTargetAgreement: boolean }
      }
    | {
          type: "CHANGE_TARGET_AGREEMENT_NUMBER"
          payload: { targetAgreementNumber: string | null }
      }
    | {
          type: "CHANGE_TARGET_AGREEMENT_DATE"
          payload: { targetAgreementDate: Date | null }
      }
    | {
          type: "CHANGE_TARGET_AGREEMENT_ENTERPRISE"
          payload: { targetAgreementEnterprise: Enterprise | null }
      }
    | {
          type: "CHANGE_TARGET_AGREEMENT_FILE"
          payload: { targetAgreementFiles: string[] }
      }
    | {
          type: "CHANGE_MUST_SERVE_IN_ARMY"
          payload: { mustServeInArmy: boolean }
      }
    | {
          type: "CHANGE_ARMY_SUBPOENA_FILE"
          payload: { armySubpoenaFiles: string[] }
      }
    | {
          type: "CHANGE_ARMY_CALL_DATE"
          payload: { armyCallDate: Date | null }
      }
    | {
          type: "CHANGE_OTHER_FILES"
          payload: { otherFiles: string[] }
      }
