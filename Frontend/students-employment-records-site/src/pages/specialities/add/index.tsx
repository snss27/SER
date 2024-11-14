import EditSpecialityForm from "@/components/specialities/editSpecialityForm"
import { SpecialityBlank } from "@/domain/specialities/models/specialityBlank"
import { Box, Typography } from "@mui/material"

const AddSpecialityPage = () => {
    return (
        <Box className="container-fill">
            <Box className="edit-page-container">
                <Typography variant="h1" textAlign="center">
                    Добавление специальности
                </Typography>
                <EditSpecialityForm initialSpecialityBlank={SpecialityBlank.empty()} />
            </Box>
        </Box>
    )
}

export default AddSpecialityPage
