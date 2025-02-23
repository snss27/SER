import { enumToArrayNumber } from "@/tools/enums/enumUtils"
import { NeverUnreachable } from "@/tools/neverUreachable"

export enum StudentStatus {
    Active = 1,
    Expelled = 2,
    Finished = 3,
}

export namespace StudentStatus {
    export function getAll(): StudentStatus[] {
        return enumToArrayNumber<StudentStatus>(StudentStatus)
    }

    export function getDisplayText(status: StudentStatus): string {
        switch (status) {
            case StudentStatus.Active:
                return "Обучается"
            case StudentStatus.Expelled:
                return "Отчислен"
            case StudentStatus.Finished:
                return "Отчислен, в связи с окончанием обучения"
            default:
                throw new NeverUnreachable(status)
        }
    }
}
