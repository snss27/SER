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
        setEditingWorkplace({ ...workplace })
        setEditMode(mode)
    }

    const handleSaveWorkplace = (updated: WorkplaceBlank) => {
        if (!editMode) return

        let updatedList: WorkplaceBlank[] = []

        if (editMode.isNew) {
            updatedList = updated.isCurrent
                ? [...studentBlank.workPlaces.filter((w) => !w.isCurrent), updated]
                : [...studentBlank.workPlaces, updated]
        } else {
            updatedList = studentBlank.workPlaces.map((w) =>
                w.clientId === updated.clientId ? updated : w
            )

            if (updated.isCurrent) {
                updatedList = updatedList
                    .filter((w) => !w.isCurrent || w.clientId === updated.clientId)
                    .concat(updated)
            }
        }

        dispatch({
            type: "CHANGE_WORKPLACES",
            payload: { workPlaces: updatedList },
        })

        closeEditor()
    }

    const closeEditor = () => {
        setEditingWorkplace(null)
        setEditMode(null)
    }

    const handleDelete = (clientId?: string) => {
        dispatch({
            type: "CHANGE_WORKPLACES",
            payload: {
                workPlaces: studentBlank.workPlaces.filter((w) => w.clientId !== clientId),
            },
        })
    }

    const currentWorkplace = studentBlank.workPlaces.find((wp) => wp.isCurrent)
    const previousWorkplaces = studentBlank.workPlaces.filter((wp) => !wp.isCurrent)

    return (
        <Stack direction="column" gap={2} paddingBottom={1}>
            <Box>
                <Typography variant="h6">Текущее место работы</Typography>
                {currentWorkplace ? (
                    <WorkplaceItem
                        workplace={currentWorkplace}
                        onEdit={() =>
                            openEditor(currentWorkplace, {
                                type: "current",
                                isNew: false,
                            })
                        }
                        onDelete={() => handleDelete(currentWorkplace.clientId)}
                    />
                ) : (
                    <Button
                        text="Добавить текущее место работы"
                        onClick={() =>
                            openEditor(WorkplaceBlank.empty(true), { type: "current", isNew: true })
                        }
                    />
                )}
            </Box>

            <Box>
                <Typography variant="h6">Предыдущие места работы</Typography>
                {previousWorkplaces.map((workplace) => (
                    <WorkplaceItem
                        key={workplace.clientId}
                        workplace={workplace}
                        onEdit={() => openEditor(workplace, { type: "previous", isNew: false })}
                        onDelete={() => handleDelete(workplace.clientId)}
                    />
                ))}
                <Button
                    text="Добавить предыдущее место работы"
                    onClick={() =>
                        openEditor(WorkplaceBlank.empty(false), { type: "previous", isNew: true })
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
