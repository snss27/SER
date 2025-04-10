import { useMask } from "@react-input/mask"
import TextInput from "../textInput"

interface Props {
    value: string | null
    label?: string
    onChange: (value: string | null) => void
}

export const HumanInnInput = (props: Props) => {
    const ref = useMask({
        mask: "____________",
        replacement: { _: /\d/ },
    })

    return <TextInput ref={ref} {...props} placeholder="000000000000" />
}
