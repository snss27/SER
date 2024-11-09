export interface SpecialityBlank {
    id: null
    name: string | null
    studyYears: number | null
}

export namespace SpecialityBlank {
    export function empty(): SpecialityBlank {
        return {
            id: null,
            name: null,
            studyYears: null,
        }
    }

    export function reducer(state: SpecialityBlank, action: Action): SpecialityBlank {
        switch (action.type) {
            case "CHANGE_NAME":
                return { ...state, name: action.payload.name }

            case "CHANGE_STUDY_YEARS":
                return { ...state, studyYears: action.payload.studyYears }

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
