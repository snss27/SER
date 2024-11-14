export interface SpecialityBlank {
    id: string | null
    name: string | null
    studyYears: number | null
    studyMonths: number | null
}

export namespace SpecialityBlank {
    export function empty(): SpecialityBlank {
        return {
            id: null,
            name: null,
            studyYears: null,
            studyMonths: null,
        }
    }

    export function reducer(state: SpecialityBlank, action: Action): SpecialityBlank {
        switch (action.type) {
            case "CHANGE_NAME":
                return { ...state, name: action.payload.name }

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
    | {
          type: "CHANGE_STUDY_YEARS"
          payload: { studyYears: number | null }
      }
    | {
          type: "CHANGE_STUDY_MONTHS"
          payload: { studyMonths: number | null }
      }
