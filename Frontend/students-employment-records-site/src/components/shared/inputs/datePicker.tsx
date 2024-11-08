import { DatePicker as MDatePicker } from "@mui/x-date-pickers"

interface Props {
    value: Date | null
    label?: string
    onChange: (value: Date | null) => void
}

const DatePicker = (props: Props) => {
    return (
        <MDatePicker
            value={props.value}
            label={props.label}
            maxDate={new Date()}
            views={["year", "month", "day"]}
            openTo="year"
            yearsOrder="desc"
            slotProps={{
                field: { clearable: true, onClear: () => props.onChange(null) },
            }}
            sx={{ width: "100%" }}
            onChange={props.onChange}
        />
    )
}

export default DatePicker
