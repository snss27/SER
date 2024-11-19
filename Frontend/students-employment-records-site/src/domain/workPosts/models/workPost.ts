import { WorkPostBlank } from "./workPostBlank"

class WorkPost {
    constructor(public readonly id: string, public readonly name: string) {}

    public static fromAny(any: any): WorkPost {
        return new WorkPost(any.id, any.name)
    }

    public toBlank(): WorkPostBlank {
        return {
            id: this.id,
            name: this.name,
        }
    }
}

export default WorkPost
