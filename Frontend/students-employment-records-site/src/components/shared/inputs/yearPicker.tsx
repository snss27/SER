import { DatePicker } from "@mui/x-date-pickers"

interface Props {
    value: number | null
    label?: string
    onChange: (value: number | null) => void
}

const YearPicker = (props: Props) => {
    function handleChange(value: Date | null) {
        if (value === null) return props.onChange(null)

        props.onChange(value.getFullYear())
    }

    return (
        <DatePicker
            value={props.value !== null ? new Date(props.value, 5) : null}
            label={props.label}
            maxDate={new Date()}
            views={["year"]}
            openTo="year"
            yearsOrder="desc"
            slotProps={{
                field: { clearable: true, onClear: () => props.onChange(null) },
            }}
            sx={{ width: "100%" }}
            onChange={handleChange}
        />
    )
}

export default YearPicker
