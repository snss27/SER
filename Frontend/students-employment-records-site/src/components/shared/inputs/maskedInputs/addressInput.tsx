import { LocationsProvider } from "@/domain/location/locationsProvider"
import { AsyncAutocomplete } from "../asyncAutocomplete"

interface Props {
    value: string | null
    label?: string
    onChange: (value: string | null) => void
}

export const AddressInput = ({ value, label, onChange }: Props) => {
    return (
        <AsyncAutocomplete
            value={value}
            label={label ?? "Адрес"}
            placeholder="Введите адрес..."
            noOptionsText="Адрес не найден"
            onChange={onChange}
            loadOptions={LocationsProvider.search}
            getOptionLabel={(address) => address}
        />
    )
}
