import { useMask } from "@react-input/mask"
import TextInput from "../textInput"

interface Props {
    value: string | null
    label?: string
    onChange: (value: string | null) => void
}

export const PhoneNumberInput = (props: Props) => {
    const ref = useMask({
        mask: "+7 (___) ___-__-__",
        replacement: { _: /\d/ },
    })

    return <TextInput ref={ref} {...props} placeholder="+7 (999) 999-99-99" />
}
