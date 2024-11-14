import { IconPosition, IconType } from "@/components/shared/buttons"
import Button from "@/components/shared/buttons/button"
import SpecialitiesTable from "@/components/specialities/specialitiesTable"
import PageUrls from "@/constants/pageUrls"
import { Box, Typography } from "@mui/material"
import { useRouter } from "next/router"

const SpecialitiesPage: React.FC = () => {
    const navigator = useRouter()

    return (
        <Box className="container-fill">
            <Box className="inner-container">
                <Box className="header-container">
                    <Typography variant="h1" sx={{ flex: 1 }} textAlign="center">
                        Специальности
                    </Typography>
                    <Button
                        text="Добавить специальность"
                        onClick={() => navigator.push(PageUrls.AddSpecialities)}
                        icon={{ type: IconType.Add, position: IconPosition.Start }}
                    />
                </Box>
                <SpecialitiesTable />
            </Box>
        </Box>
    )
}

export default SpecialitiesPage
