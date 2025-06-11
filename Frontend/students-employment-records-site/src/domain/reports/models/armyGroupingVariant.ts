export type ArmyGroupingVariant =
    | { mustServe: true; armyCallDatePeriod: [Date | null, Date | null] }
    | { mustServe: false }
