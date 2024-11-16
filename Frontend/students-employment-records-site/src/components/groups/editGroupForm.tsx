import CuratorsProvider from "@/domain/curators/curatorsProvider"
import { StructuralUnits } from "@/domain/groups/enums/structuralUnits"
import GroupsProvider from "@/domain/groups/groupsProvider"
import { GroupBlank } from "@/domain/groups/models/groupBlank"
import SpecialitiesProvider from "@/domain/specialities/specialitiesProvider"
import useNotifications from "@/hooks/useNotifications"
import { Box } from "@mui/material"
import { useRouter } from "next/router"
import { useReducer } from "react"
import { IconPosition, IconType } from "../shared/buttons"
import Button from "../shared/buttons/button"
import AsyncAutocomplete from "../shared/inputs/asyncAutocomplete"
import GroupNumberInput from "../shared/inputs/maskedInputs/groupNumberInput"
import Select from "../shared/inputs/select"
import YearPicker from "../shared/inputs/yearPicker"

interface Props {
    initialGroupBlank: GroupBlank
}

const EditGroupForm = (props: Props) => {
    const [groupBlank, dispatch] = useReducer(GroupBlank.reducer, props.initialGroupBlank)

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

    async function loadSpecialities(searchText: string) {
        return await SpecialitiesProvider.getBySearchText(searchText)
    }

    async function loadCurators(searchText: string) {
        return await CuratorsProvider.getBySearchText(searchText)
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
                value={groupBlank.speciality}
                label="Специальность"
                loadOptions={loadSpecialities}
                onChange={(speciality) =>
                    dispatch({ type: "CHANGE_SPECIALITY", payload: { speciality } })
                }
                getOptionLabel={(speciality) => speciality.name}
                isOptionEqualToValue={(first, second) => first.id === second.id}
                keyExtractor={(speciality) => speciality.id}
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
                loadOptions={loadCurators}
                onChange={(curator) => dispatch({ type: "CHANGE_CURATOR", payload: { curator } })}
                getOptionLabel={(curator) => curator.formattedFullName}
                isOptionEqualToValue={(first, second) => first.id === second.id}
                keyExtractor={(curator) => curator.id}
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

export default EditGroupForm
