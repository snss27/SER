import { NeverUnreachable } from "../neverUreachable"

export enum FormTypes {
    Add = 1,
    Edit = 2,
}

export namespace FormTypes {
    export function getSuccessSaveDisplay(type: FormTypes): string {
        switch (type) {
            case FormTypes.Add:
                return "сохранен"
            case FormTypes.Edit:
                return "изменен"
            default:
                throw new NeverUnreachable(type)
        }
    }
}
