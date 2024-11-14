import { CuratorBlank } from "./curatorBlank"

class Curator {
    constructor(
        public readonly id: string,
        public readonly name: string,
        public readonly surname: string | null,
        public readonly patronymic: string | null
    ) {}

    public get formattedFullName() {
        return `${this.surname ?? ""} ${this.name} ${this.patronymic ?? ""}`.trim()
    }

    public static fromAny(any: any): Curator {
        return new Curator(any.id, any.name, any.surname, any.patronymic)
    }

    public toBlank(): CuratorBlank {
        return {
            id: this.id,
            name: this.name,
            surname: this.surname,
            patronymic: this.patronymic,
        }
    }
}

export default Curator
