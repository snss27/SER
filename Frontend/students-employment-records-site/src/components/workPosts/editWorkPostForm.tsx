import { WorkPostBlank } from "@/domain/workPosts/models/workPostBlank"
import WorkPostsProvider from "@/domain/workPosts/workPostsProvider"
import useNotifications from "@/hooks/useNotifications"
import { Box } from "@mui/material"
import { useRouter } from "next/router"
import { useReducer } from "react"
import { IconPosition, IconType } from "../shared/buttons"
import Button from "../shared/buttons/button"
import TextInput from "../shared/inputs/textInput"

interface Props {
    initialBlank: WorkPostBlank
}

const EditWorkPostForm = (props: Props) => {
    const [workPostBlank, dispatch] = useReducer(WorkPostBlank.reducer, props.initialBlank)

    const navigator = useRouter()
    const { showError, showSuccess } = useNotifications()

    async function handleSaveButton() {
        const result = await WorkPostsProvider.save(workPostBlank)
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
                value={workPostBlank.name}
                label="Название"
                onChange={(name) => dispatch({ type: "CHANGE_NAME", payload: { name } })}
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

export default EditWorkPostForm
