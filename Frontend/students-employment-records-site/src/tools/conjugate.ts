/**
 * Возвращает слово в нужном падеже в зависимости от числа
 *
 * @param value - число
 * @param nominative - Именительный падеж. Например "День"
 * @param genitive - Родительный падеж. Например "Дня"
 * @param plural - Множественное число. Например "Дней"
 *
 */
export const conjugate = (
    value: number,
    nominative: string,
    genitive: string,
    plural: string,
    isPrintValue: boolean = true
): string => {
    let calculatingValue = value % 100

    if (calculatingValue >= 11 && calculatingValue <= 19)
        return isPrintValue ? `${value} ${plural}` : plural

    calculatingValue = calculatingValue % 10

    if (isPrintValue) {
        switch (calculatingValue) {
            case 1:
                return `${value} ${nominative}`
            case 2:
            case 3:
            case 4:
                return `${value} ${genitive}`
            default:
                return `${value} ${plural}`
        }
    }

    switch (calculatingValue) {
        case 1:
            return nominative
        case 2:
        case 3:
        case 4:
            return genitive
        default:
            return plural
    }
}
