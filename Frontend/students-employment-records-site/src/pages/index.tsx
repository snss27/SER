import { IconPosition, IconType } from "@/components/shared/buttons"
import Button from "@/components/shared/buttons/button"
import { Box } from "@mui/material"

const MainPage = () => {
    return (
        <Box>
            <Box sx={{ display: "flex", justifyContent: "flex-end" }}>
                <Button
                    text="Добавить студента"
                    href="/student/edit"
                    icon={{ type: IconType.Add, position: IconPosition.Start }}
                />
            </Box>
            {/* <StudentsTable /> */}
        </Box>
    )
}

export default MainPage
