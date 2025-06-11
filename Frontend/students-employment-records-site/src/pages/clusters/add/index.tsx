import { EditClusterForm } from "@/components/clusters/editClusterForm"
import { ClusterBlank } from "@/domain/clusters/models/clusterBlank"
import { Box, Typography } from "@mui/material"

const AddClustersPage: React.FC = () => {
    return (
        <Box className="container" sx={{ px: 4, pt: 4, g: 2 }}>
            <Typography variant="h1" textAlign="center" gutterBottom>
                Добавление кластера
            </Typography>
            <EditClusterForm initialBlank={ClusterBlank.empty()} />
        </Box>
    )
}

export default AddClustersPage
