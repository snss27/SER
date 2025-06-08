export interface AuthRequest {
    login: string
    password: string
}

export namespace AuthRequest {
    export function empty(): AuthRequest {
        return {
            login: "",
            password: "",
        }
    }

    export function reducer(state: AuthRequest, action: Action): AuthRequest {
        switch (action.type) {
            case "CHANGE_PASSWORD":
                return { ...state, password: action.payload.password }
            case "CHANGE_LOGIN":
                return { ...state, login: action.payload.login }
            case "RESET":
                empty()

            default:
                return { ...state }
        }
    }
}

type Action =
    | {
          type: "CHANGE_PASSWORD"
          payload: { password: string }
      }
    | {
          type: "CHANGE_LOGIN"
          payload: { login: string }
      }
    | {
          type: "RESET"
      }
