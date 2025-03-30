import { EditClusterForm } from "@/components/clusters/editClusterForm"
import PageUrls from "@/constants/pageUrls"
import { ClustersProvider } from "@/domain/clusters/clustersProvider"
import { ClusterBlank } from "@/domain/clusters/models/clusterBlank"
import useNotifications from "@/hooks/useNotifications"
import { Box, Typography } from "@mui/material"
import { useParams } from "next/navigation"
import { useRouter } from "next/router"
import { useEffect, useState } from "react"
const EditClusterPage: React.FC = () => {
    const [clusterBlank, setClusterBlank] = useState<ClusterBlank | null>(null)
    const navigator = useRouter()
    const { showError } = useNotifications()

    const { id } = useParams<{ id: string }>()

    useEffect(() => {
        async function loadCluster() {
            const cluster = await ClustersProvider.get(id)
            if (cluster === null) {
                showError("Кластер не найден")
                navigator.push(PageUrls.Clusters)
                return
            }

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
