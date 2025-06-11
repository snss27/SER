"use client"

import { EmployeesProvider } from "@/domain/employees/employeesProvider"
import { EmployeeBlank } from "@/domain/employees/models/employeeBlank"
import useNotifications from "@/hooks/useNotifications"
import { Box } from "@mui/material"
import { useRouter } from "next/router"
import React, { useReducer } from "react"
import { IconPosition, IconType } from "../shared/buttons"
import Button from "../shared/buttons/button"
import TextInput from "../shared/inputs/textInput"

interface Props {
    initialBlank: EmployeeBlank
}

export const EditEmployeeForm: React.FC<Props> = ({ initialBlank }) => {
    const [employeeBlank, dispatch] = useReducer(EmployeeBlank.reducer, initialBlank)

    const navigator = useRouter()
    const { showError, showSuccess } = useNotifications()

    function handleBackButton() {
        navigator.back()
    }

    async function handleSaveButton() {
        const result = await EmployeesProvider.save(employeeBlank)
        if (!result.isSuccess) return showError(result.getErrorsString)

        showSuccess(`Изменения успешно сохранены`)
        navigator.back()
    }

    return (
        <Box
            sx={{
                display: "flex",
                flexDirection: "column",
                gap: 2,
                flex: 1,
                py: 2,
                width: "50%",
                alignSelf: "center",
            }}>
            <TextInput
                value={employeeBlank.name}
                label="Имя"
                onChange={(name) => dispatch({ type: "CHANGE_NAME", payload: { name } })}
            />
            <TextInput
                value={employeeBlank.secondName}
                label="Фамилия"
                onChange={(secondName) =>
                    dispatch({ type: "CHANGE_SECOND_NAME", payload: { secondName } })
                }
            />
            <TextInput
                value={employeeBlank.lastName}
                label="Отчество"
                onChange={(lastName) =>
                    dispatch({ type: "CHANGE_LAST_NAME", payload: { lastName } })
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
