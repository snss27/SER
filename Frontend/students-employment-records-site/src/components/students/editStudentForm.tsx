import { ArmyStatuses } from "@/domain/students/enums/armyStatuses"
import { Genders } from "@/domain/students/enums/genders"
import { Peculiarities } from "@/domain/students/enums/peculiarities"
import { StudentBlank } from "@/domain/students/models/studentBlank"
import { StudentsProvider } from "@/domain/students/studentsProvider"
import useNotifications from "@/hooks/useNotifications"
import { Box } from "@mui/material"
import { useRouter } from "next/router"
import React, { useEffect, useReducer, useState } from "react"
import { IconPosition, IconType } from "../shared/buttons"
import Button from "../shared/buttons/button"
import CheckBox from "../shared/buttons/checkBox"
import DatePicker from "../shared/inputs/datePicker"
import { PhoneNumberInput } from "../shared/inputs/maskedInputs/phoneNumberInput"
import { SnilsInput } from "../shared/inputs/maskedInputs/snilsInput"
import Select from "../shared/inputs/select"
import TextInput from "../shared/inputs/textInput"
import { MailInput } from "../shared/inputs/maskedInputs/mailInput"
import { InnInput } from "../shared/inputs/maskedInputs/innInput"
import { PassportSeriesInput } from "../shared/inputs/maskedInputs/passportSeries"
import { PassportNumberInput } from "../shared/inputs/maskedInputs/passportnumberInput"
import { AddressInput } from "../shared/inputs/maskedInputs/addressInput"
import { PassportIssuedInput } from "../shared/inputs/passportIssuedInput"
import { GroupSelect } from "../shared/inputs/groupSelect"
import { Group } from "@/domain/groups/models/group"
import { GroupsProvider } from "@/domain/groups/groupsProvider"
import { AsyncAutocomplete } from "../shared/inputs/asyncAutocomplete"

interface Props {
    initialStudentBlank: StudentBlank
}

