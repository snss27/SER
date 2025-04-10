import { StudentAction, StudentBlank } from "@/domain/students/models/studentBlank"
import { WorkplaceBlank } from "@/domain/workplaces/models/workplaceBlank"
import { Box, Dialog, DialogContent, Stack, Typography } from "@mui/material"
import { useState } from "react"
import Button from "../shared/buttons/button"
import { WorkplaceEditor } from "../workplaces/workplaceEditor"
import { WorkplaceItem } from "../workplaces/workplaceItem"

type EditMode = { type: "current"; isNew: boolean } | { type: "previous"; isNew: boolean }

interface Props {
    studentBlank: StudentBlank
    dispatch: React.Dispatch<StudentAction>
}

export function EditStudentWorkplaces({ studentBlank, dispatch }: Props) {
    const [editingWorkplace, setEditingWorkplace] = useState<WorkplaceBlank | null>(null)
    const [editMode, setEditMode] = useState<EditMode | null>(null)

    const openEditor = (workplace: WorkplaceBlank, mode: EditMode) => {
        // Клонируем workplace, чтобы не мутировать напрямую
        setEditingWorkplace({ ...workplace })
        setEditMode(mode)
    }

    const handleSaveWorkplace = (updated: WorkplaceBlank) => {
        if (!editMode) return

        if (editMode.type === "current") {
            if (editMode.isNew && studentBlank.currentWorkplace) {
                // Переносим старое в предыдущие
                dispatch({
                    type: "CHANGE_PREV_WORKPLACES",
                    payload: {
                        prevWorkplaces: [
                            ...studentBlank.prevWorkplaces,
                            studentBlank.currentWorkplace,
                        ],
                    },
                })
            }
            // Устанавливаем новое текущее
            dispatch({
                type: "CHANGE_CURRENT_WORKPLACE",
                payload: { currentWorkplace: updated },
            })
        } else {
            if (editMode.isNew) {
                // Добавляем новое в предыдущие
                dispatch({
                    type: "CHANGE_PREV_WORKPLACES",
                    payload: {
                        prevWorkplaces: [...studentBlank.prevWorkplaces, updated],
                    },
                })
            } else {
                // Обновляем существующее
                const updatedList = studentBlank.prevWorkplaces.map((w) =>
                    w.clientId === updated.clientId ? updated : w
                )
                dispatch({
                    type: "CHANGE_PREV_WORKPLACES",
                    payload: { prevWorkplaces: updatedList },
                })
            }
        }

        closeEditor()
    }

    const closeEditor = () => {
        setEditingWorkplace(null)
        setEditMode(null)
    }

    const handleDeletePrev = (clientId?: string) => {
        dispatch({
            type: "CHANGE_PREV_WORKPLACES",
            payload: {
                prevWorkplaces: studentBlank.prevWorkplaces.filter((w) => w.clientId !== clientId),
            },
        })
    }

    return (
        <Stack direction="column" gap={2} paddingBottom={1}>
            <Box>
                <Typography variant="h6">Текущее место работы</Typography>
                {studentBlank.currentWorkplace ? (
                    <WorkplaceItem
                        workplace={studentBlank.currentWorkplace}
                        onEdit={() =>
                            openEditor(studentBlank.currentWorkplace!, {
                                type: "current",
                                isNew: false,
                            })
                        }
                        onDelete={() =>
                            dispatch({
                                type: "CHANGE_CURRENT_WORKPLACE",
                                payload: { currentWorkplace: null },
                            })
                        }
                    />
                ) : (
                    <Button
                        text="Добавить текущее место работы"
                        onClick={() =>
                            openEditor(WorkplaceBlank.empty(), { type: "current", isNew: true })
                        }
                    />
                )}
            </Box>

            <Box>
                <Typography variant="h6">Предыдущие места работы</Typography>
                {studentBlank.prevWorkplaces.map((workplace) => (
                    <WorkplaceItem
                        key={workplace.clientId}
                        workplace={workplace}
                        onEdit={() => openEditor(workplace, { type: "previous", isNew: false })}
                        onDelete={() => handleDeletePrev(workplace.clientId)}
                    />
                ))}
                <Button
                    text="Добавить предыдущее место работы"
                    onClick={() =>
                        openEditor(WorkplaceBlank.empty(), { type: "previous", isNew: true })
                    }
                />
            </Box>

            <Dialog open={!!editingWorkplace} onClose={closeEditor} maxWidth="md" fullWidth>
                <DialogContent>
                    {editingWorkplace && (
                        <WorkplaceEditor
                            workplace={editingWorkplace}
                            onSave={handleSaveWorkplace}
                            onClose={closeEditor}
                        />
                    )}
                </DialogContent>
            </Dialog>
        </Stack>
    )
}
