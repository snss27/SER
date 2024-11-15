import PageUrls from "@/constants/pageUrls"
import { StructuralUnits } from "@/domain/groups/enums/structuralUnits"
import GroupsProvider from "@/domain/groups/groupsProvider"
import useDialog from "@/hooks/useDialog/useDialog"
import useLazyLoad from "@/hooks/useLazyLoad"
import useNotifications from "@/hooks/useNotifications"
import {
    Paper,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
} from "@mui/material"
import { useRouter } from "next/router"
import { IconType } from "../shared/buttons"
import IconButton from "../shared/buttons/iconButtons"
import ConfirmModal from "../shared/modals/confirmModal"

const GroupsTable: React.FC = () => {
    const navigator = useRouter()
    const { showError, showSuccess } = useNotifications()
    const {
        values: groups,
        lastElementRef,
        updateValues,
    } = useLazyLoad({ paginationFunction: GroupsProvider.getPage })
    const confirmDialog = useDialog(ConfirmModal)

    function handleEditButton(id: string) {
        navigator.push(`${PageUrls.EditGroups}/${id}`)
    }

    async function handleRemoveButton(id: string) {
        const group = groups.find((group) => group.id === id) ?? null
        if (!group) return

        const dialogResult = await confirmDialog.show({
            title: `Вы уверены, что хотите удалить группу "${group.number}"?`,
        })
        if (!dialogResult) return

        const result = await GroupsProvider.remove(id)
        if (!result.isSuccess) return showError(result.getErrorsString)

        await updateValues()

        return showSuccess("Группа успешно удалена")
    }

    return (
        <TableContainer elevation={3} component={Paper} sx={{ overflow: "auto", flex: 1 }}>
            {
                <Table stickyHeader>
                    <TableHead>
                        <TableRow sx={{ paddingX: 1 }}>
                            <TableCell sx={{ width: "15%", fontWeight: "bold" }}>Номер</TableCell>
                            <TableCell sx={{ width: "15%", fontWeight: "bold" }}>
                                Структурное подразделение
                            </TableCell>
                            <TableCell sx={{ width: "30%", fontWeight: "bold" }}>
                                Специальность
                            </TableCell>
                            <TableCell sx={{ width: "15%", fontWeight: "bold" }}>
                                Год поступления
                            </TableCell>
                            <TableCell sx={{ width: "20%", fontWeight: "bold" }}>Куратор</TableCell>
                            <TableCell align="right" sx={{ width: "15%", fontWeight: "bold" }}>
                                Действия
                            </TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {groups.map((group, index) => (
                            <TableRow
                                key={group.id}
                                sx={{ paddingX: 1 }}
                                ref={index === groups.length - 1 ? lastElementRef : undefined}>
                                <TableCell sx={{ width: "15%" }}>{group.number}</TableCell>
                                <TableCell sx={{ width: "15%" }}>
                                    {StructuralUnits.getDisplayText(group.structuralUnit)}
                                </TableCell>
                                <TableCell sx={{ width: "30%" }}>
                                    {group.speciality?.name ?? "—"}
                                </TableCell>
                                <TableCell sx={{ width: "15%" }}>{group.enrollmentYear}</TableCell>
                                <TableCell sx={{ width: "20%" }}>
                                    {group.curator?.formattedFullName ?? "—"}
                                </TableCell>
                                <TableCell sx={{ width: "15%" }}>
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
            }
        </TableContainer>
    )
}

export default GroupsTable
