import useDebounce from "@/hooks/useDebounce"
import { Autocomplete, AutocompleteRenderInputParams, Chip, TextField } from "@mui/material"
import { ClearIcon } from "@mui/x-date-pickers"
import { useEffect, useState } from "react"

interface Props<T extends { id: string }> {
    values: string[]
    label: string
    disabled?: boolean
    noOptionsText?: string
    placeholder?: string
    maxSelections?: number
    onChange: (values: string[]) => void
    loadOptions: (searchString: string) => Promise<T[]>
    loadOption: (id: string) => Promise<T>
    getOptionLabel: (value: T) => string
}

interface State<T extends { id: string }> {
    selectedOptions: T[]
    options: T[]
    searchString: string
    isLoading: boolean
}

namespace State {
    export function empty<T extends { id: string }>(): State<T> {
        return {
            selectedOptions: [],
            options: [],
            searchString: "",
            isLoading: false,
        }
    }
}

export const MultiAsyncAutocomplete = <T extends { id: string }>(props: Props<T>) => {
    const [state, setState] = useState(State.empty<T>())

    useDebounce(
        () => {
            if (state.searchString) {
                loadOptions()
            }
        },
        [state.searchString],
        300,
        true
    )

    async function loadOptions() {
        setState((prev) => ({ ...prev, isLoading: true }))
        try {
            const options = await props.loadOptions(state.searchString)
            setState((prev) => ({
                ...prev,
                options: options.filter(
                    (opt) => !prev.selectedOptions.some((o) => o.id === opt.id)
                ),
                isLoading: false,
            }))
        } catch (error) {
            setState((prev) => ({ ...prev, isLoading: false }))
        }
    }

    useEffect(() => {
        async function loadInitialValues() {
            if (!props.values) {
                setState((prev) => ({ ...prev, selectedOptions: [] }))
                return
            }

            setState((prev) => ({ ...prev, isLoading: true }))
            try {
                const loadedOptions = await Promise.all(
                    props.values.map((id) => props.loadOption(id))
                )
                setState((prev) => ({
                    ...prev,
                    selectedOptions: loadedOptions,
                    isLoading: false,
                }))
            } catch (error) {
                setState((prev) => ({ ...prev, isLoading: false }))
            }
        }

        loadInitialValues()
    }, [props.values])

    const handleSelect = (_: any, newValues: T[]) => {
        if (props.maxSelections && newValues.length > props.maxSelections) return

        setState((prev) => ({
            ...prev,
            selectedOptions: newValues,
            options: prev.options.filter((opt) => !newValues.some((o) => o.id === opt.id)),
        }))

        props.onChange(newValues.map((v) => v.id))
    }

    const handleSearchChange = (value: string) => {
        setState((prev) => ({ ...prev, searchString: value }))
    }

    const renderInput = (params: AutocompleteRenderInputParams) => (
        <TextField
            {...params}
            label={props.label}
            placeholder={props.placeholder}
            onChange={(e) => handleSearchChange(e.target.value)}
        />
    )

    const renderTags = (selected: T[], getTagProps: any) =>
        selected.map((option, index) => (
            <Chip
                {...getTagProps({ index })}
                key={option.id}
                label={props.getOptionLabel(option)}
                disabled={props.disabled}
            />
        ))

    const allOptions = [
        ...state.selectedOptions,
        ...state.options.filter((opt) => !state.selectedOptions.some((o) => o.id === opt.id)),
    ]

    return (
        <Autocomplete
            multiple
            options={allOptions}
            value={state.selectedOptions}
            disabled={props.disabled}
            loading={state.isLoading}
            noOptionsText={props.noOptionsText ?? "Ничего не найдено"}
            getOptionLabel={props.getOptionLabel}
            onChange={handleSelect}
            onInputChange={(_, value) => handleSearchChange(value)}
            renderInput={renderInput}
            renderTags={renderTags}
            filterSelectedOptions
            isOptionEqualToValue={(option, value) => option.id === value.id}
            clearIcon={<ClearIcon />}
            componentsProps={{
                clearIndicator: {
                    sx: { visibility: props.disabled ? "hidden" : "visible" },
                },
            }}
        />
    )
}
