import EditSpecialityForm from "@/components/specialities/editSpecialityForm"
import { SpecialityBlank } from "@/domain/specialities/models/specialityBlank"
import { Box } from "@mui/material"

const AddSpecialityPage = () => {
    return (
        <Box
            sx={{
                width: "100%",
                height: "100%",
                padding: 2,
                display: "flex",
                justifyContent: "center",
            }}>
            <EditSpecialityForm initialSpecialityBlank={SpecialityBlank.empty()} />
        </Box>
    )
}

export default AddSpecialityPage
