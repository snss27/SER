import { EditEducationLevelForm } from "@/components/educationLevels/editEducationLevelForm"
import PageUrls from "@/constants/pageUrls"
import { EducationLevelsProvider } from "@/domain/educationLevels/educationLevelsProvider"
import { EducationLevelBlank } from "@/domain/educationLevels/models/educationLevelBlank"
import useNotifications from "@/hooks/useNotifications"
import { Box, Typography } from "@mui/material"
import { useParams } from "next/navigation"
import { useRouter } from "next/router"
import { useEffect, useState } from "react"

const EditSpecialityPage = () => {
    const [educationLevelBlank, setEducationLevelBlank] = useState<EducationLevelBlank | null>(null)

    const navigator = useRouter()
    const { showError } = useNotifications()

    const { id } = useParams<{ id: string }>()

    useEffect(() => {
        async function loadEducationLevel() {
            const educationLevel = await EducationLevelsProvider.get(id)
            if (educationLevel === null) {
                showError("Уровень образования не найден")
                navigator.push(PageUrls.EducationLevels)
                return
            }

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
                <EditEducationLevelForm initialBlank={educationLevelBlank} />
            </Box>
        </Box>
    )
}

export default EditSpecialityPage
