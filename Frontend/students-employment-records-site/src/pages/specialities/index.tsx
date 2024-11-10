import { IconPosition, IconType } from "@/components/shared/buttons"
import Button from "@/components/shared/buttons/button"
import IconButton from "@/components/shared/buttons/iconButtons"
import PageUrls from "@/constants/pageUrls"
import Speciality from "@/domain/specialities/models/speciality"
import { SpecialitiesProvider } from "@/domain/specialities/specialitiesProvider"
import useNotifications from "@/hooks/useNotifications"
import {
    Box,
    Paper,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    Typography,
} from "@mui/material"
import { useRouter } from "next/router"
import { useEffect, useState } from "react"

//TODO Скролл при большом массиве создаётся к всему экрану
const SpecialitiesPage = () => {
    const [specialities, setSpecialities] = useState<Speciality[]>([])

    const navigator = useRouter()
    const { showError, showSuccess } = useNotifications()

    useEffect(() => {
        loadSpecialities()
    }, [])

    async function loadSpecialities() {
        const specialities = await SpecialitiesProvider.getAll()

        setSpecialities(specialities)
    }

    function handleEditButton(id: string) {
        navigator.push(`${PageUrls.EditSpeciality}/${id}`)
    }

    async function handleRemoveButton(id: string) {
        const result = await SpecialitiesProvider.remove(id)
        if (!result.isSuccess) return showError(result.getErrorString)

        await loadSpecialities()
        return showSuccess("Специальность успешно удалена")
    }

    return (
        <Box sx={{ width: "100%", height: "100%", padding: 2 }}>
            <Box sx={{ width: "70%", margin: "auto" }}>
                <Box
                    sx={{
                        display: "flex",
                        justifyContent: "flex-end",
                        alignItems: "center",
                        marginBottom: 3,
                    }}>
                    <Typography variant="h1" sx={{ flex: 1 }} textAlign="center">
                        Специальности
                    </Typography>
                    <Button
                        text="Добавить специальность"
                        onClick={() => navigator.push(PageUrls.AddSpeciality)}
                        icon={{ type: IconType.Add, position: IconPosition.Start }}
                    />
                </Box>
                <Box sx={{ height: "100%" }}>
                    <TableContainer component={Paper}>
                        <Table stickyHeader>
                            <TableHead>
                                <TableRow>
                                    <TableCell
                                        sx={{ fontWeight: "bold", fontSize: 18, width: "50%" }}>
                                        Название
                                    </TableCell>
                                    <TableCell
                                        align="right"
                                        sx={{ fontWeight: "bold", fontSize: 18, width: "25%" }}>
                                        Время обучение (лет)
                                    </TableCell>
                                    <TableCell
                                        align="right"
                                        sx={{ fontWeight: "bold", fontSize: 18, width: "25%" }}>
                                        Действия
                                    </TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {specialities.map((speciality) => (
                                    <TableRow key={speciality.id}>
                                        <TableCell sx={{ fontSize: 16, width: "50%" }}>
                                            {speciality.name}
                                        </TableCell>
                                        <TableCell
                                            align="right"
                                            sx={{ fontSize: 16, width: "25%" }}>
                                            {speciality.studyYears}
                                        </TableCell>
                                        <TableCell
                                            align="right"
                                            sx={{ fontSize: 16, width: "25%" }}>
                                            <IconButton
                                                icon={IconType.Edit}
                                                onClick={() => handleEditButton(speciality.id)}
                                            />
                                            <IconButton
                                                icon={IconType.Delete}
                                                onClick={() => handleRemoveButton(speciality.id)}
                                            />
                                        </TableCell>
                                    </TableRow>
                                ))}
                            </TableBody>
                        </Table>
                    </TableContainer>
                </Box>
            </Box>
        </Box>
    )
}

export default SpecialitiesPage
