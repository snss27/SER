import { EducationLevelsProvider } from "@/domain/educationLevels/educationLevelsProvider"
import { EducationLevelTypes } from "@/domain/educationLevels/enums/EducationLevelTypes"
import { EducationLevelBlank } from "@/domain/educationLevels/models/educationLevelBlank"
import useNotifications from "@/hooks/useNotifications"
import { Box } from "@mui/material"
import { useRouter } from "next/router"
import { useReducer } from "react"
import { IconPosition, IconType } from "../shared/buttons"
import Button from "../shared/buttons/button"
import { Select } from "../shared/inputs/select"
import TextInput from "../shared/inputs/textInput"

interface Props {
    initialBlank: EducationLevelBlank
}

export const EditEducationLevelForm = ({ initialBlank }: Props) => {
    const [educationLevelBlank, dispatch] = useReducer(EducationLevelBlank.reducer, initialBlank)

    const navigator = useRouter()
    const { showError, showSuccess } = useNotifications()

    async function handleSaveButton() {
        const result = await EducationLevelsProvider.save(educationLevelBlank)
        if (!result.isSuccess) return showError(result.getErrorsString)

        showSuccess("Изменения сохранены")
        navigator.back()
    }

    function handleBackButton() {
        navigator.back()
    }

    return (
        <Box component="form" className="edit-form-container">
            <Select
                options={EducationLevelTypes.getAll()}
                value={educationLevelBlank.type}
                label="Тип уровня образования"
                getOptionLabel={EducationLevelTypes.displayName}
                onChange={(type) => dispatch({ type: "CHANGE_TYPE", payload: { type } })}
            />
            <TextInput
                value={educationLevelBlank.name}
                label="Наименование"
                onChange={(name) => dispatch({ type: "CHANGE_NAME", payload: { name } })}
            />
            <TextInput
                value={educationLevelBlank.code}
                label="Код"
                onChange={(code) => dispatch({ type: "CHANGE_CODE", payload: { code } })}
            />
            <TextInput
                value={educationLevelBlank.studyTime}
                label="Срок обучения"
                onChange={(studyTime) =>
                    dispatch({ type: "CHANGE_STUDY_TIME", payload: { studyTime } })
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
