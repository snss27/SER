export interface WorkPostBlank {
    id: string | null
    name: string | null
}

export namespace WorkPostBlank {
    export function empty(): WorkPostBlank {
        return {
            id: null,
            name: null,
        }
    }

    export function reducer(state: WorkPostBlank, action: Action): WorkPostBlank {
        switch (action.type) {
            case "CHANGE_NAME":
                return { ...state, name: action.payload.name }

            default:
                return { ...state }
        }
    }
}

type Action = {
    type: "CHANGE_NAME"
    payload: { name: string | null }
}
