export class NeverUnreachable extends Error {
    constructor(value: never) {
        super(`Unreachable statement: ${value}`)
    }
}
