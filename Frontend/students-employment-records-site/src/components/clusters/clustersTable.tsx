import PageUrls from "@/constants/pageUrls"
import { ClustersProvider } from "@/domain/clusters/clustersProvider"
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

export const ClustersTable: React.FC = () => {
    const navigator = useRouter()
    const { showError, showSuccess } = useNotifications()
    const {
        values: clusters,
        lastElementRef,
        updateValues,
    } = useLazyLoad({ paginationFunction: ClustersProvider.getPage })
    const confirmDialog = useDialog(ConfirmModal)

    async function handleEditButton(id: string) {
        await navigator.push(`${PageUrls.EditCluster}/${id}`)
    }

    async function handleRemoveButton(id: string) {
        const cluster = clusters.find((c) => c.id === id) ?? null
        if (!cluster) return

        const dialogResult = await confirmDialog.show({
            title: `Вы уверены, что хотите удалить кластер "${cluster.name}"?`,
        })
        if (!dialogResult) return

        const result = await ClustersProvider.remove(id)
        if (!result.isSuccess) return showError(result.getErrorsString)

        await updateValues()

        return showSuccess("Кластер успешно удален")
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
                        {clusters.map((cluster, index) => (
                            <TableRow
                                key={cluster.id}
                                sx={{ paddingX: 1 }}
                                ref={index === clusters.length - 1 ? lastElementRef : undefined}>
                                <TableCell sx={{ width: "75%" }}>{cluster.name}</TableCell>
                                <TableCell align="right" sx={{ width: "25%" }}>
                                    <IconButton
                                        icon={IconType.Edit}
                                        onClick={() => handleEditButton(cluster.id)}
                                    />
                                    <IconButton
                                        icon={IconType.Delete}
                                        onClick={() => handleRemoveButton(cluster.id)}
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
