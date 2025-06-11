import { EditEducationLevelForm } from "@/components/educationLevels/editEducationLevelForm"
import { EducationLevelBlank } from "@/domain/educationLevels/models/educationLevelBlank"
import { Box, Typography } from "@mui/material"

const AddEducationLevelPage = () => {
    return (
        <Box className="container" sx={{ px: 4, pt: 4, g: 2 }}>
            <Typography variant="h1" textAlign="center" gutterBottom>
                Добавление уровня образования
            </Typography>
            <EditEducationLevelForm initialBlank={EducationLevelBlank.empty()} />
        </Box>
    )
}

export default AddEducationLevelPage
