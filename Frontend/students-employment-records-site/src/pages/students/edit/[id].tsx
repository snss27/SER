import { StudentBlank } from "@/domain/students/models/studentBlank"
import { Box, Typography } from "@mui/material"
import { useParams } from "next/navigation"
import { useEffect, useState } from "react"

const EditStudentPage: React.FC = () => {
    const [studentBlank, setStudentBlank] = useState<StudentBlank | null>(null)

    const { id } = useParams<{ id: string }>()

    useEffect(() => {
        async function loadStudent() {
            // const student = await StudentsProvider.get(id)
            //
            // setStudentBlank(student.toBlank())
        }

        loadStudent()
    }, [])

    if (studentBlank === null) return null

    return (
        <Box className="container-fill">
            <Box className="edit-page-container">
                <Typography variant="h1" textAlign="center">
                    Редактирование студента
                </Typography>
                <EditStudentForm initialStudentBlank={studentBlank} />
            </Box>
        </Box>
    )
}

export default EditStudentPage
