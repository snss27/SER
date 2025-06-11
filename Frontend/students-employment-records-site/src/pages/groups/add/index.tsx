import { EditGroupForm } from "@/components/groups/editGroupForm"
import { GroupBlank } from "@/domain/groups/models/groupBlank"
import { Box, Typography } from "@mui/material"

const AddGroupPage = () => {
    return (
        <Box className="container" sx={{ px: 4, pt: 4, g: 2 }}>
            <Typography variant="h1" textAlign="center" gutterBottom>
                Добавление группы
            </Typography>
            <EditGroupForm initialBlank={GroupBlank.empty()} />
        </Box>
    )
}

export default AddGroupPage
