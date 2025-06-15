export interface ReportSelectionOptions {
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

export namespace ReportSelectionOptions {
    export function getDefault(): ReportSelectionOptions {
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

export const reportSelectionLabels: Record<keyof ReportSelectionOptions, string> = {
    gender: "Пол",
    birthDate: "Дата рождения",
    phoneNumber: "Номер телефона",
    representativePhoneNumber: "Номер телефона представителя",
    representativeAlias: "Как обратиться к представителю",
    isOnPaidStudy: "Обучение на платной основе?",
    snils: "Снилс",
    groupNumber: "Номер группы",
    structuralUnit: "Структурное подразделение",
    educationLevelType: "Тип уровня образования",
    educationLevelName: "Название уровня образования",
    educationLevelCode: "Код уровня образования",
    enrollmentYear: "Год поступления",
    curator: "Куратор",
    clusterName: "Наименование кластера",
    passportNumber: "Номер паспорта",
    passportSeries: "Серия паспорта",
    passportIssuedBy: "Кем выдан паспорт",
    passportIssueDate: "Дата получения паспорта",
    targetAgreementNumber: "Номер договора о целевом обучении",
    targetAgreementEnterpriseName: "Название предприятия (целевое обучение)",
    targetAgreementDate: "Дата договора о целевом обучении",
    armyCallDate: "Дата призыва",
    socialStatuses: "Социальные статусы",
    status: "Статус",
    address: "Адрес",
    isForeignCitizen: "Иностранный гражданин?",
    inn: "Инн",
    email: "Почта",
}
