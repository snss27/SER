import { ClustersProvider } from "@/domain/clusters/clustersProvider"
import { ClusterBlank } from "@/domain/clusters/models/clusterBlank"
import useNotifications from "@/hooks/useNotifications"
import { Box } from "@mui/material"
import { useRouter } from "next/router"
import { useReducer } from "react"
import { IconPosition, IconType } from "../shared/buttons"
import Button from "../shared/buttons/button"
import TextInput from "../shared/inputs/textInput"

interface Props {
    initialBlank: ClusterBlank
}

export const EditClusterForm: React.FC<Props> = ({ initialBlank }) => {
    const [clusterBlank, dispatch] = useReducer(ClusterBlank.reducer, initialBlank)

    const navigator = useRouter()
    const { showError, showSuccess } = useNotifications()

    async function handleSaveButton() {
        const result = await ClustersProvider.save(clusterBlank)
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
                value={clusterBlank.name}
                label="Наименование"
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
