import {
    Paper,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
} from "@mui/material"
import React from "react"

//TODO Вынести в хук логику работы с таблицей / формой добавления (они одинаковые для всех таблиц)

const StudentsTable: React.FC = () => {
    // const navigator = useRouter()
    // const { showError, showSuccess } = useNotifications()
    // const {
    //     values: students,
    //     lastElementRef,
    //     updateValues,
    // } = useLazyLoad({ paginationFunction: StudentsController.getPage })
    // const confirmDialog = useDialog(ConfirmModal)

    // function handleEditButton(id: string) {
    //     navigator.push(`${PageUrls.EditStudents}/${id}`)
    // }

    // async function handleRemoveButton(id: string) {
    //     const student = students.find((student) => student.id === id) ?? null
    //     if (!student) return

    //     const dialogResult = await confirmDialog.show({
    //         title: `Вы уверены, что хотите удалить студента "${student.formatedFullName}"?`,
    //     })
    //     if (!dialogResult) return

    //     const result = await StudentsController.remove(id)
    //     if (!result.isSuccess) return showError(result.getErrorsString)

    //     await updateValues()

    //     return showSuccess("Студент успешно удален")
    // }

    return (
        <TableContainer elevation={3} component={Paper} sx={{ overflow: "auto", flex: 1 }}>
            {
                <Table stickyHeader>
                    <TableHead>
                        <TableRow sx={{ paddingX: 1 }}>
                            <TableCell sx={{ width: "35%", fontWeight: "bold" }}>
                                Название
                            </TableCell>
                            <TableCell sx={{ width: "35%", fontWeight: "bold" }}>
                                Время обучения
                            </TableCell>
                            <TableCell align="right" sx={{ width: "25%", fontWeight: "bold" }}>
                                Действия
                            </TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {/* {students.map((student, index) => (
                            <TableRow
                                key={student.id}
                                sx={{ paddingX: 1 }}
                                ref={index === students.length - 1 ? lastElementRef : undefined}>
                                <TableCell sx={{ width: "35%" }}>{student.displayName}</TableCell>
                                <TableCell sx={{ width: "35%" }}>
                                    {student.studyPeriodString}
                                </TableCell>
                                <TableCell align="right" sx={{ width: "25%" }}>
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
                        ))} */}
                    </TableBody>
                </Table>
            }
        </TableContainer>
    )
}

export default StudentsTable
