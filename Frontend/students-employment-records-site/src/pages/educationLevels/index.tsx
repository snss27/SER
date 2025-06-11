import { EducationLevelsTable } from "@/components/educationLevels/educationLevelsTable"
import { IconPosition, IconType } from "@/components/shared/buttons"
import Button from "@/components/shared/buttons/button"
import PageUrls from "@/constants/pageUrls"
import { Box, Typography } from "@mui/material"
import { useRouter } from "next/router"
import React from "react"

const EducationLevelsPage: React.FC = () => {
    const navigator = useRouter()

    return (
        <Box className="container" sx={{ p: 4, gap: 2 }}>
            <Box className="header-container">
                <Typography variant="h1" sx={{ flex: 1 }} textAlign="center">
                    Уровни образования
                </Typography>
                <Button
                    text="Добавить уровень образования"
                    onClick={() => navigator.push(PageUrls.AddEducationLevel)}
                    icon={{ type: IconType.Add, position: IconPosition.Start }}
                />
            </Box>
            <EducationLevelsTable />
        </Box>
    )
}

export default EducationLevelsPage
