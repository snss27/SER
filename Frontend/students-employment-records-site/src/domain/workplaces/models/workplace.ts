import { Enterprise } from "@/domain/enterprises/models/enterprise"
import { WorkplaceBlank } from "./workplaceBlank"

export class Workplace {
    constructor(
        public readonly id: string,
        public readonly enterprise: Enterprise,
        public readonly post: string | null,
        public readonly workBookExtractFiles: string[],
        public readonly startDate: Date | null,
        public readonly finishDate: Date | null
    ) {}

    public toBlank(isCurrent: boolean): WorkplaceBlank {
        return WorkplaceBlank.create(this, isCurrent)
    }

    public static fromAny(any: any): Workplace {
        const enterprise = Enterprise.fromAny(any.enterprise)
        const startDate = any.startDate ? new Date(any.startDate) : null
        const finishDate = any.finishDate ? new Date(any.finishDate) : null

        return new Workplace(
            any.id,
            enterprise,
            any.post,
            any.workBookExtractFiles,
            startDate,
            finishDate
        )
    }
}
