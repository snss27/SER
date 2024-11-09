import CheckBox from "@/components/shared/buttons/checkBox"
import AsyncAutocomplete from "@/components/shared/inputs/asyncAutocomplete"
import DatePicker from "@/components/shared/inputs/datePicker"
import PhoneNumberInput from "@/components/shared/inputs/maskedInputs/phoneNumberInput"
import SnilsInput from "@/components/shared/inputs/maskedInputs/snilsInput"
import Select from "@/components/shared/inputs/select"
import TextInput from "@/components/shared/inputs/textInput"
import { ArmyStatuses } from "@/domain/students/enums/armyStatuses"
import { Genders } from "@/domain/students/enums/genders"
import { Peculiarities } from "@/domain/students/enums/peculiarities"
import { StudentBlank } from "@/domain/students/models/studentBlank"
import { Box, Typography } from "@mui/material"
import { useReducer } from "react"

const AddStudentPage = () => {
    const [studentBlank, dispatch] = useReducer(StudentBlank.reducer, StudentBlank.empty())

    return (
        <Box sx={{ display: "flex", flexDirection: "column", gap: 3, padding: 2 }}>
            <Box>
                <Typography variant="h1" textAlign="center">
                    Новый студент
                </Typography>
            </Box>

            <Box sx={{ display: "flex", flexDirection: "row", gap: 2 }}>
                <Box sx={{ flex: 1 }}>
                    <TextInput
                        value={studentBlank.name}
                        onChange={(name) => dispatch({ type: "CHANGE_NAME", payload: { name } })}
                        label="Имя"
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <TextInput
                        value={studentBlank.surname}
                        onChange={(surname) =>
                            dispatch({ type: "CHANGE_SURNAME", payload: { surname } })
                        }
                        label="Фамилия"
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <TextInput
                        value={studentBlank.patronymic}
                        onChange={(patronymic) =>
                            dispatch({ type: "CHANGE_PATRONYMIC", payload: { patronymic } })
                        }
                        label="Отчество"
                    />
                </Box>
            </Box>

            <Box sx={{ display: "flex", flexDirection: "row", gap: 2 }}>
                <Box sx={{ flex: 1 }}>
                    <Select
                        options={Genders.getAll()}
                        value={studentBlank.gender}
                        label="Пол"
                        getOptionLabel={Genders.getDisplayText}
                        onChange={(gender) =>
                            dispatch({ type: "CHANGE_GENDER", payload: { gender } })
                        }
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <DatePicker
                        value={studentBlank.birthDate}
                        label="Дата рождения"
                        onChange={(birthDate) =>
                            dispatch({ type: "CHANGE_BIRTH_DATE", payload: { birthDate } })
                        }
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <PhoneNumberInput
                        value={studentBlank.phoneNumber}
                        label="Номер телефона"
                        onChange={(phoneNumber) =>
                            dispatch({ type: "CHANGE_PHONE_NUMBER", payload: { phoneNumber } })
                        }
                    />
                </Box>
            </Box>

            <Box sx={{ display: "flex", flexDirection: "row", gap: 2 }}>
                <Box sx={{ flex: 1 }}>
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
                </Box>
                <Box sx={{ flex: 1 }}>
                    <CheckBox
                        value={studentBlank.isOnPaidStudy}
                        label="Платное обучение?"
                        onChange={() => dispatch({ type: "TOGGLE_IS_ON_PAID_STUDY" })}
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <SnilsInput
                        value={studentBlank.snils}
                        label="Снилс"
                        onChange={(snils) => dispatch({ type: "CHANGE_SNILS", payload: { snils } })}
                    />
                </Box>
            </Box>

            <Box sx={{ display: "flex", flexDirection: "row", gap: 2 }}>
                <Box sx={{ flex: 1 }}>
                    {/* TODO Сделать логику, когда будут готовы группы */}
                    <AsyncAutocomplete
                        value={12}
                        getOptionLabel={(v) => v.toString()}
                        label="Группа"
                        loadOptions={async () => [12]}
                        onChange={() => {}}
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    {/* TODO Надо думать, что с паспортом? Возможно тут это и не ID, а просто данные? */}
                </Box>
                <Box sx={{ flex: 1 }}>
                    {/* TODO Сделать логику, когда будет готова информация о месте работы. Возможно тут это и не ID, а просто данные? Ну в любом случае другая страница (или модалка) */}
                </Box>
            </Box>

            <Box sx={{ display: "flex", flexDirection: "row", gap: 2 }}>
                <Box sx={{ flex: 1 }}>
                    {/* Тут дополнительные квалификации. MultiAutoSelect, когда будут готовы квалификации */}
                </Box>
                <Box sx={{ flex: 1 }}>
                    <CheckBox
                        value={studentBlank.isTargetAgreement}
                        label="Целевой договор?"
                        onChange={() => dispatch({ type: "TOGGLE_IS_TARGET_AGREEMENT" })}
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    {/* Если studentBlank.isTargetAgreement, тогда давать возможность прикрепить файл. */}
                </Box>
            </Box>

            <Box sx={{ display: "flex", flexDirection: "row", gap: 2 }}>
                <Box sx={{ flex: 1 }}>
                    <Select
                        options={ArmyStatuses.getAll()}
                        value={studentBlank.armyStatus}
                        label="Армейский статус"
                        getOptionLabel={ArmyStatuses.getDisplayText}
                        onChange={(armyStatus) =>
                            dispatch({ type: "CHANGE_ARMY_STATUS", payload: { armyStatus } })
                        }
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    {/* Если армейский статус - годен, тогда давать возможность прикрепить файл повестки */}
                </Box>
                <Box sx={{ flex: 1 }}>
                    {/* Если армейский статус - годен, тогда давать возможность выбрать дату призыва */}
                </Box>
            </Box>

            <Box sx={{ display: "flex", flexDirection: "row", gap: 2 }}>
                <Box sx={{ flex: 1 }}>
                    <Select
                        options={Peculiarities.getAll()}
                        value={studentBlank.peculiarity}
                        label="Особенность"
                        getOptionLabel={Peculiarities.getDisplayText}
                        onChange={(peculiarity) =>
                            dispatch({ type: "CHANGE_PECULIARITY", payload: { peculiarity } })
                        }
                    />
                </Box>
            </Box>
        </Box>
    )
}

export default AddStudentPage
