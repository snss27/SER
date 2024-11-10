export class Result {
    constructor(public errors: Error[]) {}

    public isSuccess = this.errors.length === 0

    public get getError() {
        return this.errors[0].message
    }

    public static success<T>(): Result {
        return new Result([])
    }

    public static fail(errors: Error[]): Result {
        return new Result(errors)
    }

    public static fromAny(result: any): Result {
        return new Result(Error.fromAny(result.errors))
    }
}

class Error {
    constructor(public key: string | null, public message: string) {}

    public static fromAny(errors: any[]): Error[] {
        return errors.map((error) => new Error(error.key ?? null, error.message))
    }
}
