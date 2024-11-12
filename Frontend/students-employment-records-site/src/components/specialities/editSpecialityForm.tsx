import { SpecialityBlank } from "@/domain/specialities/models/specialityBlank"
import SpecialitiesProvider from "@/domain/specialities/specialitiesProvider"
import useNotifications from "@/hooks/useNotifications"
import { Box, Typography } from "@mui/material"
import { useRouter } from "next/router"
import { useReducer } from "react"
import { IconPosition, IconType } from "../shared/buttons"
import Button from "../shared/buttons/button"
import { NumberInput } from "../shared/inputs/numberInput"
import TextInput from "../shared/inputs/textInput"

interface Props {
    initialSpecialityBlank: SpecialityBlank
}

const EditSpecialityForm = (props: Props) => {
    const [specialityBlank, dispatch] = useReducer(
        SpecialityBlank.reducer,
        props.initialSpecialityBlank
    )

    const navigator = useRouter()
    const { showError, showSuccess } = useNotifications()

    async function handleSaveButton() {
        const result = await SpecialitiesProvider.save(specialityBlank)
        if (!result.isSuccess) return showError(result.getErrorsString)

        showSuccess("Изменения сохранены")
        navigator.back()
    }

    function handleBackButton() {
        navigator.back()
    }

    return (
        <Box
            sx={{
                display: "flex",
                flexDirection: "column",
                gap: 3,
                width: "50%",
            }}>
            <Typography variant="h1" textAlign="center">
                Редактор специальности
            </Typography>

            <Box sx={{ display: "flex", flexDirection: "column", gap: 2 }}>
                <TextInput
                    value={specialityBlank.name}
                    label="Название"
                    onChange={(name) => dispatch({ type: "CHANGE_NAME", payload: { name } })}
                />
                <NumberInput
                    value={specialityBlank.studyYears}
                    label="Количество лет для обучения"
                    min={1}
                    max={10}
                    onChange={(studyYears) =>
                        dispatch({ type: "CHANGE_STUDY_YEARS", payload: { studyYears } })
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
        </Box>
    )
}

export default EditSpecialityForm
