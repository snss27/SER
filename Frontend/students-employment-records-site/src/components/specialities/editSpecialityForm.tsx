import { SpecialityBlank } from "@/domain/specialities/models/specialityBlank"
import SpecialitiesProvider from "@/domain/specialities/specialitiesProvider"
import useNotifications from "@/hooks/useNotifications"
import { Box } from "@mui/material"
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
        <Box component="form" className="edit-form-container">
            <TextInput
                value={specialityBlank.name}
                label="Название"
                onChange={(name) => dispatch({ type: "CHANGE_NAME", payload: { name } })}
            />
            <TextInput
                value={specialityBlank.code}
                label="Код"
                onChange={(code) => dispatch({ type: "CHANGE_CODE", payload: { code } })}
            />
            <NumberInput
                value={specialityBlank.studyYears}
                label="Лет обучения"
                min={0}
                max={10}
                onChange={(studyYears) =>
                    dispatch({ type: "CHANGE_STUDY_YEARS", payload: { studyYears } })
                }
            />
            <NumberInput
                value={specialityBlank.studyMonths}
                label="Месяцев обучения"
                min={0}
                max={12}
                onChange={(studyMonths) =>
                    dispatch({ type: "CHANGE_STUDY_MONTHS", payload: { studyMonths } })
                }
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

export default EditSpecialityForm
