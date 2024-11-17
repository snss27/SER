import PageUrls from "@/constants/pageUrls"
import AdditionalQualificationsProvider from "@/domain/additionalQualifications/additionalQualificationsProvider"
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

const AdditionalQualificationsTable: React.FC = () => {
    const navigator = useRouter()
    const { showError, showSuccess } = useNotifications()
    const {
        values: additionalQualifications,
        lastElementRef,
        updateValues,
    } = useLazyLoad({ paginationFunction: AdditionalQualificationsProvider.getPage })
    const confirmDialog = useDialog(ConfirmModal)

    function handleEditButton(id: string) {
        navigator.push(`${PageUrls.EditAdditionalQualifications}/${id}`)
    }

    async function handleRemoveButton(id: string) {
        const additionalQualification =
            additionalQualifications.find((qualification) => qualification.id === id) ?? null
        if (!additionalQualification) return

        const dialogResult = await confirmDialog.show({
            title: `Вы уверены, что хотите удалить квалификацию "${additionalQualification.name}"?`,
        })
        if (!dialogResult) return

        const result = await AdditionalQualificationsProvider.remove(id)
        if (!result.isSuccess) return showError(result.getErrorsString)

        await updateValues()

        return showSuccess("Квалификация успешно удалена")
    }

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
                                Срок обучения
                            </TableCell>
                            <TableCell align="right" sx={{ width: "25%", fontWeight: "bold" }}>
                                Действия
                            </TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {additionalQualifications.map((qualification, index) => (
                            <TableRow
                                key={qualification.id}
                                sx={{ paddingX: 1 }}
                                ref={
                                    index === additionalQualifications.length - 1
                                        ? lastElementRef
                                        : undefined
                                }>
                                <TableCell sx={{ width: "35%" }}>
                                    {qualification.displayName}
                                </TableCell>
                                <TableCell sx={{ width: "35%" }}>
                                    {qualification.studyPeriodString}
                                </TableCell>
                                <TableCell align="right" sx={{ width: "25%" }}>
                                    <IconButton
                                        icon={IconType.Edit}
                                        onClick={() => handleEditButton(qualification.id)}
                                    />
                                    <IconButton
                                        icon={IconType.Delete}
                                        onClick={() => handleRemoveButton(qualification.id)}
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

export default AdditionalQualificationsTable
