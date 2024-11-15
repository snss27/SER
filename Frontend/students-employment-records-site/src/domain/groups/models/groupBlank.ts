import Curator from "@/domain/curators/models/curator"
import Speciality from "@/domain/specialities/models/speciality"
import { StructuralUnits } from "../enums/structuralUnits"

export interface GroupBlank {
    id: string | null
    number: string | null
    structuralUnit: StructuralUnits | null
    speciality: Speciality | null
    enrollmentYear: number | null
    curator: Curator | null
}

export namespace GroupBlank {
    export function empty(): GroupBlank {
        return {
            id: null,
            number: null,
            structuralUnit: null,
            speciality: null,
            enrollmentYear: null,
            curator: null,
        }
    }

    export function reducer(state: GroupBlank, action: Action): GroupBlank {
        switch (action.type) {
            case "CHANGE_NUMBER":
                return { ...state, number: action.payload.number }

            case "CHANGE_STRUCTURAL_UNIT":
                return { ...state, structuralUnit: action.payload.structuralUnit }

            case "CHANGE_SPECIALITY":
                return { ...state, speciality: action.payload.speciality }

            case "CHANGE_ENROLLMENT_YEAR":
                return { ...state, enrollmentYear: action.payload.enrollmentYear }

            case "CHANGE_CURATOR":
                return { ...state, curator: action.payload.curator }

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
          type: "CHANGE_SPECIALITY"
          payload: { speciality: Speciality | null }
      }
    | {
          type: "CHANGE_ENROLLMENT_YEAR"
          payload: { enrollmentYear: number | null }
      }
    | {
          type: "CHANGE_CURATOR"
          payload: { curator: Curator | null }
      }
