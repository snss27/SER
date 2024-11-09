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

export const NumberInput = (props: IProps) => {
    function getNumberDecimalPlaces(value: number) {
        return value.toString().split(".")[1]?.length || 0
    }

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
            type="number"
            label={props.label}
            fullWidth
            onChange={onChange}
            value={props.value?.toString() || ""}
            variant="outlined"
        />
    )
}
