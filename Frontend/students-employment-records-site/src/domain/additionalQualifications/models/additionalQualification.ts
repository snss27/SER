import { AdditionalQualificationBlank } from "./additionalQualificationBlank"

export class AdditionalQualification {
    constructor(
        public readonly id: string,
        public readonly name: string,
        public readonly code: string,
        public readonly studyTime: string | null
    ) {}

    public get displayName() {
        return `${this.name} (${this.code})`
    }

    public get displayTime() {
        if (this.studyTime === null || this.studyTime.trim() === "") return "Не указан"

        return this.studyTime
    }

    public static fromAny(any: any): AdditionalQualification {
        return new AdditionalQualification(any.id, any.name, any.code, any.studyTime)
    }

    public toBlank(): AdditionalQualificationBlank {
        return {
            id: this.id,
            name: this.name,
            code: this.code,
            studyTime: this.studyTime,
        }
    }
}
