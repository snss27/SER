import { ArmyStatuses } from "../enums/armyStatuses"
import { Genders } from "../enums/genders"
import { Peculiarities } from "../enums/peculiarities"

export interface StudentBlank {
    id: string | null
    name: string | null
    surname: string | null
    patronymic: string | null
    gender: Genders | null
    birthDate: Date | null
    phoneNumber: string | null
    representativePhoneNumber: string | null
    isOnPaidStudy: boolean
    snils: string | null
    groupId: string | null
    passportId: string | null
    workplaceInfoId: string | null
    additionalQualificationIds: string[]
    isTargetAgreement: boolean
    targetAgreementFile: string | null
    armyStatus: ArmyStatuses | null
    armySubpoenaFile: string | null
    armyServeDate: Date | null
    peculiarity: Peculiarities | null
}

export namespace StudentBlank {
    export function empty(): StudentBlank {
        return {
            id: null,
            name: null,
            surname: null,
            patronymic: null,
            gender: null,
            birthDate: null,
            phoneNumber: null,
            representativePhoneNumber: null,
            isOnPaidStudy: false,
            snils: null,
            groupId: null,
            passportId: null,
            workplaceInfoId: null,
            additionalQualificationIds: [],
            isTargetAgreement: false,
            targetAgreementFile: null,
            armyStatus: null,
            armySubpoenaFile: null,
            armyServeDate: null,
            peculiarity: null,
        }
    }

    //TODO Проверить используются ли все Action-ы?

    export function reducer(state: StudentBlank, action: Action): StudentBlank {
        switch (action.type) {
            case "CHANGE_NAME":
                return { ...state, name: action.payload.name }

            case "CHANGE_SURNAME":
                return { ...state, surname: action.payload.surname }

            case "CHANGE_PATRONYMIC":
                return { ...state, patronymic: action.payload.patronymic }

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

            case "TOGGLE_IS_ON_PAID_STUDY":
                return { ...state, isOnPaidStudy: !state.isOnPaidStudy }

            case "CHANGE_SNILS":
                return { ...state, snils: action.payload.snils }

            case "CHANGE_GROUP":
                return { ...state, groupId: action.payload.groupId }

            case "CHANGE_PASSPORT":
                return { ...state, passportId: action.payload.passportId }

            case "CHANGE_WORKPLACE_INFO":
                return { ...state, workplaceInfoId: action.payload.workplaceInfoId }

            case "CHANGE_ADDITIONAL_QUALIFICATIONS":
                return {
                    ...state,
                    additionalQualificationIds: action.payload.additionalQualificationIds,
                }

            case "TOGGLE_IS_TARGET_AGREEMENT":
                return { ...state, isTargetAgreement: !state.isTargetAgreement }

            case "CHANGE_TARGET_AGREEMENT_FILE":
                return { ...state, targetAgreementFile: action.payload.targetAgreementFile }

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
          type: "CHANGE_SURNAME"
          payload: { surname: string }
      }
    | {
          type: "CHANGE_PATRONYMIC"
          payload: { patronymic: string }
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
          type: "TOGGLE_IS_ON_PAID_STUDY"
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
