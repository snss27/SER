import { KppInput } from "@/components/shared/inputs/maskedInputs/kppInput"
import { MailInput } from "@/components/shared/inputs/maskedInputs/mailInput"
import { OrgnInput } from "@/components/shared/inputs/maskedInputs/orgnInput"
import { PhoneNumberInput } from "@/components/shared/inputs/maskedInputs/phoneNumberInput"
import { EnterprisesProvider } from "@/domain/enterprises/enterprisesProvider"
import { EnterpriseBlank } from "@/domain/enterprises/models/enterpriseBlank"
import useNotifications from "@/hooks/useNotifications"
import { Box } from "@mui/material"
import { useRouter } from "next/router"
import { useReducer } from "react"
import { IconPosition, IconType } from "../shared/buttons"
import Button from "../shared/buttons/button"
import CheckBox from "../shared/buttons/checkBox"
import { InnInput } from "../shared/inputs/maskedInputs/innInput"
import TextInput from "../shared/inputs/textInput"
import { PassportSeriesInput } from "../shared/inputs/maskedInputs/passportSeries"
import { PassportNumberInput } from "../shared/inputs/maskedInputs/passportnumberInput"

interface Props {
    initialBlank: EnterpriseBlank
}

export const EditEnterpriseForm = (props: Props) => {
    const [enterpriseBlank, dispatch] = useReducer(EnterpriseBlank.reducer, props.initialBlank)

    const navigator = useRouter()
    const { showError, showSuccess } = useNotifications()

    async function handleSaveButton() {
        const result = await EnterprisesProvider.save(enterpriseBlank)
        if (!result.isSuccess) return showError(result.getErrorsString)

        showSuccess("Изменения сохранены")
        navigator.back()
    }

    function handleBackButton() {
        navigator.back()
    }

    return (
        <Box component="form" className="edit-form-container">
            <TextInput
                value={enterpriseBlank.name}
                label="Наименование"
                onChange={(name) => dispatch({ type: "CHANGE_NAME", payload: { name } })}
            />
            <TextInput
                value={enterpriseBlank.legalAddress}
                label="Юридический адрес"
                onChange={(legalAddress) =>
                    dispatch({ type: "CHANGE_LEGAL_ADDRESS", payload: { legalAddress } })
                }
            />
            <TextInput
                value={enterpriseBlank.actualAddress}
                label="Фактический адрес"
                onChange={(actualAddress) =>
                    dispatch({ type: "CHANGE_ACTUAL_ADDRESS", payload: { actualAddress } })
                }
            />
            <TextInput
                value={enterpriseBlank.address}
                label="Адерс (место нахождения)"
                onChange={(address) => dispatch({ type: "CHANGE_ADDRESS", payload: { address } })}
            />
            <InnInput
                value={enterpriseBlank.INN}
                label="ИНН"
                onChange={(INN) => dispatch({ type: "CHANGE_INN", payload: { INN } })}
            />
            <KppInput
                value={enterpriseBlank.KPP}
                label="КПП"
                onChange={(KPP) => dispatch({ type: "CHANGE_KPP", payload: { KPP } })}
            />
            <OrgnInput
                value={enterpriseBlank.ORGN}
                label="ОРГН"
                onChange={(ORGN) => dispatch({ type: "CHANGE_ORGN", payload: { ORGN } })}
            />
            <PhoneNumberInput
                value={enterpriseBlank.phone}
                label="Телефон"
                onChange={(phone) => dispatch({ type: "CHANGE_PHONE", payload: { phone } })}
            />
            <MailInput
                value={enterpriseBlank.mail}
                label="Эл. почта"
                onChange={(mail) => dispatch({ type: "CHANGE_MAIL", payload: { mail } })}
            />
            <CheckBox
                value={enterpriseBlank.isOPK}
                label="Входит в ОПК"
                onChange={(isOPK) => dispatch({ type: "CHANGE_IS_OPK", payload: { isOPK } })}
            />
            <Box className="edit-form-footer">
                <Button
                    text="Назад"
                    icon={{ type: IconType.Back, position: IconPosition.Start }}
                    onClick={handleBackButton}
                />
                <Button
                    text="Сохранить"
                    variant="contained"
                    icon={{ type: IconType.Save, position: IconPosition.End }}
                    onClick={handleSaveButton}
                />
            </Box>
        </Box>
    )
}
