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
            loadOptions={async (searchText) =>
                (await LocationsProvider.search(searchText)).map((address) => ({ id: address }))
            }
            loadOption={async (searchText) => ({
                id: (await LocationsProvider.search(searchText))[0],
            })}
            getOptionLabel={(address) => address.id}
        />
    )
}
