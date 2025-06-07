import { IconPosition, IconType } from "@/components/shared/buttons"
import Button from "@/components/shared/buttons/button"
import IconButton from "@/components/shared/buttons/iconButtons"
import { AsyncAutocomplete } from "@/components/shared/inputs/asyncAutocomplete"
import { DateRangePicker } from "@/components/shared/inputs/dateRangePicker"
import { Select } from "@/components/shared/inputs/select"
import { AdditionalQualificationsProvider } from "@/domain/additionalQualifications/additionalQualificationsProvider"
import { EnterprisesProvider } from "@/domain/enterprises/enterprisesProvider"
import { GroupsProvider } from "@/domain/groups/groupsProvider"
import { ForeignCitizenVariant } from "@/domain/students/enums/foreignCitizenVariant"
import { Gender } from "@/domain/students/enums/gender"
import { MustServeInArmyVariant } from "@/domain/students/enums/mustServeInArmyVariant"
import { OnPaidStudyVariant } from "@/domain/students/enums/onPaidStudyVariant"
import { SocialStatus } from "@/domain/students/enums/socialStatus"
import { StudentStatus } from "@/domain/students/enums/studentStatus"
import { TargetAgreementVariant } from "@/domain/students/enums/targetAgreementVariant"
import { StudentsFilter } from "@/domain/students/models/studentsFilter"
import { AsyncDialogProps } from "@/hooks/useDialog/types"
import { Collapse, Dialog, DialogActions, DialogTitle, Stack } from "@mui/material"
import { useEffect, useReducer } from "react"

interface Props {
    initialFilter: StudentsFilter
}

type Returns =
    | {
          isAccepted: true
          studentsFilter: StudentsFilter
      }
    | {
          isAccepted: false
      }

