import useDebounce from "@/hooks/useDebounce"
import { Autocomplete, AutocompleteRenderInputParams, Chip, TextField } from "@mui/material"
import { ClearIcon } from "@mui/x-date-pickers"
import { useEffect, useState } from "react"

type BaseProps<T> = {
    label: string
    disabled?: boolean
    noOptionsText?: string
    placeholder?: string
    loadOptions: (searchString: string) => Promise<T[]>
    getOptionLabel: (value: T) => string
    isOptionEqualToValue?: (option: T, value: T) => boolean
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
    maxSelections?: number
}

type OptionalMultipleProps<T> = BaseProps<T> & {
    required?: false
    multiple: true
    value: T[]
    onChange: (value: T[]) => void
    maxSelections?: number
}

type Props<T> =
    | RequiredSingleProps<T>
    | OptionalSingleProps<T>
    | RequiredMultipleProps<T>
    | OptionalMultipleProps<T>

interface AutocompleteState<T> {
    selectedOptions: T | T[]
    options: T[]
    searchQuery: string
    isLoading: boolean
}

export const AsyncAutocomplete = <T,>(props: Props<T>) => {
    const [state, setState] = useState<AutocompleteState<T>>({
        selectedOptions: props.multiple ? [] : (null as unknown as T),
        options: [],
        searchQuery: "",
        isLoading: false,
    })

    const fetchOptions = async () => {
        if (!state.searchQuery) {
            setState((prev) => ({ ...prev, options: [] }))
            return
        }

        setState((prev) => ({ ...prev, isLoading: true }))
        try {
            const options = await props.loadOptions(state.searchQuery)
            setState((prev) => ({
                ...prev,
                options: Array.isArray(prev.selectedOptions)
                    ? options.filter(
                          (opt) =>
                              !(prev.selectedOptions as T[]).some((o) =>
                                  props.isOptionEqualToValue
                                      ? props.isOptionEqualToValue(o, opt)
                                      : o === opt
                              )
                      )
                    : options,
                isLoading: false,
            }))
        } catch (error) {
            setState((prev) => ({ ...prev, isLoading: false }))
        }
    }

    useDebounce(fetchOptions, [state.searchQuery], 300, true)

    useEffect(() => {
        if (props.value === state.selectedOptions) return
        setState((prev) => ({ ...prev, selectedOptions: props.value as T | T[] }))
    }, [props.value])

    const handleOptionSelect = (newValue: T | T[]) => {
        if (props.multiple && "maxSelections" in props && props.maxSelections) {
            if (Array.isArray(newValue) && newValue.length > props.maxSelections) return
        }

        setState((prev) => ({ ...prev, selectedOptions: newValue }))
        props.onChange(newValue as any)
    }

    const handleSearchChange = (query: string) => {
        setState((prev) => ({ ...prev, searchQuery: query }))
    }

    const renderInputField = (params: AutocompleteRenderInputParams) => (
        <TextField
            {...params}
            label={props.label}
            placeholder={props.placeholder}
            autoComplete="password"
            onChange={(e) => handleSearchChange(e.target.value)}
        />
    )

    const renderTags = (selected: T[], getTagProps: any) =>
        selected.map((option, index) => (
            <Chip
                {...getTagProps({ index })}
                key={index}
                label={props.getOptionLabel(option)}
                disabled={props.disabled}
            />
        ))

    const getFilteredOptions = () => {
        const allOptions = [...state.options]
        if (!state.selectedOptions) return allOptions

        if (Array.isArray(state.selectedOptions)) {
            return allOptions
        }

        return allOptions.includes(state.selectedOptions)
            ? allOptions
            : [...allOptions, state.selectedOptions]
    }

    return (
        <Autocomplete
            options={getFilteredOptions()}
            value={props.value}
            disabled={props.disabled}
            loading={state.isLoading}
            forcePopupIcon={false}
            noOptionsText={props.noOptionsText ?? "Ничего не найдено"}
            onChange={(_, option) => handleOptionSelect(option as T | T[])}
            getOptionLabel={props.getOptionLabel}
            renderInput={renderInputField}
            renderTags={props.multiple ? renderTags : undefined}
            multiple={props.multiple}
            clearIcon={<ClearIcon />}
            isOptionEqualToValue={props.isOptionEqualToValue}
            componentsProps={{
                clearIndicator: {
                    sx: { visibility: props.disabled ? "hidden" : "visible" },
                },
            }}
        />
    )
}
