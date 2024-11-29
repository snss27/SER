import { useMask } from "@react-input/mask"
import TextInput from "../textInput"

interface Props {
    value: string | null
    onChange: (value: string | null) => void
}

export const GroupNumberInput = (props: Props) => {
    const ref = useMask({
        mask: "_____",
        replacement: { _: /\d/ },
    })

    return <TextInput ref={ref} {...props} placeholder="00000" label="Номер группы" />
}
