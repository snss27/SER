import EditGroupForm from "@/components/groups/editGroupForm"
import { GroupBlank } from "@/domain/groups/models/groupBlank"
import { Box } from "@mui/material"

const AddGroupPage = () => {
    return (
        <Box
            sx={{
                width: "100%",
                height: "100%",
                padding: 2,
                display: "flex",
                justifyContent: "center",
            }}>
            <EditGroupForm initialGroupBlank={GroupBlank.empty()} />
        </Box>
    )
}

export default AddGroupPage
