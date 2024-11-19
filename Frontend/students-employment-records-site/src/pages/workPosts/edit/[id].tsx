import EditWorkPostForm from "@/components/workPosts/editWorkPostForm"
import { WorkPostBlank } from "@/domain/workPosts/models/workPostBlank"
import WorkPostsProvider from "@/domain/workPosts/workPostsProvider"
import { Box, Typography } from "@mui/material"
import { useParams } from "next/navigation"
import { useEffect, useState } from "react"

const EditWorkPostPage = () => {
    const [workPostBlank, setWorkPostBlank] = useState<WorkPostBlank | null>(null)

    const { id } = useParams<{ id: string }>()

    useEffect(() => {
        async function loadWorkPost() {
            const workPost = await WorkPostsProvider.get(id)

            setWorkPostBlank(workPost.toBlank())
        }

        loadWorkPost()
    }, [])

    if (workPostBlank === null) return null

    return (
        <Box className="container-fill">
            <Box className="edit-page-container">
                <Typography variant="h1" textAlign="center">
                    Редактирование места работы
                </Typography>
                <EditWorkPostForm initialBlank={workPostBlank} />
            </Box>
        </Box>
    )
}

export default EditWorkPostPage
