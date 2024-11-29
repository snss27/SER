import { EditAdditionalQualificationForm } from "@/components/additionalQualifications/editAdditionalQualificationForm"
import { AdditionalQualificationBlank } from "@/domain/additionalQualifications/models/additionalQualificationBlank"
import { Box, Typography } from "@mui/material"
import React from "react"

const AddAdditionalQualificationsPage: React.FC = () => {
    return (
        <Box className="container-fill">
            <Box className="edit-page-container">
                <Typography variant="h1" textAlign="center">
                    Добавление квалификации
                </Typography>
                <EditAdditionalQualificationForm
                    initialBlank={AdditionalQualificationBlank.empty()}
                />
            </Box>
        </Box>
    )
}

export default AddAdditionalQualificationsPage
