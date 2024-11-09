import { NumberInput } from "@/components/shared/inputs/numberInput"
import TextInput from "@/components/shared/inputs/textInput"
import { SpecialityBlank } from "@/domain/specialities/models/specialityBlank"
import { Box, Typography } from "@mui/material"
import { useReducer } from "react"

const AddSpecialityPage = () => {
    const [specialityBlank, dispatch] = useReducer(SpecialityBlank.reducer, SpecialityBlank.empty())

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
                        label="Лет обучения"
                        min={1}
                        max={100}
                        onChange={(studyYears) =>
                            dispatch({ type: "CHANGE_STUDY_YEARS", payload: { studyYears } })
                        }
                    />
                </Box>
            </Box>
        </Box>
    )
}

export default AddSpecialityPage
