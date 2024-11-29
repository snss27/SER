import { Box, Typography } from "@mui/material"
import { useParams } from "next/navigation"
import { useEffect, useState } from "react"
import { EnterpriseBlank } from "@/domain/enterprises/models/enterpriseBlank"
import { EnterprisesProvider } from "@/domain/enterprises/enterprisesProvider"
import { EditEnterpriseForm } from "@/components/enterprises/editEnterpriseForm"

const EditEnterprisePage = () => {
    const [enterpriseBlank, setEnterpriseBlank] = useState<EnterpriseBlank | null>(null)

    const { id } = useParams<{ id: string }>()

    useEffect(() => {
        async function loadEnterprise() {
            const enterprise = await EnterprisesProvider.get(id)

            setEnterpriseBlank(enterprise.toBlank())
        }

        loadEnterprise()
    }, [])

    if (enterpriseBlank === null) return null

    return (
        <Box className="container-fill">
            <Box className="edit-page-container">
                <Typography variant="h1" textAlign="center">
                    Редактирование организации
                </Typography>
                <EditEnterpriseForm initialBlank={enterpriseBlank} />
            </Box>
        </Box>
    )
}

export default EditEnterprisePage
