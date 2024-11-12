"use client"

import CuratorsProvider from "@/domain/curators/curatorsProvider"
import { CuratorBlank } from "@/domain/curators/models/curatorBlank"
import useNotifications from "@/hooks/useNotifications"
import { FormTypes } from "@/tools/enums/formTypes"
import { Box } from "@mui/material"
import { useRouter } from "next/router"
import { useReducer } from "react"
import { IconPosition, IconType } from "../shared/buttons"
import Button from "../shared/buttons/button"
import TextInput from "../shared/inputs/textInput"

interface Props {
    initialBlank: CuratorBlank
}

const EditCuratorForm = (props: Props) => {
    const formType = props.initialBlank.id === null ? FormTypes.Add : FormTypes.Edit

    const [curatorBlank, dispatch] = useReducer(CuratorBlank.reducer, props.initialBlank)

    const navigator = useRouter()
    const { showError, showSuccess } = useNotifications()

    function handleBackButton() {
        navigator.back()
    }

    async function handleSaveButton() {
        const result = await CuratorsProvider.save(curatorBlank)
        if (!result.isSuccess) return showError(result.getErrorsString)

        showSuccess(`Куратор успешно ${FormTypes.getSuccessSaveDisplay(formType)}`)
        navigator.back()
    }

    return (
        <Box component="form" className="edit-form-container">
            <TextInput
                value={curatorBlank.name}
                label="Имя"
                onChange={(name) => dispatch({ type: "CHANGE_NAME", payload: { name } })}
            />
            <TextInput
                value={curatorBlank.surname}
                label="Фамилия"
                onChange={(surname) => dispatch({ type: "CHANGE_SURNAME", payload: { surname } })}
            />
            <TextInput
                value={curatorBlank.patronymic}
                label="Отчество"
                onChange={(patronymic) =>
                    dispatch({ type: "CHANGE_PATRONYMIC", payload: { patronymic } })
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

export default EditCuratorForm
