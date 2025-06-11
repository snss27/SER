import { Button as CButton } from "@mui/material"
import { ButtonIcon, getIconProps } from "."

interface Props {
    text: string
    variant?: "text" | "contained" | "outlined"
    icon?: ButtonIcon
    color?: "error" | "success"
    disabled?: boolean
    onClick: () => void
}

const Button = (props: Props) => {
    const iconProps = typeof props.icon === "object" ? getIconProps(props.icon) : {}

    return (
        <CButton
            variant={props.variant ?? "outlined"}
            onClick={props.onClick}
            color={props.color}
            disabled={props.disabled}
            {...iconProps}>
            {props.text}
        </CButton>
    )
}

export default Button
