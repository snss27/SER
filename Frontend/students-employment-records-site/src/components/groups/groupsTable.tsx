import PageUrls from "@/constants/pageUrls"
import { PAGE_SIZE } from "@/constants/pagination"
import { StructuralUnits } from "@/domain/groups/enums/structuralUnits"
import { GroupsProvider } from "@/domain/groups/groupsProvider"
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
import React from "react"
import { IconType } from "../shared/buttons"
import IconButton from "../shared/buttons/iconButtons"
import ConfirmModal from "../shared/modals/confirmModal"

export const GroupsTable: React.FC = () => {
    const navigator = useRouter()
    const { showError, showSuccess } = useNotifications()
    const { values: groups, isLoading, lastElementRef, refresh } = useLazyLoad({ load: loadGroups })
    const confirmDialog = useDialog(ConfirmModal)

    async function loadGroups(page: number) {
        return await GroupsProvider.getPage(page, PAGE_SIZE)
    }

    async function handleEditButton(id: string) {
        await navigator.push(`${PageUrls.EditGroup}/${id}`)
    }

    async function handleRemoveButton(id: string) {
        const group = groups.find((g) => g.id === id) ?? null
        if (!group) return

        const dialogResult = await confirmDialog.show({
            title: `Вы уверены, что хотите удалить группу "${group.number}"?`,
        })
        if (!dialogResult) return

        const result = await GroupsProvider.remove(id)
        if (!result.isSuccess) return showError(result.getErrorsString)

        refresh()

        return showSuccess("Группа успешно удалена")
    }

    return (
        <TableContainer elevation={3} component={Paper} sx={{ overflow: "auto", flex: 1 }}>
            {
                <Table stickyHeader>
                    <TableHead>
                        <TableRow sx={{ paddingX: 1 }}>
                            <TableCell sx={{ fontWeight: "bold" }}>Номер</TableCell>
                            <TableCell sx={{ fontWeight: "bold", width: "20%" }}>
                                Структурное подразделение
                            </TableCell>
                            <TableCell sx={{ fontWeight: "bold" }}>Уровень образования</TableCell>
                            <TableCell sx={{ fontWeight: "bold" }}>Год поступления</TableCell>
                            <TableCell sx={{ fontWeight: "bold" }}>Куратор</TableCell>
                            <TableCell align="right" sx={{ fontWeight: "bold" }}>
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
                                <TableCell>{group.number}</TableCell>
                                <TableCell>
                                    {StructuralUnits.getDisplayText(group.structuralUnit)}
                                </TableCell>
                                <TableCell>{group.educationLevel?.displayName ?? "—"}</TableCell>
                                <TableCell>{group.enrollmentYear}</TableCell>
                                <TableCell>{group.curator?.displayName ?? "—"}</TableCell>
                                <TableCell align="right">
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
