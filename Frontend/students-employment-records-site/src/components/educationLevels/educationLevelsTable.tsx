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
import { EducationLevelsProvider } from "@/domain/educationLevels/educationLevelsProvider"
import { EducationLevelTypes } from "@/domain/educationLevels/enums/EducationLevelTypes"

export const EducationLevelsTable: React.FC = () => {
    const navigator = useRouter()
    const { showError, showSuccess } = useNotifications()
    const {
        values: educationLevels,
        lastElementRef,
        updateValues,
    } = useLazyLoad({ paginationFunction: EducationLevelsProvider.getPage })
    const confirmDialog = useDialog(ConfirmModal)

    async function handleEditButton(id: string) {
        await navigator.push(`${PageUrls.EditEducationLevel}/${id}`)
    }

    async function handleRemoveButton(id: string) {
        const educationLevel = educationLevels.find((e) => e.id === id) ?? null
        if (!educationLevel) return

        const dialogResult = await confirmDialog.show({
            title: `Вы уверены, что хотите удалить уровень образования "${educationLevel.name}"?`,
        })
        if (!dialogResult) return

        const result = await EducationLevelsProvider.remove(id)
        if (!result.isSuccess) return showError(result.getErrorsString)

        await updateValues()

        return showSuccess("Уровень образования успешно удалён")
    }

    return (
        <TableContainer elevation={3} component={Paper} sx={{ overflow: "auto", flex: 1 }}>
            {
                <Table stickyHeader>
                    <TableHead>
                        <TableRow sx={{ paddingX: 1 }}>
                            <TableCell sx={{ width: "20%", fontWeight: "bold" }}>Тип</TableCell>
                            <TableCell sx={{ width: "30%", fontWeight: "bold" }}>
                                Наименование
                            </TableCell>
                            <TableCell sx={{ width: "30%", fontWeight: "bold" }}>
                                Срок обучения
                            </TableCell>
                            <TableCell align="right" sx={{ width: "20%", fontWeight: "bold" }}>
                                Действия
                            </TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {educationLevels.map((educationLevel, index) => (
                            <TableRow
                                key={educationLevel.id}
                                sx={{ paddingX: 1 }}
                                ref={
                                    index === educationLevels.length - 1
                                        ? lastElementRef
                                        : undefined
                                }>
                                <TableCell sx={{ width: "20%" }}>
                                    {EducationLevelTypes.displayName(educationLevel.type)}
                                </TableCell>
                                <TableCell sx={{ width: "30%" }}>
                                    {educationLevel.displayName}
                                </TableCell>
                                <TableCell sx={{ width: "30%" }}>
                                    {educationLevel.displayTime}
                                </TableCell>
                                <TableCell align="right" sx={{ width: "20%" }}>
                                    <IconButton
                                        icon={IconType.Edit}
                                        onClick={() => handleEditButton(educationLevel.id)}
                                    />
                                    <IconButton
                                        icon={IconType.Delete}
                                        onClick={() => handleRemoveButton(educationLevel.id)}
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
