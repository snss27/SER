import { AdditionalQualificationsProvider } from "@/domain/additionalQualifications/additionalQualificationsProvider"
import { EnterprisesProvider } from "@/domain/enterprises/enterprisesProvider"
import { GroupsProvider } from "@/domain/groups/groupsProvider"
import { Gender } from "@/domain/students/enums/gender"
import { SocialStatus } from "@/domain/students/enums/socialStatus"
import { StudentStatus } from "@/domain/students/enums/studentStatus"
import { StudentBlank } from "@/domain/students/models/studentBlank"
import { StudentsProvider } from "@/domain/students/studentsProvider"
import useNotifications from "@/hooks/useNotifications"
import { Box, Collapse, Stack } from "@mui/material"
import { useRouter } from "next/router"
import React, { useReducer } from "react"
import { IconPosition, IconType } from "../shared/buttons"
import Button from "../shared/buttons/button"
import CheckBox from "../shared/buttons/checkBox"
import { AsyncAutocomplete } from "../shared/inputs/asyncAutocomplete"
import DatePicker from "../shared/inputs/datePicker"
import { FilesInput } from "../shared/inputs/filesInput"
import { AddressInput } from "../shared/inputs/maskedInputs/addressInput"
import { HumanInnInput } from "../shared/inputs/maskedInputs/humanInnInput"
import { MailInput } from "../shared/inputs/maskedInputs/mailInput"
import { PassportNumberInput } from "../shared/inputs/maskedInputs/passportNumberInput"
import { PassportSeriesInput } from "../shared/inputs/maskedInputs/passportSeriesInput"
import { PhoneNumberInput } from "../shared/inputs/maskedInputs/phoneNumberInput"
import { SnilsInput } from "../shared/inputs/maskedInputs/snilsInput"
import { Select } from "../shared/inputs/select"
import TextInput from "../shared/inputs/textInput"
import { EditStudentWorkplaces } from "./editStudentWorkPlaces"

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
                options={StudentStatus.getAll()}
                value={studentBlank.status}
                label="Статус"
                getOptionLabel={StudentStatus.getDisplayText}
                required
                defaultValue={studentBlank.status}
                onChange={(status) => dispatch({ type: "CHANGE_STATUS", payload: { status } })}
            />

            <Select
                options={Gender.getAll()}
                value={studentBlank.gender}
                label="Пол"
                required
                getOptionLabel={Gender.getDisplayText}
                onChange={(gender) => dispatch({ type: "CHANGE_GENDER", payload: { gender } })}
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

            <TextInput
                value={studentBlank.representativeAlias}
                label="Как обратится к представителю"
                onChange={(representativeAlias) =>
                    dispatch({
                        type: "CHANGE_REPRESENTATIVE_ALIAS",
                        payload: { representativeAlias },
                    })
                }
            />

            <DatePicker
                value={studentBlank.birthDate}
                label="Дата рождения"
                onChange={(birthDate) =>
                    dispatch({ type: "CHANGE_BIRTH_DATE", payload: { birthDate } })
                }
            />

            <SnilsInput
                value={studentBlank.snils}
                label="Снилс"
                onChange={(snils) => dispatch({ type: "CHANGE_SNILS", payload: { snils } })}
            />

            <Select
                options={SocialStatus.getAll()}
                value={studentBlank.socialStatuses}
                label="Социальные статусы"
                multiple
                getOptionLabel={SocialStatus.getDisplayText}
                onChange={(socialStatuses) =>
                    dispatch({ type: "CHANGE_SOCIAL_STATUSES", payload: { socialStatuses } })
                }
            />

            <AddressInput
                value={studentBlank.address}
                onChange={(address) => dispatch({ type: "CHANGE_ADDRESS", payload: { address } })}
                label="Место жительства"
            />

            <MailInput
                value={studentBlank.mail}
                label="Эл. почта"
                onChange={(mail) => dispatch({ type: "CHANGE_MAIL", payload: { mail } })}
            />

            <HumanInnInput
                value={studentBlank.inn}
                label="ИНН"
                onChange={(inn) => dispatch({ type: "CHANGE_INN", payload: { inn } })}
            />

            <AsyncAutocomplete
                value={studentBlank.group}
                label="Группа"
                getOptionLabel={(group) => group.displayName}
                onChange={(group) => dispatch({ type: "CHANGE_GROUP", payload: { group } })}
                loadOptions={GroupsProvider.getBySearchText}
            />

            <CheckBox
                value={studentBlank.isForeignCitizen}
                label="Иностранный гражданин"
                onChange={(isForeignCitizen) =>
                    dispatch({ type: "CHANGE_IS_FOREIGN_CITIZEN", payload: { isForeignCitizen } })
                }
            />

            <CheckBox
                value={studentBlank.isOnPaidStudy}
                label="Обучение на платной основе?"
                onChange={(isOnPaidStudy) =>
                    dispatch({ type: "CHANGE_IS_ON_PAID_STUDY", payload: { isOnPaidStudy } })
                }
            />

            <PassportSeriesInput
                value={studentBlank.passportSeries}
                label="Серия паспорта"
                onChange={(passportSeries) =>
                    dispatch({ type: "CHANGE_PASSPORT_SERIES", payload: { passportSeries } })
                }
            />

            <PassportNumberInput
                value={studentBlank.passportNumber}
                label="Номер паспорта"
                onChange={(passportNumber) =>
                    dispatch({ type: "CHANGE_PASSPORT_NUMBER", payload: { passportNumber } })
                }
            />

            <TextInput
                value={studentBlank.passportIssuedBy}
                label="Кем выдан"
                onChange={(passportIssuedBy) =>
                    dispatch({ type: "CHANGE_PASSPORT_ISSUED_BY", payload: { passportIssuedBy } })
                }
            />

            <DatePicker
                value={studentBlank.passportIssuedDate}
                label="Дата выдачи паспорта"
                onChange={(passportIssuedDate) =>
                    dispatch({
                        type: "CHANGE_PASSPORT_ISSUED_DATE",
                        payload: { passportIssuedDate },
                    })
                }
            />

            <FilesInput
                label="Файлы паспорта"
                maxFilesCount={studentBlank.passportFiles.maxFiles}
                urls={studentBlank.passportFiles.fileUrls}
                files={studentBlank.passportFiles.files}
                onFilesChange={(files) =>
                    dispatch({
                        type: "CHANGE_PASSPORT_FILES",
                        payload: {
                            passportFiles: studentBlank.passportFiles.withChangedFiles(files),
                        },
                    })
                }
                onUrlsChange={(urls) =>
                    dispatch({
                        type: "CHANGE_PASSPORT_FILES",
                        payload: {
                            passportFiles: studentBlank.passportFiles.withChangedUrls(urls),
                        },
                    })
                }
            />

            <EditStudentWorkplaces studentBlank={studentBlank} dispatch={dispatch} />

            <AsyncAutocomplete
                value={studentBlank.additionalQualifications}
                multiple
                label="Дополнительные квалификации"
                loadOptions={AdditionalQualificationsProvider.getBySearchText}
                getOptionLabel={(qualification) => qualification.displayName}
                onChange={(additionalQualifications) =>
                    dispatch({
                        type: "CHANGE_ADDITIONAL_QUALIFICATIONS",
                        payload: { additionalQualifications },
                    })
                }
            />

            <Box>
                <CheckBox
                    value={studentBlank.isTargetAgreement}
                    label="Целевое обучение?"
                    onChange={(isTargetAgreement) =>
                        dispatch({
                            type: "CHANGE_IS_TARGET_AGREEMENT",
                            payload: { isTargetAgreement },
                        })
                    }
                />

                <Collapse in={studentBlank.isTargetAgreement}>
                    <Stack direction="column" gap={2}>
                        <TextInput
                            value={studentBlank.targetAgreementNumber}
                            label="Номер договора"
                            onChange={(targetAgreementNumber) =>
                                dispatch({
                                    type: "CHANGE_TARGET_AGREEMENT_NUMBER",
                                    payload: { targetAgreementNumber },
                                })
                            }
                        />

                        <DatePicker
                            value={studentBlank.targetAgreementDate}
                            label="Дата заключение договора"
                            onChange={(targetAgreementDate) =>
                                dispatch({
                                    type: "CHANGE_TARGET_AGREEMENT_DATE",
                                    payload: { targetAgreementDate },
                                })
                            }
                        />

                        <AsyncAutocomplete
                            value={studentBlank.targetAgreementEnterprise}
                            label="Предприятие, с которым заключён договор"
                            loadOptions={EnterprisesProvider.getBySearchText}
                            getOptionLabel={(enterprise) => enterprise.name}
                            onChange={(targetAgreementEnterprise) =>
                                dispatch({
                                    type: "CHANGE_TARGET_AGREEMENT_ENTERPRISE",
                                    payload: { targetAgreementEnterprise },
                                })
                            }
                        />

                        <FilesInput
                            label="Файлы целевого обучения"
                            maxFilesCount={studentBlank.targetAgreementFile.maxFiles}
                            files={studentBlank.targetAgreementFile.files}
                            urls={studentBlank.targetAgreementFile.fileUrls}
                            onFilesChange={(files) =>
                                dispatch({
                                    type: "CHANGE_TARGET_AGREEMENT_FILE",
                                    payload: {
                                        targetAgreementFile:
                                            studentBlank.targetAgreementFile.withChangedFiles(
                                                files
                                            ),
                                    },
                                })
                            }
                            onUrlsChange={(urls) =>
                                dispatch({
                                    type: "CHANGE_TARGET_AGREEMENT_FILE",
                                    payload: {
                                        targetAgreementFile:
                                            studentBlank.targetAgreementFile.withChangedUrls(urls),
                                    },
                                })
                            }
                        />
                    </Stack>
                </Collapse>
            </Box>

            <Box>
                <CheckBox
                    value={studentBlank.mustServeInArmy}
                    label="Подлежит призыву?"
                    onChange={(mustServeInArmy) =>
                        dispatch({
                            type: "CHANGE_MUST_SERVE_IN_ARMY",
                            payload: { mustServeInArmy },
                        })
                    }
                />

                <Collapse in={studentBlank.mustServeInArmy}>
                    <Stack direction="column" gap={2}>
                        <DatePicker
                            value={studentBlank.armyCallDate}
                            label="Дата призыва"
                            onChange={(armyCallDate) =>
                                dispatch({
                                    type: "CHANGE_ARMY_CALL_DATE",
                                    payload: { armyCallDate },
                                })
                            }
                        />

                        <FilesInput
                            label="Файлы повестки"
                            maxFilesCount={studentBlank.armySubpoenaFile.maxFiles}
                            files={studentBlank.armySubpoenaFile.files}
                            urls={studentBlank.armySubpoenaFile.fileUrls}
                            onFilesChange={(files) =>
                                dispatch({
                                    type: "CHANGE_ARMY_SUBPOENA_FILE",
                                    payload: {
                                        armySubpoenaFile:
                                            studentBlank.armySubpoenaFile.withChangedFiles(files),
                                    },
                                })
                            }
                            onUrlsChange={(urls) =>
                                dispatch({
                                    type: "CHANGE_ARMY_SUBPOENA_FILE",
                                    payload: {
                                        armySubpoenaFile:
                                            studentBlank.armySubpoenaFile.withChangedUrls(urls),
                                    },
                                })
                            }
                        />
                    </Stack>
                </Collapse>
            </Box>

            <FilesInput
                label="Прочие файлы"
                maxFilesCount={studentBlank.otherFiles.maxFiles}
                files={studentBlank.otherFiles.files}
                urls={studentBlank.otherFiles.fileUrls}
                onFilesChange={(files) =>
                    dispatch({
                        type: "CHANGE_OTHER_FILES",
                        payload: { otherFiles: studentBlank.otherFiles.withChangedFiles(files) },
                    })
                }
                onUrlsChange={(urls) =>
                    dispatch({
                        type: "CHANGE_OTHER_FILES",
                        payload: { otherFiles: studentBlank.otherFiles.withChangedUrls(urls) },
                    })
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
