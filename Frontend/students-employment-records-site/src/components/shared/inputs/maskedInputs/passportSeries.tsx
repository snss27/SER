import { useMask } from "@react-input/mask";
import TextInput from "../textInput";

interface Props {
    value: string | null;
    label?: string;
    onChange: (value: string | null) => void;
}

export const PassportSeriesInput = (props: Props) => {
    const ref = useMask({
        mask: "____",
        replacement: { _: /\d/ },
    });

    return <TextInput ref={ref} {...props} placeholder="0000" />;
};
