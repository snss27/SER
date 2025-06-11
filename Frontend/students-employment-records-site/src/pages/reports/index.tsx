import { ArmyGroupingSection } from "@/components/reports/armyGroupingSection"
import { GroupGroupingSection } from "@/components/reports/groupGroupingSection"
import Switch from "@/components/shared/buttons/switch"
import { AsyncAutocomplete } from "@/components/shared/inputs/asyncAutocomplete"
import { DateRangePicker } from "@/components/shared/inputs/dateRangePicker"
import { Select } from "@/components/shared/inputs/select"
import StepperComponent from "@/components/shared/layouts/stepper"
import { AdditionalQualificationsProvider } from "@/domain/additionalQualifications/additionalQualificationsProvider"
import { EnterprisesProvider } from "@/domain/enterprises/enterprisesProvider"
import { WorkPlaceGroupingVariant } from "@/domain/reports/enums/workPlaceGroupingVariant"
import { ReportGroupingOptions } from "@/domain/reports/models/reportGroupingOptions"
import { ReportVisibilityOptions } from "@/domain/reports/models/reportVisibilityOptions"
import { ForeignCitizenVariant } from "@/domain/students/enums/foreignCitizenVariant"
import { Gender } from "@/domain/students/enums/gender"
import { OnPaidStudyVariant } from "@/domain/students/enums/onPaidStudyVariant"
import { SocialStatus } from "@/domain/students/enums/socialStatus"
import { StudentStatus } from "@/domain/students/enums/studentStatus"
import { Box } from "@mui/material"
import { useReducer, useState } from "react"

