import { StudentAction, StudentBlank } from "@/domain/students/models/studentBlank"
import { WorkplaceBlank } from "@/domain/workplaces/models/workplaceBlank"
import { Box, Dialog, DialogContent, Stack, Typography } from "@mui/material"
import { useState } from "react"
import Button from "../shared/buttons/button"
import { WorkplaceEditor } from "../workplaces/workplaceEditor"
import { WorkplaceItem } from "../workplaces/workplaceItem"

interface Props {
    studentBlank: StudentBlank
    dispatch: React.Dispatch<StudentAction>
}

export function EditStudentWorkplaces({ studentBlank, dispatch }: Props) {
    const [editingWorkplace, setEditingWorkplace] = useState<WorkplaceBlank | null>(null)

    const handleSaveWorkplace = (updatedWorkplace: WorkplaceBlank) => {
        if (editingWorkplace?.id) {
            if (studentBlank.currentWorkplace?.id === editingWorkplace.id) {
                dispatch({
                    type: "CHANGE_CURRENT_WORKPLACE",
                    payload: { currentWorkplace: updatedWorkplace },
                })
            } else {
                dispatch({
                    type: "CHANGE_PREV_WORKPLACES",
                    payload: {
                        prevWorkplaces: studentBlank.prevWorkplaces.map((w) =>
                            w.id === updatedWorkplace.id ? updatedWorkplace : w
                        ),
                    },
                })
            }
        } else {
            if (studentBlank.currentWorkplace) {
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
            dispatch({
                type: "CHANGE_CURRENT_WORKPLACE",
                payload: { currentWorkplace: updatedWorkplace },
            })
        }
        setEditingWorkplace(null)
    }

    return (
        <Stack direction="column" gap={2} paddingBottom={1}>
            <Box>
                <Typography variant="h6">Текущее место работы</Typography>
                {studentBlank.currentWorkplace ? (
                    <WorkplaceItem
                        workplace={studentBlank.currentWorkplace}
                        onEdit={() => setEditingWorkplace(studentBlank.currentWorkplace!)}
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
                        onClick={() => setEditingWorkplace(WorkplaceBlank.empty())}
                    />
                )}
            </Box>

            <Box>
                <Typography variant="h6">Предыдущие места работы</Typography>
                {studentBlank.prevWorkplaces.map((workplace) => (
                    <WorkplaceItem
                        key={workplace.id || "new"}
                        workplace={workplace}
                        onEdit={() => setEditingWorkplace(workplace)}
                        onDelete={() =>
                            dispatch({
                                type: "CHANGE_PREV_WORKPLACES",
                                payload: {
                                    prevWorkplaces: studentBlank.prevWorkplaces.filter(
                                        (w) => w.id !== workplace.id
                                    ),
                                },
                            })
                        }
                    />
                ))}
                <Button
                    text="Добавить предыдущее место работы"
                    onClick={() => setEditingWorkplace(WorkplaceBlank.empty())}
                />
            </Box>

            <Dialog
                open={!!editingWorkplace}
                onClose={() => setEditingWorkplace(null)}
                maxWidth="md"
                fullWidth>
                <DialogContent>
                    {editingWorkplace && (
                        <WorkplaceEditor
                            workplace={editingWorkplace}
                            onSave={handleSaveWorkplace}
                            onClose={() => setEditingWorkplace(null)}
                        />
                    )}
                </DialogContent>
            </Dialog>
        </Stack>
    )
}
