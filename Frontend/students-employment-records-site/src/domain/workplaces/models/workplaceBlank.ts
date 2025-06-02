import { Enterprise } from "@/domain/enterprises/models/enterprise"
import { Workplace } from "./workplace"
export interface WorkplaceBlank {
    id: string | null
    enterprise: Enterprise | null
    post: string | null
    workbookExtractFiles: string[]
    startDate: Date | null
    finishDate: Date | null
    isCurrent: boolean

    clientId: string
}

export namespace WorkplaceBlank {
    export function empty(isCurrent: boolean): WorkplaceBlank {
        return {
            id: null,
            enterprise: null,
            post: null,
            workbookExtractFiles: [],
            startDate: null,
            finishDate: null,
            isCurrent,

            clientId: crypto.randomUUID(),
        }
    }

    export function create(workPlace: Workplace, isCurrent: boolean): WorkplaceBlank {
        return {
            id: workPlace.id,
            enterprise: workPlace.enterprise,
            post: workPlace.post,
            workbookExtractFiles: workPlace.workBookExtractFiles,
            startDate: workPlace.startDate,
            finishDate: workPlace.finishDate,
            isCurrent,

            clientId: crypto.randomUUID(),
        }
    }

    export function reducer(state: WorkplaceBlank, action: Action): WorkplaceBlank {
        switch (action.type) {
            case "CHANGE_ENTERPRISE":
                return { ...state, enterprise: action.payload.enterprise }

            case "CHANGE_POST":
                return { ...state, post: action.payload.post }

            case "CHANGE_WORKBOOK_EXTRACT_FILES":
                return { ...state, workbookExtractFiles: action.payload.workbookExtractFiles }

            case "CHANGE_START_DATE":
                return { ...state, startDate: action.payload.startDate }

            case "CHANGE_FINISH_DATE":
                return { ...state, finishDate: action.payload.finishDate }

            default:
                return { ...state }
        }
    }
}

type Action =
    | {
          type: "CHANGE_ENTERPRISE"
          payload: { enterprise: Enterprise | null }
      }
    | {
          type: "CHANGE_POST"
          payload: { post: string | null }
      }
    | {
          type: "CHANGE_WORKBOOK_EXTRACT_FILES"
          payload: { workbookExtractFiles: string[] }
      }
    | {
          type: "CHANGE_START_DATE"
          payload: { startDate: Date | null }
      }
    | {
          type: "CHANGE_FINISH_DATE"
          payload: { finishDate: Date | null }
      }
