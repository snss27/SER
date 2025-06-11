import { EditEnterpriseForm } from "@/components/enterprises/editEnterpriseForm"
import { EnterpriseBlank } from "@/domain/enterprises/models/enterpriseBlank"
import { Box, Typography } from "@mui/material"

const AddEnterprisePage = () => {
    return (
        <Box className="container" sx={{ px: 4, pt: 4, g: 2 }}>
            <Typography variant="h1" textAlign="center" gutterBottom>
                Добавление организации
            </Typography>
            <EditEnterpriseForm initialBlank={EnterpriseBlank.empty()} />
        </Box>
    )
}

export default AddEnterprisePage
