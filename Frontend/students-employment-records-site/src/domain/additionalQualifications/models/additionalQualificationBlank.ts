export interface AdditionalQualificationBlank {
    id: string | null
    name: string | null
    code: string | null
    studyTime: string | null
}

export namespace AdditionalQualificationBlank {
    export function empty(): AdditionalQualificationBlank {
        return {
            id: null,
            name: null,
            code: null,
            studyTime: null,
        }
    }

    export function reducer(
        state: AdditionalQualificationBlank,
        action: Action
    ): AdditionalQualificationBlank {
        switch (action.type) {
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
          type: "CHANGE_NAME"
          payload: { name: string | null }
      }
    | { type: "CHANGE_CODE"; payload: { code: string | null } }
    | {
          type: "CHANGE_STUDY_TIME"
          payload: { studyTime: string | null }
      }
