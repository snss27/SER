import useDebounce from "@/hooks/useDebounce"
import { Autocomplete, AutocompleteRenderInputParams, TextField } from "@mui/material"
import { ClearIcon } from "@mui/x-date-pickers"
import { useState } from "react"

interface Props<T> {
    label: string
    value: T | null
    disabled?: boolean
    noOptionsText?: string
    placeholder?: string
    onChange: (value: T | null) => void
    loadOptions: (searchString: string) => Promise<T[]>
    getOptionLabel: (value: T) => string
    isOptionEqualToValue?: (option: T, value: T) => boolean
    keyExtractor?: (option: T) => string | number
}

interface State<T> {
    searchString: string
    options: T[]
}

namespace State {
    export function empty<T>(): State<T> {
        return {
            searchString: "",
            options: [],
        }
    }
}

//TODO Проверить работу

const AsyncAutocomplete = <T,>(props: Props<T>) => {
    const [state, setState] = useState(State.empty<T>)

    useDebounce(() => loadOptions(), [state.searchString], 300, true)

    async function loadOptions() {
        if (!state.searchString || state.searchString === "") {
            setState((state) => ({ ...state, options: [] }))
        } else {
            const options: T[] = await props.loadOptions(state.searchString)
            setState((state) => ({ ...state, options }))
        }
    }

    function isOptionEqualToValue(option: T, value: T): boolean {
        if (props.isOptionEqualToValue) return props.isOptionEqualToValue(option, value)

        return option === value
    }

    function onChangeSearch(value: string) {
        setState((state) => ({ ...state, searchString: value }))
    }

    function renderInput(params: AutocompleteRenderInputParams) {
        return (
            <TextField
                {...params}
                label={props.label}
                placeholder={props.placeholder}
                autoComplete="password"
                onChange={(event) => onChangeSearch(event.target.value)}
            />
        )
    }

    function getOptions() {
        const options = [...state.options]

        if (
            props.value !== null &&
            options.find((o) => isOptionEqualToValue(o, props.value!)) === undefined
        )
            options.push(props.value)

        return options
    }

    return (
        <Autocomplete
            getOptionLabel={props.getOptionLabel}
            onChange={(_, value) => props.onChange(value)}
            disabled={props.disabled}
            options={getOptions()}
            forcePopupIcon={false}
            isOptionEqualToValue={isOptionEqualToValue}
            renderInput={renderInput}
            value={props.value}
            clearIcon={<ClearIcon />}
            getOptionKey={props.keyExtractor}
            noOptionsText={props.noOptionsText ?? "Ничего не найдено"}
        />
    )
}

export default AsyncAutocomplete
