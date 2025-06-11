import { EditAdditionalQualificationForm } from "@/components/additionalQualifications/editAdditionalQualificationForm"
import { AdditionalQualificationBlank } from "@/domain/additionalQualifications/models/additionalQualificationBlank"
import { Box, Typography } from "@mui/material"
import React from "react"

const AddAdditionalQualificationsPage: React.FC = () => {
    return (
        <Box className="container" sx={{ px: 4, pt: 4, g: 2 }}>
            <Typography variant="h1" textAlign="center" gutterBottom>
                Добавление квалификации
            </Typography>
            <EditAdditionalQualificationForm initialBlank={AdditionalQualificationBlank.empty()} />
        </Box>
    )
}

export default AddAdditionalQualificationsPage
