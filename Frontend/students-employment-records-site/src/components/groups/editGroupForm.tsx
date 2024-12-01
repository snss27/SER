import { StructuralUnits } from "@/domain/groups/enums/structuralUnits"
import { GroupBlank } from "@/domain/groups/models/groupBlank"
import useNotifications from "@/hooks/useNotifications"
import { Box } from "@mui/material"
import { useRouter } from "next/router"
import { useReducer } from "react"
import { IconPosition, IconType } from "../shared/buttons"
import Button from "../shared/buttons/button"
import Select from "../shared/inputs/select"
import YearPicker from "../shared/inputs/yearPicker"
import { GroupsProvider } from "@/domain/groups/groupsProvider"
import { EducationLevelsProvider } from "@/domain/educationLevels/educationLevelsProvider"
import { EmployeesProvider } from "@/domain/employees/employeesProvider"
import { GroupNumberInput } from "@/components/shared/inputs/maskedInputs/groupNumberInput"
import { AsyncAutocomplete } from "@/components/shared/inputs/asyncAutocomplete"

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
        <Box component="form" className="edit-form-container">
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
                getOptionLabel={(structuralUnit) => StructuralUnits.getDisplayText(structuralUnit)}
                onChange={(structuralUnit) =>
                    dispatch({
                        type: "CHANGE_STRUCTURAL_UNIT",
                        payload: { structuralUnit },
                    })
                }
            />
            <AsyncAutocomplete
                value={groupBlank.educationLevelId}
                label="Уровень образования"
                onChange={(educationLevelId) =>
                    dispatch({ type: "CHANGE_EDUCATION_LEVEL_ID", payload: { educationLevelId } })
                }
                loadOption={EducationLevelsProvider.get}
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
                value={groupBlank.curatorId}
                label="Куратор"
                onChange={(curatorId) =>
                    dispatch({
                        type: "CHANGE_CURATOR_ID",
                        payload: { curatorId },
                    })
                }
                loadOptions={EmployeesProvider.getBySearchText}
                loadOption={EmployeesProvider.get}
                getOptionLabel={(curator) => curator.displayName}
            />

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