const ReportsPage = () => {
    const [groupingOptions, dispatch] = useReducer(
        ReportGroupingOptions.reducer,
        ReportGroupingOptions.empty()
    )

    const [visibilityOptions, setVisibilityOptions] = useState(ReportVisibilityOptions.getDefault)

    const stepContent = [
        <Box
            key="grouping"
            sx={{ p: 2, flex: 1, display: "flex", flexDirection: "column", gap: 3 }}>
            <GroupGroupingSection
                groupingOptions={groupingOptions.groupGroupingOptions}
                dispatch={dispatch}
            />

            <Box sx={{ display: "flex", gap: 2 }}>
                <Box sx={{ flex: 1 }}>
                    <Select
                        label="Пол"
                        value={groupingOptions.gender}
                        options={Gender.getAll()}
                        getOptionLabel={Gender.getDisplayText}
                        onChange={(gender) =>
                            dispatch({ type: "CHANGE_GENDER", payload: { gender } })
                        }
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <DateRangePicker
                        label="Дата рождения"
                        value={groupingOptions.birthDatePeriod}
                        onChange={(birthDatePeriod) =>
                            dispatch({
                                type: "CHANGE_BIRTH_DATE_PERIOD",
                                payload: { birthDatePeriod },
                            })
                        }
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Select
                        required
                        label="Основа обучения"
                        value={groupingOptions.onPaidStudyVariant}
                        options={OnPaidStudyVariant.getAll()}
                        getOptionLabel={OnPaidStudyVariant.getDisplayText}
                        onChange={(onPaidStudyVariant) =>
                            dispatch({
                                type: "CHANGE_ON_PAID_STUDY_VARIANT",
                                payload: { onPaidStudyVariant },
                            })
                        }
                    />
                </Box>
            </Box>

            <Box sx={{ flex: 1 }}>
                <Switch
                    label="Использовать строгое вхождение для дополнительных квалификаций?"
                    value={groupingOptions.isUseStrictMatchForAdditionalQualifications}
                    onChange={(isChecked) =>
                        dispatch({
                            type: "CHANGE_IS_USE_STRICT_MATH_FOR_ADDITIONAL_QUALIFICATIONS",
                            payload: {
                                isUseStrictMatchForAdditionalQualifications: isChecked,
                            },
                        })
                    }
                />
                <AsyncAutocomplete
                    multiple
                    label="Дополнительные квалификации"
                    value={groupingOptions.additionalQualifications}
                    loadOptions={AdditionalQualificationsProvider.getBySearchText}
                    getOptionLabel={(aq) => aq.displayName}
                    onChange={(additionalQualifIcations) =>
                        dispatch({
                            type: "CHANGE_ADDITIONAL_QUALIFICATIONS",
                            payload: { additionalQualifIcations },
                        })
                    }
                />
            </Box>

            <Box sx={{ flex: 1 }}>
                <Switch
                    label="Использовать строгое вхождение социальных статусов?"
                    value={groupingOptions.isUseStrictMatchForSocialStatuses}
                    onChange={(isChecked) =>
                        dispatch({
                            type: "CHANGE_IS_USE_STRICT_METH_FOR_SOCIAL_STATUSES",
                            payload: {
                                isUseStrictMatchForSocialStatuses: isChecked,
                            },
                        })
                    }
                />
                <Select
                    multiple
                    label="Социальные статусы"
                    value={groupingOptions.socialStatuses}
                    options={SocialStatus.getAll()}
                    getOptionLabel={SocialStatus.getDisplayText}
                    onChange={(socialStatuses) =>
                        dispatch({
                            type: "CHANGE_SOCIAL_STATUSES",
                            payload: { socialStatuses },
                        })
                    }
                />
            </Box>

            <Box sx={{ display: "flex", gap: 2 }}>
                <Box sx={{ flex: 1 }}>
                    <AsyncAutocomplete
                        multiple
                        label="Предприятия, связанные с местами работы"
                        value={groupingOptions.workPlaceEnterprises}
                        getOptionLabel={(e) => e.name}
                        loadOptions={EnterprisesProvider.getBySearchText}
                        onChange={(workPlaceEnterprises) =>
                            dispatch({
                                type: "CHANGE_WORK_PLACE_ENTERPRISES",
                                payload: { workPlaceEnterprises },
                            })
                        }
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Select
                        label="Места работы"
                        required
                        value={groupingOptions.workPlaceGroupingVariant}
                        getOptionLabel={WorkPlaceGroupingVariant.getDisplayName}
                        onChange={(workPlaceGroupingVariant) =>
                            dispatch({
                                type: "CHANGE_WORK_PLACE_GROUPING_VARIANT",
                                payload: { workPlaceGroupingVariant },
                            })
                        }
                        options={WorkPlaceGroupingVariant.getAll()}
                    />
                </Box>
            </Box>

            <Box sx={{ display: "flex", gap: 2 }}>
                <Box sx={{ flex: 1 }}>
                    <AsyncAutocomplete
                        multiple
                        value={groupingOptions.targetAgreementEnterprises}
                        loadOptions={EnterprisesProvider.getBySearchText}
                        getOptionLabel={(e) => e.name}
                        label="Предприятия, с которым заключены договоры о целевом обучении"
                        onChange={(targetAgreementEnterprises) =>
                            dispatch({
                                type: "CHANGE_TARGET_AGREEMENT_ENTERPRISES",
                                payload: { targetAgreementEnterprises },
                            })
                        }
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Select
                        multiple
                        label="Статусы"
                        options={StudentStatus.getAll()}
                        value={groupingOptions.statuses}
                        getOptionLabel={StudentStatus.getDisplayText}
                        onChange={(statuses) =>
                            dispatch({ type: "CHANGE_STATUSES", payload: { statuses } })
                        }
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Select
                        required
                        label="Граждане"
                        options={ForeignCitizenVariant.getAll()}
                        value={groupingOptions.foreignCitizenVariant}
                        getOptionLabel={ForeignCitizenVariant.getDisplayText}
                        onChange={(foreignCitizenVariant) =>
                            dispatch({
                                type: "CHANGE_FOREIGN_CITIZEN_VARIAN",
                                payload: { foreignCitizenVariant },
                            })
                        }
                    />
                </Box>
            </Box>

            <ArmyGroupingSection
                armyGroupingVariant={groupingOptions.armyGroupingVariant}
                dispatch={dispatch}
            />
        </Box>,
        <Box
            key="selection"
            sx={{ p: 2, flex: 1, display: "flex", flexDirection: "column", gap: 3 }}>
            <Box sx={{ display: "flex", gap: 2 }}>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={visibilityOptions.gender}
                        label="Пол"
                        onChange={() =>
                            setVisibilityOptions({
                                ...visibilityOptions,
                                gender: !visibilityOptions.gender,
                            })
                        }
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={visibilityOptions.birthDate}
                        label="Дата рождения"
                        onChange={() =>
                            setVisibilityOptions({
                                ...visibilityOptions,
                                birthDate: !visibilityOptions.birthDate,
                            })
                        }
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={visibilityOptions.phoneNumber}
                        label="Номер телефона"
                        onChange={() =>
                            setVisibilityOptions({
                                ...visibilityOptions,
                                phoneNumber: !visibilityOptions.phoneNumber,
                            })
                        }
                    />
                </Box>
            </Box>
            <Box sx={{ display: "flex", gap: 2 }}>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={visibilityOptions.representativePhoneNumber}
                        label="Номер телефона представителя"
                        onChange={() =>
                            setVisibilityOptions({
                                ...visibilityOptions,
                                representativePhoneNumber:
                                    !visibilityOptions.representativePhoneNumber,
                            })
                        }
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={visibilityOptions.representativeAlias}
                        label="Как обратиться к представителю"
                        onChange={() =>
                            setVisibilityOptions({
                                ...visibilityOptions,
                                representativeAlias: !visibilityOptions.representativeAlias,
                            })
                        }
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={visibilityOptions.isOnPaidStudy}
                        label="Обучение на платной основе?"
                        onChange={() =>
                            setVisibilityOptions({
                                ...visibilityOptions,
                                isOnPaidStudy: !visibilityOptions.isOnPaidStudy,
                            })
                        }
                    />
                </Box>
            </Box>
            <Box sx={{ display: "flex", gap: 2 }}>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={visibilityOptions.snils}
                        label="Снилс"
                        onChange={() =>
                            setVisibilityOptions({
                                ...visibilityOptions,
                                snils: !visibilityOptions.snils,
                            })
                        }
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={visibilityOptions.groupNumber}
                        label="Номер группы"
                        onChange={() =>
                            setVisibilityOptions({
                                ...visibilityOptions,
                                groupNumber: !visibilityOptions.groupNumber,
                            })
                        }
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={visibilityOptions.structuralUnit}
                        label="Структурное подразделение"
                        onChange={() =>
                            setVisibilityOptions({
                                ...visibilityOptions,
                                structuralUnit: !visibilityOptions.structuralUnit,
                            })
                        }
                    />
                </Box>
            </Box>
            <Box sx={{ display: "flex", gap: 2 }}>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={visibilityOptions.educationLevelType}
                        label="Тип уровня образования"
                        onChange={() =>
                            setVisibilityOptions({
                                ...visibilityOptions,
                                educationLevelType: !visibilityOptions.educationLevelType,
                            })
                        }
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={visibilityOptions.educationLevelName}
                        label="Название уровня образования"
                        onChange={() =>
                            setVisibilityOptions({
                                ...visibilityOptions,
                                educationLevelName: !visibilityOptions.educationLevelName,
                            })
                        }
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={visibilityOptions.educationLevelCode}
                        label="Код уровня образования"
                        onChange={() =>
                            setVisibilityOptions({
                                ...visibilityOptions,
                                educationLevelCode: !visibilityOptions.educationLevelCode,
                            })
                        }
                    />
                </Box>
            </Box>
            <Box sx={{ display: "flex", gap: 2 }}>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={visibilityOptions.enrollmentYear}
                        label="Год поступления"
                        onChange={() =>
                            setVisibilityOptions({
                                ...visibilityOptions,
                                enrollmentYear: !visibilityOptions.enrollmentYear,
                            })
                        }
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={visibilityOptions.curator}
                        label="Куратор"
                        onChange={() =>
                            setVisibilityOptions({
                                ...visibilityOptions,
                                curator: !visibilityOptions.curator,
                            })
                        }
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={visibilityOptions.clusterName}
                        label="Наименование кластера"
                        onChange={() =>
                            setVisibilityOptions({
                                ...visibilityOptions,
                                clusterName: !visibilityOptions.clusterName,
                            })
                        }
                    />
                </Box>
            </Box>
            <Box sx={{ display: "flex", gap: 2 }}>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={visibilityOptions.passportNumber}
                        label="Номер паспорта"
                        onChange={() =>
                            setVisibilityOptions({
                                ...visibilityOptions,
                                passportNumber: !visibilityOptions.passportNumber,
                            })
                        }
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={visibilityOptions.passportSeries}
                        label="Серия паспорта"
                        onChange={() =>
                            setVisibilityOptions({
                                ...visibilityOptions,
                                passportSeries: !visibilityOptions.passportSeries,
                            })
                        }
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={visibilityOptions.passportIssuedBy}
                        label="Кем выдан паспорт"
                        onChange={() =>
                            setVisibilityOptions({
                                ...visibilityOptions,
                                passportIssuedBy: !visibilityOptions.passportIssuedBy,
                            })
                        }
                    />
                </Box>
            </Box>
            <Box sx={{ display: "flex", gap: 2 }}>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={visibilityOptions.passportIssueDate}
                        label="Дата получения паспорта"
                        onChange={() =>
                            setVisibilityOptions({
                                ...visibilityOptions,
                                passportIssueDate: !visibilityOptions.passportIssueDate,
                            })
                        }
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={visibilityOptions.targetAgreementNumber}
                        label="Номер договора о целевом обучении"
                        onChange={() =>
                            setVisibilityOptions({
                                ...visibilityOptions,
                                targetAgreementNumber: !visibilityOptions.targetAgreementNumber,
                            })
                        }
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={visibilityOptions.targetAgreementEnterpriseName}
                        label="Название предприятие, с которым заключен договор о целевом обучении"
                        onChange={() =>
                            setVisibilityOptions({
                                ...visibilityOptions,
                                targetAgreementEnterpriseName:
                                    !visibilityOptions.targetAgreementEnterpriseName,
                            })
                        }
                    />
                </Box>
            </Box>
            <Box sx={{ display: "flex", gap: 2 }}>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={visibilityOptions.targetAgreementDate}
                        label="Дата заключения договора о целевом обучении"
                        onChange={() =>
                            setVisibilityOptions({
                                ...visibilityOptions,
                                targetAgreementDate: !visibilityOptions.targetAgreementDate,
                            })
                        }
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={visibilityOptions.armyCallDate}
                        label="Дата призыва"
                        onChange={() =>
                            setVisibilityOptions({
                                ...visibilityOptions,
                                armyCallDate: !visibilityOptions.armyCallDate,
                            })
                        }
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={visibilityOptions.socialStatuses}
                        label="Социальные статусы"
                        onChange={() =>
                            setVisibilityOptions({
                                ...visibilityOptions,
                                socialStatuses: !visibilityOptions.socialStatuses,
                            })
                        }
                    />
                </Box>
            </Box>
            <Box sx={{ display: "flex", gap: 2 }}>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={visibilityOptions.status}
                        label="Статус"
                        onChange={() =>
                            setVisibilityOptions({
                                ...visibilityOptions,
                                status: !visibilityOptions.status,
                            })
                        }
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={visibilityOptions.address}
                        label="Адрес"
                        onChange={() =>
                            setVisibilityOptions({
                                ...visibilityOptions,
                                address: !visibilityOptions.address,
                            })
                        }
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={visibilityOptions.isForeignCitizen}
                        label="Иностранный гражданин?"
                        onChange={() =>
                            setVisibilityOptions({
                                ...visibilityOptions,
                                isForeignCitizen: !visibilityOptions.isForeignCitizen,
                            })
                        }
                    />
                </Box>
            </Box>
            <Box sx={{ display: "flex", gap: 2 }}>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={visibilityOptions.inn}
                        label="Инн"
                        onChange={() =>
                            setVisibilityOptions({
                                ...visibilityOptions,
                                inn: !visibilityOptions.inn,
                            })
                        }
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={visibilityOptions.email}
                        label="Почта"
                        onChange={() =>
                            setVisibilityOptions({
                                ...visibilityOptions,
                                email: !visibilityOptions.email,
                            })
                        }
                    />
                </Box>
                <Box sx={{ flex: 1 }} />
            </Box>
        </Box>,
    ]

    return (
        <Box className="container">
            <StepperComponent
                steps={["Группировка", "Выбор данных"]}
                stepContent={stepContent}
                onComplete={() => console.log("Процесс завершен!")}
                onReset={() => {}}
            />
        </Box>
    )
}

export default ReportsPage
