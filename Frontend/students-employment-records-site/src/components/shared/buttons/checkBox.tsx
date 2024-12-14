import { FormControlLabel, Checkbox as MCheckBox } from "@mui/material"

interface Props {
    value: boolean
    label: string
    onChange: (isChecked: boolean) => void
}

const CheckBox = (props: Props) => {
    return (
        <FormControlLabel
            control={
                <MCheckBox
                    checked={props.value}
                    onChange={(_, isChecked) => props.onChange(isChecked)}
                />
            }
            label={props.label}
        />
    )
}

export default CheckBox
