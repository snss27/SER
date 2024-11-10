import { TextField } from "@mui/material"
import { ChangeEvent } from "react"

export interface IProps {
    value: number | null
    label?: string
    min?: number
    max?: number
    step?: number
    onChange: (value: number | null) => void
}
//TODO Можно вводить букву e
export const NumberInput = (props: IProps) => {
    function onChange(event: ChangeEvent<HTMLInputElement>) {
        const inputValue = event.currentTarget.value.replace(",", ".")
        if (inputValue === "") return props.onChange(null)

        const value = parseInt(inputValue)
        if (isNaN(value)) return props.onChange(null)

        if (props.min != null && value < props.min) return
        if (props.max != null && value > props.max) return

        props.onChange(value)
    }

    return (
        <TextField
            value={props.value?.toString() || ""}
            type="number"
            label={props.label}
            fullWidth
            variant="outlined"
            autoComplete="new-password"
            onChange={onChange}
        />
    )
}
