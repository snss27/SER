import { FormControlLabel, Switch as MSwitch } from "@mui/material"

interface Props {
    value: boolean
    label: string
    onChange: (isChecked: boolean) => void
}

const Switch = (props: Props) => {
    return (
        <FormControlLabel
            control={
                <MSwitch
                    checked={props.value}
                    onChange={(_, isChecked) => props.onChange(isChecked)}
                />
            }
            label={props.label}
        />
    )
}

export default Switch
