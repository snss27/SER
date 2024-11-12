import { StructuralUnits } from "@/domain/groups/enums/structuralUnits"
import GroupsProvider from "@/domain/groups/groupsProvider"
import { GroupBlank } from "@/domain/groups/models/groupBlank"
import Speciality from "@/domain/specialities/models/speciality"
import SpecialitiesProvider from "@/domain/specialities/specialitiesProvider"
import useNotifications from "@/hooks/useNotifications"
import { Box, Typography } from "@mui/material"
import { useRouter } from "next/router"
import { useEffect, useReducer, useState } from "react"
import { IconPosition, IconType } from "../shared/buttons"
import Button from "../shared/buttons/button"
import GroupNumberInput from "../shared/inputs/maskedInputs/groupNumberInput"
import Select from "../shared/inputs/select"
import TextInput from "../shared/inputs/textInput"
import YearPicker from "../shared/inputs/yearPicker"

interface Props {
    initialGroupBlank: GroupBlank
}

const EditGroupForm = (props: Props) => {
    const [groupBlank, dispatch] = useReducer(GroupBlank.reducer, props.initialGroupBlank)
    const [specialities, setSpecialities] = useState<Speciality[]>([])

    const navigator = useRouter()
    const { showError, showSuccess } = useNotifications()

    useEffect(() => {
        async function loadSpecialities() {
            const specialities = await SpecialitiesProvider.getAll()

            setSpecialities(specialities)
        }

        loadSpecialities()
    }, [])

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
        <Box sx={{ display: "flex", flexDirection: "column", gap: 3, width: "50%" }}>
            <Typography variant="h1" textAlign="center">
                Новая группа
            </Typography>

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
            <Select
                options={specialities.map((s) => s.id)}
                value={groupBlank.specialityId}
                label="Выберите специальность"
                getOptionLabel={(specialityId) =>
                    //TODO Исправить костыль
                    specialities.length > 0
                        ? specialities.find((speciality) => speciality.id === specialityId)!.name
                        : ""
                }
                onChange={(specialityId) =>
                    dispatch({ type: "CHANGE_SPECIALITY", payload: { specialityId } })
                }
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
            <TextInput
                value={groupBlank.curatorName}
                label="Куратор группы"
                onChange={(curatorName) =>
                    dispatch({ type: "CHANGE_CURATOR_NAME", payload: { curatorName } })
                }
            />

            <Box
                sx={{
                    display: "flex",
                    flexDirection: "row",
                    justifyContent: "space-between",
                }}>
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
