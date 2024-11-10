import { TextField } from "@mui/material"
import { forwardRef } from "react"

interface Props {
    value: string | null
    label?: string
    placeholder?: string
    onChange: (value: string) => void
}

const TextInput = forwardRef((props: Props, ref: any) => {
    return (
        //TODO Временное решение (наверное). props autoComplete не решает проблему, как и slotProps. Видимо не работает :)
        <TextField
            inputRef={ref}
            label={props.label ?? "Введите текст"}
            value={props.value ?? ""}
            variant="outlined"
            placeholder={props.placeholder}
            autoComplete="password"
            fullWidth
            onChange={(event) => props.onChange(event.target.value)}
        />
    )
})

export default TextInput
