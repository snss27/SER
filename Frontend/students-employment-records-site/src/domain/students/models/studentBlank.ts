import { ArmyStatuses } from "../enums/armyStatuses"
import { Genders } from "../enums/genders"
import { Peculiarities } from "../enums/peculiarities"

export interface StudentBlank {
    id: string | null
    name: string | null
    secondName: string | null
    lastName: string | null
    gender: Genders | null
    birthDate: Date | null
    phoneNumber: string | null
    representativePhoneNumber: string | null
    isOnPaidStudy: boolean
    snils: string | null
    groupId: string | null
    passportId: string | null
    passportIssued: string | null
    workplaceInfoId: string | null
    additionalQualificationIds: string[]
    isTargetAgreement: boolean
    targetAgreementFile: string | null
    targetAgreementDate: string | null // 
    targetAgreementEnterpriseId: string | null // 
    armyStatus: ArmyStatuses | null
    armySubpoenaFile: string | null
    armyServeDate: Date | null
    peculiarity: Peculiarities | null
    passportSeries: string | null
    passportNumber: string | null
    mail: string | null
    inn: string | null
    isForeignCitizen: boolean
    address: string | null
}

export namespace StudentBlank {
    export function empty(): StudentBlank {
        return {
            id: null,
            name: null,
            secondName: null,
            lastName: null,
            gender: null,
            birthDate: null,
            phoneNumber: null,
            representativePhoneNumber: null,
            isOnPaidStudy: false,
            snils: null,
            groupId: null,
            passportId: null,
            passportIssued: null, // 
            workplaceInfoId: null,
            additionalQualificationIds: [],
            isTargetAgreement: false,
            targetAgreementFile: null,
            targetAgreementDate: null, // 
            targetAgreementEnterpriseId: null, // 
            armyStatus: null,
            armySubpoenaFile: null,
            armyServeDate: null,
            peculiarity: null,
            passportSeries: null,
            passportNumber: null,
            mail: null,
            inn: null,
            isForeignCitizen: false, // 
            address: null
        }
    }

    export function reducer(state: StudentBlank, action: Action): StudentBlank {
        switch (action.type) {
            case "CHANGE_NAME":
                return { ...state, name: action.payload.name }
    
            case "CHANGE_SECOND_NAME":
                return { ...state, secondName: action.payload.secondName }
    
            case "CHANGE_LAST_NAME":
                return { ...state, lastName: action.payload.lastName }
    
            case "CHANGE_GENDER":
                return { ...state, gender: action.payload.gender }
    
            case "CHANGE_BIRTH_DATE":
                return { ...state, birthDate: action.payload.birthDate }
    
            case "CHANGE_PHONE_NUMBER":
                return { ...state, phoneNumber: action.payload.phoneNumber }
    
            case "CHANGE_REPRESENTATIVE_PHONE_NUMBER":
                return {
                    ...state,
                    representativePhoneNumber: action.payload.representativePhoneNumber,
                }
    
            case "CHANGE_IS_ON_PAID_STUDY":
                return { ...state, isOnPaidStudy: action.payload.isOnPaidStudy }
    
            case "CHANGE_SNILS":
                return { ...state, snils: action.payload.snils }
    
            case "CHANGE_GROUP":
                return { ...state, groupId: action.payload.groupId }
    
            case "CHANGE_PASSPORT":
                return { ...state, passportId: action.payload.passportId }
    
            case "CHANGE_PASSPORT_ISSUED":
                return { ...state, passportIssued: action.payload.passportIssued }
    
            case "CHANGE_WORKPLACE_INFO":
                return { ...state, workplaceInfoId: action.payload.workplaceInfoId }
    
            case "CHANGE_ADDITIONAL_QUALIFICATIONS":
                return {
                    ...state,
                    additionalQualificationIds: action.payload.additionalQualificationIds,
                }
    
            case "CHANGE_PASSPORTNUMBER":
                return {
                    ...state,
                    passportNumber: action.payload.passportNumber,
                }
    
            case "CHANGE_PASSPORTSERIES":
                return {
                    ...state,
                    passportSeries: action.payload.passportSeries,
                }
    
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
    
            case "CHANGE_IS_FOREIGN_CITIZEN":
                return {
                    ...state,
                    isForeignCitizen: action.payload.isForeignCitizen,
                }
    
            case "TOGGLE_IS_TARGET_AGREEMENT":
                return { ...state, isTargetAgreement: !state.isTargetAgreement }
    
            case "CHANGE_TARGET_AGREEMENT_FILE":
                return { ...state, targetAgreementFile: action.payload.targetAgreementFile }
    
            case "CHANGE_TARGET_AGREEMENT_DATE":
                return { ...state, targetAgreementDate: action.payload.targetAgreementDate }
    
            case "CHANGE_TARGET_AGREEMENT_ENTERPRISE_ID":
                return { ...state, targetAgreementEnterpriseId: action.payload.targetAgreementEnterpriseId }
    
            case "CHANGE_ARMY_STATUS":
                return { ...state, armyStatus: action.payload.armyStatus }
    
            case "CHANGE_ARMY_SUBPOENA_FILE":
                return { ...state, armySubpoenaFile: action.payload.armySubpoenaFile }
    
            case "CHANGE_ARMY_SERVE_DATE":
                return { ...state, armyServeDate: action.payload.armyServeDate }
    
            case "CHANGE_PECULIARITY":
                return { ...state, peculiarity: action.payload.peculiarity }
    
            default:
                return state
        }
    }
}

