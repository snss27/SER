import { EditStudentForm } from "@/components/students/editStudentForm"
import { StudentBlank } from "@/domain/students/models/studentBlank"
import { StudentsProvider } from "@/domain/students/studentsProvider"
import { Box, Typography } from "@mui/material"
import { useParams } from "next/navigation"
import { useEffect, useState } from "react"

const EditStudentPage: React.FC = () => {
    const [studentBlank, setStudentBlank] = useState<StudentBlank | null>(null)

    const { id } = useParams<{ id: string }>()

    useEffect(() => {
        async function loadStudent() {
            const student = await StudentsProvider.get(id)

            setStudentBlank(student.toBlank())
        }

        loadStudent()
    }, [])

    if (studentBlank === null) return null

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
                Редактирование студента
            </Typography>
            <EditStudentForm initialStudentBlank={studentBlank} />
        </Box>
    )
}

export default EditStudentPage
