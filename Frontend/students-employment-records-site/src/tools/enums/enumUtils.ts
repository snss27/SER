export const enumToArrayNumber = <T>(enumObj: any): T[] => {
    const enumValues: T[] = []

    for (const n in enumObj) {
        if (typeof enumObj[n] === "number") {
            enumValues.push(<any>enumObj[n])
        }
    }

    return enumValues
}
