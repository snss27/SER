import { Button as CButton } from "@mui/material"
import { ButtonIcon, getIconProps } from "."

interface Props {
    text: string
    variant?: "text" | "contained" | "outlined"
    icon?: ButtonIcon
    onClick: () => void
}

const Button = (props: Props) => {
    const iconProps = typeof props.icon === "object" ? getIconProps(props.icon) : {}

    return (
        <CButton variant={props.variant ?? "outlined"} {...iconProps} onClick={props.onClick}>
            {props.text}
        </CButton>
    )
}

export default Button
