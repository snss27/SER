import { Box, Typography } from "@mui/material"
import { EditEducationLevelForm } from "@/components/educationLevels/editEducationLevelForm"
import { EducationLevelBlank } from "@/domain/educationLevels/models/educationLevelBlank"

const AddEducationLevelPage = () => {
    return (
        <Box className="container-fill">
            <Box className="edit-page-container">
                <Typography variant="h1" textAlign="center">
                    Добавление уровня образования
                </Typography>
                <EditEducationLevelForm initialSpecialityBlank={EducationLevelBlank.empty()} />
            </Box>
        </Box>
    )
}

export default AddEducationLevelPage
