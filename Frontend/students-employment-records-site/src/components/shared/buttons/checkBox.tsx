import { FormControlLabel, Checkbox as MCheckBox } from "@mui/material"

interface Props {
    value: boolean
    label: string
    onChange: () => void
}

const CheckBox = (props: Props) => {
    return (
        <FormControlLabel
            control={<MCheckBox checked={props.value} onChange={props.onChange} />}
            label={props.label}
        />
    )
}

export default CheckBox
