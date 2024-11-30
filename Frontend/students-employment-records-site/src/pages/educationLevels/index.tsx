import { IconPosition, IconType } from "@/components/shared/buttons"
import Button from "@/components/shared/buttons/button"
import PageUrls from "@/constants/pageUrls"
import { Box, Typography } from "@mui/material"
import { useRouter } from "next/router"
import React from "react"
import { EducationLevelsTable } from "@/components/educationLevels/educationLevelsTable"

const EducationLevelsPage: React.FC = () => {
    const navigator = useRouter()

    return (
        <Box className="container-fill">
            <Box className="inner-container">
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
        </Box>
    )
}

export default EducationLevelsPage
