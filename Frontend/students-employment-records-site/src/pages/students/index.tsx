import { IconPosition, IconType } from "@/components/shared/buttons"
import Button from "@/components/shared/buttons/button"
import StudentsTable from "@/components/students/studentsTable"
import PageUrls from "@/constants/pageUrls"
import { Box, Typography } from "@mui/material"
import { useRouter } from "next/router"

const StudentsPage: React.FC = () => {
    const navigator = useRouter()

    return (
        <Box className="container-fill">
            <Box className="inner-container">
                <Box className="header-container">
                    <Typography variant="h1" sx={{ flex: 1 }} textAlign="center">
                        Студенты
                    </Typography>
                    <Button
                        text="Добавить студента"
                        onClick={() => navigator.push(PageUrls.AddStudents)}
                        icon={{ type: IconType.Add, position: IconPosition.Start }}
                    />
                </Box>
                <StudentsTable />
            </Box>
        </Box>
    )
}

export default StudentsPage
