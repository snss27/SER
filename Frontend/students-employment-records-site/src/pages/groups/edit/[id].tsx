import { GroupBlank } from "@/domain/groups/models/groupBlank"
import { Box, Typography } from "@mui/material"
import { useParams } from "next/navigation"
import React, { useEffect, useState } from "react"
import { GroupsProvider } from "@/domain/groups/groupsProvider"
import { EditGroupForm } from "@/components/groups/editGroupForm"

const EditGroupPage = () => {
    const [groupBlank, setGroupBlank] = useState<GroupBlank | null>(null)

    const { id } = useParams<{ id: string }>()

    useEffect(() => {
        async function loadGroup() {
            const group = await GroupsProvider.get(id)

            setGroupBlank(group.toBlank())
        }

        loadGroup()
    }, [])

    if (groupBlank === null) return null

    return (
        <Box className="container-fill">
            <Box className="edit-page-container">
                <Typography variant="h1" textAlign="center">
                    Редактирование группы
                </Typography>
                <EditGroupForm initialBlank={groupBlank} />
            </Box>
        </Box>
    )
}

export default EditGroupPage
