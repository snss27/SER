import useDebounce from "@/hooks/useDebounce"
import { Autocomplete, AutocompleteRenderInputParams, TextField } from "@mui/material"
import { ClearIcon } from "@mui/x-date-pickers"
import { useEffect, useState } from "react"

interface Props<T> {
    value: T | null
    label: string
    disabled?: boolean
    noOptionsText?: string
    placeholder?: string
    onChange: (value: T | null) => void
    loadOptions: (searchString: string) => Promise<T[]>
    getOptionLabel: (value: T) => string
    isOptionEqualToValue?: (option: T, value: T) => boolean
}

interface AutocompleteState<T> {
    selectedOption: T | null
    options: T[]
    searchQuery: string
}

const createInitialState = <T,>(): AutocompleteState<T> => ({
    selectedOption: null,
    options: [],
    searchQuery: "",
})

export const AsyncAutocomplete = <T,>({
    value,
    label,
    disabled,
    noOptionsText = "Ничего не найдено",
    placeholder,
    onChange,
    loadOptions,
    getOptionLabel,
    isOptionEqualToValue,
}: Props<T>) => {
    const [state, setState] = useState(createInitialState<T>())

    const fetchOptions = async () => {
        if (!state.searchQuery) {
            setState((prev) => ({ ...prev, options: [] }))
            return
        }

        const options = await loadOptions(state.searchQuery)
        setState((prev) => ({ ...prev, options }))
    }

    useDebounce(fetchOptions, [state.searchQuery], 300, true)

    useEffect(() => {
        if (value === state.selectedOption) return
        setState((prev) => ({ ...prev, selectedOption: value }))
    }, [value])

    const handleOptionSelect = (option: T | null) => {
        setState((prev) => ({ ...prev, selectedOption: option }))
        onChange(option)
    }

    const handleSearchChange = (query: string) => {
        setState((prev) => ({ ...prev, searchQuery: query }))
    }

    const renderInputField = (params: AutocompleteRenderInputParams) => (
        <TextField
            {...params}
            label={label}
            placeholder={placeholder}
            autoComplete="password"
            onChange={(e) => handleSearchChange(e.target.value)}
        />
    )

    const getFilteredOptions = () => {
        const allOptions = [...state.options]
        if (!state.selectedOption) return allOptions

        return allOptions.includes(state.selectedOption)
            ? allOptions
            : [...allOptions, state.selectedOption]
    }

    return (
        <Autocomplete
            options={getFilteredOptions()}
            value={state.selectedOption}
            disabled={disabled}
            forcePopupIcon={false}
            noOptionsText={noOptionsText}
            onChange={(_, option) => handleOptionSelect(option)}
            getOptionLabel={getOptionLabel}
            renderInput={renderInputField}
            clearIcon={<ClearIcon />}
            isOptionEqualToValue={isOptionEqualToValue}
        />
    )
}
