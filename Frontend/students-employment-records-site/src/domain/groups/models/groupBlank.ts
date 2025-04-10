import { Cluster } from "@/domain/clusters/models/cluster"
import { EducationLevel } from "@/domain/educationLevels/models/educationLevel"
import { Employee } from "@/domain/employees/models/employee"
import { StructuralUnits } from "../enums/structuralUnits"

export interface GroupBlank {
    id: string | null
    number: string | null
    structuralUnit: StructuralUnits | null
    educationLevel: EducationLevel | null
    enrollmentYear: number | null
    curator: Employee | null
    hasCluster: boolean
    cluster: Cluster | null
}

export namespace GroupBlank {
    export function empty(): GroupBlank {
        return {
            id: null,
            number: null,
            structuralUnit: null,
            educationLevel: null,
            enrollmentYear: null,
            curator: null,
            hasCluster: false,
            cluster: null,
        }
    }

    export function reducer(state: GroupBlank, action: Action): GroupBlank {
        switch (action.type) {
            case "CHANGE_NUMBER":
                return { ...state, number: action.payload.number }

            case "CHANGE_STRUCTURAL_UNIT":
                return { ...state, structuralUnit: action.payload.structuralUnit }

            case "CHANGE_EDUCATION_LEVEL":
                return { ...state, educationLevel: action.payload.educationLevel }

            case "CHANGE_ENROLLMENT_YEAR":
                return { ...state, enrollmentYear: action.payload.enrollmentYear }

            case "CHANGE_CURATOR":
                return { ...state, curator: action.payload.curator }

            case "CHANGE_HAS_CLUSTER":
                return { ...state, hasCluster: action.payload.hasCluster }

            case "CHANGE_CLUSTER":
                return { ...state, cluster: action.payload.cluster }

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
          type: "CHANGE_EDUCATION_LEVEL"
          payload: { educationLevel: EducationLevel | null }
      }
    | {
          type: "CHANGE_ENROLLMENT_YEAR"
          payload: { enrollmentYear: number | null }
      }
    | {
          type: "CHANGE_CURATOR"
          payload: { curator: Employee | null }
      }
    | {
          type: "CHANGE_HAS_CLUSTER"
          payload: { hasCluster: boolean }
      }
    | {
          type: "CHANGE_CLUSTER"
          payload: { cluster: Cluster | null }
      }