type Action =
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
          type: "CHANGE_GENDER"
          payload: { gender: Genders | null }
      }
    | {
          type: "CHANGE_BIRTH_DATE"
          payload: { birthDate: Date | null }
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
          type: "CHANGE_IS_ON_PAID_STUDY"
          payload: { isOnPaidStudy: boolean }
      }
    | {
          type: "CHANGE_SNILS"
          payload: { snils: string | null }
      }
    | {
          type: "CHANGE_GROUP"
          payload: { groupId: string | null }
      }
    | {
          type: "CHANGE_PASSPORT"
          payload: { passportId: string | null }
      }
    | {
          type: "CHANGE_WORKPLACE_INFO"
          payload: { workplaceInfoId: string | null }
      }
    | {
          type: "CHANGE_ADDITIONAL_QUALIFICATIONS"
          payload: { additionalQualificationIds: string[] }
      }
    | {
          type: "TOGGLE_IS_TARGET_AGREEMENT"
      }
    | {
          type: "CHANGE_TARGET_AGREEMENT_FILE"
          payload: { targetAgreementFile: string | null }
      }
    | {
          type: "CHANGE_TARGET_AGREEMENT_DATE" // 
          payload: { targetAgreementDate: string | null }
      }
    | {
          type: "CHANGE_TARGET_AGREEMENT_ENTERPRISE_ID" // 
          payload: { targetAgreementEnterpriseId: string | null }
      }
    | {
          type: "CHANGE_ARMY_STATUS"
          payload: { armyStatus: ArmyStatuses | null }
      }
    | {
          type: "CHANGE_ARMY_SUBPOENA_FILE"
          payload: { armySubpoenaFile: string | null }
      }
    | {
          type: "CHANGE_ARMY_SERVE_DATE"
          payload: { armyServeDate: Date | null }
      }
    | {
          type: "CHANGE_PECULIARITY"
          payload: { peculiarity: Peculiarities | null }
      }
    | {
        type: "CHANGE_ADDRESS"
        payload: { address: string | null }
    }
    | {
        type: "CHANGE_PASSPORTNUMBER"
        payload: { passportNumber: string | null }
    }
    | {
        type: "CHANGE_PASSPORTSERIES"
        payload: { passportSeries: string | null }
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
        type: "CHANGE_IS_FOREIGN_CITIZEN"
        payload: { isForeignCitizen: boolean }
    }
    | {
        type: "CHANGE_PASSPORT_ISSUED"
        payload: { passportIssued: string | null }
    }