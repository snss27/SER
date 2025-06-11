import { AdditionalQualificationsProvider } from "@/domain/additionalQualifications/additionalQualificationsProvider"
import { AdditionalQualificationBlank } from "@/domain/additionalQualifications/models/additionalQualificationBlank"
import useNotifications from "@/hooks/useNotifications"
import { Box } from "@mui/material"
import { useRouter } from "next/router"
import React, { useReducer } from "react"
import { IconPosition, IconType } from "../shared/buttons"
import Button from "../shared/buttons/button"
import TextInput from "../shared/inputs/textInput"

interface Props {
    initialBlank: AdditionalQualificationBlank
}

export const EditAdditionalQualificationForm: React.FC<Props> = ({ initialBlank }) => {
    const [additionalQualificationBlank, dispatch] = useReducer(
        AdditionalQualificationBlank.reducer,
        initialBlank
    )

    const navigator = useRouter()
    const { showError, showSuccess } = useNotifications()

    async function handleSaveButton() {
        const result = await AdditionalQualificationsProvider.save(additionalQualificationBlank)
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
                gap: 2,
                flex: 1,
                py: 2,
                width: "50%",
                alignSelf: "center",
            }}>
            <TextInput
                value={additionalQualificationBlank.name}
                label="Наименование"
                onChange={(name) => dispatch({ type: "CHANGE_NAME", payload: { name } })}
            />
            <TextInput
                value={additionalQualificationBlank.code}
                label="Код"
                onChange={(code) => dispatch({ type: "CHANGE_CODE", payload: { code } })}
            />
            <TextInput
                value={additionalQualificationBlank.studyTime}
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
