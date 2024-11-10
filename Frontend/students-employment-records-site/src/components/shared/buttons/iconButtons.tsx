import { IconButton as MIconButton } from "@mui/material"
import { getIconComponent, IconType } from "."

interface Props {
    icon: IconType
    onClick: () => void
}

const IconButton = (props: Props) => {
    return <MIconButton onClick={props.onClick}>{getIconComponent(props.icon)}</MIconButton>
}

export default IconButton
