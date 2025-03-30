import { EditEducationLevelForm } from "@/components/educationLevels/editEducationLevelForm"
import { EducationLevelBlank } from "@/domain/educationLevels/models/educationLevelBlank"
import { Box, Typography } from "@mui/material"

const AddEducationLevelPage = () => {
    return (
        <Box className="container-fill">
            <Box className="edit-page-container">
                <Typography variant="h1" textAlign="center">
                    Добавление уровня образования
                </Typography>
                <EditEducationLevelForm initialBlank={EducationLevelBlank.empty()} />
            </Box>
        </Box>
    )
}

export default AddEducationLevelPage
