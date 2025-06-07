import { Autocomplete, SxProps, TextField, Theme } from "@mui/material"

type BaseProps<T> = {
    options: T[]
    label?: string
    disabled?: boolean
    sx?: SxProps<Theme>
    defaultValue?: T
    size?: "small" | "medium"
    getOptionLabel: (option: T) => string
    isOptionEqualToValue?: (first: T, second: T) => boolean
}

type RequiredSingleProps<T> = BaseProps<T> & {
    required: true
    multiple?: false | undefined
    value: T
    onChange: (value: T) => void
}

type OptionalSingleProps<T> = BaseProps<T> & {
    required?: false
    multiple?: false | undefined
    value: T | null
    onChange: (value: T | null) => void
}

type RequiredMultipleProps<T> = BaseProps<T> & {
    required: true
    multiple: true
    value: T[]
    onChange: (value: T[]) => void
}

type OptionalMultipleProps<T> = BaseProps<T> & {
    required?: false
    multiple: true
    value: T[]
    onChange: (value: T[]) => void
}

type Props<T> =
    | RequiredSingleProps<T>
    | OptionalSingleProps<T>
    | RequiredMultipleProps<T>
    | OptionalMultipleProps<T>

export const Select = <T,>(props: Props<T>) => {
    function isOptionEqualToValue(first: T, second: T) {
        if (props.isOptionEqualToValue) return props.isOptionEqualToValue(first, second)
        return first === second
    }

    const handleChange = (_: any, value: T | T[] | null) => {
        if (props.multiple) {
            props.onChange(Array.isArray(value) ? value : [])
            return
        }

        if (props.required && !value) {
            props.onChange(props.defaultValue || props.options[0])
            return
        }
        if (props.required) {
            props.onChange(value as T)
        } else {
            props.onChange(value as T | null)
        }
    }

    return (
        <Autocomplete
            options={props.options}
            value={props.value}
            noOptionsText="Пусто"
            disabled={props.disabled}
            sx={props.sx}
            multiple={props.multiple}
            isOptionEqualToValue={isOptionEqualToValue}
            onChange={handleChange}
            getOptionLabel={props.getOptionLabel}
            renderInput={(params) => (
                <TextField {...params} label={props.label} autoComplete="password" />
            )}
            size={props.size}
        />
    )
}
