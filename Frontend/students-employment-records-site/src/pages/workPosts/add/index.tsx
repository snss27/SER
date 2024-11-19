import EditWorkPostForm from "@/components/workPosts/editWorkPostForm"
import { WorkPostBlank } from "@/domain/workPosts/models/workPostBlank"
import { Box, Typography } from "@mui/material"

const AddWorkPostPage = () => {
    return (
        <Box className="container-fill">
            <Box className="edit-page-container">
                <Typography variant="h1" textAlign="center">
                    Добавление места работы
                </Typography>
                <EditWorkPostForm initialBlank={WorkPostBlank.empty()} />
            </Box>
        </Box>
    )
}

export default AddWorkPostPage
