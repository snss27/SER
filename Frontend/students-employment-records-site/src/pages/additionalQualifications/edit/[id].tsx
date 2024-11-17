import EditAdditionalQualificationForm from "@/components/additionalQualifications/editAdditionalQualificationForm"
import AdditionalQualificationsProvider from "@/domain/additionalQualifications/additionalQualificationsProvider"
import { AdditionalQualificationBlank } from "@/domain/additionalQualifications/models/additionalQualificationBlank"
import { Box, Typography } from "@mui/material"
import { useParams } from "next/navigation"
import { useEffect, useState } from "react"

const EditAdditionalQualificationsPage: React.FC = () => {
    const [additionalQualificationBlank, setAdditionalQualificationBlank] =
        useState<AdditionalQualificationBlank | null>(null)

    const { id } = useParams<{ id: string }>()

    useEffect(() => {
        async function loadAdditionalQualifications() {
            const additionalQualification = await AdditionalQualificationsProvider.get(id)

            setAdditionalQualificationBlank(additionalQualification.toBlank())
        }

        loadAdditionalQualifications()
    }, [])

    if (additionalQualificationBlank === null) return null

    return (
        <Box className="container-fill">
            <Box className="edit-page-container">
                <Typography variant="h1" textAlign="center">
                    Редактирование квалификации
                </Typography>
                <EditAdditionalQualificationForm initialBlank={additionalQualificationBlank} />
            </Box>
        </Box>
    )
}

export default EditAdditionalQualificationsPage
