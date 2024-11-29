import PageUrls from "@/constants/pageUrls"
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
import React from "react"
import { EnterprisesProvider } from "@/domain/enterprises/enterprisesProvider"

export const EnterprisesTable: React.FC = () => {
    const navigator = useRouter()
    const { showError, showSuccess } = useNotifications()
    const {
        values: enterprises,
        lastElementRef,
        updateValues,
    } = useLazyLoad({ paginationFunction: EnterprisesProvider.getPage })
    const confirmDialog = useDialog(ConfirmModal)

    async function handleEditButton(id: string) {
        await navigator.push(`${PageUrls.EditEnterprise}/${id}`)
    }

    async function handleRemoveButton(id: string) {
        const enterprise = enterprises.find((e) => e.id === id) ?? null
        if (!enterprise) return

        const dialogResult = await confirmDialog.show({
            title: `Вы уверены, что хотите удалить организацию "${enterprise.name}"?`,
        })
        if (!dialogResult) return

        const result = await EnterprisesProvider.remove(id)
        if (!result.isSuccess) return showError(result.getErrorsString)

        await updateValues()

        return showSuccess("Организация успешно удалена")
    }

    return (
        <TableContainer elevation={3} component={Paper} sx={{ overflow: "auto", flex: 1 }}>
            {
                <Table stickyHeader>
                    <TableHead>
                        <TableRow sx={{ paddingX: 1 }}>
                            <TableCell sx={{ width: "75%", fontWeight: "bold" }}>
                                Наименование
                            </TableCell>
                            <TableCell align="right" sx={{ width: "25%", fontWeight: "bold" }}>
                                Действия
                            </TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {enterprises.map((enterprise, index) => (
                            <TableRow
                                key={enterprise.id}
                                sx={{ paddingX: 1 }}
                                ref={index === enterprises.length - 1 ? lastElementRef : undefined}>
                                <TableCell sx={{ width: "75%" }}>{enterprise.name}</TableCell>
                                <TableCell align="right" sx={{ width: "25%" }}>
                                    <IconButton
                                        icon={IconType.Edit}
                                        onClick={() => handleEditButton(enterprise.id)}
                                    />
                                    <IconButton
                                        icon={IconType.Delete}
                                        onClick={() => handleRemoveButton(enterprise.id)}
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
