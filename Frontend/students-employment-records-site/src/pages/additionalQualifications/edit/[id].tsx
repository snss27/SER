import { EditAdditionalQualificationForm } from "@/components/additionalQualifications/editAdditionalQualificationForm"
import PageUrls from "@/constants/pageUrls"
import { AdditionalQualificationsProvider } from "@/domain/additionalQualifications/additionalQualificationsProvider"
import { AdditionalQualificationBlank } from "@/domain/additionalQualifications/models/additionalQualificationBlank"
import useNotifications from "@/hooks/useNotifications"
import { Box, Typography } from "@mui/material"
import { useParams } from "next/navigation"
import { useRouter } from "next/router"
import React, { useEffect, useState } from "react"

const EditAdditionalQualificationsPage: React.FC = () => {
    const [additionalQualificationBlank, setAdditionalQualificationBlank] =
        useState<AdditionalQualificationBlank | null>(null)
    const { showError } = useNotifications()
    const navigator = useRouter()

    const { id } = useParams<{ id: string }>()

    useEffect(() => {
        async function loadAdditionalQualification() {
            const additionalQualification = await AdditionalQualificationsProvider.get(id)

            if (additionalQualification === null) {
                showError("Квалификация не найдена")
                navigator.push(PageUrls.AdditionalQualifications)
                return
            }

            setAdditionalQualificationBlank(additionalQualification.toBlank())
        }

        loadAdditionalQualification()
    }, [])

    if (additionalQualificationBlank === null) return null

    return (
        <Box className="container" sx={{ px: 4, pt: 4, g: 2 }}>
            <Typography variant="h1" textAlign="center" gutterBottom>
                Редактирование квалификации
            </Typography>
            <EditAdditionalQualificationForm initialBlank={additionalQualificationBlank} />
        </Box>
    )
}

export default EditAdditionalQualificationsPage
