interface ReportVisibilityOptions {
    gender: boolean
    birthDate: boolean
    phoneNumber: boolean
    representativePhoneNumber: boolean
    representativeAlias: boolean
    isOnPaidStudy: boolean
    snils: boolean
    groupNumber: boolean
    structuralUnit: boolean
    educationLevelType: boolean
    educationLevelName: boolean
    educationLevelCode: boolean
    enrollmentYear: boolean
    curator: boolean
    clusterName: boolean
    passportNumber: boolean
    passportSeries: boolean
    passportIssuedBy: boolean
    passportIssueDate: boolean
    targetAgreementNumber: boolean
    targetAgreementEnterpriseName: boolean
    targetAgreementDate: boolean
    armyCallDate: boolean
    socialStatuses: boolean
    status: boolean
    address: boolean
    isForeignCitizen: boolean
    inn: boolean
    email: boolean
}

export namespace ReportVisibilityOptions {
    export function getDefault(): ReportVisibilityOptions {
        return {
            gender: false,
            birthDate: false,
            phoneNumber: false,
            representativePhoneNumber: false,
            representativeAlias: false,
            isOnPaidStudy: false,
            snils: false,
            groupNumber: false,
            structuralUnit: false,
            educationLevelType: false,
            educationLevelName: false,
            educationLevelCode: false,
            enrollmentYear: false,
            curator: false,
            clusterName: false,
            passportNumber: false,
            passportSeries: false,
            passportIssuedBy: false,
            passportIssueDate: false,
            targetAgreementNumber: false,
            targetAgreementEnterpriseName: false,
            targetAgreementDate: false,
            armyCallDate: false,
            socialStatuses: false,
            status: false,
            address: false,
            isForeignCitizen: false,
            inn: false,
            email: false,
        }
    }
}
