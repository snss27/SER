export interface AdditionalQualificationBlank {
    id: string | null
    name: string | null
    code: string | null
    studyYears: number | null
    studyMonths: number | null
}

export namespace AdditionalQualificationBlank {
    export function empty(): AdditionalQualificationBlank {
        return {
            id: null,
            name: null,
            code: null,
            studyYears: null,
            studyMonths: null,
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

            case "CHANGE_STUDY_YEARS":
                return { ...state, studyYears: action.payload.studyYears }

            case "CHANGE_STUDY_MONTHS":
                return { ...state, studyMonths: action.payload.studyMonths }

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
          type: "CHANGE_STUDY_YEARS"
          payload: { studyYears: number | null }
      }
    | {
          type: "CHANGE_STUDY_MONTHS"
          payload: { studyMonths: number | null }
      }
