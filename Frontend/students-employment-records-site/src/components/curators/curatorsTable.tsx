"use client"

import PageUrls from "@/constants/pageUrls"
import CuratorsProvider from "@/domain/curators/curatorsProvider"
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

const CuratorsTable: React.FC = () => {
    const navigator = useRouter()
    const { showError, showSuccess } = useNotifications()
    const {
        values: curators,
        lastElementRef,
        updateValues,
    } = useLazyLoad({ paginationFunction: CuratorsProvider.getPage })
    const confirmDialog = useDialog(ConfirmModal)

    function handleEditButton(id: string) {
        navigator.push(`${PageUrls.EditCurators}/${id}`)
    }

    async function handleRemoveButton(id: string) {
        const curator = curators.find((curator) => curator.id === id) ?? null
        if (!curator) return

        const dialogResult = await confirmDialog.show({
            title: `Вы уверены, что хотите удалить куратора "${curator.formattedFullName}"?`,
        })
        if (!dialogResult) return

        const result = await CuratorsProvider.remove(id)
        if (!result.isSuccess) return showError(result.getErrorsString)

        await updateValues()

        return showSuccess("Куратор успешно удалён")
    }

    return (
        <TableContainer elevation={3} component={Paper} sx={{ overflow: "auto", flex: 1 }}>
            {
                <Table stickyHeader>
                    <TableHead>
                        <TableRow sx={{ paddingX: 1 }}>
                            <TableCell sx={{ width: "75%", fontWeight: "bold" }}>ФИО</TableCell>
                            <TableCell align="right" sx={{ width: "25%", fontWeight: "bold" }}>
                                Действия
                            </TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {curators.map((curator, index) => (
                            <TableRow
                                key={index}
                                sx={{ paddingX: 1 }}
                                ref={index === curators.length - 1 ? lastElementRef : undefined}>
                                <TableCell sx={{ width: "75%" }}>
                                    {curator.formattedFullName}
                                </TableCell>
                                <TableCell align="right" sx={{ width: "25%" }}>
                                    <IconButton
                                        icon={IconType.Edit}
                                        onClick={() => handleEditButton(curator.id)}
                                    />
                                    <IconButton
                                        icon={IconType.Delete}
                                        onClick={() => handleRemoveButton(curator.id)}
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

export default CuratorsTable
