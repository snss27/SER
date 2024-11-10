import { IconPosition, IconType } from "@/components/shared/buttons"
import Button from "@/components/shared/buttons/button"
import GroupNumberInput from "@/components/shared/inputs/maskedInputs/groupNumberInput"
import Select from "@/components/shared/inputs/select"
import TextInput from "@/components/shared/inputs/textInput"
import YearPicker from "@/components/shared/inputs/yearPicker"
import { StructuralUnits } from "@/domain/groups/enums/structuralUnits"
import { GroupBlank } from "@/domain/groups/models/groupBlank"
import Speciality from "@/domain/specialities/models/speciality"
import { SpecialitiesProvider } from "@/domain/specialities/specialitiesProvider"
import { Box, Typography } from "@mui/material"
import { useRouter } from "next/router"
import { useEffect, useReducer, useState } from "react"

const AddGroupPage = () => {
    const [groupBlank, dispatch] = useReducer(GroupBlank.reducer, GroupBlank.empty())
    const [specialities, setSpecialities] = useState<Speciality[]>([])

    const navigator = useRouter()

    useEffect(() => {
        async function loadSpecialities() {
            const specialities = await SpecialitiesProvider.getAll()

            setSpecialities(specialities)
        }

        loadSpecialities()
    }, [])

    function handleBackButton() {
        navigator.back()
    }

    function handleSaveButton() {}

    return (
        <Box
            sx={{
                width: "100%",
                height: "100%",
                padding: 2,
                display: "flex",
                justifyContent: "center",
            }}>
            <Box sx={{ display: "flex", flexDirection: "column", gap: 3, width: "50%" }}>
                <Typography variant="h1" textAlign="center">
                    Новая группа
                </Typography>

                <GroupNumberInput
                    value={groupBlank.number}
                    onChange={(number) =>
                        dispatch({
                            type: "CHANGE_NUMBER",
                            payload: { number },
                        })
                    }
                />
                <Select
                    options={StructuralUnits.getAll()}
                    value={groupBlank.structuralUnit}
                    label="Структурное подразделение"
                    getOptionLabel={(structuralUnit) =>
                        StructuralUnits.getDisplayText(structuralUnit)
                    }
                    onChange={(structuralUnit) =>
                        dispatch({
                            type: "CHANGE_STRUCTURAL_UNIT",
                            payload: { structuralUnit },
                        })
                    }
                />
                <Select
                    options={specialities.map((s) => s.id)}
                    value={groupBlank.specialityId}
                    label="Выберите специальность"
                    getOptionLabel={(specialityId) =>
                        specialities.find((speciality) => speciality.id === specialityId)!.name
                    }
                    onChange={(specialityId) =>
                        dispatch({ type: "CHANGE_SPECIALITY", payload: { specialityId } })
                    }
                />
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
                <TextInput
                    value={groupBlank.curatorName}
                    label="Куратор группы"
                    onChange={(curatorName) =>
                        dispatch({ type: "CHANGE_CURATOR_NAME", payload: { curatorName } })
                    }
                />

                <Box
                    sx={{
                        display: "flex",
                        flexDirection: "row",
                        justifyContent: "space-between",
                    }}>
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
        </Box>
    )
}

export default AddGroupPage
