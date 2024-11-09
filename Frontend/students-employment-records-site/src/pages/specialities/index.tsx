import { IconPosition, IconType } from "@/components/shared/buttons"
import Button from "@/components/shared/buttons/button"
import PageUrls from "@/constants/pages"
import { Box } from "@mui/material"
import { useRouter } from "next/router"

const SpecialitiesPage = () => {
    const navigator = useRouter()

    return (
        <Box>
            <Box sx={{ display: "flex", justifyContent: "flex-end" }}>
                <Button
                    text="Добавить специальность"
                    onClick={() => navigator.push(PageUrls.AddSpeciality)}
                    icon={{ type: IconType.Add, position: IconPosition.Start }}
                />
            </Box>
        </Box>
    )
}

export default SpecialitiesPage
