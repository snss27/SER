import { AsyncDialogProps } from "@/hooks/useDialog/types"
import { Dialog, DialogActions, DialogTitle } from "@mui/material"
import { IconPosition, IconType } from "../buttons"
import Button from "../buttons/button"
import IconButton from "../buttons/iconButtons"

interface Props {
    title: string
}

const ConfirmModal: React.FC<AsyncDialogProps<Props, boolean>> = ({ open, handleClose, data }) => {
    const { title } = data

    return (
        <Dialog open={open} onClose={() => handleClose(false)} fullWidth maxWidth="sm">
            <DialogTitle
                fontWeight="bold"
                sx={{ borderBottom: "1px solid #c9c9c9", paddingRight: 6.5 }}>
                {title}
                <IconButton
                    icon={IconType.Close}
                    onClick={() => handleClose(false)}
                    sx={{
                        position: "absolute",
                        right: 8,
                        top: 8,
                    }}
                />
            </DialogTitle>
            <DialogActions
                sx={{
                    borderTop: "1px solid #c9c9c9",
                    marginTop: "auto",
                    marginBottom: 0,
                    padding: 2,
                }}>
                <Button
                    text="Да"
                    color="success"
                    onClick={() => handleClose(true)}
                    icon={{ type: IconType.Check, position: IconPosition.Start }}
                />
                <Button
                    text="Нет"
                    color="error"
                    onClick={() => handleClose(false)}
                    icon={{ type: IconType.Cancel, position: IconPosition.Start }}
                />
            </DialogActions>
        </Dialog>
    )
}

export default ConfirmModal
