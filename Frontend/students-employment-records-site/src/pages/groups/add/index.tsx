import EditGroupForm from "@/components/groups/editGroupForm"
import { GroupBlank } from "@/domain/groups/models/groupBlank"
import { Box, Typography } from "@mui/material"

const AddGroupPage = () => {
    return (
        <Box className="container-fill">
            <Box className="edit-page-container">
                <Typography variant="h1" textAlign="center">
                    Добавление группы
                </Typography>
                <EditGroupForm initialGroupBlank={GroupBlank.empty()} />
            </Box>
        </Box>
    )
}

export default AddGroupPage
