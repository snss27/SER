import { DateRangePicker as CDateRangePicker } from "@mui/x-date-pickers-pro/DateRangePicker"
import { DateRange } from "@mui/x-date-pickers-pro/models"

interface Props {
    value?: DateRange<Date>
    label?: string
    disableFuture?: boolean
    onChange: (value: [Date | null, Date | null]) => void
}

export function DateRangePicker({ value, label, disableFuture = false, onChange }: Props) {
    return (
        <CDateRangePicker
            value={value}
            label={label}
            currentMonthCalendarPosition={1}
            onChange={(value) => onChange(value)}
            slotProps={{
                field: { clearable: true, onClear: () => onChange([null, null]) },
            }}
            disableFuture={disableFuture}
            sx={{ width: "100%" }}
        />
    )
}
