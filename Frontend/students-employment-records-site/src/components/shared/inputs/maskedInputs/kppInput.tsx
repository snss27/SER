import { useMask } from "@react-input/mask"
import TextInput from "@/components/shared/inputs/textInput"

interface Props {
    value: string | null
    label?: string
    onChange: (value: string | null) => void
}

export const KppInput = (props: Props) => {
    const ref = useMask({
        mask: "_________",
        replacement: { _: /\d/ },
    })

    return <TextInput ref={ref} {...props} placeholder="000000000" />
}
