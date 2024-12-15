import { EditClusterForm } from "@/components/clusters/editClusterForm"
import { ClusterBlank } from "@/domain/clusters/models/clusterBlank"
import { Box, Typography } from "@mui/material"

const AddClustersPage: React.FC = () => {
    return (
        <Box className="container-fill">
            <Box className="edit-page-container">
                <Typography variant="h1" textAlign="center">
                    Добавление кластера
                </Typography>
                <EditClusterForm initialBlank={ClusterBlank.empty()} />
            </Box>
        </Box>
    )
}

export default AddClustersPage
