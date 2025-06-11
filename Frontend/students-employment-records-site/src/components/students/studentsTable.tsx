import PageUrls from "@/constants/pageUrls"
import { PAGE_SIZE } from "@/constants/pagination"
import { StudentsFilter } from "@/domain/students/models/studentsFilter"
import { StudentsProvider } from "@/domain/students/studentsProvider"
import useDebounce from "@/hooks/useDebounce"
import useDialog from "@/hooks/useDialog/useDialog"
import useLazyLoad from "@/hooks/useLazyLoad"
import useNotifications from "@/hooks/useNotifications"
import {
    Box,
    InputAdornment,
    Paper,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
} from "@mui/material"
import { useRouter } from "next/router"
import React, { useEffect, useReducer, useRef, useState } from "react"
import { IconType } from "../shared/buttons"
import IconButton from "../shared/buttons/iconButtons"
import TextInput from "../shared/inputs/textInput"
import ConfirmModal from "../shared/modals/confirmModal"
import { StudentsFilterModal } from "./modals/studentsFilterModal"

export const StudentsTable: React.FC = () => {
    const navigator = useRouter()
    const { showError, showSuccess } = useNotifications()
    const {
        values: students,
        isLoading,
        lastElementRef,
        refresh,
    } = useLazyLoad({ load: loadStudents })
    const confirmDialog = useDialog(ConfirmModal)
    const filterDialog = useDialog(StudentsFilterModal)
    const didMountRef = useRef(false)

    const [searchText, setSearchText] = useState("")
    const [studentsFilter, dispatch] = useReducer(StudentsFilter.reducer, {
        ...StudentsFilter.empty(),
        searchText,
    })
    const studentsFilterRef = useRef(studentsFilter)

    async function loadStudents(page: number) {
        return await StudentsProvider.getPage(page, PAGE_SIZE, studentsFilterRef.current)
    }

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

        refresh()

        return showSuccess("Студент успешно удален")
    }

    async function handleOpenFilters() {
        const result = await filterDialog.show({ initialFilter: studentsFilter })

        if (!result.isAccepted) return

        dispatch({ type: "SET", payload: { studentsFilter: result.studentsFilter } })
    }

    useEffect(() => {
        studentsFilterRef.current = studentsFilter
        if (!didMountRef.current) {
            didMountRef.current = true
            return
        }
        refresh()
    }, [studentsFilter])

    useDebounce(
        () => {
            dispatch({
                type: "CHANGE_SEARCH_TEXT",
                payload: { searchText },
            })
        },
        [searchText],
        800,
        true
    )

    return (
        <Box sx={{ display: "flex", flexDirection: "column", flex: 1 }}>
            <Box sx={{ display: "flex", justifyContent: "flex-end", pb: 2 }}>
                <TextInput
                    label="Поиск"
                    size="small"
                    value={searchText}
                    onChange={setSearchText}
                    fullWidth={false}
                    endAdornment={
                        <InputAdornment position="start" sx={{ mr: -1.5 }}>
                            <IconButton icon={IconType.Settings} onClick={handleOpenFilters} />
                        </InputAdornment>
                    }
                />
            </Box>
            <TableContainer elevation={3} component={Paper} sx={{ overflow: "auto", flex: 1 }}>
                <Table stickyHeader>
                    <TableHead>
                        <TableRow>
                            <TableCell sx={{ width: "25%", fontWeight: "bold" }}>ФИО</TableCell>
                            <TableCell sx={{ width: "20%", fontWeight: "bold" }}>Группа</TableCell>
                            <TableCell sx={{ width: "15%", fontWeight: "bold" }}>
                                Целевое обучение?
                            </TableCell>
                            <TableCell sx={{ width: "20%", fontWeight: "bold" }}>
                                Иностранный гражданин?
                            </TableCell>
                            <TableCell sx={{ width: "10%", fontWeight: "bold" }}>
                                Платное обучение?
                            </TableCell>
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
                                <TableCell sx={{ width: "25%" }}>{student.displayName}</TableCell>
                                <TableCell sx={{ width: "20%" }}>
                                    {student.group.displayName}
                                </TableCell>
                                <TableCell sx={{ width: "15%" }}>
                                    {student.isTargetAgreement ? "Да" : "Нет"}
                                </TableCell>
                                <TableCell sx={{ width: "20%" }}>
                                    {student.isForeignCitizen ? "Да" : "Нет"}
                                </TableCell>
                                <TableCell sx={{ width: "10%" }}>
                                    {student.isOnPaidStudy ? "Да" : "Нет"}
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
        </Box>
    )
}
