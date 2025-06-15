import Switch from "@/components/shared/buttons/switch"
import { AsyncAutocomplete } from "@/components/shared/inputs/asyncAutocomplete"
import { DateRangePicker } from "@/components/shared/inputs/dateRangePicker"
import { Select } from "@/components/shared/inputs/select"
import { AdditionalQualificationsProvider } from "@/domain/additionalQualifications/additionalQualificationsProvider"
import { EnterprisesProvider } from "@/domain/enterprises/enterprisesProvider"
import { WorkPlaceGroupingType } from "@/domain/reports/enums/workPlaceGroupingVariant"
import {
    Action,
    ReportGroupingOptions,
} from "@/domain/reports/models/grouping/reportGroupingOptions"
import { ForeignCitizenVariant } from "@/domain/students/enums/foreignCitizenVariant"
import { Gender } from "@/domain/students/enums/gender"
import { OnPaidStudyVariant } from "@/domain/students/enums/onPaidStudyVariant"
import { SocialStatus } from "@/domain/students/enums/socialStatus"
import { StudentStatus } from "@/domain/students/enums/studentStatus"
import { Box } from "@mui/material"
import { Dispatch } from "react"
import { ArmyGroupingSection } from "./armyGroupingSection"
import { GroupGroupingSection } from "./groupGroupingSection"

interface Props {
    groupingOptions: ReportGroupingOptions
    dispatch: Dispatch<Action>
}

export function Grouping({ groupingOptions, dispatch }: Props) {
    return (
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
                    value={groupingOptions.useStrictMatchForAdditionalQualifications}
                    onChange={(isChecked) =>
                        dispatch({
                            type: "CHANGE_USE_STRICT_MATH_FOR_ADDITIONAL_QUALIFICATIONS",
                            payload: {
                                useStrictMatchForAdditionalQualifications: isChecked,
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
                    value={groupingOptions.useStrictMatchForSocialStatuses}
                    onChange={(isChecked) =>
                        dispatch({
                            type: "CHANGE_USE_STRICT_METH_FOR_SOCIAL_STATUSES",
                            payload: {
                                useStrictMatchForSocialStatuses: isChecked,
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
                        value={groupingOptions.workPlaceGroupingType}
                        getOptionLabel={WorkPlaceGroupingType.getDisplayName}
                        onChange={(workPlaceGroupingType) =>
                            dispatch({
                                type: "CHANGE_WORK_PLACE_GROUPING_TYPE",
                                payload: { workPlaceGroupingType },
                            })
                        }
                        options={WorkPlaceGroupingType.getAll()}
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
                armyGroupingOptions={groupingOptions.armyGroupingOptions}
                dispatch={dispatch}
            />
        </Box>
    )
}
