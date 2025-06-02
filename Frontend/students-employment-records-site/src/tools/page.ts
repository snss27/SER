export class Page<T> {
    constructor(
        public values: T[],
        public totalRows: number
    ) {}

    public static create<T>(values: T[], totalRows: number): Page<T> {
        return new Page(values, totalRows)
    }

    public static empty<T>(): Page<T> {
        return new Page<T>([], 0)
    }

    public static fromAny<T>(data: any, converter: (data: any) => T): Page<T> {
        const values: T[] = (data.values as any[]).map(converter)
        return Page.create(values, data.totalRows)
    }
}
