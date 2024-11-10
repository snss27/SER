import { SpecialityBlank } from "./specialityBlank"

class Speciality {
    constructor(
        public readonly id: string,
        public readonly name: string,
        public readonly studyYears: number
    ) {}

    public static fromAny(any: any): Speciality {
        return new Speciality(any.id, any.name, any.studyYears)
    }

    public toBlank(): SpecialityBlank {
        return {
            id: this.id,
            name: this.name,
            studyYears: this.studyYears,
        }
    }
}

export default Speciality
