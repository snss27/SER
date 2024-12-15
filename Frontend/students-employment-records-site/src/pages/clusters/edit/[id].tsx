import { EditClusterForm } from "@/components/clusters/editClusterForm"
import { ClustersProvider } from "@/domain/clusters/clustersProvider"
import { ClusterBlank } from "@/domain/clusters/models/clusterBlank"
import { Box, Typography } from "@mui/material"
import { useParams } from "next/navigation"
import { useEffect, useState } from "react"

const EditClusterPage: React.FC = () => {
    const [clusterBlank, setClusterBlank] = useState<ClusterBlank | null>(null)

    const { id } = useParams<{ id: string }>()

    useEffect(() => {
        async function loadCluster() {
            const cluster = await ClustersProvider.get(id)

            setClusterBlank(cluster.toBlank())
        }

        loadCluster()
    }, [])

    if (clusterBlank === null) return null

    return (
        <Box className="container-fill">
            <Box className="edit-page-container">
                <Typography variant="h1" textAlign="center">
                    Редактирование кластера
                </Typography>
                <EditClusterForm initialBlank={clusterBlank} />
            </Box>
        </Box>
    )
}

export default EditClusterPage
