import { EditEnterpriseForm } from "@/components/enterprises/editEnterpriseForm"
import PageUrls from "@/constants/pageUrls"
import { EnterprisesProvider } from "@/domain/enterprises/enterprisesProvider"
import { EnterpriseBlank } from "@/domain/enterprises/models/enterpriseBlank"
import useNotifications from "@/hooks/useNotifications"
import { Box, Typography } from "@mui/material"
import { useParams } from "next/navigation"
import { useRouter } from "next/router"
import { useEffect, useState } from "react"

const EditEnterprisePage = () => {
    const [enterpriseBlank, setEnterpriseBlank] = useState<EnterpriseBlank | null>(null)
    const navigator = useRouter()
    const { showError } = useNotifications()

    const { id } = useParams<{ id: string }>()

    useEffect(() => {
        async function loadEnterprise() {
            const enterprise = await EnterprisesProvider.get(id)
            if (enterprise === null) {
                showError("Организация не найдена")
                navigator.push(PageUrls.Enterprises)
                return
            }

            setEnterpriseBlank(enterprise.toBlank())
        }

        loadEnterprise()
    }, [])

    if (enterpriseBlank === null) return null

    return (
        <Box className="container" sx={{ px: 4, pt: 4, g: 2 }}>
            <Typography variant="h1" textAlign="center" gutterBottom>
                Редактирование организации
            </Typography>
            <EditEnterpriseForm initialBlank={enterpriseBlank} />
        </Box>
    )
}

export default EditEnterprisePage