export const StudentsFilterModal: React.FC<AsyncDialogProps<Props, Returns>> = ({
    data,
    handleClose,
    open,
}) => {
    const [studentsFilter, dispatch] = useReducer(StudentsFilter.reducer, data.initialFilter)

    useEffect(() => {
        if (!open) return

        dispatch({ type: "SET", payload: { studentsFilter: data.initialFilter } })
    }, [open])

    function handleReset() {
        dispatch({ type: "RESET_FILTERS" })
    }

    function handleAccept() {
        handleClose({ isAccepted: true, studentsFilter })
    }

    function onClose() {
        handleClose({ isAccepted: false })
    }

    return (
        <Dialog open={open} onClose={onClose} fullWidth maxWidth="sm">
            <DialogTitle fontWeight="bold" sx={{ paddingRight: 6.5 }}>
                Фильтрация
                <IconButton
                    icon={IconType.Close}
                    onClick={onClose}
                    sx={{
                        position: "absolute",
                        right: 8,
                        top: 8,
                    }}
                />
            </DialogTitle>
            <Stack direction="column" sx={{ px: 2, gap: 2 }}>
                <Select
                    options={StudentStatus.getAll()}
                    multiple
                    label="Статусы"
                    value={studentsFilter.statuses}
                    getOptionLabel={StudentStatus.getDisplayText}
                    onChange={(statuses) =>
                        dispatch({ type: "CHANGE_STATUSES", payload: { statuses } })
                    }
                />
                <Select
                    label="Пол"
                    value={studentsFilter.gender}
                    options={Gender.getAll()}
                    getOptionLabel={Gender.getDisplayText}
                    onChange={(gender) => dispatch({ type: "CHANGE_GENDER", payload: { gender } })}
                />
                <DateRangePicker
                    label="Дата рождения"
                    value={studentsFilter.birthDatePeriod}
                    onChange={(birthDatePeriod) =>
                        dispatch({ type: "CHANGE_BIRTH_DATE_PERIOD", payload: { birthDatePeriod } })
                    }
                    disableFuture
                />
                <Select
                    options={SocialStatus.getAll()}
                    multiple
                    value={studentsFilter.socialStatuses}
                    label="Социальные статусы"
                    getOptionLabel={SocialStatus.getDisplayText}
                    onChange={(socialStatuses) =>
                        dispatch({ type: "CHANGE_SOCIAL_STATUSES", payload: { socialStatuses } })
                    }
                />
                <AsyncAutocomplete
                    multiple
                    value={studentsFilter.groups}
                    label="Группы"
                    loadOptions={GroupsProvider.getBySearchText}
                    getOptionLabel={(group) => group.displayName}
                    onChange={(groups) => dispatch({ type: "CHANGE_GROUPS", payload: { groups } })}
                />
                <Select
                    required
                    label="Основа обучения"
                    options={OnPaidStudyVariant.getAll()}
                    value={studentsFilter.onPaidStudyVariant}
                    getOptionLabel={OnPaidStudyVariant.getDisplayText}
                    onChange={(onPaidStudyVariant) =>
                        dispatch({
                            type: "CHANGE_ON_PAID_STUDY_VARIANT",
                            payload: { onPaidStudyVariant },
                        })
                    }
                />
                <Select
                    required
                    label="Граждане"
                    options={ForeignCitizenVariant.getAll()}
                    value={studentsFilter.foreignCitizenVariant}
                    getOptionLabel={ForeignCitizenVariant.getDisplayText}
                    onChange={(foreignCitizenVariant) =>
                        dispatch({
                            type: "CHANGE_FOREIGN_CITIZEN_VARIANT",
                            payload: { foreignCitizenVariant },
                        })
                    }
                />
                <AsyncAutocomplete
                    multiple
                    value={studentsFilter.additionalQualifications}
                    label="Дополнительные квалификации"
                    loadOptions={AdditionalQualificationsProvider.getBySearchText}
                    getOptionLabel={(aq) => aq.displayName}
                    onChange={(additionalQualifications) =>
                        dispatch({
                            type: "CHANGE_ADDITIONAL_QUALIFICATIONS",
                            payload: { additionalQualifications },
                        })
                    }
                />
                <Select
                    required
                    label="Целевое обучение"
                    options={TargetAgreementVariant.getAll()}
                    value={studentsFilter.targetAgreementVariant}
                    getOptionLabel={TargetAgreementVariant.getDisplayText}
                    onChange={(targetAgreementVariant) =>
                        dispatch({
                            type: "CHANGE_TARGET_AGREEMENT_VARIANT",
                            payload: { targetAgreementVariant },
                        })
                    }
                />

                <Collapse
                    in={TargetAgreementVariant.isWithTargetAgreement(
                        studentsFilter.targetAgreementVariant
                    )}>
                    <AsyncAutocomplete
                        multiple
                        value={studentsFilter.targetAgreementEnterprises}
                        label="Предприятия с которыми заключены целевые договоры"
                        loadOptions={EnterprisesProvider.getBySearchText}
                        getOptionLabel={(e) => e.name}
                        onChange={(targetAgreementEnterprises) =>
                            dispatch({
                                type: "CHANGE_TARGET_AGREEMENT_ENTERPRISES",
                                payload: { targetAgreementEnterprises },
                            })
                        }
                    />
                </Collapse>

                <Select
                    required
                    label="Служба в армии"
                    options={MustServeInArmyVariant.getAll()}
                    value={studentsFilter.mustServeInArmyVariant}
                    getOptionLabel={MustServeInArmyVariant.getDisplayText}
                    onChange={(mustServeInArmyVariant) =>
                        dispatch({
                            type: "CHANGE_MUST_SERVE_IN_ARMY_VARIANT",
                            payload: { mustServeInArmyVariant },
                        })
                    }
                />

                <Collapse
                    in={MustServeInArmyVariant.isMustServe(studentsFilter.mustServeInArmyVariant)}>
                    <DateRangePicker
                        label="Дата призыва"
                        value={studentsFilter.armyCallDatePeriod}
                        onChange={(armyCallDatePeriod) =>
                            dispatch({
                                type: "CHANGE_ARMY_CALL_DATE_PERIOD",
                                payload: { armyCallDatePeriod },
                            })
                        }
                    />
                </Collapse>
            </Stack>
            <DialogActions sx={{ padding: 2 }}>
                <Button
                    text="Сбросить"
                    color="error"
                    onClick={handleReset}
                    icon={{ type: IconType.Cancel, position: IconPosition.Start }}
                />
                <Button
                    text="Применить"
                    color="success"
                    onClick={handleAccept}
                    icon={{ type: IconType.Save, position: IconPosition.Start }}
                />
            </DialogActions>
        </Dialog>
    )
}
