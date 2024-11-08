import { IconPosition, IconType } from "@/components/shared/buttons"
import Button from "@/components/shared/buttons/button"
import Pages from "@/constants/pages"
import { Box } from "@mui/material"
import { useRouter } from "next/navigation"

const MainPage = () => {
    const navigator = useRouter()

    return (
        <Box>
            <Box sx={{ display: "flex", justifyContent: "flex-end" }}>
                <Button
                    text="Добавить студента"
                    onClick={() => navigator.push(Pages.AddStudent)}
                    icon={{ type: IconType.Add, position: IconPosition.Start }}
                />
            </Box>
            {/* <StudentsTable /> */}
        </Box>
    )
}

export default MainPage
