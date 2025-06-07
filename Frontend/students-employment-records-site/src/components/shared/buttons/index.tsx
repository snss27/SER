import { NeverUnreachable } from "@/tools/neverUreachable"
import AddIcon from "@mui/icons-material/Add"
import ArrowBackIcon from "@mui/icons-material/ArrowBack"
import CancelIcon from "@mui/icons-material/Cancel"
import CheckIcon from "@mui/icons-material/Check"
import CloseIcon from "@mui/icons-material/Close"
import ContentCopyIcon from "@mui/icons-material/ContentCopy"
import DeleteIcon from "@mui/icons-material/Delete"
import DownloadIcon from "@mui/icons-material/Download"
import EditIcon from "@mui/icons-material/Edit"
import SaveIcon from "@mui/icons-material/Save"
import SettingsIcon from "@mui/icons-material/Settings"

export enum IconPosition {
    Start,
    End,
}

export enum IconType {
    Save,
    Edit,
    Add,
    Back,
    Delete,
    Close,
    Check,
    Cancel,
    Copy,
    Download,
    Settings,
}

export type ButtonIcon = { type: IconType; position?: IconPosition }

export function getIconProps({ type, position = IconPosition.End }: ButtonIcon) {
    switch (position) {
        case IconPosition.End:
            return { endIcon: getIconComponent(type) }
        case IconPosition.Start:
            return { startIcon: getIconComponent(type) }
        default:
            throw new NeverUnreachable(position)
    }
}

export function getIconComponent(type: IconType): JSX.Element {
    switch (type) {
        case IconType.Save:
            return <SaveIcon />
        case IconType.Edit:
            return <EditIcon />
        case IconType.Add:
            return <AddIcon />
        case IconType.Back:
            return <ArrowBackIcon />
        case IconType.Delete:
            return <DeleteIcon />
        case IconType.Check:
            return <CheckIcon />
        case IconType.Close:
            return <CloseIcon />
        case IconType.Cancel:
            return <CancelIcon />
        case IconType.Copy:
            return <ContentCopyIcon />
        case IconType.Download:
            return <DownloadIcon />
        case IconType.Settings:
            return <SettingsIcon />
        default:
            throw new NeverUnreachable(type)
    }
}
