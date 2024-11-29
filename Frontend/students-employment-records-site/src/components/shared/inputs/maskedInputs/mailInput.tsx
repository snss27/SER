import TextInput from "@/components/shared/inputs/textInput"

interface Props {
    value: string | null
    label?: string
    onChange: (value: string | null) => void
}

//TODO Сделать валидацию
export const MailInput = (props: Props) => {
    return <TextInput {...props} placeholder="example@mail.com" />
}
