import { EducationLevelTypes } from "@/domain/educationLevels/enums/EducationLevelTypes"
import { StructuralUnits } from "@/domain/groups/enums/structuralUnits"
import { Gender } from "@/domain/students/enums/gender"
import { SocialStatus } from "@/domain/students/enums/socialStatus"
import { StudentStatus } from "@/domain/students/enums/studentStatus"
import { Student } from "@/domain/students/models/student"
import { AsyncDialogProps } from "@/hooks/useDialog/types"
import { Box, Dialog, Divider, Paper, Typography } from "@mui/material"
import Grid from "@mui/material/Grid"
import { PropsWithChildren } from "react"

interface Props {
    student: Student
}

type FieldProps = {
    label: string
    value?: string | null
}

export const StudentInfoModal: React.FC<AsyncDialogProps<Props, void>> = ({
    data,
    handleClose,
    open,
}) => {
    const { student } = data

    const Section = ({ title, children }: PropsWithChildren<{ title: string }>) => (
        <Paper sx={{ p: 2, mb: 3 }}>
            <Typography variant="h6" gutterBottom>
                {title}
            </Typography>
            <Divider sx={{ mb: 2 }} />
            {children}
        </Paper>
    )

    const Field = ({ label, value }: FieldProps) => (
        <Grid size={4}>
            <Typography variant="body2" color="text.secondary">
                {label}
            </Typography>
            <Typography variant="body1">{value || "—"}</Typography>
        </Grid>
    )

    return (
        <Dialog open={open} onClose={() => handleClose()} maxWidth="lg">
            <Box sx={{ p: 3 }}>
                <Section title="Основная информация">
                    <Grid container spacing={2}>
                        <Field label="ФИО" value={student.displayName} />
                        <Field label="Пол" value={Gender.getDisplayText(student.gender)} />
                        <Field
                            label="Статус"
                            value={StudentStatus.getDisplayText(student.status)}
                        />
                        <Field
                            label="Дата рождения"
                            value={student.birthDate?.toLocaleDateString()}
                        />
                        <Field
                            label="Иностранный гражданин?"
                            value={student.isForeignCitizen ? "Да" : "Нет"}
                        />
                        <Field
                            label="Обучение на платной основе?"
                            value={student.isOnPaidStudy ? "Да" : "Нет"}
                        />
                        <Field label="СНИЛС" value={student.snils} />
                        <Field label="ИНН" value={student.inn} />
                        <Field label="Почта" value={student.mail} />
                        <Field label="Адрес" value={student.address} />
                        <Field label="Номер телефона" value={student.phoneNumber} />
                        <Field
                            label="Номер телефона представителя"
                            value={student.representativePhoneNumber}
                        />
                        <Field
                            label="Как обратится к представителю"
                            value={student.representativeAlias}
                        />
                    </Grid>
                </Section>

                <Section title="Паспортные данные">
                    <Grid container spacing={2}>
                        <Field label="Серия" value={student.passportSeries} />
                        <Field label="Номер" value={student.passportNumber} />
                        <Field label="Кем выдан" value={student.passportIssuedBy} />
                        <Field
                            label="Дата выдачи"
                            value={student.passportIssuedDate?.toLocaleDateString()}
                        />
                    </Grid>
                </Section>

                <Section title="Группа">
                    <Grid container spacing={2}>
                        <Field label="Номер группы" value={student.group?.number} />
                        <Field
                            label="Уровень образования"
                            value={student.group.educationLevel.displayName}
                        />
                        <Field
                            label="Год поступления"
                            value={student.group.enrollmentYear.toString()}
                        />
                        <Field
                            label="Время обучения"
                            value={student.group.educationLevel.displayTime}
                        />
                        <Field
                            label="Структурное подразделение"
                            value={StructuralUnits.getDisplayText(student.group.structuralUnit)}
                        />
                        <Field
                            label="Тип уровня образования"
                            value={EducationLevelTypes.displayName(
                                student.group.educationLevel.type
                            )}
                        />
                        <Field label="Куратор" value={student.group.curator?.displayName} />
                        <Field label="Кластер" value={student.group.cluster?.name} />
                    </Grid>
                </Section>

                <Section title="Работа">
                    {student.currentWorkplace ? (
                        <Grid container spacing={2}>
                            <Field
                                label="Предприятие"
                                value={student.currentWorkplace.enterprise.name}
                            />
                            <Field label="Должность" value={student.currentWorkplace.post} />
                            <Field
                                label="Начало"
                                value={student.currentWorkplace.startDate?.toLocaleDateString()}
                            />
                            <Field
                                label="Окончание"
                                value={student.currentWorkplace.finishDate?.toLocaleDateString()}
                            />
                        </Grid>
                    ) : (
                        <Typography variant="body1">Нет текущего места работы</Typography>
                    )}
                </Section>

                <Section title="Целевое обучение">
                    <Grid container spacing={2}>
                        <Field
                            label="Есть договор"
                            value={student.isTargetAgreement ? "Да" : "Нет"}
                        />
                        <Field label="Номер договора" value={student.targetAgreementNumber} />
                        <Field
                            label="Дата договора"
                            value={student.targetAgreementDate?.toLocaleDateString()}
                        />
                        <Field
                            label="Предприятие"
                            value={student.targetAgreementEnterprise?.name}
                        />
                    </Grid>
                </Section>

                <Section title="Дополнительные квалификации">
                    {student.additionalQualifications.length > 0 ? (
                        <Typography variant="body1">
                            {student.additionalQualifications
                                .map((aq) => aq.displayName)
                                .join(", ")}
                        </Typography>
                    ) : (
                        <Typography variant="body1">Нет данных</Typography>
                    )}
                </Section>

                <Section title="Социальные статусы и армия">
                    <Grid container spacing={2}>
                        <Field
                            label="Подлежит призыву?"
                            value={student.mustServeInArmy ? "Да" : "Нет"}
                        />
                        <Field
                            label="Дата повестки"
                            value={student.armyCallDate?.toLocaleDateString()}
                        />
                        <Field
                            label="Социальные статусы"
                            value={student.socialStatuses
                                .map(SocialStatus.getDisplayText)
                                .join(", ")}
                        />
                    </Grid>
                </Section>
            </Box>
        </Dialog>
    )
}
