import EditSpecialityForm from "@/components/specialities/editSpecialityForm"
import { SpecialityBlank } from "@/domain/specialities/models/specialityBlank"
import { SpecialitiesProvider } from "@/domain/specialities/specialitiesProvider"
import { Box } from "@mui/material"
import { useParams } from "next/navigation"
import { useEffect, useState } from "react"

const EditSpecialityPage = () => {
    const [specialityBlank, setSpecialityBlank] = useState<SpecialityBlank | null>(null)

    const { id } = useParams<{ id: string }>()

    useEffect(() => {
        async function loadSpeciality() {
            const speciality = await SpecialitiesProvider.get(id)

            setSpecialityBlank(speciality.toBlank())
        }

        loadSpeciality()
    }, [])

    if (specialityBlank === null) return null

    return (
        <Box
            sx={{
                width: "100%",
                height: "100%",
                padding: 2,
                display: "flex",
                justifyContent: "center",
            }}>
            <EditSpecialityForm initialSpecialityBlank={specialityBlank} />
        </Box>
    )
}

export default EditSpecialityPage