export const EditStudentForm: React.FC<Props> = ({ initialStudentBlank }) => {
    const [studentBlank, dispatch] = useReducer(StudentBlank.reducer, initialStudentBlank)
    const [groups, setGroups] = useState<Group[]>([]); 
    const [selectedGroup, setGroup] = useState<Group | null>(null);

    const navigator = useRouter()
    const { showError, showSuccess } = useNotifications()

    useEffect(() => {
        const loadGroups = async () => {
            const groups = await GroupsProvider.getAll();
            setGroups(groups);
        };
    
        loadGroups();
    }, []); 

    async function handleSaveButton() {
        const result = await StudentsProvider.save(studentBlank)
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
                value={studentBlank.name}
                onChange={(name) => dispatch({ type: "CHANGE_NAME", payload: { name } })}
                label="Имя"
            />

            <TextInput
                value={studentBlank.secondName}
                onChange={(secondName) =>
                    dispatch({ type: "CHANGE_SECOND_NAME", payload: { secondName } })
                }
                label="Фамилия"
            />

            <TextInput
                value={studentBlank.lastName}
                onChange={(lastName) =>
                    dispatch({ type: "CHANGE_LAST_NAME", payload: { lastName } })
                }
                label="Отчество"
            />

            <Select
                options={Genders.getAll()}
                value={studentBlank.gender}
                label="Пол"
                getOptionLabel={Genders.getDisplayText}
                onChange={(gender) => dispatch({ type: "CHANGE_GENDER", payload: { gender } })}
            />

            <DatePicker
                value={studentBlank.birthDate}
                label="Дата рождения"
                onChange={(birthDate) =>
                    dispatch({ type: "CHANGE_BIRTH_DATE", payload: { birthDate } })
                }
            />

            <PhoneNumberInput
                value={studentBlank.phoneNumber}
                label="Номер телефона"
                onChange={(phoneNumber) =>
                    dispatch({ type: "CHANGE_PHONE_NUMBER", payload: { phoneNumber } })
                }
            />

            <PhoneNumberInput
                value={studentBlank.representativePhoneNumber}
                label="Номер телефона представителя"
                onChange={(representativePhoneNumber) =>
                    dispatch({
                        type: "CHANGE_REPRESENTATIVE_PHONE_NUMBER",
                        payload: { representativePhoneNumber },
                    })
                }
            />

            <PassportSeriesInput
                value={studentBlank.passportSeries}
                label="Серия паспорта"
                onChange={(passportSeries) => dispatch({ type: "CHANGE_PASSPORTSERIES", payload: { passportSeries } })}
            />

            <PassportNumberInput
                value={studentBlank.passportNumber}
                label="Номер паспорта"
                onChange={(passportNumber) => dispatch({ type: "CHANGE_PASSPORTNUMBER", payload: { passportNumber } })}
            />

            <PassportIssuedInput
                value={studentBlank.passportIssued}
                label="Кем выдан паспорт"
                onChange={(passportIssued) => dispatch({ type: "CHANGE_PASSPORT_ISSUED", payload: { passportIssued } })}
            />

            <AddressInput
                value={studentBlank.address}
                onChange={(address) =>
                    dispatch({ type: "CHANGE_ADDRESS", payload: { address } })
                }
                label="Место жительства"
            />

            <MailInput
                value={studentBlank.mail}
                label="Эл. почта"
                onChange={(mail) => dispatch({ type: "CHANGE_MAIL", payload: { mail } })}
            />

            <InnInput
                value={studentBlank.inn}
                label="ИНН"
                onChange={(inn) => dispatch({ type: "CHANGE_INN", payload: { inn } })}
            />

            <CheckBox
                value={studentBlank.isForeignCitizen}
                label="Иностранный гражданин"
                onChange={(isForeignCitizen) =>
                    dispatch({ type: "CHANGE_IS_FOREIGN_CITIZEN", payload: { isForeignCitizen } })
                }
            />

            <SnilsInput
                value={studentBlank.snils}
                label="Снилс"
                onChange={(snils) => dispatch({ type: "CHANGE_SNILS", payload: { snils } })}
            />

            <CheckBox
                value={studentBlank.isOnPaidStudy}
                label="Обучение на платной основе?"
                onChange={(isOnPaidStudy) =>
                    dispatch({ type: "CHANGE_IS_ON_PAID_STUDY", payload: { isOnPaidStudy } })
                }
            />

           {/* <AsyncAutocomplete
                           value={studentBlank.groupId}
                           label="Уровень образования"
                           onChange={(groupId) =>
                               dispatch({ type: "CHANGE_GROUP", payload: { groupId } })
                           }
                           loadOption={}
                           loadOptions={EducationLevelsProvider.getBySearchText}
                           getOptionLabel={(educationLevel) => educationLevel.displayName}
                       /> */}

            <TextInput
                value={studentBlank.workplaceInfoId}
                label="Место работы"
                onChange={(workplaceInfoId) => dispatch({ type: "CHANGE_WORKPLACE_INFO", payload: { workplaceInfoId } })}
            />

            <CheckBox
                value={studentBlank.isTargetAgreement}
                label="Целевое обучение?"
                onChange={() => dispatch({ type: "TOGGLE_IS_TARGET_AGREEMENT" })}
            />

            {/* {studentBlank.isTargetAgreement && (
                <>
                    <MuiFileInput></MuiFileInput>

                    <DatePicker
                        value={studentBlank.targetAgreementDate}
                        label="Дата целевого соглашения"
                        onChange={(targetAgreementDate) => dispatch({ type: "CHANGE_TARGET_AGREEMENT_DATE", payload: { targetAgreementDate } })}
                    />
                </>
            )} */}

            <Select
                options={ArmyStatuses.getAll()}
                value={studentBlank.armyStatus}
                label="Армейский статус"
                getOptionLabel={ArmyStatuses.getDisplayText}
                onChange={(armyStatus) =>
                    dispatch({ type: "CHANGE_ARMY_STATUS", payload: { armyStatus } })
                }
            />

            {/* {studentBlank.armyStatus === ArmyStatuses.Fit && (
                <>
                    <FileUploadInput
                        value={studentBlank.armySubpoenaFile}
                        label="Файл повестки"
                        onChange={(armySubpoenaFile) => dispatch({ type: "CHANGE_ARMY_SUBPOENA_FILE", payload: { armySubpoenaFile } })}
                    />

                    <DatePicker
                        value={studentBlank.armyServeDate}
                        label="Дата призыва"
                        onChange={(armyServeDate) => dispatch({ type: "CHANGE_ARMY_SERVE_DATE", payload: { armyServeDate } })}
                    />
                </>
            )} */}

            <Select
                options={Peculiarities.getAll()}
                value={studentBlank.peculiarity}
                label="Особенность"
                getOptionLabel={Peculiarities.getDisplayText}
                onChange={(peculiarity) =>
                    dispatch({ type: "CHANGE_PECULIARITY", payload: { peculiarity } })
                }
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

