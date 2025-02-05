import { useMask } from "@react-input/mask";
import TextInput from "../textInput";

interface Props {
    value: string | null;
    label?: string;
    onChange: (value: string | null) => void;
}

export const AddressInput = (props: Props) => {
    const ref = useMask({
        mask: "г.________, ул.________, д.____, кв.____", 
        replacement: {
            _: /[а-яА-ЯёЁa-zA-Z0-9]/,
        },
    });

    return (
        <TextInput
            ref={ref}
            {...props}
            placeholder="г.Город, ул.Улица, д.Дом, кв.Квартира"
        />
    );
};