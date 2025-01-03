import { EditStudentForm } from "@/components/students/editStudentForm"
import { StudentBlank } from "@/domain/students/models/studentBlank"
import { Box, Typography } from "@mui/material"

const AddStudentPage = () => {
    return (
        <Box
            sx={{
                width: "100%",
                height: "100%",
                display: "flex",
                flexDirection: "column",
                gap: 1.5,
            }}>
            <Typography variant="h1" textAlign="center">
                Добавление студента
            </Typography>
            <EditStudentForm initialStudentBlank={StudentBlank.empty()} />
        </Box>
    )
}

export default AddStudentPage
