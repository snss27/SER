import GroupNumberInput from "@/components/shared/inputs/maskedInputs/groupNumberInput"
import Select from "@/components/shared/inputs/select"
import TextInput from "@/components/shared/inputs/textInput"
import YearPicker from "@/components/shared/inputs/yearPicker"
import { StructuralUnits } from "@/domain/groups/enums/structuralUnits"
import { GroupBlank } from "@/domain/groups/models/groupBlank"
import { Box, Typography } from "@mui/material"
import { useReducer } from "react"

const AddGroupPage = () => {
    const [groupBlank, dispatch] = useReducer(GroupBlank.reducer, GroupBlank.empty())

    return (
        <Box sx={{ display: "flex", flexDirection: "column", gap: 2, padding: 2 }}>
            <Box>
                <Typography variant="h1" textAlign="center">
                    Добавление группы
                </Typography>
            </Box>

            <Box sx={{ display: "flex", flexDirection: "row", gap: 2 }}>
                <Box sx={{ flex: 1 }}>
                    <GroupNumberInput
                        value={groupBlank.number}
                        onChange={(number) =>
                            dispatch({
                                type: "CHANGE_NUMBER",
                                payload: { number },
                            })
                        }
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <Select
                        options={StructuralUnits.getAll()}
                        value={groupBlank.structuralUnit}
                        getOptionLabel={(structuralUnit) =>
                            StructuralUnits.getDisplayText(structuralUnit)
                        }
                        onChange={(structuralUnit) =>
                            dispatch({
                                type: "CHANGE_STRUCTURAL_UNIT",
                                payload: { structuralUnit },
                            })
                        }
                        label="Структурное подразделение"
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    {/* TODO Когда будет готов справочник на специальности - сделать тут AsyncAutocomplete(наверное) */}
                </Box>
            </Box>
            <Box sx={{ display: "flex", flexDirection: "row", gap: 2 }}>
                <Box sx={{ flex: 1 }}>
                    <YearPicker
                        value={groupBlank.enrollmentYear}
                        label="Год поступления"
                        onChange={(enrollmentYear) =>
                            dispatch({
                                type: "CHANGE_ENROLLMENT_YEAR",
                                payload: { enrollmentYear },
                            })
                        }
                    />
                </Box>
                <Box sx={{ flex: 1 }}>
                    <TextInput
                        value={groupBlank.curatorName}
                        label="Куратор группы"
                        onChange={(curatorName) =>
                            dispatch({ type: "CHANGE_CURATOR_NAME", payload: { curatorName } })
                        }
                    />
                </Box>
            </Box>
        </Box>
    )
}

export default AddGroupPage
