import PageUrls from "@/constants/pageUrls"
import SpecialitiesProvider from "@/domain/specialities/specialitiesProvider"
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

const SpecialitiesTable: React.FC = () => {
    const navigator = useRouter()
    const { showError, showSuccess } = useNotifications()
    const {
        values: specialities,
        lastElementRef,
        updateValues,
    } = useLazyLoad({ paginationFunction: SpecialitiesProvider.getPage })
    const confirmDialog = useDialog(ConfirmModal)

    function handleEditButton(id: string) {
        navigator.push(`${PageUrls.EditSpecialities}/${id}`)
    }

    async function handleRemoveButton(id: string) {
        const speciality = specialities.find((speciality) => speciality.id === id) ?? null
        if (!speciality) return

        const dialogResult = await confirmDialog.show({
            title: `Вы уверены, что хотите удалить специальность "${speciality.name}"?`,
        })
        if (!dialogResult) return

        const result = await SpecialitiesProvider.remove(id)
        if (!result.isSuccess) return showError(result.getErrorsString)

        await updateValues()

        return showSuccess("Специальность успешно удалена")
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
                                Время обучения
                            </TableCell>
                            <TableCell align="right" sx={{ width: "25%", fontWeight: "bold" }}>
                                Действия
                            </TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {specialities.map((speciality, index) => (
                            <TableRow
                                key={speciality.id}
                                sx={{ paddingX: 1 }}
                                ref={
                                    index === specialities.length - 1 ? lastElementRef : undefined
                                }>
                                <TableCell sx={{ width: "35%" }}>
                                    {speciality.displayName}
                                </TableCell>
                                <TableCell sx={{ width: "35%" }}>
                                    {speciality.studyPeriodString}
                                </TableCell>
                                <TableCell align="right" sx={{ width: "25%" }}>
                                    <IconButton
                                        icon={IconType.Edit}
                                        onClick={() => handleEditButton(speciality.id)}
                                    />
                                    <IconButton
                                        icon={IconType.Delete}
                                        onClick={() => handleRemoveButton(speciality.id)}
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

export default SpecialitiesTable
