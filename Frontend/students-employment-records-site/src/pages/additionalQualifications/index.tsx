import { AdditionalQualificationsTable } from "@/components/additionalQualifications/additionalQualificationsTable"
import { IconPosition, IconType } from "@/components/shared/buttons"
import Button from "@/components/shared/buttons/button"
import PageUrls from "@/constants/pageUrls"
import { Box, Typography } from "@mui/material"
import { useRouter } from "next/router"
import React from "react"

const AdditionalQualificationsPage: React.FC = () => {
    const navigator = useRouter()

    return (
        <Box className="container" sx={{ p: 4, gap: 2 }}>
            <Box className="header-container">
                <Typography variant="h1" sx={{ flex: 1 }} textAlign="center">
                    Дополнительные квалификации
                </Typography>
                <Button
                    text="Добавить квалификацию"
                    onClick={() => navigator.push(PageUrls.AddAdditionalQualification)}
                    icon={{ type: IconType.Add, position: IconPosition.Start }}
                />
            </Box>
            <AdditionalQualificationsTable />
        </Box>
    )
}

export default AdditionalQualificationsPage
