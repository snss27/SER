import { IconButton as MIconButton, SxProps, Theme } from "@mui/material"
import { MouseEvent } from "react"
import { getIconComponent, IconType } from "."

interface Props {
    icon: IconType
    size?: "small" | "medium" | "large"
    disabled?: boolean
    tooltip?: string
    sx?: SxProps<Theme>
    onClick?: (e: MouseEvent<HTMLButtonElement, globalThis.MouseEvent>) => void
}

const IconButton = (props: Props) => {
    return (
        <MIconButton
            onClick={(e) => (props.onClick ? props.onClick(e) : {})}
            size={props.size ?? "medium"}
            disabled={props.disabled}
            sx={props.sx}>
            {getIconComponent(props.icon)}
        </MIconButton>
    )
}

export default IconButton
