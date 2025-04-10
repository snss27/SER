import { EnterprisesProvider } from "@/domain/enterprises/enterprisesProvider"
import { WorkplaceBlank } from "@/domain/workplaces/models/workplaceBlank"
import { Box } from "@mui/material"
import { useReducer } from "react"
import Button from "../shared/buttons/button"
import { AsyncAutocomplete } from "../shared/inputs/asyncAutocomplete"
import DatePicker from "../shared/inputs/datePicker"
import { FilesInput } from "../shared/inputs/filesInput"
import TextInput from "../shared/inputs/textInput"

interface Props {
    workplace: WorkplaceBlank
    onSave: (workplace: WorkplaceBlank) => void
    onClose: () => void
}

export const WorkplaceEditor: React.FC<Props> = ({ workplace, onSave, onClose }) => {
    const [editedWorkplace, dispatch] = useReducer(WorkplaceBlank.reducer, workplace)

    const handleSave = () => {
        onSave(editedWorkplace)
        onClose()
    }

    return (
        <Box sx={{ p: 2, display: "flex", flexDirection: "column", gap: 2 }}>
            <AsyncAutocomplete
                value={editedWorkplace.enterprise}
                label="Предприятие"
                loadOptions={EnterprisesProvider.getBySearchText}
                getOptionLabel={(enterprise) => enterprise.name}
                onChange={(enterprise) =>
                    dispatch({
                        type: "CHANGE_ENTERPRISE",
                        payload: { enterprise },
                    })
                }
            />

            <TextInput
                value={editedWorkplace.post}
                label="Должность"
                onChange={(post) =>
                    dispatch({
                        type: "CHANGE_POST",
                        payload: { post },
                    })
                }
            />

            <DatePicker
                value={editedWorkplace.startDate}
                label="Дата оформления"
                onChange={(startDate) =>
                    dispatch({
                        type: "CHANGE_START_DATE",
                        payload: { startDate },
                    })
                }
            />

            <DatePicker
                value={editedWorkplace.finishDate}
                label="Дата увольнения"
                onChange={(finishDate) =>
                    dispatch({
                        type: "CHANGE_FINISH_DATE",
                        payload: { finishDate },
                    })
                }
            />

            <FilesInput
                label="Файлы выписки из трудовой книжки"
                maxFilesCount={editedWorkplace.workbookExtractFile.maxFiles}
                urls={editedWorkplace.workbookExtractFile.fileUrls}
                files={editedWorkplace.workbookExtractFile.files}
                onFilesChange={(files) =>
                    dispatch({
                        type: "CHANGE_WORKBOOK_EXTRACT_FILE",
                        payload: {
                            workbookExtractFile:
                                editedWorkplace.workbookExtractFile.withChangedFiles(files),
                        },
                    })
                }
                onUrlsChange={(urls) =>
                    dispatch({
                        type: "CHANGE_WORKBOOK_EXTRACT_FILE",
                        payload: {
                            workbookExtractFile:
                                editedWorkplace.workbookExtractFile.withChangedUrls(urls),
                        },
                    })
                }
            />

            <Box sx={{ display: "flex", gap: 2, justifyContent: "flex-end" }}>
                <Button text="Отмена" onClick={onClose} />
                <Button text="Сохранить" variant="contained" onClick={handleSave} />
            </Box>
        </Box>
    )
}
