"use client"

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
import React from "react"
import { IconType } from "../shared/buttons"
import IconButton from "../shared/buttons/iconButtons"
import ConfirmModal from "../shared/modals/confirmModal"
import { EmployeesProvider } from "@/domain/employees/employeesProvider"

export const EmployeesTable: React.FC = () => {
    const navigator = useRouter()
    const { showError, showSuccess } = useNotifications()
    const {
        values: employees,
        lastElementRef,
        updateValues,
    } = useLazyLoad({ paginationFunction: EmployeesProvider.getPage })
    const confirmDialog = useDialog(ConfirmModal)

    async function handleEditButton(id: string) {
        await navigator.push(`${PageUrls.EditEmployee}/${id}`)
    }

    async function handleRemoveButton(id: string) {
        const employee = employees.find((e) => e.id === id) ?? null
        if (!employee) return

        const dialogResult = await confirmDialog.show({
            title: `Вы уверены, что хотите удалить сотрудника "${employee.displayName}"?`,
        })
        if (!dialogResult) return

        const result = await EmployeesProvider.remove(id)
        if (!result.isSuccess) return showError(result.getErrorsString)

        await updateValues()

        return showSuccess("Сотрудник успешно удалён")
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
                        {employees.map((employee, index) => (
                            <TableRow
                                key={employee.id}
                                sx={{ paddingX: 1 }}
                                ref={index === employees.length - 1 ? lastElementRef : undefined}>
                                <TableCell sx={{ width: "75%" }}>{employee.displayName}</TableCell>
                                <TableCell align="right" sx={{ width: "25%" }}>
                                    <IconButton
                                        icon={IconType.Edit}
                                        onClick={() => handleEditButton(employee.id)}
                                    />
                                    <IconButton
                                        icon={IconType.Delete}
                                        onClick={() => handleRemoveButton(employee.id)}
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
