import { BlankFiles } from "@/tools/blankFiles"

export interface WorkplaceBlank {
    id: string | null
    enterpriseId: string | null
    post: string | null
    workbookExtractFile: BlankFiles
    startDate: Date | null
    finishDate: Date | null
}

export namespace WorkplaceBlank {
    export function empty(): WorkplaceBlank {
        return {
            id: null,
            enterpriseId: null,
            post: null,
            workbookExtractFile: BlankFiles.create(1),
            startDate: null,
            finishDate: null,
        }
    }

    export function create(id: string | null): WorkplaceBlank {
        return {
            ...empty(),
            id,
        }
    }

    export function reducer(state: WorkplaceBlank, action: Action): WorkplaceBlank {
        switch (action.type) {
            case "CHANGE_ENTERPRISE_ID":
                return { ...state, enterpriseId: action.payload.enterpriseId }

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
          type: "CHANGE_ENTERPRISE_ID"
          payload: { enterpriseId: string | null }
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
