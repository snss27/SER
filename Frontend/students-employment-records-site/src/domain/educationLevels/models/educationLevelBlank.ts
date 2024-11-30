import { EducationLevelTypes } from "@/domain/educationLevels/enums/EducationLevelTypes"

export interface EducationLevelBlank {
    id: string | null
    type: EducationLevelTypes | null
    name: string | null
    code: string | null
    studyTime: string | null
}

export namespace EducationLevelBlank {
    export function empty(): EducationLevelBlank {
        return {
            id: null,
            type: null,
            name: null,
            code: null,
            studyTime: null,
        }
    }

    export function reducer(state: EducationLevelBlank, action: Action): EducationLevelBlank {
        switch (action.type) {
            case "CHANGE_TYPE":
                return { ...state, type: action.payload.type }

            case "CHANGE_NAME":
                return { ...state, name: action.payload.name }

            case "CHANGE_CODE":
                return { ...state, code: action.payload.code }

            case "CHANGE_STUDY_TIME":
                return { ...state, studyTime: action.payload.studyTime }

            default:
                return { ...state }
        }
    }
}

type Action =
    | {
          type: "CHANGE_TYPE"
          payload: { type: EducationLevelTypes | null }
      }
    | {
          type: "CHANGE_NAME"
          payload: { name: string | null }
      }
    | { type: "CHANGE_CODE"; payload: { code: string | null } }
    | {
          type: "CHANGE_STUDY_TIME"
          payload: { studyTime: string | null }
      }
