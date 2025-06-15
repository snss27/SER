import { AsyncAutocomplete } from "@/components/shared/inputs/asyncAutocomplete"
import { DateRangePicker } from "@/components/shared/inputs/dateRangePicker"
import { Select } from "@/components/shared/inputs/select"
import { ClustersProvider } from "@/domain/clusters/clustersProvider"
import { EducationLevelsProvider } from "@/domain/educationLevels/educationLevelsProvider"
import { EducationLevelTypes } from "@/domain/educationLevels/enums/EducationLevelTypes"
import { EmployeesProvider } from "@/domain/employees/employeesProvider"
import { StructuralUnits } from "@/domain/groups/enums/structuralUnits"
import { GroupsProvider } from "@/domain/groups/groupsProvider"
import { EducationLevelGroupingType } from "@/domain/reports/enums/educationLevelGroupingVariant"
import { GroupGroupingType } from "@/domain/reports/enums/groupGroupingType"
import { GroupGroupingOptions } from "@/domain/reports/models/grouping/groupGroupingOptions"
import { Action } from "@/domain/reports/models/grouping/reportGroupingOptions"
import { Box } from "@mui/material"

export const GroupGroupingSection = ({
    groupingOptions,
    dispatch,
}: {
    groupingOptions: GroupGroupingOptions
    dispatch: React.Dispatch<Action>
}) => {
    const handleTypeChange = (type: GroupGroupingType) => {
        if (type === GroupGroupingType.NotGrouping) {
            dispatch({
                type: "CHANGE_GROUP_GROUPING_OPTIONS",
                payload: {
                    groupGroupingOptions: { type: GroupGroupingType.NotGrouping },
                },
            })
            return
        }

        let baseOptions: GroupGroupingOptions

        switch (type) {
            case GroupGroupingType.Groups:
                baseOptions = { type, groups: [] }
                break
            case GroupGroupingType.StructuralUnits:
                baseOptions = { type, structuralUnits: [] }
                break
            case GroupGroupingType.EducationLevel:
                baseOptions = {
                    type,
                    educationLevelGroupingOptions: {
                        variant: EducationLevelGroupingType.EducationLevelTypes,
                        educationLevelTypes: [],
                    },
                }
                break
            case GroupGroupingType.EnrollmentYearPeriod:
                baseOptions = {
                    type,
                    enrollmentYearPeriod: [null, null],
                }
                break
            case GroupGroupingType.Curators:
                baseOptions = { type, curators: [] }
                break
            case GroupGroupingType.Clusters:
                baseOptions = { type, clusters: [] }
                break
            default:
                baseOptions = { type: GroupGroupingType.NotGrouping }
        }

        dispatch({
            type: "CHANGE_GROUP_GROUPING_OPTIONS",
            payload: { groupGroupingOptions: baseOptions },
        })
    }

    const handleEducationVariantChange = (variant: EducationLevelGroupingType) => {
        if (groupingOptions.type !== GroupGroupingType.EducationLevel) return

        const updatedOptions: GroupGroupingOptions = {
            ...groupingOptions,
            educationLevelGroupingOptions:
                variant === EducationLevelGroupingType.EducationLevelTypes
                    ? {
                          variant: EducationLevelGroupingType.EducationLevelTypes,
                          educationLevelTypes: [],
                      }
                    : {
                          variant: EducationLevelGroupingType.EducationLevels,
                          educationLevels: [],
                      },
        }

        dispatch({
            type: "CHANGE_GROUP_GROUPING_OPTIONS",
            payload: { groupGroupingOptions: updatedOptions },
        })
    }

    return (
        <Box sx={{ display: "flex", flexDirection: "column", gap: 2 }}>
            <Select
                required
                label="Фильтрация по группам"
                value={groupingOptions.type}
                options={GroupGroupingType.getAll()}
                getOptionLabel={GroupGroupingType.getDisplayName}
                onChange={handleTypeChange}
            />

            {groupingOptions.type !== GroupGroupingType.NotGrouping && (
                <>
                    {groupingOptions.type === GroupGroupingType.Groups && (
                        <AsyncAutocomplete
                            multiple
                            label="Выберите группы"
                            value={groupingOptions.groups}
                            loadOptions={GroupsProvider.getBySearchText}
                            getOptionLabel={(group) => group.displayName}
                            onChange={(groups) =>
                                dispatch({
                                    type: "CHANGE_GROUP_GROUPING_OPTIONS",
                                    payload: {
                                        groupGroupingOptions: {
                                            ...groupingOptions,
                                            groups,
                                        },
                                    },
                                })
                            }
                        />
                    )}

                    {groupingOptions.type === GroupGroupingType.StructuralUnits && (
                        <Select
                            multiple
                            label="Структурные подразделения"
                            value={groupingOptions.structuralUnits}
                            options={StructuralUnits.getAll()}
                            getOptionLabel={StructuralUnits.getDisplayText}
                            onChange={(structuralUnits) =>
                                dispatch({
                                    type: "CHANGE_GROUP_GROUPING_OPTIONS",
                                    payload: {
                                        groupGroupingOptions: {
                                            ...groupingOptions,
                                            structuralUnits,
                                        },
                                    },
                                })
                            }
                        />
                    )}

                    {groupingOptions.type === GroupGroupingType.EducationLevel && (
                        <Box sx={{ display: "flex", flexDirection: "column", gap: 2 }}>
                            <Select
                                required
                                label="Вариант фильтрации"
                                value={groupingOptions.educationLevelGroupingOptions.variant}
                                options={EducationLevelGroupingType.getAll()}
                                getOptionLabel={(variant) =>
                                    variant === EducationLevelGroupingType.EducationLevelTypes
                                        ? "По типу уровня образования"
                                        : "По уровню образования"
                                }
                                onChange={handleEducationVariantChange}
                            />

                            {groupingOptions.educationLevelGroupingOptions.variant ===
                                EducationLevelGroupingType.EducationLevelTypes && (
                                <Select
                                    multiple
                                    label="Типы уровней образования"
                                    value={
                                        groupingOptions.educationLevelGroupingOptions
                                            .educationLevelTypes
                                    }
                                    options={EducationLevelTypes.getAll()}
                                    getOptionLabel={EducationLevelTypes.displayName}
                                    onChange={(educationLevelTypes) =>
                                        dispatch({
                                            type: "CHANGE_GROUP_GROUPING_OPTIONS",
                                            payload: {
                                                groupGroupingOptions: {
                                                    ...groupingOptions,
                                                    educationLevelGroupingOptions: {
                                                        variant:
                                                            EducationLevelGroupingType.EducationLevelTypes,
                                                        educationLevelTypes,
                                                    },
                                                },
                                            },
                                        })
                                    }
                                />
                            )}

                            {groupingOptions.educationLevelGroupingOptions.variant ===
                                EducationLevelGroupingType.EducationLevels && (
                                <AsyncAutocomplete
                                    multiple
                                    label="Уровни образования"
                                    value={
                                        groupingOptions.educationLevelGroupingOptions
                                            .educationLevels
                                    }
                                    loadOptions={EducationLevelsProvider.getBySearchText}
                                    getOptionLabel={(level) => level.displayName}
                                    onChange={(educationLevels) =>
                                        dispatch({
                                            type: "CHANGE_GROUP_GROUPING_OPTIONS",
                                            payload: {
                                                groupGroupingOptions: {
                                                    ...groupingOptions,
                                                    educationLevelGroupingOptions: {
                                                        variant:
                                                            EducationLevelGroupingType.EducationLevels,
                                                        educationLevels,
                                                    },
                                                },
                                            },
                                        })
                                    }
                                />
                            )}
                        </Box>
                    )}

                    {groupingOptions.type === GroupGroupingType.EnrollmentYearPeriod && (
                        <DateRangePicker
                            label="Период поступления"
                            value={groupingOptions.enrollmentYearPeriod}
                            onChange={(enrollmentYearPeriod) =>
                                dispatch({
                                    type: "CHANGE_GROUP_GROUPING_OPTIONS",
                                    payload: {
                                        groupGroupingOptions: {
                                            ...groupingOptions,
                                            enrollmentYearPeriod,
                                        },
                                    },
                                })
                            }
                        />
                    )}

                    {groupingOptions.type === GroupGroupingType.Curators && (
                        <AsyncAutocomplete
                            multiple
                            label="Кураторы"
                            value={groupingOptions.curators}
                            loadOptions={EmployeesProvider.getBySearchText}
                            getOptionLabel={(employee) => employee.displayName}
                            onChange={(curators) =>
                                dispatch({
                                    type: "CHANGE_GROUP_GROUPING_OPTIONS",
                                    payload: {
                                        groupGroupingOptions: {
                                            ...groupingOptions,
                                            curators,
                                        },
                                    },
                                })
                            }
                        />
                    )}

                    {groupingOptions.type === GroupGroupingType.Clusters && (
                        <AsyncAutocomplete
                            multiple
                            label="Кластеры"
                            value={groupingOptions.clusters}
                            loadOptions={ClustersProvider.getBySearchText}
                            getOptionLabel={(cluster) => cluster.name}
                            onChange={(clusters) =>
                                dispatch({
                                    type: "CHANGE_GROUP_GROUPING_OPTIONS",
                                    payload: {
                                        groupGroupingOptions: {
                                            ...groupingOptions,
                                            clusters,
                                        },
                                    },
                                })
                            }
                        />
                    )}
                </>
            )}
        </Box>
    )
}
