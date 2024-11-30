import { EducationLevelBlank } from "./educationLevelBlank"
import { EducationLevelTypes } from "@/domain/educationLevels/enums/EducationLevelTypes"

export class EducationLevel {
    constructor(
        public readonly id: string,
        public readonly type: EducationLevelTypes,
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

    public static fromAny(any: any): EducationLevel {
        return new EducationLevel(any.id, any.type, any.name, any.code, any.studyTime)
    }

    public toBlank(): EducationLevelBlank {
        return {
            id: this.id,
            type: this.type,
            name: this.name,
            code: this.code,
            studyTime: this.studyTime,
        }
    }
}
