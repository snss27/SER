import { Autocomplete, SxProps, TextField, Theme } from "@mui/material"

export interface Props<T> {
    options: T[]
    value: T | null
    label?: string
    disabled?: boolean
    sx?: SxProps<Theme>

    getOptionLabel: (option: T) => string
    onChange: (value: T | null) => void
    isOptionEqualToValue?: (first: T, second: T) => boolean
}

//TODO Возможно лучше написать через TextField с пропсом Select (в документации MUI есть пример)
const Select = <T,>(props: Props<T>) => {
    function isOptionEqualToValue(first: T, second: T) {
        if (props.isOptionEqualToValue) return props.isOptionEqualToValue(first, second)

        return first === second
    }

    return (
        <Autocomplete
            options={props.options}
            value={props.value}
            noOptionsText="Пусто"
            disabled={props.disabled}
            sx={props.sx}
            isOptionEqualToValue={isOptionEqualToValue}
            onChange={(_, value) => props.onChange(value)}
            getOptionLabel={props.getOptionLabel}
            renderInput={(params) => (
                <TextField {...params} label={props.label} autoComplete="password" />
            )}
        />
    )
}

export default Select
