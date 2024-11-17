import { conjugate } from "@/tools/conjugate"
import { AdditionalQualificationBlank } from "./additionalQualificationBlank"

class AdditionalQualification {
    constructor(
        public readonly id: string,
        public readonly name: string,
        public readonly code: string,
        public readonly studyYears: number,
        public readonly studyMonths: number
    ) {}

    public get displayName() {
        console.log(this)
        return `${this.name} (${this.code})`
    }

    public get studyPeriodString() {
        return this.studyYearsString + " " + this.studyMonthsString
    }

    private get studyYearsString() {
        return conjugate(this.studyYears, "год", "года", "лет")
    }

    private get studyMonthsString() {
        return conjugate(this.studyMonths, "месяц", "месяца", "месяцев")
    }

    public static fromAny(any: any): AdditionalQualification {
        return new AdditionalQualification(
            any.id,
            any.name,
            any.code,
            any.studyYears,
            any.studyMonths
        )
    }

    public toBlank(): AdditionalQualificationBlank {
        return {
            id: this.id,
            name: this.name,
            code: this.code,
            studyYears: this.studyYears,
            studyMonths: this.studyMonths,
        }
    }
}

export default AdditionalQualification
