import { WorkplaceBlank } from "@/domain/workplaces/models/workplaceBlank"
import { BlankFiles } from "@/tools/blankFiles"
import { Gender } from "../enums/gender"
import { SocialStatus } from "../enums/socialStatus"
import { StudentStatus } from "../enums/studentStatus"

export interface StudentBlank {
    id: string | null
    name: string | null
    secondName: string | null
    lastName: string | null
    status: StudentStatus | null
    gender: Gender | null
    phoneNumber: string | null
    representativePhoneNumber: string | null
    birthDate: Date | null
    snils: string | null
    socialStatuses: SocialStatus[]
    address: string | null
    mail: string | null
    inn: string | null
    groupId: string | null
    isForeignCitizen: boolean
    isOnPaidStudy: boolean

    passportSeries: string | null
    passportNumber: string | null
    passportIssuedBy: string | null
    passportIssuedDate: Date | null
    passportFiles: BlankFiles

    currentWorkplace: WorkplaceBlank | null
    prevWorkplaces: WorkplaceBlank[]

    additionalQualificationIds: string[]

    isTargetAgreement: boolean
    targetAgreementDate: Date | null
    targetAgreementEnterpriseId: string | null
    targetAgreementFile: BlankFiles

    mustServeInArmy: boolean
    armySubpoenaFile: BlankFiles
    armyCallDate: Date | null

    otherFiles: BlankFiles
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
            birthDate: null,
            snils: null,
            socialStatuses: [],
            address: null,
            mail: null,
            inn: null,
            groupId: null,
            isForeignCitizen: false,
            isOnPaidStudy: false,

            passportSeries: null,
            passportNumber: null,
            passportIssuedBy: null,
            passportIssuedDate: null,
            passportFiles: BlankFiles.create(5),

            currentWorkplace: null,
            prevWorkplaces: [],

            additionalQualificationIds: [],

            isTargetAgreement: false,
            targetAgreementDate: null,
            targetAgreementEnterpriseId: null,
            targetAgreementFile: BlankFiles.create(1),

            mustServeInArmy: false,
            armySubpoenaFile: BlankFiles.create(1),
            armyCallDate: null,

            otherFiles: BlankFiles.create(10),
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

            case "CHANGE_GROUP_ID":
                return { ...state, groupId: action.payload.groupId }

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

            case "CHANGE_ADDITIONAL_QUALIFICATION_IDS":
                return {
                    ...state,
                    additionalQualificationIds: action.payload.additionalQualificationIds,
                }

            case "CHANGE_IS_TARGET_AGREEMENT":
                return { ...state, isTargetAgreement: action.payload.isTargetAgreement }

            case "CHANGE_TARGET_AGREEMENT_DATE":
                return { ...state, targetAgreementDate: action.payload.targetAgreementDate }

            case "CHANGE_TARGET_AGREEMENT_ENTERPRISE_ID":
                return {
                    ...state,
                    targetAgreementEnterpriseId: action.payload.targetAgreementEnterpriseId,
                }

            case "CHANGE_TARGET_AGREEMENT_FILE":
                return { ...state, targetAgreementFile: action.payload.targetAgreementFile }

            case "CHANGE_MUST_SERVE_IN_ARMY":
                return { ...state, mustServeInArmy: action.payload.mustServeInArmy }

            case "CHANGE_ARMY_SUBPOENA_FILE":
                return { ...state, armySubpoenaFile: action.payload.armySubpoenaFile }

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
          payload: { status: StudentStatus | null }
      }
    | {
          type: "CHANGE_GENDER"
          payload: { gender: Gender | null }
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
          type: "CHANGE_GROUP_ID"
          payload: { groupId: string | null }
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
          payload: { passportFiles: BlankFiles }
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
          type: "CHANGE_ADDITIONAL_QUALIFICATION_IDS"
          payload: { additionalQualificationIds: string[] }
      }
    | {
          type: "CHANGE_IS_TARGET_AGREEMENT"
          payload: { isTargetAgreement: boolean }
      }
    | {
          type: "CHANGE_TARGET_AGREEMENT_DATE"
          payload: { targetAgreementDate: Date | null }
      }
    | {
          type: "CHANGE_TARGET_AGREEMENT_ENTERPRISE_ID"
          payload: { targetAgreementEnterpriseId: string | null }
      }
    | {
          type: "CHANGE_TARGET_AGREEMENT_FILE"
          payload: { targetAgreementFile: BlankFiles }
      }
    | {
          type: "CHANGE_MUST_SERVE_IN_ARMY"
          payload: { mustServeInArmy: boolean }
      }
    | {
          type: "CHANGE_ARMY_SUBPOENA_FILE"
          payload: { armySubpoenaFile: BlankFiles }
      }
    | {
          type: "CHANGE_ARMY_CALL_DATE"
          payload: { armyCallDate: Date | null }
      }
    | {
          type: "CHANGE_OTHER_FILES"
          payload: { otherFiles: BlankFiles }
      }
