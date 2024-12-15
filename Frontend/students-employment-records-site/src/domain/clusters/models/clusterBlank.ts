export interface ClusterBlank {
    id: string | null
    name: string | null
}

export namespace ClusterBlank {
    export function empty(): ClusterBlank {
        return {
            id: null,
            name: null,
        }
    }

    export function reducer(state: ClusterBlank, action: Action): ClusterBlank {
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
