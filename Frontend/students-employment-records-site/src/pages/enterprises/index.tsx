import { IconPosition, IconType } from "@/components/shared/buttons"
import Button from "@/components/shared/buttons/button"
import PageUrls from "@/constants/pageUrls"
import { Box, Typography } from "@mui/material"
import { useRouter } from "next/router"
import React from "react"
import { EnterprisesTable } from "@/components/enterprises/enterprisesTable"

const EnterprisesPage: React.FC = () => {
    const navigator = useRouter()

    return (
        <Box className="container-fill">
            <Box className="inner-container">
                <Box className="header-container">
                    <Typography variant="h1" sx={{ flex: 1 }} textAlign="center">
                        Организации
                    </Typography>
                    <Button
                        text="Добавить организацию"
                        onClick={() => navigator.push(PageUrls.AddEnterprise)}
                        icon={{ type: IconType.Add, position: IconPosition.Start }}
                    />
                </Box>
                <EnterprisesTable />
            </Box>
        </Box>
    )
}

export default EnterprisesPage
