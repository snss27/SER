import { NeverUnreachable } from "@/tools/neverUreachable"
import AddIcon from "@mui/icons-material/Add"
import EditIcon from "@mui/icons-material/Edit"
import SaveIcon from "@mui/icons-material/Save"

export enum IconPosition {
    Start,
    End,
}

export enum IconType {
    Save,
    Edit,
    Add,
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

    function getIconComponent(type: IconType): JSX.Element {
        switch (type) {
            case IconType.Save:
                return <SaveIcon />
            case IconType.Edit:
                return <EditIcon />
            case IconType.Add:
                return <AddIcon />
            default:
                throw new NeverUnreachable(type)
        }
    }
}
