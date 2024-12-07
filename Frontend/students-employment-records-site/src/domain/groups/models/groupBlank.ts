import { StructuralUnits } from "../enums/structuralUnits"

export interface GroupBlank {
    id: string | null
    number: string | null
    structuralUnit: StructuralUnits | null
    specialityId: string | null
    enrollmentYear: number | null
    curatorId: string | null
}

export namespace GroupBlank {
    export function empty(): GroupBlank {
        return {
            id: null,
            number: null,
            structuralUnit: null,
            specialityId: null,
            enrollmentYear: null,
            curatorId: null,
        }
    }

    export function reducer(state: GroupBlank, action: Action): GroupBlank {
        switch (action.type) {
            case "CHANGE_NUMBER":
                return { ...state, number: action.payload.number }

            case "CHANGE_STRUCTURAL_UNIT":
                return { ...state, structuralUnit: action.payload.structuralUnit }

            case "CHANGE_SPECIALITY_ID":
                return { ...state, specialityId: action.payload.specialityId }

            case "CHANGE_ENROLLMENT_YEAR":
                return { ...state, enrollmentYear: action.payload.enrollmentYear }

            case "CHANGE_CURATOR_ID":
                return { ...state, curatorId: action.payload.curatorId }

            default:
                return { ...state }
        }
    }
}

type Action =
    | {
          type: "CHANGE_NUMBER"
          payload: { number: string | null }
      }
    | {
          type: "CHANGE_STRUCTURAL_UNIT"
          payload: { structuralUnit: StructuralUnits | null }
      }
    | {
          type: "CHANGE_SPECIALITY_ID"
          payload: { specialityId: string | null }
      }
    | {
          type: "CHANGE_ENROLLMENT_YEAR"
          payload: { enrollmentYear: number | null }
      }
    | {
          type: "CHANGE_CURATOR_ID"
          payload: { curatorId: string | null }
      }
