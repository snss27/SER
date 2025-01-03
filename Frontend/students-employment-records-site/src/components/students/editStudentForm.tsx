import { ArmyStatuses } from "@/domain/students/enums/armyStatuses"
import { Genders } from "@/domain/students/enums/genders"
import { Peculiarities } from "@/domain/students/enums/peculiarities"
import { StudentBlank } from "@/domain/students/models/studentBlank"
import { StudentsProvider } from "@/domain/students/studentsProvider"
import useNotifications from "@/hooks/useNotifications"
import { Box } from "@mui/material"
import { useRouter } from "next/router"
import React, { useReducer } from "react"
import { IconPosition, IconType } from "../shared/buttons"
import Button from "../shared/buttons/button"
import CheckBox from "../shared/buttons/checkBox"
import DatePicker from "../shared/inputs/datePicker"
import { PhoneNumberInput } from "../shared/inputs/maskedInputs/phoneNumberInput"
import { SnilsInput } from "../shared/inputs/maskedInputs/snilsInput"
import Select from "../shared/inputs/select"
import TextInput from "../shared/inputs/textInput"

interface Props {
    initialStudentBlank: StudentBlank
}

export const EditStudentForm: React.FC<Props> = ({ initialStudentBlank }) => {
    const [studentBlank, dispatch] = useReducer(StudentBlank.reducer, initialStudentBlank)

    const navigator = useRouter()
    const { showError, showSuccess } = useNotifications()

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

            {/* TODO Группы (нужны запросы) */}

            {/* TODO Паспорт (серия, номер, дата выдачи, кем выдан) */}

            {/* TODO Сделать логику, когда будет готова информация о месте работы. Возможно тут это и не ID, а просто данные? Ну в любом случае другая страница (или модалка) */}

            <CheckBox
                value={studentBlank.isTargetAgreement}
                label="Целевое обучение?"
                onChange={() => dispatch({ type: "TOGGLE_IS_TARGET_AGREEMENT" })}
            />

            {/* Если studentBlank.isTargetAgreement, тогда давать возможность прикрепить файл. */}

            <Select
                options={ArmyStatuses.getAll()}
                value={studentBlank.armyStatus}
                label="Армейский статус"
                getOptionLabel={ArmyStatuses.getDisplayText}
                onChange={(armyStatus) =>
                    dispatch({ type: "CHANGE_ARMY_STATUS", payload: { armyStatus } })
                }
            />

            {/* Если армейский статус - годен, тогда давать возможность прикрепить файл повестки */}

            {/* Если армейский статус - годен, тогда давать возможность выбрать дату призыва */}

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
