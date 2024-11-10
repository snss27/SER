import CloseIcon from "@mui/icons-material/Close"
import { IconButton } from "@mui/material"
import { useSnackbar } from "notistack"

const useNotifications = () => {
    const { enqueueSnackbar, closeSnackbar } = useSnackbar()

    function showSuccess(message: string) {
        const snackbarId = enqueueSnackbar(message, {
            variant: "success",
            anchorOrigin: { vertical: "top", horizontal: "right" },
            action: (
                <IconButton
                    sx={{ color: "#fff" }}
                    size="small"
                    onClick={() => closeSnackbar(snackbarId)}>
                    <CloseIcon fontSize="small" />
                </IconButton>
            ),
        })
    }

    function showInfo(message: string) {
        const snackbarId = enqueueSnackbar(message, {
            variant: "info",
            anchorOrigin: { vertical: "top", horizontal: "right" },
            action: (
                <IconButton
                    sx={{ color: "#fff" }}
                    size="small"
                    onClick={() => closeSnackbar(snackbarId)}>
                    <CloseIcon fontSize="small" />
                </IconButton>
            ),
        })
    }

    function showError(message: string) {
        const snackbarId = enqueueSnackbar(message, {
            variant: "error",
            anchorOrigin: { vertical: "top", horizontal: "right" },
            action: (
                <IconButton
                    sx={{ color: "#fff" }}
                    size="small"
                    onClick={() => closeSnackbar(snackbarId)}>
                    <CloseIcon fontSize="small" />
                </IconButton>
            ),
        })
    }

    return { showSuccess, showInfo, showError }
}

export default useNotifications
