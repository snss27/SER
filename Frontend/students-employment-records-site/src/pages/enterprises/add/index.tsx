import { Box, Typography } from "@mui/material"
import { EditEnterpriseForm } from "@/components/enterprises/editEnterpriseForm"
import { EnterpriseBlank } from "@/domain/enterprises/models/enterpriseBlank"

const AddEnterprisePage = () => {
    return (
        <Box className="container-fill">
            <Box className="edit-page-container">
                <Typography variant="h1" textAlign="center">
                    Добавление организации
                </Typography>
                <EditEnterpriseForm initialBlank={EnterpriseBlank.empty()} />
            </Box>
        </Box>
    )
}

export default AddEnterprisePage
