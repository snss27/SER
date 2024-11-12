import { IconPosition, IconType } from "@/components/shared/buttons"
import Button from "@/components/shared/buttons/button"
import IconButton from "@/components/shared/buttons/iconButtons"
import PageUrls from "@/constants/pageUrls"
import { StructuralUnits } from "@/domain/groups/enums/structuralUnits"
import GroupsProvider from "@/domain/groups/groupsProvider"
import Group from "@/domain/groups/models/group"
import Speciality from "@/domain/specialities/models/speciality"
import SpecialitiesProvider from "@/domain/specialities/specialitiesProvider"
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
import { useRouter } from "next/navigation"
import { useLayoutEffect, useState } from "react"

const GroupsPage = () => {
    const [groups, setGroups] = useState<Group[]>([])
    const [specialities, setSpecialities] = useState<Speciality[]>([])

    const navigator = useRouter()
    const { showError, showSuccess } = useNotifications()

    useLayoutEffect(() => {
        async function loadData() {
            const [groups, specialities] = await Promise.all([
                GroupsProvider.getAll(),
                SpecialitiesProvider.getAll(),
            ])

            setGroups(groups)
            setSpecialities(specialities)
        }

        loadData()
    }, [])

    async function loadGroups() {
        const groups = await GroupsProvider.getAll()

        setGroups(groups)
    }

    function handleEditButton(id: string) {
        navigator.push(`${PageUrls.EditGroup}/${id}`)
    }

    async function handleRemoveButton(id: string) {
        const result = await GroupsProvider.remove(id)
        if (!result.isSuccess) return showError(result.getErrorsString)

        await loadGroups()
        return showSuccess("Группа успешно удалена")
    }

    console.log(groups)

    return (
        <Box sx={{ width: "100%", height: "100%", padding: 2 }}>
            <Box
                sx={{
                    display: "flex",
                    justifyContent: "flex-end",
                    alignItems: "center",
                    marginBottom: 3,
                }}>
                <Typography variant="h1" sx={{ flex: 1 }} textAlign="center">
                    Группы
                </Typography>
                <Button
                    text="Добавить группу"
                    onClick={() => navigator.push(PageUrls.AddGroup)}
                    icon={{ type: IconType.Add, position: IconPosition.Start }}
                />
            </Box>
            <Box sx={{ height: "100%" }}>
                <TableContainer component={Paper}>
                    <Table stickyHeader>
                        <TableHead>
                            <TableRow>
                                <TableCell sx={{ fontWeight: "bold", fontSize: 18 }}>
                                    Номер
                                </TableCell>
                                <TableCell sx={{ fontWeight: "bold", fontSize: 18 }}>
                                    Структурное подразделение
                                </TableCell>
                                <TableCell sx={{ fontWeight: "bold", fontSize: 18 }}>
                                    Специальность
                                </TableCell>
                                <TableCell sx={{ fontWeight: "bold", fontSize: 18 }}>
                                    Год поступления
                                </TableCell>
                                <TableCell align="right" sx={{ fontWeight: "bold", fontSize: 18 }}>
                                    Куратор
                                </TableCell>
                                <TableCell align="right" sx={{ fontWeight: "bold", fontSize: 18 }}>
                                    Действия
                                </TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {groups.map((group) => (
                                <TableRow key={group.id}>
                                    <TableCell sx={{ fontSize: 16 }}>{group.number}</TableCell>
                                    <TableCell sx={{ fontSize: 16 }}>
                                        {StructuralUnits.getDisplayText(group.structuralUnit)}
                                    </TableCell>
                                    <TableCell sx={{ fontSize: 16 }}>
                                        {
                                            specialities.find(
                                                (speciality) => group.specialityId === speciality.id
                                            )?.name
                                        }
                                    </TableCell>
                                    <TableCell sx={{ fontSize: 16 }}>
                                        {group.enrollmentYear}
                                    </TableCell>
                                    <TableCell align="right" sx={{ fontSize: 16 }}>
                                        {group.curatorName}
                                    </TableCell>
                                    <TableCell align="right" sx={{ fontSize: 16 }}>
                                        <IconButton
                                            icon={IconType.Edit}
                                            onClick={() => handleEditButton(group.id)}
                                        />
                                        <IconButton
                                            icon={IconType.Delete}
                                            onClick={() => handleRemoveButton(group.id)}
                                        />
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            </Box>
        </Box>
    )
}

export default GroupsPage
