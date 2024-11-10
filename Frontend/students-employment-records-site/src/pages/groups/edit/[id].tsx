import EditGroupForm from "@/components/groups/editGroupForm"
import GroupsProvider from "@/domain/groups/groupsProvider"
import { GroupBlank } from "@/domain/groups/models/groupBlank"
import { Box } from "@mui/material"
import { useParams } from "next/navigation"
import { useEffect, useState } from "react"

const EditGroupPage = () => {
    const [groupBlank, setGroupBlank] = useState<GroupBlank | null>(null)

    const { id } = useParams<{ id: string }>()

    useEffect(() => {
        async function loadGroup() {
            const group = await GroupsProvider.get(id)

            console.log(group)

            setGroupBlank(group.toBlank())
        }

        loadGroup()
    }, [])

    if (groupBlank === null) return null

    return (
        <Box
            sx={{
                width: "100%",
                height: "100%",
                padding: 2,
                display: "flex",
                justifyContent: "center",
            }}>
            <EditGroupForm initialGroupBlank={groupBlank} />
        </Box>
    )
}

export default EditGroupPage
