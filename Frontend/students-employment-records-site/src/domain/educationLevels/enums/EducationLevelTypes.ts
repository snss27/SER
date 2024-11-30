import { enumToArrayNumber } from "@/tools/enums/enumUtils"
import { NeverUnreachable } from "@/tools/neverUreachable"

export enum EducationLevelTypes {
    Speciality = 1,
    Profession = 2,
    ProfessionalEducation = 3,
}

export namespace EducationLevelTypes {
    export function getAll() {
        return enumToArrayNumber<EducationLevelTypes>(EducationLevelTypes)
    }

    export function displayName(type: EducationLevelTypes) {
        switch (type) {
            case EducationLevelTypes.Speciality:
                return "Специальность"
            case EducationLevelTypes.Profession:
                return "Профессия"
            case EducationLevelTypes.ProfessionalEducation:
                return "Профессиональное обучение"
            default:
                throw new NeverUnreachable(type)
        }
    }
}
