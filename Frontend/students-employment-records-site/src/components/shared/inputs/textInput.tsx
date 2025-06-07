import { SxProps, TextField, Theme } from "@mui/material"
import { forwardRef } from "react"

interface Props {
    value: string | null
    label?: string
    placeholder?: string
    size?: "small" | "medium"
    fullWidth?: boolean
    endAdornment?: any
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
            sx,
            onChange,
        }: Props,
        ref: any
    ) => {
        return (
            <TextField
                inputRef={ref}
                label={label ?? "Введите текст"}
                value={value ?? ""}
                variant="outlined"
                placeholder={placeholder}
                size={size}
                autoComplete="password"
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
