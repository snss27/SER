import { EditGroupForm } from "@/components/groups/editGroupForm"
import PageUrls from "@/constants/pageUrls"
import { GroupsProvider } from "@/domain/groups/groupsProvider"
import { GroupBlank } from "@/domain/groups/models/groupBlank"
import useNotifications from "@/hooks/useNotifications"
import { Box, Typography } from "@mui/material"
import { useParams, useRouter } from "next/navigation"
import { useEffect, useState } from "react"

const EditGroupPage = () => {
    const [groupBlank, setGroupBlank] = useState<GroupBlank | null>(null)

    const { showError } = useNotifications()
    const navigator = useRouter()

    const { id } = useParams<{ id: string }>()

    useEffect(() => {
        async function loadGroup() {
            const group = await GroupsProvider.get(id)

            if (group === null) {
                showError("Группа не найдена")
                navigator.push(PageUrls.Groups)
                return
            }

            setGroupBlank(group.toBlank())
        }

        loadGroup()
    }, [])

    if (groupBlank === null) return null

    return (
        <Box className="container" sx={{ px: 4, pt: 4, g: 2 }}>
            <Typography variant="h1" textAlign="center" gutterBottom>
                Редактирование группы
            </Typography>
            <EditGroupForm initialBlank={groupBlank} />
        </Box>
    )
}

export default EditGroupPage
