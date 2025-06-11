import { EditStudentForm } from "@/components/students/editStudentForm"
import { StudentBlank } from "@/domain/students/models/studentBlank"
import { Box, Typography } from "@mui/material"

const AddStudentPage = () => {
    return (
        <Box className="container" sx={{ px: 4, pt: 4, g: 2 }}>
            <Typography variant="h1" textAlign="center" gutterBottom>
                Добавление студента
            </Typography>
            <EditStudentForm initialStudentBlank={StudentBlank.empty()} />
        </Box>
    )
}

export default AddStudentPage
