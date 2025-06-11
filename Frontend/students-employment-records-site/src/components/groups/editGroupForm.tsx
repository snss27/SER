import { AsyncAutocomplete } from "@/components/shared/inputs/asyncAutocomplete"
import { GroupNumberInput } from "@/components/shared/inputs/maskedInputs/groupNumberInput"
import { Select } from "@/components/shared/inputs/select"
import { ClustersProvider } from "@/domain/clusters/clustersProvider"
import { EducationLevelsProvider } from "@/domain/educationLevels/educationLevelsProvider"
import { EmployeesProvider } from "@/domain/employees/employeesProvider"
import { StructuralUnits } from "@/domain/groups/enums/structuralUnits"
import { GroupsProvider } from "@/domain/groups/groupsProvider"
import { GroupBlank } from "@/domain/groups/models/groupBlank"
import useNotifications from "@/hooks/useNotifications"
import { Box, Collapse } from "@mui/material"
import { useRouter } from "next/router"
import { useReducer } from "react"
import { IconPosition, IconType } from "../shared/buttons"
import Button from "../shared/buttons/button"
import CheckBox from "../shared/buttons/checkBox"
import YearPicker from "../shared/inputs/yearPicker"

interface Props {
    initialBlank: GroupBlank
}

export const EditGroupForm = (props: Props) => {
    const [groupBlank, dispatch] = useReducer(GroupBlank.reducer, props.initialBlank)

    const navigator = useRouter()
    const { showError, showSuccess } = useNotifications()

    function handleBackButton() {
        navigator.back()
    }

    async function handleSaveButton() {
        const result = await GroupsProvider.save(groupBlank)
        if (!result.isSuccess) return showError(result.getErrorsString)

        showSuccess("Изменения сохранены")
        navigator.back()
    }

    return (
        <Box
            sx={{
                display: "flex",
                flexDirection: "column",
                gap: 2,
                flex: 1,
                py: 2,
                width: "50%",
                alignSelf: "center",
            }}>
            <GroupNumberInput
                value={groupBlank.number}
                onChange={(number) =>
                    dispatch({
                        type: "CHANGE_NUMBER",
                        payload: { number },
                    })
                }
            />
            <Select
                options={StructuralUnits.getAll()}
                value={groupBlank.structuralUnit}
                label="Структурное подразделение"
                getOptionLabel={(structuralUnit: StructuralUnits) =>
                    StructuralUnits.getDisplayText(structuralUnit)
                }
                onChange={(structuralUnit: StructuralUnits | null) =>
                    dispatch({
                        type: "CHANGE_STRUCTURAL_UNIT",
                        payload: { structuralUnit },
                    })
                }
            />
            <AsyncAutocomplete
                value={groupBlank.educationLevel}
                label="Уровень образования"
                onChange={(educationLevel) =>
                    dispatch({ type: "CHANGE_EDUCATION_LEVEL", payload: { educationLevel } })
                }
                loadOptions={EducationLevelsProvider.getBySearchText}
                getOptionLabel={(educationLevel) => educationLevel.displayName}
            />
            <YearPicker
                value={groupBlank.enrollmentYear}
                label="Год поступления"
                onChange={(enrollmentYear) =>
                    dispatch({
                        type: "CHANGE_ENROLLMENT_YEAR",
                        payload: { enrollmentYear },
                    })
                }
            />
            <AsyncAutocomplete
                value={groupBlank.curator}
                label="Куратор"
                onChange={(curator) =>
                    dispatch({
                        type: "CHANGE_CURATOR",
                        payload: { curator },
                    })
                }
                loadOptions={EmployeesProvider.getBySearchText}
                getOptionLabel={(curator) => curator.displayName}
            />

            <Box>
                <CheckBox
                    value={groupBlank.hasCluster}
                    label="Принадлежит к кластеру?"
                    onChange={(hasCluster) =>
                        dispatch({ type: "CHANGE_HAS_CLUSTER", payload: { hasCluster } })
                    }
                />

                <Collapse in={groupBlank.hasCluster}>
                    <AsyncAutocomplete
                        value={groupBlank.cluster}
                        label="Кластер"
                        onChange={(cluster) =>
                            dispatch({ type: "CHANGE_CLUSTER", payload: { cluster } })
                        }
                        loadOptions={ClustersProvider.getBySearchText}
                        getOptionLabel={(cluster) => cluster.name}
                    />
                </Collapse>
            </Box>

            <Box className="edit-form-footer">
                <Button
                    text="Назад"
                    icon={{ type: IconType.Back, position: IconPosition.Start }}
                    onClick={handleBackButton}
                />
                <Button
                    text="Сохранить"
                    variant="contained"
                    icon={{ type: IconType.Save, position: IconPosition.End }}
                    onClick={handleSaveButton}
                />
            </Box>
        </Box>
    )
}
