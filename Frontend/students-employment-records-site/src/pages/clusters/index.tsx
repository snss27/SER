import { ClustersTable } from "@/components/clusters/clustersTable"
import { IconPosition, IconType } from "@/components/shared/buttons"
import Button from "@/components/shared/buttons/button"
import PageUrls from "@/constants/pageUrls"
import { Box, Typography } from "@mui/material"
import { useRouter } from "next/router"

const ClustersPage: React.FC = () => {
    const navigator = useRouter()

    return (
        <Box className="container" sx={{ p: 4, gap: 2 }}>
            <Box className="header-container">
                <Typography variant="h1" sx={{ flex: 1 }} textAlign="center">
                    Кластеры
                </Typography>
                <Button
                    text="Добавить кластер"
                    onClick={() => navigator.push(PageUrls.AddCluster)}
                    icon={{ type: IconType.Add, position: IconPosition.Start }}
                />
            </Box>
            <ClustersTable />
        </Box>
    )
}

export default ClustersPage
