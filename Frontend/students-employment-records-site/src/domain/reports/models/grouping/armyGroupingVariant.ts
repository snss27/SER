export type ArmyGroupingOptions =
    | { mustServe: true; armyCallDatePeriod: [Date | null, Date | null] }
    | { mustServe: false }
