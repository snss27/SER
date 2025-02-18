import TextInput from "./textInput"

interface Props {
    value: string | null
    label?: string
    onChange: (value: string | null) => void
}

export const PassportIssuedInput = (props: Props) => {
    return (
        <TextInput
            {...props}
            placeholder="Кем выдан паспорт"
        />
    )
}