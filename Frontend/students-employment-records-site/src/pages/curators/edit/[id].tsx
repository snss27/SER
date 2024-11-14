import EditCuratorForm from "@/components/curators/editCuratorForm"
import CuratorsProvider from "@/domain/curators/curatorsProvider"
import { CuratorBlank } from "@/domain/curators/models/curatorBlank"
import { Box, Typography } from "@mui/material"
import { useParams } from "next/navigation"
import { useEffect, useState } from "react"

const EditCuratorPage: React.FC = () => {
    const [curatorBlank, setCuratorBlank] = useState<CuratorBlank | null>(null)

    const { id } = useParams<{ id: string }>()

    useEffect(() => {
        async function loadCurator() {
            const curator = await CuratorsProvider.get(id)

            setCuratorBlank(curator.toBlank())
        }

        loadCurator()
    }, [])

    if (curatorBlank === null) return null

    return (
        <Box className="container-fill">
            <Box className="edit-page-container">
                <Typography variant="h1" textAlign="center">
                    Редактирование куратора
                </Typography>
                <EditCuratorForm initialBlank={curatorBlank} />
            </Box>
        </Box>
    )
}

export default EditCuratorPage
