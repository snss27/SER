import { Box, Typography } from "@mui/material"
import { useParams } from "next/navigation"
import { useEffect, useState } from "react"
import { EducationLevelBlank } from "@/domain/educationLevels/models/educationLevelBlank"
import { EducationLevelsProvider } from "@/domain/educationLevels/educationLevelsProvider"
import { EditEducationLevelForm } from "@/components/educationLevels/editEducationLevelForm"

const EditSpecialityPage = () => {
    const [educationLevelBlank, setEducationLevelBlank] = useState<EducationLevelBlank | null>(null)

    const { id } = useParams<{ id: string }>()

    useEffect(() => {
        async function loadEducationLevel() {
            const educationLevel = await EducationLevelsProvider.get(id)

            setEducationLevelBlank(educationLevel.toBlank())
        }

        loadEducationLevel()
    }, [])

    if (educationLevelBlank === null) return null

    return (
        <Box className="container-fill">
            <Box className="edit-page-container">
                <Typography variant="h1" textAlign="center">
                    Редактирование уровня образования
                </Typography>
                <EditEducationLevelForm initialSpecialityBlank={educationLevelBlank} />
            </Box>
        </Box>
    )
}

export default EditSpecialityPage
