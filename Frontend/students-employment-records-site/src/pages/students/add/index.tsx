import EditStudentForm from "@/components/students/editStudentForm"
import { StudentBlank } from "@/domain/students/models/studentBlank"
import { Box, Typography } from "@mui/material"

const AddStudentPage = () => {
    return (
        <Box className="container-fill">
            <Box className="edit-page-container">
                <Typography variant="h1" textAlign="center">
                    Добавление студента
                </Typography>
                <EditStudentForm initialStudentBlank={StudentBlank.empty()} />
            </Box>
        </Box>
    )
}

export default AddStudentPage
