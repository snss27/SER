export interface CuratorBlank {
    id: string | null
    name: string | null
    surname: string | null
    patronymic: string | null
}

export namespace CuratorBlank {
    export function empty(): CuratorBlank {
        return {
            id: null,
            name: null,
            surname: null,
            patronymic: null,
        }
    }

    export function reducer(state: CuratorBlank, action: Action): CuratorBlank {
        switch (action.type) {
            case "CHANGE_NAME":
                return { ...state, name: action.payload.name }

            case "CHANGE_SURNAME":
                return { ...state, surname: action.payload.surname }

            case "CHANGE_PATRONYMIC":
                return { ...state, patronymic: action.payload.patronymic }

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
          type: "CHANGE_SURNAME"
          payload: { surname: string | null }
      }
    | {
          type: "CHANGE_PATRONYMIC"
          payload: { patronymic: string | null }
      }
