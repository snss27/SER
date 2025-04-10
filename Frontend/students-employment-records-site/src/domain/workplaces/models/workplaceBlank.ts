import { Enterprise } from "@/domain/enterprises/models/enterprise"
import { BlankFiles } from "@/tools/blankFiles"
export interface WorkplaceBlank {
    id: string | null
    enterprise: Enterprise | null
    post: string | null
    workbookExtractFile: BlankFiles
    startDate: Date | null
    finishDate: Date | null

    clientId: string
}

export namespace WorkplaceBlank {
    export function empty(): WorkplaceBlank {
        return {
            id: null,
            enterprise: null,
            post: null,
            workbookExtractFile: BlankFiles.create(1),
            startDate: null,
            finishDate: null,

            clientId: crypto.randomUUID(),
        }
    }

    export function reducer(state: WorkplaceBlank, action: Action): WorkplaceBlank {
        switch (action.type) {
            case "CHANGE_ENTERPRISE":
                return { ...state, enterprise: action.payload.enterprise }

            case "CHANGE_POST":
                return { ...state, post: action.payload.post }

            case "CHANGE_WORKBOOK_EXTRACT_FILE":
                return { ...state, workbookExtractFile: action.payload.workbookExtractFile }

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
          type: "CHANGE_WORKBOOK_EXTRACT_FILE"
          payload: { workbookExtractFile: BlankFiles }
      }
    | {
          type: "CHANGE_START_DATE"
          payload: { startDate: Date | null }
      }
    | {
          type: "CHANGE_FINISH_DATE"
          payload: { finishDate: Date | null }
      }
