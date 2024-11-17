import AdditionalQualificationsTable from "@/components/additionalQualifications/additionalQualificationTable"
import { IconPosition, IconType } from "@/components/shared/buttons"
import Button from "@/components/shared/buttons/button"
import PageUrls from "@/constants/pageUrls"
import { Box, Typography } from "@mui/material"
import { useRouter } from "next/router"

const AdditionalQualificationsPage: React.FC = () => {
    const navigator = useRouter()

    return (
        <Box className="container-fill">
            <Box className="inner-container">
                <Box className="header-container">
                    <Typography variant="h1" sx={{ flex: 1 }} textAlign="center">
                        Дополнительные квалификации
                    </Typography>
                    <Button
                        text="Добавить квалификацию"
                        onClick={() => navigator.push(PageUrls.AddAdditionalQualifications)}
                        icon={{ type: IconType.Add, position: IconPosition.Start }}
                    />
                </Box>
                <AdditionalQualificationsTable />
            </Box>
        </Box>
    )
}

export default AdditionalQualificationsPage
