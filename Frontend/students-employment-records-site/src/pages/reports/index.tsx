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
import { ForeignCitizenVariant } from "@/domain/students/enums/foreignCitizenVariant"
import { Gender } from "@/domain/students/enums/gender"
import { OnPaidStudyVariant } from "@/domain/students/enums/onPaidStudyVariant"
import { SocialStatus } from "@/domain/students/enums/socialStatus"
import { StudentStatus } from "@/domain/students/enums/studentStatus"
import { Box } from "@mui/material"
import { useReducer } from "react"

const ReportsPage = () => {
    const [groupingOptions, dispatch] = useReducer(
        ReportGroupingOptions.reducer,
        ReportGroupingOptions.empty()
    )

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
                    <Switch value={true} label="Пол" onChange={() => {}} />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch value={true} label="Дата рождения" onChange={() => {}} />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch value={true} label="Номер телефона" onChange={() => {}} />
                </Box>
            </Box>
            <Box sx={{ display: "flex", gap: 2 }}>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={false}
                        label="Номер телефона представителя"
                        onChange={() => {}}
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={false}
                        label="Как обратиться к представителю"
                        onChange={() => {}}
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch value={false} label="Обучение на платной основе?" onChange={() => {}} />
                </Box>
            </Box>
            <Box sx={{ display: "flex", gap: 2 }}>
                <Box sx={{ flex: 1 }}>
                    <Switch value={false} label="Снилс" onChange={() => {}} />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch value={false} label="Номер группы" onChange={() => {}} />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch value={true} label="Структурное подразделение" onChange={() => {}} />
                </Box>
            </Box>
            <Box sx={{ display: "flex", gap: 2 }}>
                <Box sx={{ flex: 1 }}>
                    <Switch value={false} label="Тип уровня образования" onChange={() => {}} />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch value={true} label="Название уровня образования" onChange={() => {}} />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch value={true} label="Код уровня образования" onChange={() => {}} />
                </Box>
            </Box>
            <Box sx={{ display: "flex", gap: 2 }}>
                <Box sx={{ flex: 1 }}>
                    <Switch value={false} label="Год поступления" onChange={() => {}} />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch value={false} label="Куратор" onChange={() => {}} />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch value={false} label="Название кластера" onChange={() => {}} />
                </Box>
            </Box>
            <Box sx={{ display: "flex", gap: 2 }}>
                <Box sx={{ flex: 1 }}>
                    <Switch value={false} label="Номер паспорта" onChange={() => {}} />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch value={false} label="Серия паспорта" onChange={() => {}} />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch value={true} label="Кем выдан паспорт" onChange={() => {}} />
                </Box>
            </Box>
            <Box sx={{ display: "flex", gap: 2 }}>
                <Box sx={{ flex: 1 }}>
                    <Switch value={false} label="Дата получения паспорта" onChange={() => {}} />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={true}
                        label="Номер договора о целевом обучении"
                        onChange={() => {}}
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={true}
                        label="Название предприятие, с которым заключен договор о целевом обучении"
                        onChange={() => {}}
                    />
                </Box>
            </Box>
            <Box sx={{ display: "flex", gap: 2 }}>
                <Box sx={{ flex: 1 }}>
                    <Switch
                        value={true}
                        label="Дата заключения договора о целевом обучении"
                        onChange={() => {}}
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch value={true} label="Дата призыва" onChange={() => {}} />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch value={true} label="Социальные статусы" onChange={() => {}} />
                </Box>
            </Box>
            <Box sx={{ display: "flex", gap: 2 }}>
                <Box sx={{ flex: 1 }}>
                    <Switch value={false} label="Статус" onChange={() => {}} />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch value={false} label="Адрес" onChange={() => {}} />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch value={false} label="Иностранный гражданин?" onChange={() => {}} />
                </Box>
            </Box>
            <Box sx={{ display: "flex", gap: 2 }}>
                <Box sx={{ flex: 1 }}>
                    <Switch value={false} label="Инн" onChange={() => {}} />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Switch value={false} label="Почта" onChange={() => {}} />
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
