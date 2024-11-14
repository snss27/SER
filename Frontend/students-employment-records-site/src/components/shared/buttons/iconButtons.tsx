import { IconButton as MIconButton, SxProps, Theme } from "@mui/material"
import { getIconComponent, IconType } from "."

interface Props {
    icon: IconType
    onClick: () => void
    sx?: SxProps<Theme>
}

const IconButton = (props: Props) => {
    return (
        <MIconButton onClick={props.onClick} sx={props.sx}>
            {getIconComponent(props.icon)}
        </MIconButton>
    )
}

export default IconButton
