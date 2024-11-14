import EditCuratorForm from "@/components/curators/editCuratorForm"
import { CuratorBlank } from "@/domain/curators/models/curatorBlank"
import { Box, Typography } from "@mui/material"

const AddCuratorsPage: React.FC = () => {
    return (
        <Box className="container-fill">
            <Box className="edit-page-container">
                <Typography variant="h1" textAlign="center">
                    Добавление куратора
                </Typography>
                <EditCuratorForm initialBlank={CuratorBlank.empty()} />
            </Box>
        </Box>
    )
}

export default AddCuratorsPage
