import { Button as CButton, SxProps, Theme } from "@mui/material"
import { ButtonIcon, getIconProps } from "."

interface Props {
    text: string
    href?: string
    variant?: "text" | "contained" | "outlined"
    icon?: ButtonIcon
    sx?: SxProps<Theme>
}

const Button = (props: Props) => {
    const iconProps = typeof props.icon === "object" ? getIconProps(props.icon) : {}

    return (
        <CButton
            href={props.href}
            variant={props.variant ?? "outlined"}
            {...iconProps}
            sx={props.sx}>
            {props.text}
        </CButton>
    )
}

export default Button
