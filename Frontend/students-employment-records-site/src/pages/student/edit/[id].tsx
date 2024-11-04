import { Box } from "@mui/material"
import { useRouter } from "next/router"

const EditStudent = () => {
    const router = useRouter()
    const { id } = router.query

    return <Box>Edit students id = {id}</Box>
}

export default EditStudent
