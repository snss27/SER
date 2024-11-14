import CuratorsTable from "@/components/curators/curatorsTable"
import { IconPosition, IconType } from "@/components/shared/buttons"
import Button from "@/components/shared/buttons/button"
import PageUrls from "@/constants/pageUrls"
import { Box, Typography } from "@mui/material"
import { useRouter } from "next/router"
import React from "react"

const CuratorsPage: React.FC = () => {
    const navigator = useRouter()

    return (
        <Box className="container-fill">
            <Box className="inner-container">
                <Box className="header-container">
                    <Typography variant="h1" sx={{ flex: 1 }} textAlign="center">
                        Кураторы
                    </Typography>
                    <Button
                        text="Добавить куратора"
                        onClick={() => navigator.push(PageUrls.AddCurators)}
                        icon={{ type: IconType.Add, position: IconPosition.Start }}
                    />
                </Box>
                <CuratorsTable />
            </Box>
        </Box>
    )
}

export default CuratorsPage
