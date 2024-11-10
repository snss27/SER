import Button from "@/components/shared/buttons/button"
import { NumberInput } from "@/components/shared/inputs/numberInput"
import TextInput from "@/components/shared/inputs/textInput"
import { SpecialityBlank } from "@/domain/specialities/models/specialityBlank"
import { SpecialitiesProvider } from "@/domain/specialities/specialitiesProvider"
import useNotifications from "@/hooks/useNotifications"
import { Box, Typography } from "@mui/material"
import { useReducer } from "react"

const AddSpecialityPage = () => {
    const [specialityBlank, dispatch] = useReducer(SpecialityBlank.reducer, SpecialityBlank.empty())

    const { showError, showSuccess } = useNotifications()

    async function handleSaveButton() {
        const result = await SpecialitiesProvider.save(specialityBlank)
        if (!result.isSuccess) return showError(result.getError)
        else showSuccess("Специальность успешно сохранена")
    }

    return (
        <Box sx={{ display: "flex", flexDirection: "column", gap: 3, padding: 2 }}>
            <Box>
                <Typography variant="h1" textAlign="center">
                    Новая специальность
                </Typography>
            </Box>
            <Box sx={{ display: "flex", flexDirection: "row", gap: 2 }}>
                <Box sx={{ flex: 1 }}>
                    <TextInput
                        value={specialityBlank.name}
                        label="Название"
                        onChange={(name) => dispatch({ type: "CHANGE_NAME", payload: { name } })}
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <NumberInput
                        value={specialityBlank.studyYears}
                        label="Количество лет для обучения"
                        min={1}
                        max={100}
                        onChange={(studyYears) =>
                            dispatch({ type: "CHANGE_STUDY_YEARS", payload: { studyYears } })
                        }
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Button text="Сохранить" onClick={handleSaveButton} />
                </Box>
            </Box>
        </Box>
    )
}

export default AddSpecialityPage
