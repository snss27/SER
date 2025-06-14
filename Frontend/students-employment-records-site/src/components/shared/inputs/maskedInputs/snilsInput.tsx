import { useMask } from "@react-input/mask"
import TextInput from "../textInput"

interface Props {
    value: string | null
    label?: string
    onChange: (value: string | null) => void
}

export const SnilsInput = (props: Props) => {
    const ref = useMask({
        mask: "___-___-___ __",
        replacement: { _: /\d/ },
    })

    return <TextInput ref={ref} {...props} placeholder="000-000-000 00" />
}
