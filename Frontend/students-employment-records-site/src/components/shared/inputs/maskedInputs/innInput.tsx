import { useMask } from "@react-input/mask"
import TextInput from "@/components/shared/inputs/textInput"

interface Props {
    value: string | null
    label?: string
    onChange: (value: string | null) => void
}

export const InnInput = (props: Props) => {
    const ref = useMask({
        mask: "__________",
        replacement: { _: /\d/ },
    })

    return <TextInput ref={ref} {...props} placeholder="0000000000" />
}
