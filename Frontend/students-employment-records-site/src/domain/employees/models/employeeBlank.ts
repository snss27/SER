export interface EmployeeBlank {
    id: string | null
    name: string | null
    secondName: string | null
    lastName: string | null
}

export namespace EmployeeBlank {
    export function empty(): EmployeeBlank {
        return {
            id: null,
            name: null,
            secondName: null,
            lastName: null,
        }
    }

    export function reducer(state: EmployeeBlank, action: Action): EmployeeBlank {
        switch (action.type) {
            case "CHANGE_NAME":
                return { ...state, name: action.payload.name }

            case "CHANGE_SECOND_NAME":
                return { ...state, secondName: action.payload.secondName }

            case "CHANGE_LAST_NAME":
                return { ...state, lastName: action.payload.lastName }

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
          type: "CHANGE_SECOND_NAME"
          payload: { secondName: string | null }
      }
    | {
          type: "CHANGE_LAST_NAME"
          payload: { lastName: string | null }
      }
