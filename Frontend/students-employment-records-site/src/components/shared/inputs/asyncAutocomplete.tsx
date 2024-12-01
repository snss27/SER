import useDebounce from "@/hooks/useDebounce"
import { Autocomplete, AutocompleteRenderInputParams, TextField } from "@mui/material"
import { ClearIcon } from "@mui/x-date-pickers"
import React, { useEffect, useState } from "react"

interface Props<T extends { id: string }> {
    value: string | null
    label: string
    disabled?: boolean
    noOptionsText?: string
    placeholder?: string
    onChange: (value: string | null) => void
    loadOptions: (searchString: string) => Promise<T[]>
    loadOption: (id: string) => Promise<T>
    getOptionLabel: (value: T) => string
}

interface State<T extends { id: string }> {
    currentOption: T | null
    options: T[]
    searchString: string
}

namespace State {
    export function empty<T extends { id: string }>(): State<T> {
        return {
            currentOption: null,
            options: [],
            searchString: "",
        }
    }
}

export const AsyncAutocomplete = <T extends { id: string }>(props: Props<T>) => {
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

    useEffect(() => {
        async function loadValue() {
            if (props.value === state.currentOption?.id) return

            if (props.value === null)
                return setState((prevState) => ({ ...prevState, currentOption: null }))

            const value = await props.loadOption(props.value)
            setState((prevState) => ({ ...state, currentOption: value }))
        }

        loadValue()
    }, [props.value])

    async function onSelectOption(option: T | null) {
        setState((prevState) => ({ ...state, currentOption: option }))
        props.onChange(option?.id ?? null)
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

        const currentOption = state.currentOption
        if (currentOption === null) return options

        if (!options.some((o) => o.id === currentOption.id)) options.push(currentOption)

        return options
    }

    return (
        <Autocomplete
            options={getOptions()}
            value={state.currentOption}
            disabled={props.disabled}
            forcePopupIcon={false}
            noOptionsText={props.noOptionsText ?? "Ничего не найдено"}
            onChange={(_, selectedOption) => onSelectOption(selectedOption)}
            getOptionLabel={props.getOptionLabel}
            renderInput={renderInput}
            clearIcon={<ClearIcon />}
            getOptionKey={(option) => option.id}
            isOptionEqualToValue={(option, value) => option.id === value.id}
        />
    )
}
