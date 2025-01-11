import { StructuralUnits } from "../enums/structuralUnits"

export interface GroupBlank {
    id: string | null
    number: string | null
    structuralUnit: StructuralUnits | null
    educationLevelId: string | null
    enrollmentYear: number | null
    curatorId: string | null
    hasCluster: boolean
    clusterId: string | null
}

export namespace GroupBlank {
    export function empty(): GroupBlank {
        return {
            id: null,
            number: null,
            structuralUnit: null,
            educationLevelId: null,
            enrollmentYear: null,
            curatorId: null,
            hasCluster: false,
            clusterId: null,
        }
    }

    export function reducer(state: GroupBlank, action: Action): GroupBlank {
        switch (action.type) {
            case "CHANGE_NUMBER":
                return { ...state, number: action.payload.number }

            case "CHANGE_STRUCTURAL_UNIT":
                return { ...state, structuralUnit: action.payload.structuralUnit }

            case "CHANGE_EDUCATION_LEVEL_ID":
                return { ...state, educationLevelId: action.payload.educationLevelId }

            case "CHANGE_ENROLLMENT_YEAR":
                return { ...state, enrollmentYear: action.payload.enrollmentYear }

            case "CHANGE_CURATOR_ID":
                return { ...state, curatorId: action.payload.curatorId }

            case "CHANGE_HAS_CLUSTER":
                return { ...state, hasCluster: action.payload.hasCluster }

            case "CHANGE_CLUSTER_ID":
                return { ...state, clusterId: action.payload.clusterId }

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
          type: "CHANGE_EDUCATION_LEVEL_ID"
          payload: { educationLevelId: string | null }
      }
    | {
          type: "CHANGE_ENROLLMENT_YEAR"
          payload: { enrollmentYear: number | null }
      }
    | {
          type: "CHANGE_CURATOR_ID"
          payload: { curatorId: string | null }
      }
    | {
          type: "CHANGE_HAS_CLUSTER"
          payload: { hasCluster: boolean }
      }
    | {
          type: "CHANGE_CLUSTER_ID"
          payload: { clusterId: string | null }
      }
