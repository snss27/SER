import { SxProps, TextField, Theme } from "@mui/material"
import { forwardRef, HTMLInputTypeAttribute } from "react"

interface Props {
    value: string | null
    label?: string
    placeholder?: string
    size?: "small" | "medium"
    fullWidth?: boolean
    endAdornment?: any
    type?: HTMLInputTypeAttribute
    sx?: SxProps<Theme>
    onChange: (value: string) => void
}

const TextInput = forwardRef(
    (
        {
            value,
            label,
            placeholder,
            size = "medium",
            fullWidth = true,
            endAdornment,
            type,
            sx,
            onChange,
        }: Props,
        ref: any
    ) => {
        return (
            <TextField
                type={type}
                autoComplete="current-password"
                inputRef={ref}
                label={label ?? "Введите текст"}
                value={value ?? ""}
                variant="outlined"
                placeholder={placeholder}
                size={size}
                fullWidth={fullWidth}
                onChange={(event) => onChange(event.target.value)}
                slotProps={{
                    input: {
                        endAdornment,
                    },
                }}
                sx={sx}
            />
        )
    }
)

export default TextInput
