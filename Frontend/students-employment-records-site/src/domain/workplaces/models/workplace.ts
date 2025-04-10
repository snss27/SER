import { BlankFiles } from "@/tools/blankFiles"

export class Workplace {
    constructor(
        public readonly id: string,
        public readonly enterpriseId: string,
        public readonly post: string | null,
        public readonly workBookExtractFile: BlankFiles,
        public readonly startDate: Date | null,
        public readonly finishDate: Date | null
    ) {}
}
