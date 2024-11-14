import EditSpecialityForm from "@/components/specialities/editSpecialityForm"
import { SpecialityBlank } from "@/domain/specialities/models/specialityBlank"
import SpecialitiesProvider from "@/domain/specialities/specialitiesProvider"
import { Box, Typography } from "@mui/material"
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
        <Box className="container-fill">
            <Box className="edit-page-container">
                <Typography variant="h1" textAlign="center">
                    Редактирование специальности
                </Typography>
                <EditSpecialityForm initialSpecialityBlank={specialityBlank} />
            </Box>
        </Box>
    )
}

export default EditSpecialityPage
