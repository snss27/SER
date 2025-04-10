import PageUrls from "@/constants/pageUrls"
import { StudentsProvider } from "@/domain/students/studentsProvider"
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

export const StudentsTable: React.FC = () => {
    const navigator = useRouter()
    const { showError, showSuccess } = useNotifications()
    const {
        values: students,
        lastElementRef,
        updateValues,
    } = useLazyLoad({ paginationFunction: StudentsProvider.getPage })
    const confirmDialog = useDialog(ConfirmModal)

    async function handleEditButton(id: string) {
        await navigator.push(`${PageUrls.EditStudents}/${id}`)
    }

    async function handleRemoveButton(id: string) {
        const student = students.find((student) => student.id === id) ?? null
        if (!student) return

        const dialogResult = await confirmDialog.show({
            title: `Вы уверены, что хотите удалить студента "${student.displayName}"?`,
        })
        if (!dialogResult) return

        const result = await StudentsProvider.remove(id)
        if (!result.isSuccess) return showError(result.getErrorsString)

        await updateValues()

        return showSuccess("Студент успешно удален")
    }

    return (
        <TableContainer elevation={3} component={Paper} sx={{ overflow: "auto", flex: 1 }}>
            <Table stickyHeader>
                <TableHead>
                    <TableRow sx={{ paddingX: 1 }}>
                        <TableCell sx={{ width: "25%", fontWeight: "bold" }}>ФИО</TableCell>
                        <TableCell sx={{ width: "20%", fontWeight: "bold" }}>Группа</TableCell>
                        <TableCell sx={{ width: "15%", fontWeight: "bold" }}>Телефон</TableCell>
                        <TableCell sx={{ width: "20%", fontWeight: "bold" }}>Почта</TableCell>
                        <TableCell sx={{ width: "10%", fontWeight: "bold" }}>Статус</TableCell>
                        <TableCell align="right" sx={{ width: "10%", fontWeight: "bold" }}>
                            Действия
                        </TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {students.map((student, index) => (
                        <TableRow
                            key={student.id}
                            sx={{ paddingX: 1 }}
                            ref={index === students.length - 1 ? lastElementRef : undefined}>
                            <TableCell sx={{ width: "25%" }}>
                                {student.lastName} {student.name} {student.secondName}
                            </TableCell>
                            <TableCell sx={{ width: "20%" }}>{student.group.number}</TableCell>
                            <TableCell sx={{ width: "15%" }}>{student.phoneNumber}</TableCell>
                            <TableCell sx={{ width: "20%" }}>{student.mail}</TableCell>
                            <TableCell sx={{ width: "10%" }}>
                                {student.isOnPaidStudy ? "Платное" : "Бюджетное"}
                            </TableCell>
                            <TableCell align="right" sx={{ width: "10%" }}>
                                <IconButton
                                    icon={IconType.Edit}
                                    onClick={() => handleEditButton(student.id)}
                                />
                                <IconButton
                                    icon={IconType.Delete}
                                    onClick={() => handleRemoveButton(student.id)}
                                />
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    )
}
