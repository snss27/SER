export interface EnterpriseBlank {
    id: string | null
    name: string | null
    legalAddress: string | null
    actualAddress: string | null
    address: string | null
    INN: string | null
    KPP: string | null
    ORGN: string | null
    phone: string | null
    mail: string | null
    isOPK: boolean
}

export namespace EnterpriseBlank {
    export function empty(): EnterpriseBlank {
        return {
            id: null,
            name: null,
            legalAddress: null,
            actualAddress: null,
            address: null,
            INN: null,
            KPP: null,
            ORGN: null,
            phone: null,
            mail: null,
            isOPK: false,
        }
    }

    export function reducer(state: EnterpriseBlank, action: Action): EnterpriseBlank {
        switch (action.type) {
            case "CHANGE_NAME":
                return { ...state, name: action.payload.name }

            case "CHANGE_LEGAL_ADDRESS":
                return { ...state, legalAddress: action.payload.legalAddress }

            case "CHANGE_ACTUAL_ADDRESS":
                return { ...state, actualAddress: action.payload.actualAddress }

            case "CHANGE_ADDRESS":
                return { ...state, address: action.payload.address }

            case "CHANGE_INN":
                return { ...state, INN: action.payload.INN }

            case "CHANGE_KPP":
                return { ...state, KPP: action.payload.KPP }

            case "CHANGE_ORGN":
                return { ...state, ORGN: action.payload.ORGN }

            case "CHANGE_PHONE":
                return { ...state, phone: action.payload.phone }

            case "CHANGE_MAIL":
                return { ...state, mail: action.payload.mail }

            case "CHANGE_IS_OPK":
                return { ...state, isOPK: action.payload.isOPK }

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
          type: "CHANGE_LEGAL_ADDRESS"
          payload: { legalAddress: string | null }
      }
    | {
          type: "CHANGE_ACTUAL_ADDRESS"
          payload: { actualAddress: string | null }
      }
    | {
          type: "CHANGE_ADDRESS"
          payload: { address: string | null }
      }
    | {
          type: "CHANGE_INN"
          payload: { INN: string | null }
      }
    | {
          type: "CHANGE_KPP"
          payload: { KPP: string | null }
      }
    | {
          type: "CHANGE_ORGN"
          payload: { ORGN: string | null }
      }
    | {
          type: "CHANGE_PHONE"
          payload: { phone: string | null }
      }
    | {
          type: "CHANGE_MAIL"
          payload: { mail: string | null }
      }
    | {
          type: "CHANGE_IS_OPK"
          payload: { isOPK: boolean }
      }
