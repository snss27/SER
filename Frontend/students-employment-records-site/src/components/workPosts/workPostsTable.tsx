import PageUrls from "@/constants/pageUrls"
import WorkPostsProvider from "@/domain/workPosts/workPostsProvider"
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

const WorkPostsTable: React.FC = () => {
    const navigator = useRouter()
    const { showError, showSuccess } = useNotifications()
    const {
        values: workPosts,
        lastElementRef,
        updateValues,
    } = useLazyLoad({ paginationFunction: WorkPostsProvider.getPage })
    const confirmDialog = useDialog(ConfirmModal)

    function handleEditButton(id: string) {
        navigator.push(`${PageUrls.EditWorkPosts}/${id}`)
    }

    async function handleRemoveButton(id: string) {
        const workPost = workPosts.find((workPost) => workPost.id === id) ?? null
        if (!workPost) return

        const dialogResult = await confirmDialog.show({
            title: `Вы уверены, что хотите удалить место работы "${workPost.name}"?`,
        })
        if (!dialogResult) return

        const result = await WorkPostsProvider.remove(id)
        if (!result.isSuccess) return showError(result.getErrorsString)

        await updateValues()

        return showSuccess("Место работы успешно удалено")
    }

    return (
        <TableContainer elevation={3} component={Paper} sx={{ overflow: "auto", flex: 1 }}>
            {
                <Table stickyHeader>
                    <TableHead>
                        <TableRow sx={{ paddingX: 1 }}>
                            <TableCell sx={{ width: "75%", fontWeight: "bold" }}>
                                Название
                            </TableCell>
                            <TableCell align="right" sx={{ width: "25%", fontWeight: "bold" }}>
                                Действия
                            </TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {workPosts.map((workPost, index) => (
                            <TableRow
                                key={workPost.id}
                                sx={{ paddingX: 1 }}
                                ref={index === workPosts.length - 1 ? lastElementRef : undefined}>
                                <TableCell sx={{ width: "75%" }}>{workPost.name}</TableCell>
                                <TableCell align="right" sx={{ width: "25%" }}>
                                    <IconButton
                                        icon={IconType.Edit}
                                        onClick={() => handleEditButton(workPost.id)}
                                    />
                                    <IconButton
                                        icon={IconType.Delete}
                                        onClick={() => handleRemoveButton(workPost.id)}
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

export default WorkPostsTable
