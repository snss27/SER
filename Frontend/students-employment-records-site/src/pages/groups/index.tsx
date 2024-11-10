import { IconPosition, IconType } from "@/components/shared/buttons"
import Button from "@/components/shared/buttons/button"
import PageUrls from "@/constants/pageUrls"
import { Box } from "@mui/material"
import { useRouter } from "next/navigation"

const GroupsPage = () => {
    const navigator = useRouter()

    return (
        <Box>
            <Box sx={{ display: "flex", justifyContent: "flex-end" }}>
                <Button
                    text="Добавить группу"
                    onClick={() => navigator.push(PageUrls.AddGroup)}
                    icon={{ type: IconType.Add, position: IconPosition.Start }}
                />
            </Box>
        </Box>
    )
}

export default GroupsPage
